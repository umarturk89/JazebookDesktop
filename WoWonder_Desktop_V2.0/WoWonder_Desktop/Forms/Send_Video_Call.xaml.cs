using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Newtonsoft.Json;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.language;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for Send_Video_Call.xaml
    /// </summary>

    public partial class Send_Video_Call : Window
    {
        #region Variables

        public static string Call_ID = "";
        public static string Url = "";
        public static string Access_Token = "";
        public static string recipt_ID = "";
        public static string User_Name = "";
        public static string main_avatar = "";
        private MainWindow Main_Window;
        #endregion

        #region Timer And MediaPlayer

        DispatcherTimer timer = null;
        DispatcherTimer timer2 = null;
        private int counTCallTime = 0;
        MediaPlayer mediaPlayer = new MediaPlayer();

        #endregion

        public Send_Video_Call(string UserName, string reciptID, ImageSource avatar, MainWindow main)
        {
            try
            {
                InitializeComponent();
                this.Title = "Send Video Call (" + Settings.Application_Name + ")";

                TextOfcall.Text = LocalResources.label_Calling + " " + UserName;
                ProfileImage.Source = avatar;
                main_avatar = avatar.ToString();
                recipt_ID = reciptID;
                User_Name = UserName;
                Main_Window = main;

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


                CreateCallAsync(reciptID);



                var Callsound = Functions.Main_Destination + "calling.mp3";
                if (Directory.Exists(Callsound) == false)
                {
                    File.WriteAllBytes(Callsound, Properties.Resources.calling);
                }

                mediaPlayer.Open(new Uri(Callsound));
                mediaPlayer.Volume = 1;
                mediaPlayer.Play();
                StartTimer();
                StartApiWaitAnswer();

                ModeDark_Window();
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        async void CreateCallAsync(string reciptID)
        {
            //Send_Video_Call
            var data = await WoWonderClient.Requests.RequestsAsync.Create_Video_Call_Answer_Web(UserDetails.User_id, reciptID);

            if (data.Item1 == 200)
            {
                //to do : NOT FINISHED 
            }

        }

        void StartTimer()
        {
            try
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(18);
                timer.Tick += new EventHandler(timer_Elapsed);
                timer.Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        void StartApiWaitAnswer()
        {
            try
            {
                timer2 = new DispatcherTimer();
                timer2.Interval = TimeSpan.FromSeconds(5);
                timer2.Tick += new EventHandler(Cheker_Elapsed);
                timer2.Start();
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
                if (counTCallTime <= 75)
                {
                    var Callsound = Functions.Main_Destination + "calling.mp3";
                    mediaPlayer.Open(new Uri(Callsound));
                    mediaPlayer.Volume = 1;
                    mediaPlayer.Play();
                    counTCallTime += 18;
                }
                else
                {

                    WoWonderClient.Requests.RequestsAsync.Send_Video_Call_Answer_Web("close", Call_ID).ConfigureAwait(false);

                    this.Close();
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        void Cheker_Elapsed(object sender, EventArgs e)
        {
            try
            {
                Checke_Video_Call_Answer(Call_ID).ConfigureAwait(false);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public async Task Checke_Video_Call_Answer(string Call_ID)  //begovsky maybe change 
        {
            try
            {
                var response =
                    await WoWonderClient.Requests.RequestsAsync.API_Check_For_Video_Answer(UserDetails.User_id,
                        Send_Video_Call.Call_ID);

                if (response.Item1 == 200)
                {
                    timer2.Stop();
                    timer.Stop();

                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        mediaPlayer.Stop();

                        Classes.Call_Video CV = new Classes.Call_Video();

                        CV.Call_Video_user_id = recipt_ID;
                        CV.Call_Video_User_Name = User_Name;
                        CV.Call_Video_Avatar = main_avatar;
                        CV.Call_Video_Call_id = Call_ID;
                        CV.Call_Video_Tupe_icon = "CallMade";
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
                        Main_Window.Calls_list.ItemsSource = MainWindow.ListCall;

                        SQLiteCommandSender.Insert_To_CallVideoTable(CV);

                        Video_Call_Window Video_CallFrm = new Video_Call_Window(Url, Main_Window, CV);
                        Video_CallFrm.Show();
                        this.Close();
                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        mediaPlayer.Stop();
                        TextOfcall.Text = User_Name + LocalResources.label_Declined_your_call;

                        Task.Run(() =>
                        {
                            Thread.Sleep(4500);
                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                mediaPlayer.Close();
                                this.Close();
                            });
                        });
                    });
                }

            }
            catch (Exception ex)
            {
                var ff = ex.ToString();
                this.Close();
            }
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
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

            Classes.Call_Video CV = new Classes.Call_Video();

            CV.Call_Video_user_id = recipt_ID;
            CV.Call_Video_User_Name = User_Name;
            CV.Call_Video_Call_id = Call_ID;
            CV.Call_Video_Avatar = main_avatar;
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
            Main_Window.Calls_list.ItemsSource = MainWindow.ListCall;
            SQLiteCommandSender.Insert_To_CallVideoTable(CV);
            if (Main_Window.No_call.Visibility == Visibility.Visible)
            {
                Main_Window.No_call.Visibility = Visibility.Collapsed;
            }


            WoWonderClient.Requests.RequestsAsync.Send_Video_Call_Answer_Web("close", Call_ID).ConfigureAwait(false);
            

            timer2.Stop();
            timer.Stop();
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
