using System;
using System.Threading;

namespace CronInterpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //minuto, hora, dia, mês, dia da semana
            var crontab = "20-25 * * * *";

            var teste = new CronJob(crontab, DateTime.Now);
            while (true)
            {
               var atual = DateTime.Now.CreateWithoutSeconds();
               if (atual == teste.NovoDateTime)
                {
                    Console.WriteLine("Disparooo", DateTime.Now);
                    teste = new CronJob(crontab, DateTime.Now);
                }

               Thread.Sleep(1000);
            }
        }
    }
}
