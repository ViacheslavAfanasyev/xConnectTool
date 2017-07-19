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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            this.LoadProgressBar();
        }

        private void LoadProgressBar()
        {
            Duration dur = new Duration(TimeSpan.FromSeconds(1));
            DoubleAnimation dblani = new DoubleAnimation(100, dur);
            //ProgressBar.BeginAnimation(ProgressBar.ValueProperty, dblani);
        }

        private void GeneralSettings(object sender, RoutedEventArgs e)
        {
            var generalSettingsWindow = new Windows.GeneralSettings();
            generalSettingsWindow.ShowDialog();
        }
    }
}
