using System;
using System.Collections.Generic;
using System.Linq;

namespace CronInterpreter
{
    public static class CronExtension
    {

        public static IEnumerable<double?> NextValueListSeparator(this string value, char seperator, DateTime date)
        {
            var listaMinutos = value.Split(seperator).Select(e => e.ToDouble());

            return listaMinutos.Where(item => item > date.Minute).ToList().OrderBy(d => d).ToList();
        }

        public static IEnumerable<double?> NextRangeOfValues(this string value, char seperator, DateTime date)
        {
            var listaMinutos = value.Split(seperator).Select(e => e.ToInt()).OrderBy(d => d).ToList();

            var primeiroMinuto = listaMinutos.First();
            var ultimoMinuto = listaMinutos.Last();

            var data = Enumerable.Range(listaMinutos.First(), listaMinutos.Last()).Where(item => item > date.Minute && item <= listaMinutos.Last());

            return data.Select(e => (double?)e).ToList();
        }

        public static IEnumerable<double?> NextStepValues(this string value, char seperator, DateTime date)
        {
            var listaMinutos = value.Split(seperator).ToList();

            int inicioMinuto = listaMinutos.First() != "*" ? listaMinutos.First().ToInt() : 1;
            int posicaoMinuto = listaMinutos.Last().ToInt();

            var data = Enumerable.Range(inicioMinuto, 59).Where((item, i) => item > date.Minute && i % posicaoMinuto == 0);

            return data.Select(e => (double?)e).ToList();
        }
    }
}
