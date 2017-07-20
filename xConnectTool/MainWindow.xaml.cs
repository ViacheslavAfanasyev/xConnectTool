using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using xConnectAPI;

namespace xConnectTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadingControlXConnect.Visibility = Visibility.Visible;
            //xConnectAPI.xConnect.Initialize();
            var result = RunInitializationXConnectApi();
            if (result==Result.NoConfigFile||result==Result.ConfigFileIsEmpty)
            {
                OpenGeneralSettingsWindow();
            }

            //LoadingControlXConnect.Visibility = Visibility.Hidden;
        }

        //public static Result RunInitializationXConnectApi()
        //{
        //    System.Threading.ThreadStart ts = new System.Threading.ThreadStart(xConnect.Initialize);
        //    System.Threading.Thread th = new System.Threading.Thread(ts);
        //    th.Start();
        //}
        public static Result RunInitializationXConnectApi()
        {
            return xConnect.Initialize();
        }

    private void GeneralSettings(object sender, RoutedEventArgs e)
        {
            OpenGeneralSettingsWindow();
        }
        private void OpenGeneralSettingsWindow()
        {
            var generalSettingsWindow = new Windows.GeneralSettings();
            generalSettingsWindow.ShowDialog();
        }
    }
}
