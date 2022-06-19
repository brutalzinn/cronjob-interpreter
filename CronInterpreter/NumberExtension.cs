using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter
{
    public static class NumberExtension
    {
        public static int ToInt(this double value)
        {
            int resultado = 0;
            int.TryParse(value.ToString(), out resultado);
            return resultado;
        }
    }
}
