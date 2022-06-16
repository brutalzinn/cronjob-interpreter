﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter
{
    public static class Extension
    {

        public static IEnumerable<double?> FilterValueListSeparator(this string value, char seperator, DateTime date)
        {
            var listaMinutos = value.Split(seperator).Select(e => double.Parse(e) as double?);
            return listaMinutos.Where(item => item > date.Minute).ToList().OrderBy(d => d).ToList();
        }

        public static IEnumerable<double?> FilterRangeOfValues(this string value, char seperator, DateTime date)
        {
            var listaMinutos = value.Split(seperator).Select(e => int.Parse(e)).OrderBy(d => d).ToList();

            var primeiroMinuto = listaMinutos.First();
            var ultimoMinuto = listaMinutos.Last();

            var data = Enumerable.Range(listaMinutos.First(), listaMinutos.Last()).Where(item => item > date.Minute && item <= listaMinutos.Last());

            return data.Select(e => (double?)e).ToList();
        }

        public static IEnumerable<double?> FilterStepValues(this string value, char seperator, DateTime date)
        {
            var listaMinutos = value.Split(seperator).ToList();

            int inicioMinuto = listaMinutos.First() != "*" ? listaMinutos.First().ToInt() : 1;
            int posicaoMinuto = listaMinutos.Last().ToInt();

            var data = Enumerable.Range(inicioMinuto, 59).Where((item, i) => item > date.Minute && i % posicaoMinuto == 0);

            return data.Select(e => (double?)e).ToList();
        }
        private static int ToInt(this string value)
        {
            int resultado = 0;
            int.TryParse(value, out resultado);
            return resultado;
        }

    }
}
