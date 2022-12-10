using CronInterpreter;
using System;
using Xunit;

namespace Testes
{
    public class UnitTest1
    {

        [UseCulture("pt-BR")]
        [Fact]
        public void ExpressaoSimples()
        {
            var cronExpression = "* * * * *";
            var dataTime = DateTime.Parse("16/07/2022 20:00:00");
            var dataDisparo = DateTime.Parse("16/07/2022 20:01:00");
            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }
        [UseCulture("pt-BR")]
        [Fact]
        public void ExpressaoComValueSeperator()
        {
            var cronExpression = "1,2,3,12,13,15,16,30 * * * *";
            var dataTime = DateTime.Parse("16/07/2022 20:00:00");
            var dataDisparo = DateTime.Parse("16/07/2022 20:01:00");
            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

        [UseCulture("pt-BR")]
        [Fact]
        public void ExpressaoComRangeSeparator()
        {
            var cronExpression = "*/2 * * * *";
            var dataTime = DateTime.Parse("16/07/2022 15:50:00");
            var dataDisparo = DateTime.Parse("16/07/2022 15:52:00");
            var cronJob = new CronJob(cronExpression, dataTime);

            Assert.True(cronJob.IsDispatchTime(dataDisparo));
        }

    }
}