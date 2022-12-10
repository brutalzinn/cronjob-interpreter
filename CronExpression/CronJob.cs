using CronExpression.EntidadesTempo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter
{
    public class CronJob
    {
        private DateTime DateInicio { get; set; }
        public CronStringStruct CronString { get; set; }

        public CronJob()
        {

        }
        /// <summary>
        /// CronJob() é a representação de uma CronExpression.
        /// </summary>
        /// <param name="cronString">Expressão cron em string</param>
        /// <param name="dateInicio">DateTime usado como base para os cálculos de comparação</param>
        public CronJob(string cronString, DateTime dataInicio)
        {
            DateInicio = dataInicio.CreateWithoutSeconds();
            CronString = CronStringStruct.Parse(cronString);
        }

        public CronJob(string cronString)
        {
            DateInicio = DateTime.Now.CreateWithoutSeconds();
            CronString = CronStringStruct.Parse(cronString);
        }


        public DateTime CalcularProximoDisparo()
        {
            var proximoDisparo = DateInicio;

            var entidadeMinuto = new CalculaProximoMinuto(CronString);
            var entidadeHora = new CalculaProximaHora(CronString);

            var proximoMinuto = entidadeMinuto.CalcularProximoMinuto(proximoDisparo);
            var proximaHora = entidadeHora.CalcularProximaHora(proximoDisparo);

            proximoDisparo = proximoDisparo.RecreateWithTime(proximaHora.Hours, proximoMinuto.Minutes);

            return proximoDisparo;
        }

        public bool IsDispatchTime(DateTime? dataDisparo = null)
        {
            var tempoCalc = CalcularProximoDisparo();
            DateInicio = dataDisparo.GetValueOrDefault(DateTime.Now).CreateWithoutSeconds();
            if (tempoCalc == DateInicio)
            {
                return true;
            }
            return false;
        }

    }
}
