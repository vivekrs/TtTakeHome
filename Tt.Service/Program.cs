using System;
using System.ServiceProcess;

namespace Tt.Service
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            var servicesToRun = new CollectorService();

            if (Environment.UserInteractive)
            {
                servicesToRun.StartService(args);
                Console.WriteLine("Press any key to stop program");
                Console.Read();
                servicesToRun.StopService();
            }
            else 
            ServiceBase.Run(servicesToRun);
        }
    }
}
