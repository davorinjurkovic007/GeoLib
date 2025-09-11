using GeoLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.ConsoleHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));
            hostGeoManager.Open();

            Console.WriteLine("Service started. Press [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
