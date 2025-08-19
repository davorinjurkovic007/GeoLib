using GeoLib.Contracts;
using GeoLib.Services;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace GeoLib.ConsoleHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));

            //string address = "net.tcp://localhost:8009/GeoService";
            //Binding binding = new NetTcpBinding();
            //Type contract = typeof(IGeoService);

            //hostGeoManager.AddServiceEndpoint(contract, binding, address);

            //address = "http://localhost/GeoService";
            //Binding bindingHttp = new BasicHttpBinding();

            //hostGeoManager.AddServiceEndpoint(contract, bindingHttp, address);

            //// Add Behavior Programmatically
            //ServiceDebugBehavior behavior = hostGeoManager.Description.Behaviors.Find<ServiceDebugBehavior>();
            //if (behavior == null)
            //{
            //    behavior = new ServiceDebugBehavior();
            //    behavior.IncludeExceptionDetailInFaults = true;
            //    hostGeoManager.Description.Behaviors.Add(behavior);
            //}
            //else
            //    behavior.IncludeExceptionDetailInFaults = true;



                hostGeoManager.Open();

            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
