using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class MesModel : TempoBase
    {
        private CronStruct CronString { get; set; }

        public MesModel(string cronjob, DateTime dateInicio)
        {
            CronString = new CronStruct(cronjob);
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double mes = 0;
            switch (CronString.GetType(CronString.Months))
            {
                case CronType.AnyValue:
                    //mes = 0;
                    //ProximoDisparo = ProximoDisparo.CreateNextDispatch(month: (int)mes);
                    break;

                case CronType.ValueListSeperator:
                    mes = CronString.Months.NextValueListSeparator(CronStruct.ValueListSeperator).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(month:(int)mes);
                    break;

                case CronType.RangeOfValues:
                    mes = CronString.Months.NextRangeOfValues(CronStruct.RangeOfValues).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(month: (int)mes);
                    break;

                case CronType.StepValues:
                    mes = CronString.Months.NextStepValues(CronStruct.StepValues).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(month: (int)mes);
                    break;

                default:
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(ProximoDisparo.Date.Day, CronString.Months.ToInt(), ProximoDisparo.Date.Year);
                    break;
            }
            return ProximoDisparo;
        }
    }
}
