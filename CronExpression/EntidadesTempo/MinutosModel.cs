using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class MinutosModel : TempoBase
    {
        private CronStruct CronString { get; set; }

        public MinutosModel(string cronjob, DateTime dateInicio)
        {
            CronString = new CronStruct(cronjob);
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double minutes = 0;
               
            switch (CronString.GetType(CronString.Minutes))
            {
                case CronType.AnyValue:
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(minute: ProximoDisparo.Minute + 1);
                    break;

                case CronType.ValueListSeperator:
                    minutes = CronString.Minutes.NextValueListSeparator(CronStruct.ValueListSeperator, item=> item > ProximoDisparo.Minute).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(minute: (int)minutes);

                    break;

                case CronType.RangeOfValues:
                    minutes = CronString.Minutes.NextRangeOfValues(CronStruct.RangeOfValues, item => item > ProximoDisparo.Minute).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(minute: (int)minutes);

                    break;

                case CronType.StepValues:
                    minutes = CronString.Minutes.NextStepValues(CronStruct.StepValues, item => item > ProximoDisparo.Minute).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(minute: (int)minutes);

                    break;

                default:
                    minutes = double.Parse(CronString.Minutes);
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(minute: (int)minutes);

                break;
            }
            
            return ProximoDisparo;
        }

       
    }
}
