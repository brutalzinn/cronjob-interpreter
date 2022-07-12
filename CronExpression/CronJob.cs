using CronInterpreter.EntidadesTempo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter
{
    public class CronJob
    {

        private DateTime Minutos { get; set; }
        private DateTime Horas { get; set; }
        private DateTime Dias { get; set; }
        private DateTime Mes { get; set; }
        private DateTime DateInicio { get; set; }

        public string CronString { get; set; }

        public CronJob()
        {

        }
        //(int year, int month, int day, int hour, int minute, int second)
        public CronJob(string cronString, DateTime dateInicio)
        {
            DateInicio = dateInicio.CreateWithoutSeconds();
            CronString = cronString;
        }
        public DateTime CalcularTempo()
        {
            Minutos = new MinutosModel(CronString, DateInicio).CalcularProximaDateTime();
            Horas = new HorasModel(CronString, DateInicio).CalcularProximaDateTime();
            Dias = new DiaModel(CronString, DateInicio).CalcularProximaDateTime();
            Mes = new MesModel(CronString, DateInicio).CalcularProximaDateTime();
            var diaSemana = new DiaSemanaModel(CronString, DateInicio).CalcularProximaDateTime();
            int calcDia = Dias.Day;
            if (diaSemana.Day != DateInicio.Day)
            {
                calcDia = Dias.AddDays(diaSemana.Day - Dias.Day).Day;
            }
            return new DateTime(year: Dias.Year, month: Mes.Month, day: calcDia, hour: Horas.Hour, minute: Minutos.Minute, second: 0).CreateWithoutSeconds();
        }

        public bool IsDispatchTime(DateTime? dateTime = null)
        {
            var tempoCalc = CalcularTempo();
            DateInicio = dateTime.GetValueOrDefault(DateTime.Now.CreateWithoutSeconds()).CreateWithoutSeconds();
            if (tempoCalc == DateInicio)
            {
                return true;
            }
            return false;
        }

    }
}
