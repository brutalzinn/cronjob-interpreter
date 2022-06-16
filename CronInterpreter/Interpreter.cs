using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronInterpreter
{
    public class Interpreter
    {


        private string CronJob { get; set; }

        TimeSpan Minutos { get; set; }
        TimeSpan Horas { get; set; }
        TimeSpan Segundos { get; set; }
        DateTime Data { get; set; }

        public Interpreter(string cronjob)
        {
            CronJob = cronjob;
          //  cronjob.Split(' ')[0].FirstOrDefault(e => e.Equals(AnyValue));
        }
 
    
    }
}
