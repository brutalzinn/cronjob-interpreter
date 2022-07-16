using CronInterpreter;
using System;
using Xunit;

namespace Testes
{
    public class UnitTest1
    {
        //executar a cada minuto

        [UseCulture("pt-BR")]
        [Theory]
        [InlineData("* * * * *")]
        public void ExpressaoSimples(string cronExpression)
        {
            var dataTime = DateTime.Parse("16/07/2022 20:00:00");
            var dataDisparo = DateTime.Parse("16/07/2022 20:01:00");
            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        //executar a cada cinco minutos
        [UseCulture("pt-BR")]
        [Theory]
        [InlineData("5 * * * *")]
        public void ExpressaoMinutosEspecificos(string cronExpression)
        {
            var dataTime = DateTime.Parse("16/07/2022 20:00:00");
            var dataDisparo = DateTime.Parse("16/07/2022 20:05:00");
            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        //executar caso seja um range entre minutos 20:01 e 20:05
        [UseCulture("pt-BR")]
        [Theory]
        [InlineData("1-5 * * * *", "11/07/2022 20:00:00", "11/07/2022 20:01:00")]
        public void ExpressaoMinutosRange(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }
        //executar toda segunda feira, às 14:30
        [UseCulture("pt-BR")]
        [Theory]
        [InlineData("30 14 * * 1", "11/07/2022 12:00:00", "18/07/2022 14:30:00")]
        [InlineData("30 14 * * 1", "13/07/2022 12:00:00", "18/07/2022 14:30:00")]
        [InlineData("30 14 * * 1", "18/07/2022 12:00:00", "25/07/2022 14:30:00")]
        [InlineData("30 14 * * 1", "25/07/2022 12:00:00", "01/08/2022 14:30:00")]
        public void ExpressaoComplexaUm(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        [UseCulture("pt-BR")]
        [Theory]
        [InlineData("0 22 * * 1-5", "13/07/2022 18:22:00", "13/07/2022 22:00:00")]
        public void ExpressaoComplexaDois(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        [UseCulture("pt-BR")]
        [Theory]
        [InlineData("* * 14-17 * *", "14/07/2022 19:56:00", "14/07/2022 19:57:00")]
        public void ExpressaoComplexaTres(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        [UseCulture("pt-BR")]
        [Theory]
        [InlineData("23 19-21 * * *", "15/07/2022 19:20:00", "15/07/2022 19:23:00")]
        public void ExpressaoComHoraComplexaUm(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        [UseCulture("pt-BR")]
        [Theory]
        [InlineData("0 0 16 2 *", "15/07/2022 19:20:00", "16/02/2023 00:00:00")]
        public void ExpressaoComMeuAniversario(string cronExpression, string dataInicio, string dataFinal)
        {
            var dataTime = DateTime.Parse(dataInicio);
            var dataDisparo = DateTime.Parse(dataFinal);

            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }
    }
}