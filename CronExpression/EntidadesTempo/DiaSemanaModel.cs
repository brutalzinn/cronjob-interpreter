using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace CronInterpreter.EntidadesTempo
    {
        public class DiaSemanaModel : TempoBase
        {
            private CronStruct CronString { get; set; }

            public DiaSemanaModel(string cronjob, DateTime dateInicio)
            {
                CronString = new CronStruct(cronjob);
                ProximoDisparo = dateInicio;
            }

            public DateTime CalcularProximaDateTime()
            {
                double dias = 0;
                switch (CronString.GetType(CronString.DaysWeek))
                {
                    case CronType.AnyValue:
                      //  dias = 0;
                        break;

                    case CronType.ValueListSeperator:
                        dias = CronString.DaysWeek.NextValueListSeparator(CronStruct.ValueListSeperator).FirstOrDefault().GetValueOrDefault();
                        ProximoDisparo = ProximoDisparo.CreateNextDispatch(days: (int)dias);
                        break;

                    case CronType.RangeOfValues:
                        ProximoDisparo = CronString.DaysWeek.NextRangeValuesByWeeks(ProximoDisparo, CronStruct.RangeOfValues).FirstOrDefault();
                 
                        break;

                    case CronType.StepValues:
                        dias = CronString.DaysWeek.NextStepValues(CronStruct.StepValues).FirstOrDefault().GetValueOrDefault();
                        ProximoDisparo = ProximoDisparo.CreateNextDispatch(days: (int)dias);
                        break;

                    default:
            
                        ProximoDisparo = ProximoDisparo.AddDays(1).GetNextWeekday(CronString.DaysWeek.ToDayOfWeeks());
                        break;
                }
                return ProximoDisparo;
            }



        }
    }


