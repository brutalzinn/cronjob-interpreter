using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class DiaModel : TempoBase
    {
        private CronStruct CronString { get; set; }

        public DiaModel(string cronjob, DateTime dateInicio)
        {
            CronString = new CronStruct(cronjob);
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double dias = 0;
            switch (CronString.GetType(CronString.Days))
            {
                case CronType.AnyValue:
                   // dias = 0;
                   // ProximoDisparo = ProximoDisparo.CreateNextDispatch((int)dias);
                    break;

                case CronType.ValueListSeperator:
                    dias = CronString.Days.NextValueListSeparator(CronStruct.ValueListSeperator, item => item > ProximoDisparo.Day).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch((int)dias);
                    break;

                case CronType.RangeOfValues:
                    dias = CronString.Days.NextRangeOfValues(CronStruct.RangeOfValues, item => item > ProximoDisparo.Day).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch((int)dias);
                    break;

                case CronType.StepValues:
                    dias = CronString.Days.NextStepValues(CronStruct.StepValues, item=> item > ProximoDisparo.Day, ProximoDisparo.GetMonthDays()).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch((int)dias);
                    break;
                default:
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(CronString.Days.ToInt(), ProximoDisparo.Date.Month, ProximoDisparo.Date.Year);
                    break;
            }
            
            return ProximoDisparo;
        }

      

    }
}
