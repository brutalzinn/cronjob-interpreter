using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class MinutosModel : TempoBase
    {
        private string MinutosChar { get; set; }

        public MinutosModel(string cronjob, DateTime dateInicio)
        {
            MinutosChar = cronjob.Split(SpaceSeparator)[0];
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double minutes = 0;
               
            switch (ObterTipo(MinutosChar))
            {
                case CronType.AnyValue:
                    minutes = 1;
                break;

                case CronType.ValueListSeperator:
                    minutes = MinutosChar.NextValueListSeparator(ValueListSeperator, item=> item > ProximoDisparo.Minute).FirstOrDefault().GetValueOrDefault();
                break;

                case CronType.RangeOfValues:
                    minutes = MinutosChar.NextRangeOfValues(RangeOfValues, item => item > ProximoDisparo.Minute).FirstOrDefault().GetValueOrDefault();
                break;

                case CronType.StepValues:
                    minutes = MinutosChar.NextStepValues(StepValues, item => item > ProximoDisparo.Minute).FirstOrDefault().GetValueOrDefault();
                break;

                default:
                    minutes = double.Parse(MinutosChar);
                break;
            }
            
            ProximoDisparo = ProximoDisparo.AddMinutes(minutes);
            return ProximoDisparo;
        }

       
    }
}
