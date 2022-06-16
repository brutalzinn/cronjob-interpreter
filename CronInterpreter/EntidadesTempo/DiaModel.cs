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
                    dias = DiaMesChar.FilterValueListSeparator(ValueListSeperator, ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;

                case CronType.RangeOfValues:
                    dias = DiaMesChar.FilterRangeOfValues(RangeOfValues,ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;

                case CronType.StepValues:
                    dias = DiaMesChar.FilterStepValues(StepValues, ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;
                default:
                    dias = double.Parse(DiaMesChar);
                    break;
            }
            ProximoDisparo = ProximoDisparo.AddDays(dias);
            return ProximoDisparo;
        }

      

    }
}
