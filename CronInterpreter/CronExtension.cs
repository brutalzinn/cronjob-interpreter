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

        public static DateTime CreateProximoDisparoWithDays(this DateTime dateTime, int days)
        {
            return new DateTime(dateTime.Date.Year, month: dateTime.Date.Month, day: days);
        }
    }
}
