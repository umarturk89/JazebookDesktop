using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for MsgPopupWindow.xaml
    /// </summary>
    public partial class MsgPopupWindow : Window
    {
        private string user_id;
        public MsgPopupWindow(string messeges, string username, string image, string userId)
        {
            InitializeComponent();
            this.Title = "Msg Popup (" + Settings.Application_Name + ")";

            P_username.Text = username;
            P_msgContent.Text = messeges;
            user_id = userId;
            profileimage.Source = new BitmapImage(new Uri(image, UriKind.Absolute));
            Stylechanger();

            if (Settings.WebException_Security)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                       | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12
                                                       | SecurityProtocolType.Ssl3;
            }

            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }

            if (Settings.NotificationPlaysound == "true")
            {
                var s = SQLite_Entity.Connection.Table<DataBase.UsersContactProfileTable>().FirstOrDefault(a => a.UCP_Id == user_id);
                if (s != null)
                {
                    if (s.UCP_Notifications_Message_Sound_user == "true")
                    {
                        var Popupsound = Functions.Main_Destination + "Popupsound.mp3";
                        if (Directory.Exists(Popupsound) == false)
                        {
                            File.WriteAllBytes(Popupsound, Properties.Resources.Popupsound);
                        }

                        MediaPlayer mediaPlayer = new MediaPlayer();
                        mediaPlayer.Open(new Uri(Popupsound));
                        mediaPlayer.Play();
                        StartTimer();
                    }
                }
            }
            ModeDark_Window();
        }

        DispatcherTimer timer = null;
        void StartTimer()
        {
            try
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(5);
                timer.Tick += new EventHandler(timer_Elapsed);
                timer.Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        void timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                timer.Stop();
                PopUpform.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        public void Stylechanger()
        {
            try
            {
                Color BgColor = (Color)ColorConverter.ConvertFromString(Settings.PopUpBackroundColor);
                Color FrColor = (Color)ColorConverter.ConvertFromString(Settings.PopUpTextFromcolor);
                Color FrmsgColor = (Color)ColorConverter.ConvertFromString(Settings.PopUpMsgTextcolor);

                Backround.Background = new SolidColorBrush(BgColor);
                P_username.Foreground = new SolidColorBrush(FrColor);
                P_msgContent.Foreground = new SolidColorBrush(FrmsgColor);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void ModeDark_Window()
        {
            var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
            var LigthBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

            if (MainWindow.ModeDarkstlye)
            {
                this.Background = new SolidColorBrush(DarkBackgroundColor);

                Backround.Background = new SolidColorBrush(DarkBackgroundColor);
                mask.Background = new SolidColorBrush(DarkBackgroundColor);

                P_username.Foreground = new SolidColorBrush(LigthBackgroundColor);
                P_msgContent.Foreground = new SolidColorBrush(LigthBackgroundColor);
            }
        }
    }
}
