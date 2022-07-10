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
        public DateTime DateInicio { get; set; }
        public DateTime NovoDateTime { get; set; }

        public string CronString { get; set; }


        //(int year, int month, int day, int hour, int minute, int second)
        public CronJob(string cronString, DateTime dateInicio)
        {
            DateInicio = dateInicio;
            CronString = cronString;
            CalcularTempo();
        }

        public void CalcularTempo()
        {
            Minutos = new MinutosModel(CronString, DateInicio).CalcularProximaDateTime();
            Horas = new HorasModel(CronString, DateInicio).CalcularProximaDateTime();
            Dias = new DiaModel(CronString, DateInicio).CalcularProximaDateTime();
            Mes = new MesModel(CronString, DateInicio).CalcularProximaDateTime();
            var diaSemana = new DiaSemanaModel(CronString, DateInicio).CalcularProximaDateTime();
            int calcDia = Dias.Day;
            if (diaSemana.Day != DateTime.Now.Day)
            {
                calcDia = Dias.AddDays(diaSemana.Day).Day;
            }
            //cuidado aqui...
            NovoDateTime = new DateTime(year: Dias.Year, month: Mes.Month, day: calcDia, hour: Horas.Hour, minute: Minutos.Minute, second: 0);
        }

        public bool IsDispatchTime()
        {
            var atual = DateTime.Now.CreateWithoutSeconds();
            this.DateInicio = atual;
            if(atual == this.NovoDateTime)
            {
                CalcularTempo();
                return true;
            };

            return false;
        }

    }
}
