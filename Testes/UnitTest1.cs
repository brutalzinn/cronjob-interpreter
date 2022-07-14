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
        //executar toda segunda feira, às 14:30
        [Theory]
        [InlineData("30 14 * * 1", "11/07/2022", "18/07/2022 14:30")]
        [InlineData("30 14 * * 1", "13/07/2022", "18/07/2022 14:30")]
        [InlineData("30 14 * * 1", "18/07/2022", "25/07/2022 14:30")]
        [InlineData("30 14 * * 1", "25/07/2022", "01/08/2022 14:30")]
        public void ExpressaoComplexaUm(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        [Theory]
        [InlineData("0 22 * * 1-5", "13/07/2022 18:22", "13/07/2022 22:00")]
        public void ExpressaoComplexaDois(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        [Theory]
        [InlineData("* * 14-17 * *", "14/07/2022 19:56", "14/07/2022 19:57")]
        public void ExpressaoComplexaTres(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }
    }
}