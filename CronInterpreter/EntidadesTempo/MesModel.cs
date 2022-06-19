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
                    //mes = 0;
                    //ProximoDisparo = ProximoDisparo.CreateNextDispatch(month: (int)mes);
                    break;

                case CronType.ValueListSeperator:
                    mes = MesChar.NextValueListSeparator(ValueListSeperator).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(month:(int)mes);
                    break;

                case CronType.RangeOfValues:
                    mes = MesChar.NextRangeOfValues(RangeOfValues).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(month: (int)mes);
                    break;

                case CronType.StepValues:
                    mes = MesChar.NextStepValues(StepValues).FirstOrDefault().GetValueOrDefault();
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(month: (int)mes);
                    break;

                default:
                    ProximoDisparo = ProximoDisparo.CreateNextDispatch(ProximoDisparo.Date.Day, MesChar.ToInt(), ProximoDisparo.Date.Year);
                    break;
            }
            return ProximoDisparo;
        }
    }
}
