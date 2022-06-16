using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class MesModel : TempoBase
    {
        private string DiaMesChar { get; set; }

        public MesModel(string cronjob, DateTime dateInicio)
        {
            DiaMesChar = cronjob.Split(' ')[3];
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

            }
            ProximoDisparo = ProximoDisparo.AddDays(minutes);
            return ProximoDisparo;
        }

        private IEnumerable<double?> FilterValueListSeparator(DateTime date)
        {
            var listaMinutos = DiaMesChar.Split(ValueListSeperator).Select(e => double.Parse(e) as double?);
            return listaMinutos.Where(item => item > date.Minute).ToList().OrderBy(d => d).ToList();
        }

        private IEnumerable<double?> FilterRangeOfValues(DateTime date)
        {
            var listaMinutos = DiaMesChar.Split(RangeOfValues).Select(e => int.Parse(e)).OrderBy(d => d).ToList();

            var primeiroMinuto = listaMinutos.First();
            var ultimoMinuto = listaMinutos.Last();

            var data = Enumerable.Range(listaMinutos.First(), listaMinutos.Last()).Where(item => item > date.Minute && item <= listaMinutos.Last());

            return data.Select(e => (double?)e).ToList();
        }

        private IEnumerable<double?> FilterStepValues(DateTime date)
        {
            var listaMinutos = DiaMesChar.Split(StepValues).Select(e => int.Parse(e)).OrderBy(d => d).ToList();

            var inicioMinuto = listaMinutos.First();
            var posicaoMinuto = listaMinutos.Last();

            var data = Enumerable.Range(inicioMinuto, 59).Where((item, i) => i % posicaoMinuto == 0);

            return data.Select(e => (double?)e).ToList();
        }

        private CronType ObterTipo()
        {
            if (DiaMesChar.Contains(AnyValue))
                return CronType.AnyValue;

            if (DiaMesChar.Contains(ValueListSeperator))
                return CronType.ValueListSeperator;

            if (DiaMesChar.Contains(RangeOfValues))
                return CronType.RangeOfValues;

            if (DiaMesChar.Contains(StepValues))
                return CronType.StepValues;

            return CronType.CUSTOM;
        }

    }
}
