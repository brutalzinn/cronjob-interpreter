using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace CronInterpreter.EntidadesTempo
    {
        public class DiaSemanaModel : TempoBase
        {
            private string DiaSemanaChar { get; set; }

            public DiaSemanaModel(string cronjob, DateTime dateInicio)
            {
                DiaSemanaChar = cronjob.Split(SpaceSeparator)[4];
                ProximoDisparo = dateInicio;
            }

            public DateTime CalcularProximaDateTime()
            {
                double dias = 0;
                switch (ObterTipo(DiaSemanaChar))
                {
                    case CronType.AnyValue:
                        //dias = 0;
                        break;

                    case CronType.ValueListSeperator:
                        dias = DiaSemanaChar.NextValueListSeparator(ValueListSeperator).FirstOrDefault().GetValueOrDefault();
                        ProximoDisparo = ProximoDisparo.CreateNextDispatch(days: (int)dias);
                        break;

                    case CronType.RangeOfValues:
                        dias = DiaSemanaChar.NextRangeOfValues(RangeOfValues).FirstOrDefault().GetValueOrDefault();
                        ProximoDisparo = ProximoDisparo.CreateNextDispatch(days: (int)dias);
                        break;

                    case CronType.StepValues:
                        dias = DiaSemanaChar.NextStepValues(StepValues).FirstOrDefault().GetValueOrDefault();
                        ProximoDisparo = ProximoDisparo.CreateNextDispatch(days: (int)dias);
                        break;

                    default:
                        dias = double.Parse(DiaSemanaChar);
                        ProximoDisparo = ProximoDisparo.GetNextWeekday((DayOfWeek)dias.ToInt());
                        break;
                }
                return ProximoDisparo;
            }



        }
    }

}
