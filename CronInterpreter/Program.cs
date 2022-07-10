using System;
using System.Threading;

namespace CronInterpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //minuto, hora, dia, mês, dia da semana
            var crontab = "* * * * *";

            var teste = new CronJob(crontab, DateTime.Now);
            while (true)
            {
                if(teste.IsDispatchTime())
                {
                    Console.WriteLine(string.Format("Disparoo {0}", DateTime.Now));
                }
                Console.WriteLine(string.Format("Tempo atual {0}", DateTime.Now));

                Thread.Sleep(1000);
            }
        }
    }
}
