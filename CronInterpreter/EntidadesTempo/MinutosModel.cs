using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class MinutosModel : TempoBase
    {
        private string MinutosChar { get; set; }

        public MinutosModel(string cronjob, DateTime dateInicio)
        {
            MinutosChar = cronjob.Split(' ')[0];
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double minutes = 0;
            switch (ObterTipo())
            {
                case CronType.AnyValue:
                    minutes = 1;
                break;

                case CronType.ValueListSeperator:
                    minutes = FilterValueListSeparator(ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                break;

                case CronType.RangeOfValues:
                    minutes = FilterRangeOfValues(ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                break;

                case CronType.StepValues:
                    minutes = FilterStepValues(ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                break;

                default:
                    minutes =  double.Parse(MinutosChar);
                break;
            }
            
            ProximoDisparo = ProximoDisparo.AddMinutes(minutes);
            return ProximoDisparo;
        }

        private IEnumerable<double?> FilterValueListSeparator(DateTime date)
        {
            var listaMinutos = MinutosChar.Split(ValueListSeperator).Select(e => double.Parse(e) as double?);
            return listaMinutos.Where(item => item > date.Minute).ToList().OrderBy(d => d).ToList();    
        }

        private IEnumerable<double?> FilterRangeOfValues(DateTime date)
        {
            var listaMinutos = MinutosChar.Split(RangeOfValues).Select(e => int.Parse(e)).OrderBy(d => d).ToList();
            
            var primeiroMinuto = listaMinutos.First();
            var ultimoMinuto = listaMinutos.Last();

            var data = Enumerable.Range(listaMinutos.First(), listaMinutos.Last()).Where(item => item > date.Minute && item <= listaMinutos.Last());

            return data.Select(e => (double?)e).ToList();
        }

        private IEnumerable<double?> FilterStepValues(DateTime date)
        {
            var listaMinutos = MinutosChar.Split(StepValues).Select(e => int.Parse(e)).OrderBy(d => d).ToList();

            var inicioMinuto = listaMinutos.First();
            var posicaoMinuto = listaMinutos.Last();

            var data = Enumerable.Range(inicioMinuto, 59).Where((item, i) => i % posicaoMinuto == 0);
           
            return data.Select(e => (double?)e).ToList();
        }

        private CronType ObterTipo()
        {
            if (MinutosChar.Contains(AnyValue))
                return CronType.AnyValue;

            if (MinutosChar.Contains(ValueListSeperator))
                return CronType.ValueListSeperator;

            if (MinutosChar.Contains(RangeOfValues))
                return CronType.RangeOfValues;

            if (MinutosChar.Contains(StepValues))
                return CronType.StepValues;

            return CronType.CUSTOM;
        }
    }
}
