using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WoWonder_Desktop.Pages
{
    /// <summary>
    /// Interaction logic for W_MobilePage.xaml
    /// </summary>
    public partial class W_MobilePage : Page
    {
        public W_MobilePage()
        {
            InitializeComponent();
            if (Settings.MobileURL_Visibility)
            {
                Panel_GetAndroidLink.Visibility = Visibility.Visible;
            }
            else
            {
                Panel_GetAndroidLink.Visibility = Visibility.Collapsed;
            }
        }

        private void GetAndroidLinkButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Settings.MobileURL);
        }
    }
}
