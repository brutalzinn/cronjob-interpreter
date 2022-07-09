using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter
{
    public static class StringExtension
    {
        public static int ToInt(this string value)
        {
            int resultado = 0;
            int.TryParse(value, out resultado);

            return resultado;
        }

        public static double? ToDouble(this string value)
        {
            double resultado = 0;
            double.TryParse(value, out resultado);

            return resultado;
        }
    }
}
