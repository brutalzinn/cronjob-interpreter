using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter
{
    public struct CronStruct
    {
        public static char AnyValue = '*';
        public static char ValueListSeperator = ',';
        public static char RangeOfValues = '-';
        public static char StepValues = '/';
        public static char SpaceSeparator = ' ';

        public string Minutes { get; set; }
        public string Hours { get; set; }
        public string Days { get; set; }
        public string Months { get; set; }
        public string DaysWeek { get; set; }

        public CronStruct(string cronjob)
        {
            Minutes = cronjob.Split(SpaceSeparator)[0];
            Hours = cronjob.Split(SpaceSeparator)[1];
            Days = cronjob.Split(SpaceSeparator)[2];
            Months = cronjob.Split(SpaceSeparator)[3];
            DaysWeek = cronjob.Split(SpaceSeparator)[4];
        }

        public CronType GetType(string value)
        {
            if (value.Contains(ValueListSeperator))
                return CronType.ValueListSeperator;

            if (value.Contains(RangeOfValues))
                return CronType.RangeOfValues;

            if (value.Contains(StepValues))
                return CronType.StepValues;

            if (value.Contains(AnyValue))
                return CronType.AnyValue;

            return CronType.CUSTOM;
        }

    }
}
