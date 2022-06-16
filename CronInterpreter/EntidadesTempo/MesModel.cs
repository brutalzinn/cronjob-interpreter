using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
    public class MesModel : TempoBase
    {
        private string MesChar { get; set; }

        public MesModel(string cronjob, DateTime dateInicio)
        {
            MesChar = cronjob.Split(' ')[3];
            ProximoDisparo = dateInicio;
        }

        public DateTime CalcularProximaDateTime()
        {
            double mes = 0;
            switch (ObterTipo(MesChar))
            {
                case CronType.AnyValue:
                    mes = 1;
                    break;

                case CronType.ValueListSeperator:
                    mes = MesChar.FilterValueListSeparator(ValueListSeperator, ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;

                case CronType.RangeOfValues:
                    mes = MesChar.FilterRangeOfValues(RangeOfValues, ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;

                case CronType.StepValues:
                    mes = MesChar.FilterStepValues(StepValues, ProximoDisparo).FirstOrDefault().GetValueOrDefault();
                    break;

            }
            ProximoDisparo = ProximoDisparo.AddMonths((int)mes);
            return ProximoDisparo;
        }
    }
}
