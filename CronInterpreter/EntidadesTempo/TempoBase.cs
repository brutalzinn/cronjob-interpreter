using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
   public class TempoBase
    {
        protected static char AnyValue = '*';
        protected static char ValueListSeperator = ',';
        protected static char RangeOfValues = '-';
        protected static char StepValues = '/';
        public DateTime ProximoDisparo { get; set; }

        protected TempoBase()
        {

        }

        protected CronType ObterTipo(string value)
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
