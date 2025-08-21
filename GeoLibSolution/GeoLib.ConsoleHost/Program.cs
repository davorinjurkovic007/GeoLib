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

            ////////////////////////////////////////////////////////////////////
            // Programmatically add host and behavior
            // Exactly like in config, just programmatically
            //ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager),
            //                                new Uri("http://localhost:8080"),
            //                                new Uri("net.tcp://localhost:8009"));

            //ServiceMetadataBehavior behavior = hostGeoManager.Description.Behaviors.Find<ServiceMetadataBehavior>();
            
            //// Http techinck required http enabled. 
            //// If you use MEX endpoint, you don't need to do http enbled, but you still need to add endpoint
            //if (behavior == null)
            //{
            //    behavior = new ServiceMetadataBehavior();
            //    behavior.HttpGetEnabled = true;
            //    hostGeoManager.Description.Behaviors.Add(behavior);
            //}


            //hostGeoManager.AddServiceEndpoint(typeof(IMetadataExchange), 
            //    MetadataExchangeBindings.CreateMexTcpBinding(), "MEX");
            /////////////////////////////////////

            // Adding base address to host, just add another parameter
            //ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager), );

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

            ServiceHost hostStatefulGeoManager = new ServiceHost(typeof(StatefulGeoManager));
            hostStatefulGeoManager.Open();


            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
            hostStatefulGeoManager.Close();
        }
    }
}
