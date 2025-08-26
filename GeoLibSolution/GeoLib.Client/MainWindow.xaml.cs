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
            //_Proyx = new GeoClient();
            _Proxy = new StatefulGeoClient();

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                " | Process " + Process.GetCurrentProcess().Id.ToString();
        }

        //GeoClient _Proyx = null;
        StatefulGeoClient _Proxy = null;

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text != "")
            {
                //GeoClient proxy = new GeoClient("httpEP") ;
                //GeoClient proxy = new GeoClient("webEP") ;
                
                //GeoClient proxy = new GeoClient("tcpEP") ;

                //ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
                //if (data != null)
                //{
                //    lblCity.Content = data.City;
                //    lblState.Content = data.State;
                //}

                //proxy.Close();

                //// Called by the service generated code
                //ServiceReference1.GeoServiceClient proxy = new ServiceReference1.GeoServiceClient();
                //var data = proxy.GetZipInfo(txtZipCode.Text);
                //if (data != null)
                //{
                //    lblCity.Content = data.City;
                //    lblState.Content = data.State;
                //}

                //proxy.Close();

                GeoClient proxy = new GeoClient();

                //ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
                //ZipCodeData data = _Proyx.GetZipInfo(txtZipCode.Text);

                try
                {
                    //ZipCodeData data = _Proxy.GetZipInfo();
                    ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
                    if (data != null)
                    {
                        lblCity.Content = data.City;
                        lblState.Content = data.State;
                    }

                    proxy.Close();
                }
                catch (FaultException<ExceptionDetail> ex)
                {
                    MessageBox.Show("Exception 1 thrown by service.\n\rException type: " +
                        ex.GetType().Name + "\n\r" +
                        "Message: " + ex.Message + "\n\r" +
                        "Proxy state: " + proxy.State.ToString());
                }
                catch (FaultException<ApplicationException> ex)
                {
                    MessageBox.Show("FaultException<ApplicationException> thrown by service.\n\rException type: " +
                        ex.GetType().Name + "\n\r" +
                        "Reason: " + ex.Message + "\n\r" +
                        "Message: " + ex.Detail.Message + "\n\r" +
                        "Proxy state: " + proxy.State.ToString());
                }
                catch(FaultException<NotFoundData> ex)
                {
                    MessageBox.Show("FaultException<NotFoundData> thrown by service.\n\rException type: " +
                        ex.GetType().Name + "\n\r" +
                        "Reason: " + ex.Message + "\n\r" +
                        "Message: " + ex.Detail.Message + "\n\r" +
                        "Proxy state: " + proxy.State.ToString());
                }
                catch (FaultException ex)
                {
                    MessageBox.Show("FaultException thrown by service.\n\rException type: " +
                        ex.GetType().Name + "\n\r" +
                        "Message: " + ex.Message + "\n\r" +
                        "Proxy state: " + proxy.State.ToString());
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Exception thrown by service.\n\rException type: " +
                        ex.GetType().Name + "\n\r" +
                        "Message: " + ex.Message + "\n\r" +
                        "Proxy state: " + proxy.State.ToString());
                }
            }
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (txtState.Text != null)
            {
                //EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                //System.ServiceModel.Channels.Binding binding = new NetTcpBinding();

                //GeoClient proxy = new GeoClient(binding, address);

                //GeoClient proxy = new GeoClient("tcpEP");
                GeoClient proxy = new GeoClient();


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

        private void btnGetInRange_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text != "" && txtRange.Text != "")
            {
                //GeoClient proxy = new GeoClient();

                //IEnumerable<ZipCodeData> data = proxy.GetZips(txtZipCode.Text, int.Parse(txtRange.Text));
                IEnumerable<ZipCodeData> data = _Proxy.GetZips(int.Parse(txtRange.Text));
                if (data != null)
                    lstZips.ItemsSource = data;

                //proxy.Close();
            }
        }

        private void btnPush_Click(object sender, RoutedEventArgs e)
        {
            if(txtZipCode.Text != "")
            {
                _Proxy.PushZip(txtZipCode.Text);
            }
        }
    }
}
