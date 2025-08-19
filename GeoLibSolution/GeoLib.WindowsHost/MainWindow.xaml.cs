using GeoLib.Contracts;
using GeoLib.Services;
using GeoLib.WindowsHost.Contracts;
using GeoLib.WindowsHost.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            MainUI = this;

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId + " | Process " + Process.GetCurrentProcess().Id.ToString() + ")";

            _synchronizationContext = SynchronizationContext.Current;
        }

        ServiceHost _HostGeoManager = null;
        ServiceHost _HostMessageManager = null;

        SynchronizationContext _synchronizationContext = null;

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager = new ServiceHost(typeof(GeoManager));
            _HostMessageManager = new ServiceHost(typeof(MessageManager));

            //// Just another way of doing things, not using App.config
            //string address = "net.tcp://localhost:8009/GeoService";
            //System.ServiceModel.Channels.Binding binding = new System.ServiceModel.NetTcpBinding();
            //Type contract = typeof(IGeoService);

            //_HostGeoManager.AddServiceEndpoint(contract, binding, address);

            _HostGeoManager.Open();
            _HostMessageManager.Open();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager?.Close();
            _HostMessageManager?.Close();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            SendOrPostCallback callback = new SendOrPostCallback(arg =>
            {
                lblMessage.Content = message + Environment.NewLine + 
                    " (marhalled from thread " + threadId.ToString() + " ti thread " +
                    Thread.CurrentThread.ManagedThreadId.ToString() + Environment.NewLine +
                    " | Process " + Process.GetCurrentProcess().Id.ToString() + ")";
            });

            // NOTE: Second argument, null, is send/get passsed directly to the "arg" and can be used with in this code
            // arg -> object which we can use. 
            _synchronizationContext.Send(callback, null);
        }

        /// <summary>
        /// This Thread is neede, with code in the ShowMessage and code on the MessageManager =>
        /// [ServiceBehavior(UseSynchronizationContext = false)], for the purpose that we send message from
        /// woeking thread to the UI, without any problem or stopping UI.
        /// This is how it should it be done. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInProc_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() => 
            { 
                // There's that little bug the end point name so we have to give it a blank name
                // if we're not addressing an end point by name.
                ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");

                IMessageService proxy = factory.CreateChannel();

                proxy.ShowMessage(DateTime.Now.ToLongTimeString() + " from in-process call.");

                factory.Close();
            });

            // The only reason I make it a background thread is because it's a low priority process.
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
