using CronInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronExpression.EntidadesTempo
{
    public class CalculaProximoDia
    {
        private CronStringStruct CronStringStruct { get; set; }
        public CalculaProximoDia(CronStringStruct cronStringStruct)
        {
            CronStringStruct = cronStringStruct;
        }

        public TimeSpan CalcularProximoDia(DateTime dataReferencia)
        {
            var dateTimeRef = dataReferencia.Day;
            var cronStringStructField = CronStringStruct.Days;
            switch (CronStringStruct.GetType(cronStringStructField))
            {
                case CronType.AnyValue:
                    return new TimeSpan(0, dateTimeRef + 1, 0);

                case CronType.ValueListSeperator:
                    var listMinutesWithSeparator = CronStringStruct.NextValueListSeparator(cronStringStructField, (x) => x > dataReferencia.Minute);
                    return new TimeSpan(0, listMinutesWithSeparator.First(), 0);

                case CronType.RangeOfValues:
                    var listMinutesWithRange = CronStringStruct.NextRangeOfValues(cronStringStructField);
                    var firstMinuteWithRange = listMinutesWithRange.First();
                    var lastMinuteWithRange = listMinutesWithRange.Last();
                    var minuteRange = cronStringStructField.ToInt();
                    if (firstMinuteWithRange <= minuteRange && minuteRange <= lastMinuteWithRange)
                    {
                        return new TimeSpan(0, dateTimeRef + 1, 0);
                    }
                    return new TimeSpan(0, listMinutesWithRange.First(), 0);

                case CronType.StepValues:
                    var listMinuteWithStep = CronStringStruct.NextStepValues(cronStringStructField);
                    var firstMinuteithStep = listMinuteWithStep.First();
                    var lastMinuteithStep = listMinuteWithStep.Last();
                    var minuteStep = cronStringStructField.ToInt();
                    if (firstMinuteithStep <= minuteStep && minuteStep <= lastMinuteithStep)
                    {
                        return new TimeSpan(0, dateTimeRef + 1, 0);
                    }
                    return new TimeSpan(0, listMinuteWithStep.First(), 0);

                default:
                    return new TimeSpan(0, cronStringStructField.ToInt(), 0);
            }
        }
    }
}
