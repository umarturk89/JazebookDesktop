using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.language;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for Edit_MyProfile_Window.xaml
    /// </summary>
    public partial class Edit_MyProfile_Window : Window
    {
        private MainWindow main_Window;

        public Edit_MyProfile_Window(MainWindow main)
        {
            InitializeComponent();
            this.Title = "Edit your profile information (" + Settings.Application_Name + ")";

            GetdataMyProfile();
            My_Profile();
            main_Window = main;
            Txt_Birthday.DisplayDate.ToString("dd-MM-yyyy");
            Txt_About_me.Text = LocalResources.label_Txt_About_me + "(" + Settings.Application_Name + ")";

            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }

            ModeDark_Window();
        }

        //Function Get data My Profile
        public void GetdataMyProfile()
        {
            try
            {
                var data = MemoryVariables.UsersProfileList.FirstOrDefault(a => a.pm_UserId == UserDetails.User_id);
                if (data != null)
                {
                    if (data.pm_Username == "")
                        Txt_Username.Text = LocalResources.label_Empty;
                    else
                        Txt_Username.Text = data.pm_Username;

                    if (data.pm_Email == "")
                        Txt_Email.Text = LocalResources.label_Empty;
                    else
                        Txt_Email.Text = data.pm_Email;

                    if (data.pm_Birthday == "")
                        Txt_Birthday.Text = LocalResources.label_Empty;
                    else
                        Txt_Birthday.Text = data.pm_Birthday;

                    if (data.pm_Phone_number == "")
                        Txt_Phone_number.Text = LocalResources.label_Empty;
                    else
                        Txt_Phone_number.Text = data.pm_Phone_number;

                    if (data.pm_Website == "")
                        Txt_Website.Text = "";
                    else
                        Txt_Website.Text = data.pm_Website;

                    if (data.pm_Address == "")
                        Txt_Address.Text = LocalResources.label_Empty;
                    else
                        Txt_Address.Text = data.pm_Address;

                    if (data.pm_School == "")
                        Txt_School.Text = LocalResources.label_Empty;
                    else
                        Txt_School.Text = data.pm_School;

                    if (data.pm_Gender == "")
                        Sel_Gender.Text = LocalResources.label_Empty;
                    else
                        Sel_Gender.Text = data.pm_Gender;

                    if (data.pm_Facebook == "")
                        Txt_Facebook.Text = LocalResources.label_Empty;
                    else
                        Txt_Facebook.Text = data.pm_Facebook;

                    if (data.pm_Google == "")
                        Txt_Google.Text = LocalResources.label_Empty;
                    else
                        Txt_Google.Text = data.pm_Google;

                    if (data.pm_Twitter == "")
                        Txt_Twitter.Text = LocalResources.label_Empty;
                    else
                        Txt_Twitter.Text = data.pm_Twitter;

                    if (data.pm_Youtube == "")
                        Txt_Youtube.Text = LocalResources.label_Empty;
                    else
                        Txt_Youtube.Text = data.pm_Youtube;

                    if (data.pm_Linkedin == "")
                        Txt_Linkedin.Text = LocalResources.label_Empty;
                    else
                        Txt_Linkedin.Text = data.pm_Linkedin;

                    if (data.pm_Instagram == "")
                        Txt_Instagram.Text = LocalResources.label_Empty;
                    else
                        Txt_Instagram.Text = data.pm_Instagram;

                    if (data.pm_Vk == "")
                        Txt_Vk.Text = LocalResources.label_Empty;
                    else
                        Txt_Vk.Text = data.pm_Vk;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void My_Profile()
        {
            try
            {
                var Data_Memory =
                    MemoryVariables.UsersProfileList.FirstOrDefault(a => a.pm_UserId == UserDetails.User_id);
                if (Data_Memory == null)
                {
                    var data = SQLite_Entity.Connection.Table<DataBase.ProfilesTable>()
                        .FirstOrDefault(a => a.pm_UserId == UserDetails.User_id);
                    if (data != null)
                    {
                        if (data.pm_Username == "")
                            Txt_Username.Text = LocalResources.label_Empty;
                        else
                            Txt_Username.Text = data.pm_Username;

                        if (data.pm_Email == "")
                            Txt_Email.Text = LocalResources.label_Empty;
                        else
                            Txt_Email.Text = data.pm_Email;

                        if (data.pm_Birthday == "")
                            Txt_Birthday.Text = LocalResources.label_Empty;
                        else
                            Txt_Birthday.Text = data.pm_Birthday;

                        if (data.pm_Phone_number == "")
                            Txt_Phone_number.Text = LocalResources.label_Empty;
                        else
                            Txt_Phone_number.Text = data.pm_Phone_number;

                        if (data.pm_Website == "")
                            Txt_Website.Text = "";
                        else
                            Txt_Website.Text = data.pm_Website;

                        if (data.pm_Address == "")
                            Txt_Address.Text = LocalResources.label_Empty;
                        else
                            Txt_Address.Text = data.pm_Address;

                        if (data.pm_School == "")
                            Txt_School.Text = LocalResources.label_Empty;
                        else
                            Txt_School.Text = data.pm_School;

                        if (data.pm_Gender == "")
                            Sel_Gender.Text = LocalResources.label_Empty;
                        else
                            Sel_Gender.Text = data.pm_Gender;

                        if (data.pm_Facebook == "")
                            Txt_Facebook.Text = LocalResources.label_Empty;
                        else
                            Txt_Facebook.Text = data.pm_Facebook;

                        if (data.pm_Google == "")
                            Txt_Google.Text = LocalResources.label_Empty;
                        else
                            Txt_Google.Text = data.pm_Google;

                        if (data.pm_Twitter == "")
                            Txt_Twitter.Text = LocalResources.label_Empty;
                        else
                            Txt_Twitter.Text = data.pm_Twitter;

                        if (data.pm_Youtube == "")
                            Txt_Youtube.Text = LocalResources.label_Empty;
                        else
                            Txt_Youtube.Text = data.pm_Youtube;

                        if (data.pm_Linkedin == "")
                            Txt_Linkedin.Text = LocalResources.label_Empty;
                        else
                            Txt_Linkedin.Text = data.pm_Linkedin;

                        if (data.pm_Instagram == "")
                            Txt_Instagram.Text = LocalResources.label_Empty;
                        else
                            Txt_Instagram.Text = data.pm_Instagram;

                        if (data.pm_Vk == "")
                            Txt_Vk.Text = LocalResources.label_Empty;
                        else
                            Txt_Vk.Text = data.pm_Vk;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Function Edit data My Profile in tap and DataBase
        public void Edit_My_profile()
        {
            var data = SQLite_Entity.Connection.Table<DataBase.ProfilesTable>()
                .FirstOrDefault(a => a.pm_UserId == UserDetails.User_id);
            if (data != null)
            {
                if (data.pm_Birthday != Txt_Birthday.Text)
                {
                    data.pm_Birthday = Txt_Birthday.Text;
                    main_Window.Lbl_Birthday.Text = Txt_Birthday.DisplayDate.ToString("dd-MM-yyyy");
                }
                if (data.pm_Phone_number != Txt_Phone_number.Text)
                {
                    data.pm_Phone_number = Txt_Phone_number.Text;
                    main_Window.Lbl_Phone_number.Text = Txt_Phone_number.Text;
                }
                if (data.pm_Website != Txt_Website.Text)
                {
                    data.pm_Website = Txt_Website.Text;
                    main_Window.Lbl_Website.Text = Txt_Website.Text;
                }
                if (data.pm_Address != Txt_Address.Text)
                {
                    data.pm_Address = Txt_Address.Text;
                    main_Window.Lbl_Address.Text = Txt_Address.Text;
                }
                if (data.pm_School != Txt_School.Text)
                {
                    data.pm_School = Txt_School.Text;
                    main_Window.Lbl_School.Text = Txt_Address.Text;
                }
                if (data.pm_Gender != Sel_Gender.Text)
                {
                    data.pm_Gender = Sel_Gender.Text;
                    main_Window.Lbl_Gender.Text = Sel_Gender.Text;
                }
                if (data.pm_Facebook != Txt_Facebook.Text)
                {
                    data.pm_Facebook = Txt_Facebook.Text;
                    main_Window.Lbl_Facebook.Text = Txt_Facebook.Text;
                }
                if (data.pm_Google != Txt_Google.Text)
                {
                    data.pm_Google = Txt_Google.Text;
                    main_Window.Lbl_Google.Text = Txt_Google.Text;
                }
                if (data.pm_Twitter != Txt_Twitter.Text)
                {
                    data.pm_Twitter = Txt_Twitter.Text;
                    main_Window.Lbl_Twitter.Text = Txt_Twitter.Text;
                }
                if (data.pm_Youtube != Txt_Youtube.Text)
                {
                    data.pm_Youtube = Txt_Youtube.Text;
                    main_Window.Lbl_Youtube.Text = Txt_Youtube.Text;
                }
                if (data.pm_Linkedin != Txt_Linkedin.Text)
                {
                    data.pm_Linkedin = Txt_Linkedin.Text;
                    main_Window.Lbl_Linkedin.Text = Txt_Linkedin.Text;
                }
                if (data.pm_Instagram != Txt_Instagram.Text)
                {
                    data.pm_Instagram = Txt_Instagram.Text;
                    main_Window.Lbl_Instagram.Text = Txt_Instagram.Text;
                }
                if (data.pm_Vk != Txt_Vk.Text)
                {
                    data.pm_Vk = Txt_Vk.Text;
                    main_Window.Lbl_Vk.Text = Txt_Vk.Text;
                }
                SQLiteCommandSender.Insert_Or_Replace_To_ProfileTable(data);
            }

        }

        // click Button save Edit data My Profile
        private void Btn_save_EditProfile_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Txt_Username.Text == null)
                {
                    Txt_Username.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Email.Text))
                {
                    Txt_Email.Text = LocalResources.label_Empty;
                }
                if (Txt_Birthday.Text == null)
                {
                    Txt_Birthday.Text = Txt_Birthday.DisplayDate.ToString("dd-MM-yyyy");
                }
                if (String.IsNullOrEmpty(Txt_Website.Text))
                {
                    Txt_Website.Text = "";
                }
                if (String.IsNullOrEmpty(Txt_Address.Text))
                {
                    Txt_Address.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Phone_number.Text))
                {
                    Txt_Phone_number.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_School.Text))
                {
                    Txt_School.Text = LocalResources.label_Empty;
                }
                if (Sel_Gender.Text == null)
                {
                    Sel_Gender.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Facebook.Text))
                {
                    Txt_Facebook.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Google.Text))
                {
                    Txt_Google.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Twitter.Text))
                {
                    Txt_Twitter.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Youtube.Text))
                {
                    Txt_Youtube.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Linkedin.Text))
                {
                    Txt_Linkedin.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Instagram.Text))
                {
                    Txt_Instagram.Text = LocalResources.label_Empty;
                }
                if (String.IsNullOrEmpty(Txt_Vk.Text))
                {
                    Txt_Vk.Text = LocalResources.label_Empty;
                }

                Regex RX_Website = new Regex(
                    @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*$");

                if (!RX_Website.IsMatch(Txt_Website.Text))
                {
                    Txt_Website.Focus();
                    MessageBox.Show("The Website format is not correct !", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                }
                else
                {
                    if (Txt_Birthday.Text != LocalResources.label_Empty || Txt_Website.Text != LocalResources.label_Empty || Txt_Address.Text != LocalResources.label_Empty
                        || Txt_Phone_number.Text != LocalResources.label_Empty || Txt_School.Text != LocalResources.label_Empty || Sel_Gender.Text != LocalResources.label_Empty
                        || Txt_Facebook.Text != LocalResources.label_Empty || Txt_Google.Text != LocalResources.label_Empty || Txt_Twitter.Text != LocalResources.label_Empty ||
                        Txt_Youtube.Text != LocalResources.label_Empty || Txt_Linkedin.Text != LocalResources.label_Empty || Txt_Instagram.Text != LocalResources.label_Empty ||
                        Txt_Vk.Text != LocalResources.label_Empty
                    )
                    {
                        Edit_My_profile();

                        try
                        {
                            var dictionary_profile = new Dictionary<string, string>
                                {
                                    {"facebook", Txt_Facebook.Text},
                                    {"google", Txt_Google.Text},
                                    {"twitter", Txt_Twitter.Text},
                                    {"youtube", Txt_Youtube.Text},
                                    {"linkedin", Txt_Linkedin.Text},
                                    {"instagram", Txt_Instagram.Text},
                                    {"vk", Txt_Vk.Text},
                                };

                            WoWonderClient.Requests.RequestsAsync.UpdateSettings_Http(UserDetails.User_id, JsonConvert.SerializeObject(dictionary_profile), "profile_settings").ConfigureAwait(false);

                            var dictionary_custom = new Dictionary<string, string>
                                {
                                    {"website", Txt_Website.Text},
                                    {"school", Txt_School.Text},
                                    {"birthday", Txt_Birthday.DisplayDate.ToString("dd-MM-yyyy")},
                                    {"address", Txt_Address.Text},
                                    {"gender", Sel_Gender.Text.ToLower()},
                                };

                            WoWonderClient.Requests.RequestsAsync.UpdateSettings_Http(UserDetails.User_id, JsonConvert.SerializeObject(dictionary_custom), "custom_settings").ConfigureAwait(false);

                            var dictionary_general = new Dictionary<string, string>
                                {
                                    {"phone", Txt_Website.Text},
                                };
                            WoWonderClient.Requests.RequestsAsync.UpdateSettings_Http(UserDetails.User_id, JsonConvert.SerializeObject(dictionary_general), "general_settings").ConfigureAwait(false);


                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                    }
                }
            }
            catch (Exception es)
            {
                es.ToString();
            }
        }

        private void Btn_Close_OnClick(object sender, RoutedEventArgs e)
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

                //Left Grid >>>
                LeftGrid.Background = new SolidColorBrush(DarkBackgroundColor);

                Lbl_Profile_Settings.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Profile_SettingsGrid.Background = new SolidColorBrush(DarkBackgroundColor);
                Icon_Username.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Username.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Email_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Email.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Icon_Username.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Username.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Birthday_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Birthday.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Phone_number_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Phone_number.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Website_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Website.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Address_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Address.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                School_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_School.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Gender_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Sel_Gender.Background = new SolidColorBrush(DarkBackgroundColor);
                Sel_Gender.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                BoxItem_male.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                BoxItem_female.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                About_me_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_About_me.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                //Right Grid >>>
                RightGrid.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Social_Links.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Social_LinksGrid.Background = new SolidColorBrush(DarkBackgroundColor);
                Txt_Facebook.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Google.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Twitter.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Youtube.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Linkedin.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Instagram.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Vk.Foreground = new SolidColorBrush(WhiteBackgroundColor);
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

        private void TitleApp_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var TitleApp = sender as TextBlock;
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

                if (MainWindow.ModeDarkstlye)
                {
                    TitleApp.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                }
                else
                {
                    TitleApp.Foreground = new SolidColorBrush(DarkBackgroundColor);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_Minimize_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var ButtonWindow = sender as Button;
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

                if (MainWindow.ModeDarkstlye)
                {
                    ButtonWindow.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                }
                else
                {
                    ButtonWindow.Foreground = new SolidColorBrush(DarkBackgroundColor);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

    }
}
