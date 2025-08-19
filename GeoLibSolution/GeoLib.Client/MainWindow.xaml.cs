using GeoLib.Client.Contracts;
using GeoLib.Contracts;
using GeoLib.Proxies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
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

namespace GeoLib.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _Proyx = new GeoClient();

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                " | Process " + Process.GetCurrentProcess().Id.ToString();
        }

        GeoClient _Proyx = null;

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text != "")
            {
                //GeoClient proxy = new GeoClient("httpEP") ;
                //GeoClient proxy = new GeoClient("webEP") ;
                GeoClient proxy = new GeoClient("tcpEP") ;

                ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
                if (data != null)
                {
                    lblCity.Content = data.City;
                    lblState.Content = data.State;
                }

                proxy.Close();
            }
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (txtState.Text != null)
            {
                //EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                //System.ServiceModel.Channels.Binding binding = new NetTcpBinding();

                //GeoClient proxy = new GeoClient(binding, address);

                GeoClient proxy = new GeoClient("tcpEP");

                IEnumerable<ZipCodeData> data = proxy.GetZips(txtState.Text);
                if (data != null)
                {
                    lstZips.ItemsSource = data;
                }

                proxy.Close();
            }
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {
            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8010/MessageService");
            System.ServiceModel.Channels.Binding binding = new NetTcpBinding();

            // This is a bug. If we have just a one end point, we don't heve to name it, but we need to put empty string
            //ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
            
            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>(binding, address);


            // Now we essentially have a proxy. 
            // We've just crated it virtually without having a physical proxy classs,
            // and we can just it now
            IMessageService proxy = factory.CreateChannel();

            proxy.ShowMsg(txtMessage.Text);

            factory.Close();
        }
    }
}
