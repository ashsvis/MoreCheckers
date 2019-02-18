using System;
using System.ServiceProcess;

namespace CheckersAppServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Если запускает пользователь сам
            if (Environment.UserInteractive)
            {
                Console.WriteLine("Запуск шашечного сервиса..." + Environment.NewLine);
                var s = WcfCheckersService.Service;
                s.Start();
                Console.WriteLine("Шашечный сервис запущен." + Environment.NewLine +
                    "Для завершения работы введите слово exit и нажмите клавишу Enter...");
                var readLine = Console.ReadLine();
                while (readLine != null && readLine.ToLower() != "exit") { }
                s.Stop();
                return;
            }
            var servicesToRun = new ServiceBase[] { new WinCheckersService() };
            ServiceBase.Run(servicesToRun);
        }
    }
}
