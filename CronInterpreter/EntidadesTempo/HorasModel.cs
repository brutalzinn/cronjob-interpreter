using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class HorasModel : TempoBase
    {
        private CronStruct CronString { get; set; }
        public HorasModel(string cronjob, DateTime dateInicio)
        {
            CronString = new CronStruct(cronjob);
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double horas = 0;
            switch (CronString.GetType(CronString.Hours))
            {
                case CronType.AnyValue:
                   // ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                break;

                case CronType.ValueListSeperator:
                    horas = CronString.Hours.NextValueListSeparator(CronStruct.ValueListSeperator).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                    break;

                case CronType.RangeOfValues:
                    horas = CronString.Hours.NextRangeOfValues(CronStruct.RangeOfValues).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                    break;

                case CronType.StepValues:
                    horas = CronString.Hours.NextStepValues(CronStruct.StepValues).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                    break;

                default:
                    horas = double.Parse(CronString.Hours);
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(hours: (int)horas);
                    break;

            }
        
            return ProximoDisparo;
        }
    }
}
