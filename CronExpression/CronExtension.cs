using System;
using System.Collections.Generic;
using System.Linq;

namespace CronInterpreter
{
    public static class CronExtension
    {

        public static IEnumerable<int> NextValueListSeparator(this CronStringStruct cronStruct, string value, Func<int?, bool> extraCondition = null)
        {
            var listaChars = value.Split(cronStruct.GetSeparator(CronType.ValueListSeperator)).Select(e => e.ToInt());
            if (extraCondition != null)
            {
                return listaChars.Where(item => extraCondition!(item)).ToList().OrderBy(d => d);
            }
            return listaChars.ToList().OrderBy(d => d);
        }

        public static IEnumerable<int> NextRangeOfValues(this CronStringStruct cronStruct, string value, Func<int, bool> extraCondition = null)
        {
            var listChars = value.Split(cronStruct.GetSeparator(CronType.RangeOfValues)).Select(e => e.ToInt()).ToList().OrderBy(d => d);
            return listChars.ToList();
        }

        public static IEnumerable<DateTime> NextRangeValuesByWeeks(this string value, DateTime dateBase, char seperator)
        {
            var listaChars = value.Split(seperator).Select(e => e.ToInt()).OrderBy(d => d).ToList();

            var primeiroDia = listaChars.First();
            var ultimoDia = listaChars.Last();
            List<DateTime> data = new List<DateTime>();
            List<int> teste = new List<int>();
            for (int i = primeiroDia; i <= ultimoDia; i++)
            {
                if (i >= primeiroDia && i <= ultimoDia)
                {
                    teste.Add(i);
                    data.Add(dateBase.GetNextWeekday((DayOfWeek)i));
                }
            }
            var resultado = data.OrderBy(x => x.Date);

            return resultado;
        }

        public static IEnumerable<int> NextStepValues(this CronStringStruct cronStruct, string value, Func<int, int, bool> extraCondition = null, int maxRange = 59)
        {
            var listaChars = value.Split(cronStruct.GetSeparator(CronType.StepValues)).ToList();

            int inicioChar = listaChars.First() != "*" ? listaChars.First().ToInt() : 0;
            int posicaoChar = listaChars.Last().ToInt();
            IEnumerable<int> data;

            if (extraCondition != null)
            {
                data = Enumerable.Range(inicioChar, maxRange).Where((item, i) => extraCondition(item, posicaoChar));
            }

            data = Enumerable.Range(inicioChar, maxRange).Where((item, i) => item > 0 && (i % posicaoChar == 0)).ToList();
            return data;
        }

        public static int GetMonthDays(this DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Date.Year, dateTime.Date.Month);
        }

        public static DateTime CreateWithoutSeconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Date.Year, dateTime.Date.Month, dateTime.Date.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        public static DateTime CreateWithoutTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Date.Year, dateTime.Date.Month, dateTime.Date.Day, 0, 0, 0);
        }

        public static DateTime RecreateWithTime(this DateTime dateTime, int hours, int minute)
        {
            return new DateTime(dateTime.Date.Year, dateTime.Date.Month, dateTime.Date.Day, hours, minute, 0);
        }
        //https://stackoverflow.com/questions/6346119/datetime-get-next-tuesday
        public static DateTime GetNextWeekday(this DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.Date.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }
}
