using CronInterpreter.EntidadesTempo;
using System;
using System.Threading;

namespace CronInterpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //minuto, hora, dia
            var crontab = "* * 16,18,25,26 * *";
            var dateTimeInicial = DateTime.Now;

            Console.WriteLine("Inicio");
            Console.WriteLine(dateTimeInicial);
            Console.WriteLine("---");

            while (true)
            {
                var teste = new CronJob(crontab, DateTime.Now);
                Console.WriteLine(teste.NovoDateTime);

                Thread.Sleep(1000);
            }
        }
    }
}
