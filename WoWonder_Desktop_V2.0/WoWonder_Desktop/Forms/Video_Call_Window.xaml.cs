using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Timers;
using CefSharp;
using WoWonder_Desktop.Controls;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for Video_Call_Window.xaml
    /// </summary>
    public partial class Video_Call_Window : Window
    {
        private Timer t;
        private int h, m, s;
        private MainWindow Main_Window;
        private Classes.Call_Video CV;
        public Video_Call_Window(string Result , MainWindow main , Classes.Call_Video cv)
        {
            InitializeComponent();
            this.Title = "Video Call (" + Settings.Application_Name + ")";

            VideoWEBRTC.Address = Result;
            Main_Window = main;
            CV = cv;

            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }

            if (Settings.WebException_Security)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                       | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12
                                                       | SecurityProtocolType.Ssl3;
            }

            // Timer Control
            t = new Timer();
            t.Interval = 1000; // 1 Sec
            t.Elapsed += OnTimerEvent;
            t.Start();
        }

        // Window State Minimized 
        private void Hide_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Window State Maximized 
        private void BtnFullScreenExpand_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        // Event Timer
        private void OnTimerEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                s++;
                if (s == 60)
                {
                    s = 0;
                    m++;
                }
                if (m == 60)
                {
                    m = 0;
                    h++;
                }
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    lbl_Status_time.Content = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'),
                        m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        // Close Timer
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Main_Call_Comming_Form = "False";
            t.Close();
            this.Close();
        }

        private void BtnMessage_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var dataUsers = MainWindow.ListUsers.FirstOrDefault(a => a.U_Id == CV.Call_Video_user_id);
                if (dataUsers != null)
                {
                    var index = MainWindow.ListUsers.IndexOf(MainWindow.ListUsers.Where(a => a.U_Id == CV.Call_Video_user_id).FirstOrDefault());
                    if (index > -1)
                    {
                        Main_Window.ChatActivityList.SelectedIndex = index;
                        this.WindowState = WindowState.Minimized;
                    }
                }
                else
                {
                    var data_Users = MainWindow.ListUsersContact.FirstOrDefault(a => a.UC_Id == CV.Call_Video_user_id);
                    if (data_Users != null)
                    {
                        var index = MainWindow.ListUsersContact.IndexOf(MainWindow.ListUsersContact.Where(a => a.UC_Id == CV.Call_Video_user_id).FirstOrDefault());
                        if (index > -1)
                        {
                            Main_Window.UserContacts_list.SelectedIndex = index;
                            this.WindowState = WindowState.Minimized;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void VideoWEBRTC_OnMouseEnter(object sender, MouseEventArgs e)
        {
            BorderVideoControls.Visibility = Visibility.Visible;
        }

        private void VideoWEBRTC_OnMouseLeave(object sender, MouseEventArgs e)
        {
            BorderVideoControls.Visibility = Visibility.Collapsed;
        }

        private void VideoWEBRTC_OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
          
        }
        private void VideoControls_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (BorderVideoControls.IsVisible)
            {

                BorderVideoControls.Visibility = Visibility.Collapsed;
            }  
            else
             {
                 BorderVideoControls.Visibility = Visibility.Visible;
            }
       }

        private void BtnFullScreenCompress_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void VideoWEBRTC_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (BorderVideoControls.IsVisible)
            {

                BorderVideoControls.Visibility = Visibility.Collapsed;
            }
            else
            {
                BorderVideoControls.Visibility = Visibility.Visible;
            }
        }
   
    }
}
