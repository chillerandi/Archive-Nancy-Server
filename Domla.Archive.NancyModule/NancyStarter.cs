using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domla.Archive.Nancy
{
    public class NancyStarter
    {
        public static void Main(string[] args)
        {
            _hostConfig();

            startHost();
        }

        public static void _hostConfig()
        {
            var hostConfig = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
        }

        public static void startHost()
        {
            var endpoint = "http://localhost:1234";
            using (var host = new NancyHost(new Uri(endpoint)))
            {
                host.Start();
                Console.WriteLine("Running on " + Settings.Default.ServerName + " " + Settings.Default.ServerPort);
                Console.Read();
            }
        }

       
    }
}
