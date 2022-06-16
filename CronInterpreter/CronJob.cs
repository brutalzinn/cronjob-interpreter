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
        private string CronString { get; set; }

        private DateTime Minutos { get; set; }
        private DateTime Horas { get; set; }
        private DateTime Dias { get; set; }

        public DateTime NovoDateTime { get; set; }
        //(int year, int month, int day, int hour, int minute, int second)
        public CronJob(string cronjob, DateTime dateInicio)
        {
            Minutos = new MinutosModel(cronjob, dateInicio).CalcularProximaDateTime();
            Horas = new HorasModel(cronjob, dateInicio).CalcularProximaDateTime();
            Dias = new DiaModel(cronjob, dateInicio).CalcularProximaDateTime();

            NovoDateTime = new DateTime(year: Dias.Year, month:Dias.Month, day: Dias.Day, hour: Horas.Hour, minute: Minutos.Minute, second: 0);
        }


    }
}
