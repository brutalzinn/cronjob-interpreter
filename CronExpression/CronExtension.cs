using System;
using System.Collections.Generic;
using System.Linq;

namespace CronInterpreter
{
    public static class CronExtension
    {

        public static IEnumerable<double?> NextValueListSeparator(this string value, char seperator, Func<double?, bool> extraCondition = null)
        {
            var listaChars = value.Split(seperator).Select(e => e.ToDouble());

            if (extraCondition != null)
            {
                return listaChars.Where(item => extraCondition!(item)).ToList().OrderBy(d => d).ToList();
            }
            return listaChars.ToList().OrderBy(d => d).ToList();
        }

        public static IEnumerable<double?> NextRangeOfValues(this string value, char seperator, Func<int, bool> extraCondition = null)
        {
            var listaChars = value.Split(seperator).Select(e => e.ToInt()).OrderBy(d => d).ToList();

            var primeiroMinuto = listaChars.First();
            var ultimoMinuto = listaChars.Last();
            IEnumerable<int> data;

            data = Enumerable.Range(listaChars.First(), listaChars.Last()).Where(item => item <= listaChars.Last());

            if (extraCondition != null)
            {
                data = Enumerable.Range(listaChars.First(), listaChars.Last()).Where(item => extraCondition(item) && item <= listaChars.Last());
                return data.Select(e => (double?)e).ToList();
            }

            return data.Select(e => (double?)e).ToList();
        }

        public static IEnumerable<double?> NextStepValues(this string value, char seperator,Func<int,bool> extraCondition = null, int maxRange = 59)
        {
            var listaChars = value.Split(seperator).ToList();

            int inicioChar = listaChars.First() != "*" ? listaChars.First().ToInt() : 1;
            int posicaoChar = listaChars.Last().ToInt();
            IEnumerable<int> data;


            if (extraCondition != null)
            {
                data = Enumerable.Range(inicioChar, maxRange).Where((item, i) => extraCondition(item) && i % posicaoChar == 0);
                return data.Select(e => (double?)e).ToList();
            }

            data = Enumerable.Range(inicioChar, maxRange).Where((item, i) => i % posicaoChar == 0);

            return data.Select(e => (double?)e).ToList();
        }

        public static int GetMonthDays(this DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Date.Year, dateTime.Date.Month);
        }

        public static DateTime CreateWithoutSeconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Date.Year, dateTime.Date.Month, dateTime.Date.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        public static DateTime CreateNextDispatch(this DateTime dateTime, int? days = null, int? month = null, int? year = null, int? hours = null, int? minute = null, int? second = null)
        {
            return new DateTime(year.GetValueOrDefault(dateTime.Date.Year), month.GetValueOrDefault(dateTime.Date.Month), days.GetValueOrDefault(dateTime.Date.Day), hours.GetValueOrDefault(dateTime.Date.Hour), minute.GetValueOrDefault(dateTime.Date.Minute), second.GetValueOrDefault(dateTime.Date.Second));
        }
        //https://stackoverflow.com/questions/6346119/datetime-get-next-tuesday
        public static DateTime GetNextWeekday(this DateTime dateTime, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)dateTime.DayOfWeek + 7) % 7;
            return dateTime.AddDays(daysToAdd);
        }
    }
}
