using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.language;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for UsersBlocked_Window.xaml
    /// </summary>
    public partial class Recieve_Call_Window : Window
    {
        #region Variables

        string Main_IDuser = "";
        string Main_Name = "";
        string Main_callID = "";
        string Main_avatar = "";
        private MainWindow _MainWindow;

        DispatcherTimer timer = null;
        private int counTCallTime = 0;


        #endregion
        MediaPlayer mediaPlayer = new MediaPlayer();

        public Recieve_Call_Window(string IDuser, string avatar, string Name, string callID, MainWindow main)
        {
            InitializeComponent();
            this.Title = "Recieve Call (" + Settings.Application_Name + ")";

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

            try
            {
                Main_IDuser = IDuser;
                Main_Name = Name;
                Main_callID = callID;
                Main_avatar = avatar;
                string AvatarSplit = Main_avatar.Split('/').Last();
                ProfileImage.Source = new BitmapImage(new Uri(Functions.Get_image(Main_IDuser, AvatarSplit, Main_avatar)));
                _MainWindow = main;

                TextOfcall.Text = Name + " " + LocalResources.label_is_calling_you;

                var Callsound = Functions.Main_Destination + "videocall.mp3";
                if (Directory.Exists(Callsound) == false)
                {
                    File.WriteAllBytes(Callsound, Properties.Resources.video_call);
                }

                mediaPlayer.Open(new Uri(Callsound));
                mediaPlayer.Volume = 1;
                mediaPlayer.Play();
                StartTimer();


                ModeDark_Window();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void StartTimer()
        {
            try
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(3);
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
                if (counTCallTime <= 40 && MainWindow.Main_Call_Comming == "True")
                {
                    var Callsound = Functions.Main_Destination + "videocall.mp3";
                    mediaPlayer.Open(new Uri(Callsound));
                    mediaPlayer.Volume = 1;
                    mediaPlayer.Play();
                    counTCallTime += 3;
                }
                else
                {
                    this.Close();
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        private async void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mediaPlayer.Stop();
                var Callsound = Functions.Main_Destination + "Decline_Call.mp3";
                if (Directory.Exists(Callsound) == false)
                {
                    File.WriteAllBytes(Callsound, Properties.Resources.Decline_Call);
                }

                mediaPlayer.Open(new Uri(Callsound));
                mediaPlayer.Volume = 1;
                mediaPlayer.Play();

                string AvatarSplit = Main_avatar.Split('/').Last();

                Classes.Call_Video CV = new Classes.Call_Video();

                CV.Call_Video_user_id = Main_IDuser;
                CV.Call_Video_Avatar = Functions.Get_image(Main_IDuser, AvatarSplit, Main_avatar);
                CV.Call_Video_User_Name = Main_Name;
                CV.Call_Video_Call_id = Main_callID;
                CV.Call_Video_Tupe_icon = "CallMissed";
                CV.Call_Video_Color_icon = "red";
                CV.Call_Video_User_DataTime = Functions.Get_datatime();
                if (MainWindow.ModeDarkstlye)
                {
                    CV.S_Color_Background = "#232323";
                    CV.S_Color_Foreground = "#efefef";
                }
                else
                {
                    CV.S_Color_Background = "#ffff";
                    CV.S_Color_Foreground = "#444";
                }

                MainWindow.ListCall.Insert(0, CV);
                _MainWindow.Calls_list.ItemsSource = MainWindow.ListCall;

                SQLiteCommandSender.Insert_To_CallVideoTable(CV);
                if (_MainWindow.No_call.Visibility == Visibility.Visible)
                {
                    _MainWindow.No_call.Visibility = Visibility.Collapsed;
                }

                timer.Stop();

                await WoWonderClient.Requests.RequestsAsync.Send_Video_Call_Answer_Web("decline", Main_callID).ConfigureAwait(false);

                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private async void Btn_Accept_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                timer.Stop();

                var data = await WoWonderClient.Requests.RequestsAsync.Send_Video_Call_Answer_Web("answer", Main_callID);

                if (data.Item1 == 200)
                {

                    string AvatarSplit = Main_avatar.Split('/').Last();

                    Classes.Call_Video CV = new Classes.Call_Video();

                    CV.Call_Video_user_id = Main_IDuser;
                    CV.Call_Video_Avatar = Functions.Get_image(Main_IDuser, AvatarSplit, Main_avatar);
                    CV.Call_Video_User_Name = Main_Name;
                    CV.Call_Video_Call_id = Main_callID;
                    CV.Call_Video_Tupe_icon = "CallReceived";
                    CV.Call_Video_Color_icon = "green";
                    CV.Call_Video_User_DataTime = Functions.Get_datatime();
                    if (MainWindow.ModeDarkstlye)
                    {
                        CV.S_Color_Background = "#232323";
                        CV.S_Color_Foreground = "#efefef";
                    }
                    else
                    {
                        CV.S_Color_Background = "#ffff";
                        CV.S_Color_Foreground = "#444";
                    }

                    MainWindow.ListCall.Insert(0, CV);
                    _MainWindow.Calls_list.ItemsSource = MainWindow.ListCall;
                    SQLiteCommandSender.Insert_To_CallVideoTable(CV);
                    if (_MainWindow.No_call.Visibility == Visibility.Visible)
                    {
                        _MainWindow.No_call.Visibility = Visibility.Collapsed;
                    }
                    Video_Call_Window VideoCallFrm = new Video_Call_Window(data.Item2["url"], _MainWindow, CV); //maybe bug check
                    VideoCallFrm.Show();
                    this.Close();

                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        public void ModeDark_Window()
        {
            var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
            var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

            if (MainWindow.ModeDarkstlye)
            {
                this.Background = new SolidColorBrush(DarkBackgroundColor);

                Border.Background = new SolidColorBrush(DarkBackgroundColor);

                Lbl_callingUser.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                TextOfcall.Foreground = new SolidColorBrush(WhiteBackgroundColor);
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
