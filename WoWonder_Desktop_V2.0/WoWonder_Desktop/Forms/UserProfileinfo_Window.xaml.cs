using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.language;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for UserProfileinfo_Window.xaml
    /// </summary>
    public partial class UserProfileinfo_Window : Window
    {
        public UserProfileinfo_Window(UsersContactProfile  data , UsersContact datauserContact , string typedata)
        {
            InitializeComponent();
            this.Title = "User Profile information (" + Settings.Application_Name + ")";

            GetdataMyProfile(data, datauserContact, typedata);
            lbl_About_me.Content = LocalResources.label_Txt_About_me + "(" + Settings.Application_Name + ")";

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
            ModeDark_Window();
        }
        //Function Get data My Profile

        public void GetdataMyProfile(UsersContactProfile data , UsersContact datauserContact , string typedata)
        {
            try
            {
                if (typedata == "UsersContactProfile")
                {
                    if (data != null)
                    {
                        if (data.UCP_username == "")
                            Lbl_ucp_Username.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Username.Content = data.UCP_first_name + " " + data.UCP_last_name + " ( " +
                                                       data.UCP_username + " ) ";
                        if (data.UCP_email == "")
                            Lbl_ucp_Email.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Email.Content = data.UCP_email;

                        if (data.UCP_birthday == "")
                            Lbl_ucp_Birthday.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Birthday.Content = data.UCP_birthday;

                        if (data.UCP_phone_number == "")
                            Lbl_ucp_Phone_number.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Phone_number.Content = data.UCP_phone_number;

                        if (data.UCP_website == "")
                            Lbl_ucp_Website.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Website.Content = data.UCP_website;

                        if (data.UCP_address == "")
                            Lbl_ucp_Address.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Address.Content = data.UCP_address;

                        if (data.UCP_school == "")
                            Lbl_ucp_School.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_School.Content = data.UCP_school;

                        if (UseLayoutRounding)
                            Lbl_ucp_Gender.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Gender.Content = data.UCP_gender;

                        if (data.UCP_facebook == "")
                            Lbl_ucp_Facebook.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Facebook.Content = data.UCP_facebook;

                        if (data.UCP_google == "")
                            Lbl_ucp_Google.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Google.Content = data.UCP_google;

                        if (data.UCP_twitter == "")
                            Lbl_ucp_Twitter.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Twitter.Content = data.UCP_twitter;

                        if (data.UCP_youtube == "")
                            Lbl_ucp_Youtube.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Youtube.Content = data.UCP_youtube;

                        if (data.UCP_linkedin == "")
                            Lbl_ucp_Linkedin.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Linkedin.Content = data.UCP_linkedin;

                        if (data.UCP_instagram == "")
                            Lbl_ucp_Instagram.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Instagram.Content = data.UCP_instagram;

                        if (data.UCP_vk == "")
                            Lbl_ucp_Vk.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Vk.Content = data.UCP_vk;
                    }
                }
                else
                {
                    if (datauserContact != null)
                    {
                        if (datauserContact.UC_username == "")
                            Lbl_ucp_Username.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Username.Content = datauserContact.UC_first_name + " " + datauserContact.UC_last_name + " ( " +
                                                       datauserContact.UC_username + " ) ";
                        if (datauserContact.UC_email == "")
                            Lbl_ucp_Email.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Email.Content = datauserContact.UC_email;

                        if (datauserContact.UC_birthday == "")
                            Lbl_ucp_Birthday.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Birthday.Content = datauserContact.UC_birthday;

                        if (datauserContact.UC_phone_number == "")
                            Lbl_ucp_Phone_number.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Phone_number.Content = datauserContact.UC_phone_number;

                        if (datauserContact.UC_website == "")
                            Lbl_ucp_Website.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Website.Content = datauserContact.UC_website;

                        if (datauserContact.UC_address == "")
                            Lbl_ucp_Address.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Address.Content = datauserContact.UC_address;

                        if (datauserContact.UC_school == "")
                            Lbl_ucp_School.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_School.Content = datauserContact.UC_school;

                        if (UseLayoutRounding)
                            Lbl_ucp_Gender.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Gender.Content = datauserContact.UC_gender;

                        if (datauserContact.UC_facebook == "")
                            Lbl_ucp_Facebook.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Facebook.Content = datauserContact.UC_facebook;

                        if (datauserContact.UC_google == "")
                            Lbl_ucp_Google.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Google.Content = datauserContact.UC_google;

                        if (datauserContact.UC_twitter == "")
                            Lbl_ucp_Twitter.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Twitter.Content = datauserContact.UC_twitter;

                        if (datauserContact.UC_youtube == "")
                            Lbl_ucp_Youtube.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Youtube.Content = datauserContact.UC_youtube;

                        if (datauserContact.UC_linkedin == "")
                            Lbl_ucp_Linkedin.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Linkedin.Content = datauserContact.UC_linkedin;

                        if (datauserContact.UC_instagram == "")
                            Lbl_ucp_Instagram.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Instagram.Content = datauserContact.UC_instagram;

                        if (datauserContact.UC_vk == "")
                            Lbl_ucp_Vk.Content = LocalResources.label_Empty;
                        else
                            Lbl_ucp_Vk.Content = datauserContact.UC_vk;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_CloseProfile_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                Lbl_ucp_Username.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Email_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Email.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Birthday_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Birthday.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Phone_number.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Phone_number_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Website.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Website_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Address.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Address_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_School.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                School_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Gender.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Gender_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                lbl_About_me.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                About_me_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                //Right Grid >>>
                RightGrid.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Social_Links.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Social_LinksGrid.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_ucp_Facebook.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Google.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Twitter.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Youtube.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Linkedin.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Instagram.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_ucp_Vk.Foreground = new SolidColorBrush(WhiteBackgroundColor);
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

        private void Btn_Close_OnLoaded(object sender, RoutedEventArgs e)
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
    }
}
