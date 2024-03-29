﻿using System;
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

        public static IEnumerable<DateTime> NextRangeValuesByWeeks(this string value, DateTime dateBase,char seperator)
        {
            var listaChars = value.Split(seperator).Select(e => e.ToInt()).OrderBy(d => d).ToList();

            var primeiroDia = listaChars.First();
            var ultimoDia = listaChars.Last();
            List<DateTime> data = new List<DateTime>();
            List<int> teste = new List<int>();
            for(int i = primeiroDia; i <= ultimoDia; i++)
            {
                if(i >= primeiroDia && i <= ultimoDia)
                {
                    teste.Add(i);
                    data.Add(dateBase.GetNextWeekday((DayOfWeek)i));
                }
            }
            var resultado = data.OrderBy(x => x.Date);

            return resultado;
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
            return new DateTime(year.GetValueOrDefault(dateTime.Date.Year), month.GetValueOrDefault(dateTime.Date.Month), days.GetValueOrDefault(dateTime.Date.Day), hours.GetValueOrDefault(dateTime.Hour), minute.GetValueOrDefault(dateTime.Minute), second.GetValueOrDefault(dateTime.Second));
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
