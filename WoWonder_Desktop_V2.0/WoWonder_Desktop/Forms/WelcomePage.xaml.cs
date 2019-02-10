using System;
using System.Windows;
using WoWonder_Desktop.Pages;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Window
    {
        public static int Numberpage = 0;
        public WelcomePage()
        {
            InitializeComponent();
            this.Title = "Welcome (" + Settings.Application_Name + ")";

        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            try
            {
                Window window = (Window)sender;
                window.Topmost = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }           
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RightArrowbutton.Kind == MaterialDesignThemes.Wpf.PackIconKind.Check)
                {
                    this.Close();
                }

                if (Numberpage == 0)
                {
                    FrameNavigator.Content = new W_MobilePage();
                    Numberpage = 1;
                }
                else if (Numberpage == 1)
                {
                    FrameNavigator.Content = new W_StickersNewPage();
                    Numberpage = 2;
                }
                else if (Numberpage == 2)
                {
                    FrameNavigator.Content = new W_VideoPage();
                    Numberpage = 3;
                    RightArrowbutton.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FrameNavigator.CanGoBack)
                {
                    Numberpage--;
                    FrameNavigator.GoBack();
                    RightArrowbutton.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowRight;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
