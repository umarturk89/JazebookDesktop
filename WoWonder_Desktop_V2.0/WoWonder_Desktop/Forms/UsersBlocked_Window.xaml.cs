using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WoWonderClient.Classes.User;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for UsersBlocked_Window.xaml
    /// </summary>
    public partial class UsersBlocked_Window : Window
    {
        #region Variables

        string Id_user = "";
        string Id_to = "";
        string Id_from = "";
        private MainWindow _MainWindow;

        #endregion

        public static BackgroundWorker bgd_Worker_Block_User = new BackgroundWorker();

        public UsersBlocked_Window(string IDuser, string ID_From, string ID_To, MainWindow main)
        {
            InitializeComponent();
            this.Title = "Users Blocked (" + Settings.Application_Name + ")";

            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }

            Id_user = IDuser;
            Id_from = ID_From;
            Id_to = ID_To;
            _MainWindow = main;

            if (Settings.WebException_Security)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                       | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12
                                                       | SecurityProtocolType.Ssl3;
            }
            ModeDark_Window();
        }

        private void Btn_Block_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bgd_Worker_Block_User = new BackgroundWorker();
                bgd_Worker_Block_User.DoWork += bgd_Worker_Block_User_DoWork;
                bgd_Worker_Block_User.ProgressChanged += bgd_Worker_Block_User_ProgressChanged;
                bgd_Worker_Block_User.RunWorkerCompleted += bgd_Worker_Block_User_RunWorkerCompleted;
                bgd_Worker_Block_User.WorkerSupportsCancellation = true;
                bgd_Worker_Block_User.WorkerReportsProgress = true;
                bgd_Worker_Block_User.RunWorkerAsync();

                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Run background worker : bgd_Worker_Block_User
        async void bgd_Worker_Block_User_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                var response = await WoWonderClient.Requests.RequestsAsync.Block_User(UserDetails.User_id,Id_user);
                if (response.Item1 == 200)
                {
                    if (bgd_Worker_Block_User.CancellationPending == true)
                    {
                        e.Cancel = true;
                    }

                    if (response.Item2 is BlockUserObject result)
                    {
                        SQLiteCommandSender.removeUser_All_Table(Id_user, Id_from, Id_to);

                        Functions.Delete_dataFile_user(Id_user);

                        var delete = MainWindow.ListUsers.FirstOrDefault(a => a.U_Id == Id_user);
                        if (delete != null)
                        {
                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                MainWindow.ListUsers.Remove(delete);
                                MainWindow.ListMessages.Clear();
                                MainWindow.ListSharedFiles.Clear();
                                MainWindow.ListUsersProfile.Clear();

                                _MainWindow.ChatActivityList.SelectedIndex = 0;

                                //Scroll Top >> 
                                _MainWindow.ChatActivityList.ScrollIntoView(_MainWindow.ChatActivityList.SelectedItem);
                                _MainWindow.RightMainPanel.Visibility = Visibility.Collapsed;
                            });
                        }
                    }
                   
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        void bgd_Worker_Block_User_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //ProgressBar_Search_Gifs.Value = e.ProgressPercentage;
        }

        void bgd_Worker_Block_User_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (bgd_Worker_Block_User.WorkerSupportsCancellation == true)
                {
                    bgd_Worker_Block_User.CancelAsync();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
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

                Lbl_blocked.Foreground = new SolidColorBrush(WhiteBackgroundColor);
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
