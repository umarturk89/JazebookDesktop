using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WoWonder_Desktop.Controls;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for Edit_MyProfile_Window.xaml
    /// </summary>
    public partial class PickColorWindow : Window
    {
        private string IDuser;
        private MainWindow Main;
        public PickColorWindow(string Id_user, MainWindow window)
        {
            InitializeComponent();
            this.Title = "Pick Color (" + Settings.Application_Name + ")";

            if (Settings.WebException_Security)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                       | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12
                                                       | SecurityProtocolType.Ssl3;
            }

            IDuser = Id_user;
            Main = window;

            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            ModeDark_Window();
        }

        public void ChangeColor(string Hex_Color)
        {
            try
            {

                var Updater_color =
                    MainWindow.ListMessages.Where(a => a.Mes_To_Id == IDuser);
                if (Updater_color.Count() > 0)
                {
                    foreach (var items in Updater_color)
                    {
                        items.Color_box_message = Hex_Color;
                    }

                    var ChatPanelColor = (Color)ColorConverter.ConvertFromString(Hex_Color);
                    var ChatForegroundColor = (Color)ColorConverter.ConvertFromString("#ffff");

                    if (Settings.Change_ChatPanelColor)
                    {
                        Main.ChatInfoPanel.Background = new SolidColorBrush(ChatPanelColor);
                    }


                    Main.ProfileToggle.Background = new SolidColorBrush(ChatPanelColor);
                    Main.ProfileToggle.Foreground = new SolidColorBrush(ChatForegroundColor);
                    MainWindow.ChatColor = Hex_Color;
                    Main.DropDownMenueOnMessageBox.Foreground = new SolidColorBrush(ChatForegroundColor);
                    Main.ChatTitleChange.Foreground = new SolidColorBrush(ChatForegroundColor);
                    Main.ChatSeen.Foreground = new SolidColorBrush(ChatForegroundColor);
                   
                    WoWonderClient.Requests.RequestsAsync.Change_Colors_Http(UserDetails.User_id,IDuser, Hex_Color).ConfigureAwait(false);
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Btn_b582af_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#b582af");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_a84849_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#a84849");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_fc9cde_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#fc9cde");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_f9c270_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#f9c270");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_70a0e0_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#70a0e0");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_56c4c5_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#56c4c5");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_f33d4c_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#f33d4c");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_a1ce79_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#a1ce79");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_a085e2_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#a085e2");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_ed9e6a_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#ed9e6a");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_2b87ce_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#2b87ce");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_f2812b_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#f2812b");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_0ba05d_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#0ba05d");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_0e71ea_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#0e71ea");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_aa2294_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeColor("#aa2294");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ModeDark_Window()
        {
            var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
            var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

            if (MainWindow.ModeDarkstlye)
            {
                this.Background = new SolidColorBrush(DarkBackgroundColor);

                Border.Background = new SolidColorBrush(DarkBackgroundColor);

                Lbl_Pick_color.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Everyone_in_this_conversation.Foreground = new SolidColorBrush(WhiteBackgroundColor);
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var GridWindow = sender as Grid;
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

                if (MainWindow.ModeDarkstlye)
                {
                    GridWindow.Background = new SolidColorBrush(DarkBackgroundColor);
                }
                else
                {
                    GridWindow.Background = new SolidColorBrush(WhiteBackgroundColor);

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
