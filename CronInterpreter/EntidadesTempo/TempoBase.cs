using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter.EntidadesTempo
{
   public class TempoBase
    {
        public DateTime ProximoDisparo { get; set; }

        protected TempoBase()
        {

        }
    }
}
