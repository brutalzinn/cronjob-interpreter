using CronInterpreter.EntidadesTempo;
using System;
using System.Threading;

namespace CronInterpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var crontab = "* * * * *";
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
