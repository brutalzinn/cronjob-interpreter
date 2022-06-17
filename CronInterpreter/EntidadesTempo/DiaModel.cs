using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class DiaModel : TempoBase
    {
        private string DiaMesChar { get; set; }

        public DiaModel(string cronjob, DateTime dateInicio)
        {
            DiaMesChar = cronjob.Split(' ')[2];
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double dias = 0;
            switch (ObterTipo(DiaMesChar))
            {
                case CronType.AnyValue:
                    dias = 0;
                    break;

                case CronType.ValueListSeperator:
                    dias = DiaMesChar.NextValueListSeparator(ValueListSeperator, item => item > ProximoDisparo.Day).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateProximoDisparoWithDays((int)dias);
                    break;

                case CronType.RangeOfValues:
                    dias = DiaMesChar.NextRangeOfValues(RangeOfValues, item => item > ProximoDisparo.Day).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateProximoDisparoWithDays((int)dias);
                    break;

                case CronType.StepValues:
                    dias = DiaMesChar.NextStepValues(StepValues, item=> item > ProximoDisparo.Day, ProximoDisparo.GetMonthDays()).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateProximoDisparoWithDays((int)dias);
                    break;
                default:
                    ProximoDisparo = new DateTime(ProximoDisparo.Date.Year, month: ProximoDisparo.Date.Month, day: DiaMesChar.ToInt());
                    break;
            }
            
            return ProximoDisparo;
        }

      

    }
}
