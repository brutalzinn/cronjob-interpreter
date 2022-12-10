using CronInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronExpression.EntidadesTempo
{
    public class CalculaProximaHora
    {
        private CronStringStruct CronStringStruct { get; set; }
        public CalculaProximaHora(CronStringStruct cronStringStruct)
        {
            CronStringStruct = cronStringStruct;
        }

        public TimeSpan CalcularProximaHora(DateTime dataReferencia)
        {
            var dateTimeRef = dataReferencia.Hour;
            var cronStringStructField = CronStringStruct.Hours;
            switch (CronStringStruct.GetType(cronStringStructField))
            {
                case CronType.AnyValue:
                    return new TimeSpan(dateTimeRef, 0, 0);

                case CronType.ValueListSeperator:
                    var listMinutesWithSeparator = CronStringStruct.NextValueListSeparator(cronStringStructField, (x) => x > dataReferencia.Minute);
                    return new TimeSpan(listMinutesWithSeparator.First(), 0, 0);

                case CronType.RangeOfValues:
                    var listMinutesWithRange = CronStringStruct.NextRangeOfValues(cronStringStructField);
                    var firstMinuteWithRange = listMinutesWithRange.First();
                    var lastMinuteWithRange = listMinutesWithRange.Last();
                    var minuteRange = cronStringStructField.ToInt();
                    if (firstMinuteWithRange <= minuteRange && minuteRange <= lastMinuteWithRange)
                    {
                        return new TimeSpan(dateTimeRef + 1, 0, 0);
                    }
                    return new TimeSpan(listMinutesWithRange.First(), 0, 0);

                case CronType.StepValues:
                    var listMinuteWithStep = CronStringStruct.NextStepValues(cronStringStructField);
                    var firstMinuteithStep = listMinuteWithStep.First();
                    var lastMinuteithStep = listMinuteWithStep.Last();
                    var minuteStep = cronStringStructField.ToInt();
                    if (firstMinuteithStep <= minuteStep && minuteStep <= lastMinuteithStep)
                    {
                        return new TimeSpan(dateTimeRef + 1, 0, 0);
                    }
                    return new TimeSpan(listMinuteWithStep.First(), 0, 0);

                default:
                    return new TimeSpan(cronStringStructField.ToInt(), 0, 0);
            }
        }
    }
}
