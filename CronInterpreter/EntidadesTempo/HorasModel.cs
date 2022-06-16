using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class HorasModel : TempoBase
    {
        private string HorasChar { get; set; }
        public HorasModel(string cronjob, DateTime dateInicio)
        {
            HorasChar = cronjob.Split(' ')[1];
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double horas = 0;
            switch (ObterTipo())
            {
                case CronType.AnyValue:
                    horas = 0;
                    break;

                case CronType.ValueListSeperator:
                    horas = FilterValueListSeparator(ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;

                case CronType.RangeOfValues:
                    horas = FilterRangeOfValues(ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;

                case CronType.StepValues:
                    horas = FilterStepValues(ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;
                default:
                    horas = double.Parse(HorasChar);
                    break;

            }
            ProximoDisparo = ProximoDisparo.AddHours(horas);
            return ProximoDisparo;
        }

        private IEnumerable<double?> FilterValueListSeparator(DateTime date)
        {
            var listaMinutos = HorasChar.Split(ValueListSeperator).Select(e => double.Parse(e) as double?);
            return listaMinutos.Where(item => item > date.Minute).ToList().OrderBy(d => d).ToList();
        }

        private IEnumerable<double?> FilterRangeOfValues(DateTime date)
        {
            var listaMinutos = HorasChar.Split(RangeOfValues).Select(e => int.Parse(e)).OrderBy(d => d).ToList();

            var primeiroMinuto = listaMinutos.First();
            var ultimoMinuto = listaMinutos.Last();

            var data = Enumerable.Range(listaMinutos.First(), listaMinutos.Last()).Where(item => item > date.Minute && item <= listaMinutos.Last());

            return data.Select(e => (double?)e).ToList();
        }

        private IEnumerable<double?> FilterStepValues(DateTime date)
        {
            var listaMinutos = HorasChar.Split(StepValues).Select(e => int.Parse(e)).OrderBy(d => d).ToList();

            var inicioMinuto = listaMinutos.First();
            var posicaoMinuto = listaMinutos.Last();

            var data = Enumerable.Range(inicioMinuto, 59).Where((item, i) => i % posicaoMinuto == 0);

            return data.Select(e => (double?)e).ToList();
        }

        private CronType ObterTipo()
        {
            if (HorasChar.Contains(AnyValue))
                return CronType.AnyValue;

            if (HorasChar.Contains(ValueListSeperator))
                return CronType.ValueListSeperator;

            if (HorasChar.Contains(RangeOfValues))
                return CronType.RangeOfValues;

            if (HorasChar.Contains(StepValues))
                return CronType.StepValues;

            return CronType.CUSTOM;
        }

    }
}
