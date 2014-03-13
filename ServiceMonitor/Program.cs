using System.ServiceProcess;

namespace ServiceMonitor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ServiceWatcher()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}