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
            switch (ObterTipo(HorasChar))
            {
                case CronType.AnyValue:
                   // ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                break;

                case CronType.ValueListSeperator:
                    horas = HorasChar.NextValueListSeparator(ValueListSeperator).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                    break;

                case CronType.RangeOfValues:
                    horas = HorasChar.NextRangeOfValues(RangeOfValues).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                    break;

                case CronType.StepValues:
                    horas = HorasChar.NextStepValues(StepValues).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                    break;

                default:
                    horas = double.Parse(HorasChar);
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                    break;

            }
        
            return ProximoDisparo;
        }
    }
}
