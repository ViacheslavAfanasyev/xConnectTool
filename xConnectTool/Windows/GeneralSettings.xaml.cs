using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using xConnectAPI;

namespace xConnectTool.Windows
{
    /// <summary>
    /// Interaction logic for GeneralSettings.xaml
    /// </summary>
    public partial class GeneralSettings : Window
    {
        public GeneralSettings()
        {
            InitializeComponent();
            xConnectApply.IsEnabled = false;
            xConnectSiteInput.Text = xConnect.xConnectUrl;
            InfoLabel.Visibility = Visibility.Hidden;
            CertificateList.ItemsSource = Certificates.CertificatesName;
            var currentCert = Certificates.CertificatesFromMyStore.Where(x => x.ThumPrint == xConnectAPI.xConnect.CleintThumbprint).FirstOrDefault();
            if (currentCert!=null)
            {
                CertificateList.SelectedValue = Certificates.CertificatesName.Find(x => x == currentCert.Name);
            }

        }

        string warningNoHttp = "xConnect site url must start with 'https://'";
        string warningEmptyOrNull = "Specify xConnect site url";
        string infoNewSetingIsApplied = "New setting is applied";

        private void xConnectSiteInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (xConnectApply.IsEnabled == false)
            {
                xConnectApply.IsEnabled = true;
            }
        }

        private void CertificateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (xConnectApply.IsEnabled == false)
            {
                xConnectApply.IsEnabled = true;
            }
        }

        private void xConnectApply_Click(object sender, RoutedEventArgs e)
        {
            InfoLabel.Visibility = Visibility.Hidden;
            var url = xConnectSiteInput.Text;
            if (!string.IsNullOrEmpty(url))
            {
                if (url.ToLower().StartsWith("https://"))
                {
                    this.ChangeXConnectUrl(url.ToLower());
                    this.MakeInfo(this.infoNewSetingIsApplied);
                    this.ChangeCertificate();
                    xConnectAPI.xConnect.SumbitConfiguration();

                }
                else
                {
                    this.MakeWarning(this.warningNoHttp);
                }
            }
            else
            {
                this.MakeWarning(this.warningEmptyOrNull);
            }
            this.ResetControls();
        }

        private void ChangeCertificate()
        {
            var cert = xConnectAPI.Certificates.CertificatesFromMyStore.Where(x => x.Name == CertificateList.SelectedValue.ToString()).FirstOrDefault();


           if(cert!=null)
            {
                xConnectAPI.xConnect.CleintThumbprint = cert.ThumPrint;
            }
           else
            {

            }
        }

        private void ChangeXConnectUrl(string url)
        {
            xConnectAPI.xConnect.xConnectUrl = xConnectSiteInput.Text;
        }
        private void MakeWarning(string msg)
        {
            InfoLabel.Content = msg;
            InfoLabel.Foreground = new SolidColorBrush(Colors.Red);
            InfoLabel.Visibility = Visibility.Visible;
        }

        private void MakeInfo(string msg)
        {
            InfoLabel.Content = msg;
            InfoLabel.Foreground = new SolidColorBrush(Colors.Green);
            InfoLabel.Visibility = Visibility.Visible;
        }

        private void ResetControls()
        {
            xConnectApply.IsEnabled = false;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Threading.ThreadStart ts = new System.Threading.ThreadStart(xConnect.Initialize);
            System.Threading.Thread th = new System.Threading.Thread(ts);
            th.Start();
        }
    }
}
