using CronInterpreter;
using System;
using Xunit;

namespace Testes
{
    public class UnitTest1
    {
        //executar a cada minuto

        [Theory]
        [InlineData("* * * * *")]
        public void ExpressaoSimples(string cronExpression)
        {
            var dataTime = DateTime.Parse("16/07/2022 20:00");
            var dataDisparo = DateTime.Parse("16/07/2022 20:01");
            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        //executar a cada cinco minutos
        [Theory]
        [InlineData("5 * * * *")]
        public void ExpressaoMinutosEspecificos(string cronExpression)
        {
            var dataTime = DateTime.Parse("16/07/2022 20:00");
            var dataDisparo = DateTime.Parse("16/07/2022 20:05");
            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        //executar caso seja um range entre minutos 20:01 e 20:05
        [Theory]
        [InlineData("1-5 * * * *", "11/07/2022 20:00", "11/07/2022 20:01")]
        public void ExpressaoMinutosRange(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }
        //executar toda segunda feira, �s 14:30
        [Theory]
        [InlineData("30 14 * * 1", "12/07/2022","18/07/2022 14:30")]
        public void ExpressaoComplexaUm(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }
    }
}