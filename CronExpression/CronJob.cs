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
        /// <summary>
        /// CronJob() é a representação de uma CronExpression.
        /// </summary>
        /// <param name="cronString">Expressão cron em string</param>
        /// <param name="dateInicio">DateTime usado como base para os cálculos de comparação</param>
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
            var timeSpan = diaSemana.Subtract(Dias);
            Mes = Mes.Add(timeSpan);
            Dias = Dias.Add(timeSpan);
            
            return new DateTime(year: Dias.Year, month: Mes.Month, day: Dias.Day, hour: Horas.Hour, minute: Minutos.Minute, second: 0).CreateWithoutSeconds();
        }

        /// <summary>
        /// Metódo responsável pela comparação de DateTime data uma data a ser comparada na instância do CronJob ou passada aqui
        /// Caso o param seja null, o DateTime será um DateTime.Now
        /// </summary>
        /// <param name="dateTime">Data a ser comparada</param>
        /// <returns></returns>
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
