using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WoWonder_Desktop.language;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for NotificationsControlWindow.xaml
    /// </summary>
    public partial class NotificationsControlWindow : Window
    {
        private string user_id;

        public NotificationsControlWindow(string UserID)
        {
            InitializeComponent();
            this.Title = "Notifications Control (" + Settings.Application_Name + ")";

            user_id = UserID;
            UserInfo(UserID);
            getNotificationsettings();

            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            ModeDark_Window();
        }

        // Functions Get User Info
        private void UserInfo(string UserID)
        {
            try
            {
                var UserInfo = MainWindow.ListUsers.FirstOrDefault(a => a.U_Id == UserID);
                if (UserInfo != null)
                {
                    Lbl_Control_your_Notifications.Content = LocalResources.label_Control_your_Notifications + UserInfo.U_name;
                }
                else
                {
                    var UserContact = MainWindow.ListUsersContact.FirstOrDefault(a => a.UC_Id == UserID);
                    if (UserContact != null)
                    {
                        Lbl_Control_your_Notifications.Content = LocalResources.label_Control_your_Notifications + UserContact.UC_name;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Functions Get  Notification Settings User
        private void getNotificationsettings()
        {
            try
            {
               
                var setting = SQLite_Entity.Connection.Table<DataBase.UsersContactProfileTable>().FirstOrDefault(a => a.UCP_Id == user_id);
                if (setting != null)
                {
                    Chk_Receive_notifications.IsChecked = Convert.ToBoolean(setting.UCP_Notifications_Message_user);
                    Chk_Enable_Sound_notifications.IsChecked = Convert.ToBoolean(setting.UCP_Notifications_Message_Sound_user);
                }
             
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Close 
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            var Chek1 = "false";
            var Chek2 = "false";
            if (Chk_Enable_Sound_notifications.IsChecked == true)
            {
                Chek1 = "true";
            }

            if (Chk_Receive_notifications.IsChecked == true)
            {
                Chek2 = "true";
            }
           
            var s = SQLite_Entity.Connection.Table<DataBase.UsersContactProfileTable>().FirstOrDefault(a => a.UCP_Id == user_id);
            if (s != null)
            {
                s.UCP_Notifications_Message_Sound_user = Chek1;
                s.UCP_Notifications_Message_user = Chek2;

                SQLiteCommandSender.Update_one_UsersContactTable(s);

            }
            var sss = SQLite_Entity.Connection.Table<DataBase.UsersContactProfileTable>().FirstOrDefault(a => a.UCP_Id == user_id);

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

                Lbl_Conversation_Notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Control_your_Notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                Lbl_Message_Notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Chk_Receive_notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Message_Sound.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Chk_Enable_Sound_notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);
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
