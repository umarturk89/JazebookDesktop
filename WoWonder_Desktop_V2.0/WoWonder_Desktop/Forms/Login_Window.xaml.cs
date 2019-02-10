using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.SQLite;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Threading.Tasks;
using WoWonderClient;
using WoWonderClient.Classes.Global;
using WoWonderClient.Classes.User;
using WoWonder_Desktop.language;



namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for Login_Window.xaml
    /// </summary>
    public partial class Login_Window : Window
    {
        #region Variables Declaration

        public static bool DisplayWorkthrothPages = false;
        public static string Username = "";
        public static string Password = "";
        public static string RePassword = "";
        public static string Email = "";
        public static string Cheeked = "";
        public static string StatusApiKey = "";
        public string Img_logo = Settings.logo_Img;

        public static bool IsCanceled = false;

        #endregion

        public Login_Window()
        {
            InitializeComponent();
            this.Title = "Login (" + Settings.Application_Name + ")";
            StyleChanger();

            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }
        //########################## login ##########################

        #region login

        // On Click Button login


        // On Click Button forgot password
        private void Btn_Forgot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(WoWonderClient.Client.WebsiteUrl + "forgot-password");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Enter Button login
        private void Btn_Login_OnKeyDown(object sender, KeyEventArgs e)
        {
            Btn_Login_Async();
        }

        private void Btn_Login_OnClick(object sender, RoutedEventArgs e)
        {
            Btn_Login_Async();
        }

        private async void Btn_Login_Async()
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    if (Settings.WebException_Security)
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                               | SecurityProtocolType.Tls11
                                                               | SecurityProtocolType.Tls12
                                                               | SecurityProtocolType.Ssl3;
                    }
                    try
                    {
                        if (Btn_check.IsChecked == true)
                            Cheeked = "1";

                        if (IsCanceled)
                            IsCanceled = false;

                        Current.AccessToken = Functions.RandomString(40);

                        TextBlockMessege.Text = LocalResources.label_TextBlockMessege;
                        ProgressBarMessege.Visibility = Visibility.Visible;
                        IconError.Visibility = Visibility.Collapsed;

                        if (NameTextBox.Text == "")
                        {
                            TextBlockMessege.Focus();
                            TextBlockMessege.Text = LocalResources.label_Please_write_your_username;
                            ProgressBarMessege.Visibility = Visibility.Collapsed;
                            IconError.Visibility = Visibility.Visible;
                        }
                        else if (PasswordBox.Password == "")
                        {
                            PasswordBox.Focus();
                            TextBlockMessege.Text = LocalResources.label_Please_write_your_password;
                            ProgressBarMessege.Visibility = Visibility.Collapsed;
                            IconError.Visibility = Visibility.Visible;
    
                        }
                        else
                        {
                            try
                            {
                                Password = PasswordBox.Password;
                                Username = NameTextBox.Text;
                                var client = new Client(Settings.TripleDesAppServiceProvider);

                                var (apiStatus, data_setting) = await WoWonderClient.Current.GetSettings();

                                if (apiStatus == 200)
                                {

                                    if (data_setting is GetSiteSettingsObject.Config result)
                                    {
                                        #region Insert_To_SettingsTable

                                        DataBase.SettingsTable Settingstable = new DataBase.SettingsTable
                                        {
                                            S_SiteName = result.SiteName,
                                            S_SiteTitle = result.SiteTitle,
                                            S_SiteKeywords = result.SiteKeywords,
                                            S_SiteDesc = result.SiteDesc,
                                            S_DefaultLang = result.DefualtLang,
                                            S_FileSharing = result.FileSharing,
                                            S_ChatSystem = result.ChatSystem,
                                            S_User_lastseen = result.UserLastseen,
                                            S_Age = result.Age,
                                            S_DeleteAccount = result.DeleteAccount,
                                            S_ConnectivitySystem = result.ConnectivitySystem,
                                            S_MaxUpload = result.MaxUpload,
                                            S_MaxCharacters = result.MaxCharacters,
                                            S_Message_seen = result.MessageSeen,
                                            S_Message_typing = result.MessageTyping,
                                            S_AllowedExtenstion = result.AllowedExtenstion,
                                            S_Theme = result.Theme,
                                            //S_DefaulColor = result.color,
                                            S_Header_hover_border = result.HeaderHoverBorder,
                                            S_Header_color = result.HeaderColor,
                                            S_Body_background = result.BodyBackground,
                                            S_btn_color = result.BtnColor,
                                            //S_SecondryColor = result.sec,
                                            S_btn_hover_color = result.BtnHoverColor,
                                            S_btn_hover_background_color = result.BtnHoverBackgroundColor,
                                            Setting_Header_color = result.HeaderColor,
                                            Setting_Header_background = result.HeaderBackground,
                                            Setting_Active_sidebar_color = result.SettingActiveSidebarColor,
                                            Setting_Active_sidebar_background = result.SettingActiveSidebarBackground,
                                            Setting_Sidebar_background = result.SettingSidebarBackground,
                                            Setting_Sidebar_color = result.SettingSidebarColor,
                                            S_Logo_extension = result.LogoExtension,
                                            S_Background_extension = result.BackgroundExtension,
                                            S_Video_upload = result.VideoUpload,
                                            S_Audio_upload = result.AudioUpload,
                                            S_Header_search_color = result.HeaderSearchColor,
                                            S_Header_button_shadow = result.HeaderButtonShadow,
                                            S_btn_disabled = result.BtnDisabled,
                                            S_User_registration = result.UserRegistration,
                                            S_Favicon_extension = result.FaviconExtension,
                                            S_Chat_outgoing_background = result.ChatOutgoingBackground,
                                            S_Windows_app_version = result.WindowsAppVersion,
                                            S_Credit_card = result.CreditCard,
                                            S_Bitcoin = result.Bitcoin,
                                            S_m_withdrawal = result.Withdrawal,
                                            S_Affiliate_type = result.AffiliateType,
                                            S_Affiliate_system = result.AffiliateSystem,
                                            S_Classified = result.Classified,
                                            S_Bucket_name = result.BucketName,
                                            S_Region = result.Region,
                                            S_Footer_background = result.footerBackground,
                                            S_Is_utf8 = result.IsUtf8,
                                            S_Alipay = result.Alipay,
                                            S_Audio_chat = result.AudioChat,
                                            S_Sms_provider = result.SmsProvider,
                                            // Settingstable.S_Updated_latest = settings_updated_latest;
                                            S_Footer_background_2 = result.footerBackground2,
                                            S_Footer_background_n = result.footerBackground_n,
                                            S_Blogs = result.Blogs,
                                            S_Can_blogs = result.CanBlogs,
                                            S_Push = result.Push,
                                            S_Push_id = result.pushId,
                                            S_Push_key = result.pushKey,
                                            S_Events = result.Events,
                                            S_Forum = result.Forum,
                                            S_Last_update = result.LastUpdate,
                                            S_Movies = result.Movies,
                                            S_Yndex_translation_api = result.YandexTranslationApi,
                                            S_Update_db_15 = result.UpdateDb15,
                                            S_Ad_v_price = result.adPrice_v,
                                            S_Ad_c_price = result.adPrice_c,
                                            S_Emo_cdn = result.EmoCdn,
                                            S_User_ads = result.UserAds,
                                            S_User_status = result.UserStatus,
                                            S_Date_style = result.DateStyle,
                                            S_Stickers = result.Stickers,
                                            S_Giphy_api = result.GiphyApi,
                                            S_Find_friends = result.FindFriends,
                                            //S_Update_available = result.SiteName,
                                            S_Logo_url = result.LogoUrl,
                                            //S_User_messages = result.,

                                            //stye
                                            S_NotificationDesktop = Settings.NotificationDesktop,
                                            S_NotificationPlaysound = Settings.NotificationPlaysound,
                                            S_BackgroundChats_images = Settings.BackgroundChats_images,
                                            Lang_Resources = Settings.Lang_Resources
                                        };

                                        SQLiteCommandSender.Insert_To_SettingsTable(Settingstable);

                                        #endregion

                                        var (apiStatus2, data_setting2) =
                                            await WoWonderClient.Requests.RequestsAsync.Request_Login_Http(Username,
                                                Password, "UTC");

                                        if (apiStatus2 == 200)
                                        {
                                            if (data_setting2 is UserLoginObject result2)
                                            {
                                                UserDetails.User_id = result2.UserId;
                                                UserDetails.Status = result2.ApiStatus;
                                                UserDetails.Time = result2.Timezone;

                                                if (Cheeked == "1")
                                                {
                                                    DataBase.LoginTable login = new DataBase.LoginTable();
                                                    login.UserId = result2.UserId;
                                                    login.Username = Username;
                                                    login.Password = Password;
                                                    login.Session = Current.AccessToken;
                                                    login.Status = "Active";
                                                    SQLiteCommandSender.Insert_To_LoginTable(login);
                                                }

                                                var dataProfile = await WoWonderClient.Requests.RequestsAsync.Get_User_Profile_Http(UserDetails.User_id);

                                                if (dataProfile.Item1 == 200)
                                                {
                                                    if (dataProfile.Item2 is GetUserDataObject.UserData userData)
                                                    {
                                                        //Functions.Get_User_Profile(data_Profile);

                                                        #region Insert_Or_Replace_To_ProfileTable

                                                        DataBase.ProfilesTable ProfilesTable =
                                                           new DataBase.ProfilesTable
                                                           {
                                                               pm_UserId = userData.UserId,
                                                               pm_Username = userData.Username,
                                                               pm_Email = userData.Email,
                                                               pm_First_name = userData.FirstName,
                                                               pm_Last_name = userData.LastName,
                                                               pm_Avatar = Functions.Get_image(UserDetails.User_id,
                                                                   userData.Avatar.Split('/').Last(), userData.Avatar),
                                                               pm_Cover = userData.Cover,
                                                               pm_Relationship_id = userData.RelationshipId,
                                                               pm_Address = userData.Address,
                                                               pm_Working = userData.Working,
                                                               pm_Working_link = userData.WorkingLink,
                                                               pm_About = userData.About,
                                                               pm_School = userData.School,
                                                               pm_Gender = userData.Gender,
                                                               pm_Birthday = userData.Birthday,
                                                               pm_Website = userData.Website,
                                                               pm_Facebook = userData.Facebook,
                                                               pm_Google = userData.Google,
                                                               pm_Twitter = userData.Twitter,
                                                               pm_Linkedin = userData.Linkedin,
                                                               pm_Youtube = userData.Youtube,
                                                               pm_Vk = userData.Vk,
                                                               pm_Instagram = userData.Instagram,
                                                               pm_Language = userData.Language,
                                                               pm_Ip_address = userData.IpAddress,
                                                               pm_Verified = userData.Verified,
                                                               pm_Lastseen = userData.Lastseen,
                                                               pm_Showlastseen = userData.Showlastseen,
                                                               pm_Status = userData.Status,
                                                               pm_Active = userData.Active,
                                                               pm_Admin = userData.Admin,
                                                               pm_Registered = userData.Registered,
                                                               pm_Phone_number = userData.PhoneNumber,
                                                               pm_Is_pro = userData.IsPro,
                                                               pm_Pro_type = userData.ProType,
                                                               pm_Joined = userData.Joined,
                                                               pm_Timezone = userData.Timezone,
                                                               pm_Referrer = userData.Referrer,
                                                               pm_Balance = userData.Balance,
                                                               pm_Paypal_email = userData.PaypalEmail,
                                                               pm_Notifications_sound = userData.NotificationsSound,
                                                               pm_Order_posts_by = userData.OrderPostsBy,
                                                               pm_Social_login = userData.SocialLogin,
                                                               pm_Device_id = userData.DeviceId,
                                                               pm_Url = userData.Url,
                                                               pm_Name = userData.FirstName
                                                           };

                                                        SQLiteCommandSender.Insert_Or_Replace_To_ProfileTable(
                                                            ProfilesTable);

                                                        #endregion

                                                    }
                                                }

                                                if (IsCanceled!=true)
                                                {

                                                    //Show Main Window
                                                    DialogHost.IsOpen = false;
                                                    DisplayWorkthrothPages = true;
                                                    SQLiteCommandSender.Insert_To_StickersTable();
                                                    this.Hide();
                                                    MainWindow wm = new MainWindow();
                                                    wm.Show();
                                                }   
                                            }
                                        }
                                        else
                                        {
                                            if (data_setting2 is ErrorObject error)
                                            {
                                                TextBlockMessege.Text = error._errors.Error_text;
                                                ProgressBarMessege.Visibility = Visibility.Collapsed;
                                                IconError.Visibility = Visibility.Visible;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    if (data_setting is ErrorObject error)
                                    {
                                        TextBlockMessege.Text = error._errors.Error_text;
                                        ProgressBarMessege.Visibility = Visibility.Visible;
                                        IconError.Visibility = Visibility.Collapsed;
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    TextBlockMessege.Text = LocalResources.label_Please_check_your_internet;
                    ProgressBarMessege.Visibility = Visibility.Collapsed;
                    IconError.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Register ##########################

        #region Register

        // Run background worker : Register

        private async void Btn_Register_Async()
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    try
                    {
                        if (IsCanceled)
                            IsCanceled = false;

                        Current.AccessToken = Functions.RandomString(40);
                        TextBlockMessege.Text = LocalResources.label_TextBlockMessege;
                        ProgressBarMessege.Visibility = Visibility.Visible;
                        IconError.Visibility = Visibility.Collapsed;

                        if (RegisterUsernameTextBox.Text == "")
                        {
                            TextBlockMessege.Focus();
                            TextBlockMessege.Text = LocalResources.label_Please_write_your_username;
                            ProgressBarMessege.Visibility = Visibility.Collapsed;
                            IconError.Visibility = Visibility.Visible;
                        }
                        else if (RegisterEmailTextBox.Text == "")
                        {
                            PasswordBox.Focus();
                            TextBlockMessege.Text = LocalResources.label_Please_write_your_Email;
                            ProgressBarMessege.Visibility = Visibility.Collapsed;
                            IconError.Visibility = Visibility.Visible;
                        }
                        else if (RegisterPasswordTextBox.Password == "")
                        {
                            PasswordBox.Focus();
                            TextBlockMessege.Text = LocalResources.label_Please_write_your_password;
                            ProgressBarMessege.Visibility = Visibility.Collapsed;
                            IconError.Visibility = Visibility.Visible;
                        }
                        else if (RegisterConfirmPasswordTextBox.Password == "")
                        {
                            PasswordBox.Focus();
                            TextBlockMessege.Text = LocalResources.label_Please_write_confirm_password;
                            ProgressBarMessege.Visibility = Visibility.Collapsed;
                            IconError.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            Username = RegisterUsernameTextBox.Text;
                            Email = RegisterEmailTextBox.Text;
                            Password = RegisterPasswordTextBox.Password;
                            RePassword = RegisterConfirmPasswordTextBox.Password;

                            var client = new Client(Settings.TripleDesAppServiceProvider);

                            var data = await Current.GetSettings();

                            if (data.Item1 == 200)
                            {
                                if (data.Item2 is GetSiteSettingsObject.Config result)
                                {
                                    #region Insert_To_SettingsTable

                                    DataBase.SettingsTable Settingstable = new DataBase.SettingsTable
                                    {
                                        S_SiteName = result.SiteName,
                                        S_SiteTitle = result.SiteTitle,
                                        S_SiteKeywords = result.SiteKeywords,
                                        S_SiteDesc = result.SiteDesc,
                                        S_DefaultLang = result.DefualtLang,
                                        S_FileSharing = result.FileSharing,
                                        S_ChatSystem = result.ChatSystem,
                                        S_User_lastseen = result.UserLastseen,
                                        S_Age = result.Age,
                                        S_DeleteAccount = result.DeleteAccount,
                                        S_ConnectivitySystem = result.ConnectivitySystem,
                                        S_MaxUpload = result.MaxUpload,
                                        S_MaxCharacters = result.MaxCharacters,
                                        S_Message_seen = result.MessageSeen,
                                        S_Message_typing = result.MessageTyping,
                                        S_AllowedExtenstion = result.AllowedExtenstion,
                                        S_Theme = result.Theme,
                                        //S_DefaulColor = result.color,
                                        S_Header_hover_border = result.HeaderHoverBorder,
                                        S_Header_color = result.HeaderColor,
                                        S_Body_background = result.BodyBackground,
                                        S_btn_color = result.BtnColor,
                                        //S_SecondryColor = result.sec,
                                        S_btn_hover_color = result.BtnHoverColor,
                                        S_btn_hover_background_color = result.BtnHoverBackgroundColor,
                                        Setting_Header_color = result.HeaderColor,
                                        Setting_Header_background = result.HeaderBackground,
                                        Setting_Active_sidebar_color = result.SettingActiveSidebarColor,
                                        Setting_Active_sidebar_background = result.SettingActiveSidebarBackground,
                                        Setting_Sidebar_background = result.SettingSidebarBackground,
                                        Setting_Sidebar_color = result.SettingSidebarColor,
                                        S_Logo_extension = result.LogoExtension,
                                        S_Background_extension = result.BackgroundExtension,
                                        S_Video_upload = result.VideoUpload,
                                        S_Audio_upload = result.AudioUpload,
                                        S_Header_search_color = result.HeaderSearchColor,
                                        S_Header_button_shadow = result.HeaderButtonShadow,
                                        S_btn_disabled = result.BtnDisabled,
                                        S_User_registration = result.UserRegistration,
                                        S_Favicon_extension = result.FaviconExtension,
                                        S_Chat_outgoing_background = result.ChatOutgoingBackground,
                                        S_Windows_app_version = result.WindowsAppVersion,
                                        S_Credit_card = result.CreditCard,
                                        S_Bitcoin = result.Bitcoin,
                                        S_m_withdrawal = result.Withdrawal,
                                        S_Affiliate_type = result.AffiliateType,
                                        S_Affiliate_system = result.AffiliateSystem,
                                        S_Classified = result.Classified,
                                        S_Bucket_name = result.BucketName,
                                        S_Region = result.Region,
                                        S_Footer_background = result.footerBackground,
                                        S_Is_utf8 = result.IsUtf8,
                                        S_Alipay = result.Alipay,
                                        S_Audio_chat = result.AudioChat,
                                        S_Sms_provider = result.SmsProvider,
                                        // Settingstable.S_Updated_latest = settings_updated_latest;
                                        S_Footer_background_2 = result.footerBackground2,
                                        S_Footer_background_n = result.footerBackground_n,
                                        S_Blogs = result.Blogs,
                                        S_Can_blogs = result.CanBlogs,
                                        S_Push = result.Push,
                                        S_Push_id = result.pushId,
                                        S_Push_key = result.pushKey,
                                        S_Events = result.Events,
                                        S_Forum = result.Forum,
                                        S_Last_update = result.LastUpdate,
                                        S_Movies = result.Movies,
                                        S_Yndex_translation_api = result.YandexTranslationApi,
                                        S_Update_db_15 = result.UpdateDb15,
                                        S_Ad_v_price = result.adPrice_v,
                                        S_Ad_c_price = result.adPrice_c,
                                        S_Emo_cdn = result.EmoCdn,
                                        S_User_ads = result.UserAds,
                                        S_User_status = result.UserStatus,
                                        S_Date_style = result.DateStyle,
                                        S_Stickers = result.Stickers,
                                        S_Giphy_api = result.GiphyApi,
                                        S_Find_friends = result.FindFriends,
                                        //S_Update_available = result.SiteName,
                                        S_Logo_url = result.LogoUrl,
                                        //S_User_messages = result.,

                                        //stye
                                        S_NotificationDesktop = Settings.NotificationDesktop,
                                        S_NotificationPlaysound = Settings.NotificationPlaysound,
                                     
                                        S_BackgroundChats_images = Settings.BackgroundChats_images,
                                        Lang_Resources = Settings.Lang_Resources
                                    };

                                    SQLiteCommandSender.Insert_To_SettingsTable(Settingstable);

                                    #endregion

                                    var dataRegistration = await WoWonderClient.Requests.RequestsAsync.User_Registration_Http(Username, Password, RePassword, Email, "");

                                    if (dataRegistration.Item1 == 200)
                                    {
                                        if (dataRegistration.Item2 is CreatAccountObject resultRegistration)
                                        {

                                            var (apiStatus2, data_setting2) =
                                                await WoWonderClient.Requests.RequestsAsync.Request_Login_Http(Username,
                                                    Password, "UTC");

                                            if (apiStatus2 == 200)
                                            {
                                                if (data_setting2 is UserLoginObject result2)
                                                {
                                                    UserDetails.User_id = result2.UserId;
                                                    UserDetails.Status = result2.ApiStatus;
                                                    UserDetails.Time = result2.Timezone;

                                                    if (Cheeked == "1")
                                                    {
                                                        DataBase.LoginTable login = new DataBase.LoginTable();
                                                        login.UserId = result2.UserId;
                                                        login.Username = Username;
                                                        login.Password = Password;
                                                        login.Session = Current.AccessToken;
                                                        login.Status = "Active";
                                                        SQLiteCommandSender.Insert_To_LoginTable(login);
                                                    }

                                                    var dataProfile = await WoWonderClient.Requests.RequestsAsync.Get_User_Profile_Http(UserDetails.User_id);

                                                    if (dataProfile.Item1 == 200)
                                                    {
                                                        if (dataProfile.Item2 is GetUserDataObject.UserData userData)
                                                        {
                                                            //Functions.Get_User_Profile(data_Profile);

                                                            #region Insert_Or_Replace_To_ProfileTable

                                                            DataBase.ProfilesTable ProfilesTable =
                                                               new DataBase.ProfilesTable
                                                               {
                                                                   pm_UserId = userData.UserId,
                                                                   pm_Username = userData.Username,
                                                                   pm_Email = userData.Email,
                                                                   pm_First_name = userData.FirstName,
                                                                   pm_Last_name = userData.LastName,
                                                                   pm_Avatar = Functions.Get_image(UserDetails.User_id,
                                                                       userData.Avatar.Split('/').Last(), userData.Avatar),
                                                                   pm_Cover = userData.Cover,
                                                                   pm_Relationship_id = userData.RelationshipId,
                                                                   pm_Address = userData.Address,
                                                                   pm_Working = userData.Working,
                                                                   pm_Working_link = userData.WorkingLink,
                                                                   pm_About = userData.About,
                                                                   pm_School = userData.School,
                                                                   pm_Gender = userData.Gender,
                                                                   pm_Birthday = userData.Birthday,
                                                                   pm_Website = userData.Website,
                                                                   pm_Facebook = userData.Facebook,
                                                                   pm_Google = userData.Google,
                                                                   pm_Twitter = userData.Twitter,
                                                                   pm_Linkedin = userData.Linkedin,
                                                                   pm_Youtube = userData.Youtube,
                                                                   pm_Vk = userData.Vk,
                                                                   pm_Instagram = userData.Instagram,
                                                                   pm_Language = userData.Language,
                                                                   pm_Ip_address = userData.IpAddress,
                                                                   pm_Verified = userData.Verified,
                                                                   pm_Lastseen = userData.Lastseen,
                                                                   pm_Showlastseen = userData.Showlastseen,
                                                                   pm_Status = userData.Status,
                                                                   pm_Active = userData.Active,
                                                                   pm_Admin = userData.Admin,
                                                                   pm_Registered = userData.Registered,
                                                                   pm_Phone_number = userData.PhoneNumber,
                                                                   pm_Is_pro = userData.IsPro,
                                                                   pm_Pro_type = userData.ProType,
                                                                   pm_Joined = userData.Joined,
                                                                   pm_Timezone = userData.Timezone,
                                                                   pm_Referrer = userData.Referrer,
                                                                   pm_Balance = userData.Balance,
                                                                   pm_Paypal_email = userData.PaypalEmail,
                                                                   pm_Notifications_sound = userData.NotificationsSound,
                                                                   pm_Order_posts_by = userData.OrderPostsBy,
                                                                   pm_Social_login = userData.SocialLogin,
                                                                   pm_Device_id = userData.DeviceId,
                                                                   pm_Url = userData.Url,
                                                                   pm_Name = userData.FirstName
                                                               };

                                                            SQLiteCommandSender.Insert_Or_Replace_To_ProfileTable(
                                                                ProfilesTable);

                                                            #endregion

                                                        }
                                                    }

                                                    if (!IsCanceled)
                                                    {
                                                        //Show Main Window
                                                        DialogHost.IsOpen = false;
                                                        DisplayWorkthrothPages = true;
                                                        SQLiteCommandSender.Insert_To_StickersTable();
                                                        this.Hide();
                                                        MainWindow wm = new MainWindow();
                                                        wm.Show(); 
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (data_setting2 is ErrorObject error)
                                                {
                                                    TextBlockMessege.Focus();
                                                    TextBlockMessege.Text = error._errors.Error_text;
                                                    ProgressBarMessege.Visibility = Visibility.Collapsed;
                                                    IconError.Visibility = Visibility.Visible;

                                                }
                                            }


                                        }
                                    }
                                    else if (dataRegistration.Item1 == 220)
                                    {
                                        if (dataRegistration.Item2 is MessageObject message)
                                        {

                                            TextBlockMessege.Text = message.Message;
                                            ProgressBarMessege.Visibility = Visibility.Collapsed;
                                            IconError.Visibility = Visibility.Visible;
                                            IconError.Kind = PackIconKind.AlertCircle;
                                            IconError.Foreground = Brushes.Orange;
                                        }
                                    }
                                    else
                                    {
                                        if (dataRegistration.Item2 is ErrorObject error)
                                        {

                                            TextBlockMessege.Focus();
                                            TextBlockMessege.Text = error._errors.Error_text;
                                            ProgressBarMessege.Visibility = Visibility.Collapsed;
                                            IconError.Visibility = Visibility.Visible;

                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (data.Item2 is ErrorObject error)
                                {
                                    TextBlockMessege.Focus();
                                    TextBlockMessege.Text = error._errors.Error_text;
                                    ProgressBarMessege.Visibility = Visibility.Collapsed;
                                    IconError.Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TextBlockMessege.Focus();
                        TextBlockMessege.Text = ex.Message;
                        ProgressBarMessege.Visibility = Visibility.Collapsed;
                        IconError.Visibility = Visibility.Visible;
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    TextBlockMessege.Text = LocalResources.label_Please_check_your_internet;
                    ProgressBarMessege.Visibility = Visibility.Collapsed;
                    IconError.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exception)
            {
                TextBlockMessege.Focus();
                TextBlockMessege.Text = exception.Message;
                ProgressBarMessege.Visibility = Visibility.Collapsed;
                IconError.Visibility = Visibility.Visible;

                Console.WriteLine(exception);
            }

        }

        // On Click Button Register 
        private void Registerbutton_OnClick(object sender, RoutedEventArgs e)
        {
            Btn_Register_Async();
        }

        // On Click Button New Acount
        private void newAcountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RightBigText.Content = LocalResources.label_RightBigText_Signin;
                RightSmallText.Text = LocalResources.label_RightSmallText_Signin;
                RightImage.ImageSource = new BitmapImage(
                    new Uri(
                        @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +
                        "/Images/Backgrounds/" + Settings.Login_Img, UriKind.Absolute));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## login using Social Meida ##########################

        #region login Social Meida

        // On Click Button login using Facebook 
        private void FacebookButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SocialLogin sl = new SocialLogin("Facebook");
                sl.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // On Click Button login using Twitter 
        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SocialLogin sl = new SocialLogin("Twitter");
                sl.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // On Click Button login using Vk 
        private void VkButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SocialLogin sl = new SocialLogin("Vk");
                sl.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // On Click Button login using Instagram 
        private void InstagramButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SocialLogin sl = new SocialLogin("Instagram");
                sl.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // On Click Button login using Google 
        private void GoogleButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SocialLogin sl = new SocialLogin("Google");
                sl.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Settings ##########################

        #region Settings

        //Functions Get path and data from class Settings
        public void StyleChanger()
        {
            try
            {
                if (Settings.Facebook_Icon == true)
                {
                    FacebookButton.Visibility = Visibility.Visible;
                }
                if (Settings.Twitter_Icon == true)
                {
                    TwitterButton.Visibility = Visibility.Visible;
                }
                if (Settings.Instagram_Icon == true)
                {
                    InstagramButton.Visibility = Visibility.Visible;
                }
                if (Settings.Vk_Icon == true)
                {
                    VkButton.Visibility = Visibility.Visible;
                }
                if (Settings.Google_Icon == true)
                {
                    GoogleButton.Visibility = Visibility.Visible;
                    Lbl_Orvia.Visibility = Visibility.Visible;
                }
                Logowebsite.Source = new BitmapImage(new Uri(Settings.logo_Img, UriKind.Relative));
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Functions Get Settings User
        public static void Getsetting(JObject data)
        {
            try
            {
                if (data != null)
                {
                    JObject Alfa = JObject.FromObject(data);

                    var settings_siteName = Alfa["siteName"].ToString();
                    var settings_siteTitle = Alfa["siteTitle"].ToString();
                    var settings_siteKeywords = Alfa["siteKeywords"].ToString();
                    var settings_siteDesc = Alfa["siteDesc"].ToString();
                    var settings_Lang = Alfa["defualtLang"].ToString();
                    var settings_fileSharing = Alfa["fileSharing"].ToString();
                    var settings_chatSystem = Alfa["chatSystem"].ToString();
                    var settings_user_lastseen = Alfa["user_lastseen"].ToString();
                    var settings_age = Alfa["age"].ToString();
                    var settings_deleteAccount = Alfa["deleteAccount"].ToString();
                    var settings_connectivitySystem = Alfa["connectivitySystem"].ToString();
                    var settings_maxUpload = Alfa["maxUpload"].ToString();
                    var settings_maxCharacters = Alfa["maxCharacters"].ToString();
                    var settings_message_seen = Alfa["message_seen"].ToString();
                    var settings_message_typing = Alfa["message_typing"].ToString();
                    var settings_allowedExtenstion = Alfa["allowedExtenstion"].ToString();
                    var settings_theme = Alfa["theme"].ToString();
                    var settings_header_background = Alfa["header_background"].ToString();
                    var settings_header_hover_border = Alfa["header_hover_border"].ToString();
                    var settings_header_color = Alfa["header_color"].ToString();
                    var settings_body_background = Alfa["body_background"].ToString();
                    var settings_btn_color = Alfa["btn_color"].ToString();
                    var settings_background_color = Alfa["btn_background_color"].ToString();
                    var settings_btn_hover_color = Alfa["btn_hover_color"].ToString();
                    var settings_btn_hover_background_color = Alfa["btn_hover_background_color"].ToString();
                    var settings_s_header_color = Alfa["setting_header_color"].ToString();
                    var settings_s_header_background = Alfa["setting_header_background"].ToString();
                    var settings_s_active_sidebar_color = Alfa["setting_active_sidebar_color"].ToString();
                    var settings_s_active_sidebar_background = Alfa["setting_active_sidebar_background"].ToString();
                    var settings_s_sidebar_background = Alfa["setting_sidebar_background"].ToString();
                    var settings_s_sidebar_color = Alfa["setting_sidebar_color"].ToString();
                    var settings_logo_extension = Alfa["logo_extension"].ToString();
                    var settings_background_extension = Alfa["background_extension"].ToString();
                    var settings_video_upload = Alfa["video_upload"].ToString();
                    var settings_audio_upload = Alfa["audio_upload"].ToString();
                    var settings_header_search_color = Alfa["header_search_color"].ToString();
                    var settings_header_button_shadow = Alfa["header_button_shadow"].ToString();
                    var settings_btn_disabled = Alfa["btn_disabled"].ToString();
                    var settings_user_registration = Alfa["user_registration"].ToString();
                    var settings_favicon_extension = Alfa["favicon_extension"].ToString();
                    var settings_chat_outgoing_background = Alfa["chat_outgoing_background"].ToString();
                    var settings_windows_app_version = Alfa["windows_app_version"].ToString();
                    //
                    var settings_app_api_id = Alfa["widnows_app_api_id"].ToString();
                    var settings_app_api_key = Alfa["widnows_app_api_key"].ToString();
                    //
                    var settings_credit_card = Alfa["credit_card"].ToString();
                    var settings_bitcoin = Alfa["bitcoin"].ToString();
                    var settings_m_withdrawal = Alfa["m_withdrawal"].ToString();
                    var settings_affiliate_type = Alfa["affiliate_type"].ToString();
                    var settings_affiliate_system = Alfa["affiliate_system"].ToString();
                    var settings_classified = Alfa["classified"].ToString();
                    var settings_bucket_name = Alfa["bucket_name"].ToString();
                    var settings_region = Alfa["region"].ToString();
                    var settings_footer_background = Alfa["footer_background"].ToString();
                    var settings_is_utf8 = Alfa["is_utf8"].ToString();
                    var settings_alipay = Alfa["alipay"].ToString();
                    var settings_audio_chat = Alfa["audio_chat"].ToString();
                    var settings_sms_provider = Alfa["sms_provider"].ToString();
                    //
                    var settings_footer_text_color = Alfa["footer_text_color"].ToString();
                    //
                    //var settings_updated_latest = Alfa["updated_latest"].ToString();
                    var settings_footer_background_2 = Alfa["footer_background_2"].ToString();
                    var settings_footer_background_n = Alfa["footer_background_n"].ToString();
                    var settings_blogs = Alfa["blogs"].ToString();
                    var settings_can_blogs = Alfa["can_blogs"].ToString();
                    var settings_push = Alfa["push"].ToString();
                    var settings_push_id = Alfa["push_id"].ToString();
                    var settings_push_key = Alfa["push_key"].ToString();
                    var settings_events = Alfa["events"].ToString();
                    var settings_forum = Alfa["forum"].ToString();
                    var settings_last_update = Alfa["last_update"].ToString();
                    var settings_movies = Alfa["movies"].ToString();
                    var settings_yandex_translation_api = Alfa["yandex_translation_api"].ToString();
                    var settings_update_db_15 = Alfa["update_db_15"].ToString();
                    var settings_ad_v_price = Alfa["ad_v_price"].ToString();
                    var settings_ad_c_price = Alfa["ad_c_price"].ToString();
                    var settings_emo_cdn = Alfa["emo_cdn"].ToString();
                    var settings_user_ads = Alfa["user_ads"].ToString();
                    var settings_user_status = Alfa["user_status"].ToString();
                    var settings_date_style = Alfa["date_style"].ToString();
                    var settings_stickers = Alfa["stickers"].ToString();
                    var settings_giphy_api = Alfa["giphy_api"].ToString();
                    var settings_find_friends = Alfa["find_friends"].ToString();
                    var settings_update_available = Alfa["update_available"].ToString();
                    var settings_logo_url = Alfa["logo_url"].ToString();
                    var settings_user_messages = Alfa["user_messages"].ToString();

                    //if (settings_app_api_id == Settings.API_ID && settings_app_api_key == Settings.API_KEY &&
                    //    settings_footer_text_color != "")
                    //{
                    StatusApiKey = "true";

                    // Insert data user in database
                    DataBase.SettingsTable Settingstable = new DataBase.SettingsTable();

                    Settingstable.S_SiteName = settings_siteName;
                    Settingstable.S_SiteTitle = settings_siteTitle;
                    Settingstable.S_SiteKeywords = settings_siteKeywords;
                    Settingstable.S_SiteDesc = settings_siteDesc;
                    Settingstable.S_DefaultLang = settings_Lang;
                    Settingstable.S_FileSharing = settings_fileSharing;
                    Settingstable.S_ChatSystem = settings_chatSystem;
                    Settingstable.S_User_lastseen = settings_user_lastseen;
                    Settingstable.S_Age = settings_age;
                    Settingstable.S_DeleteAccount = settings_deleteAccount;
                    Settingstable.S_ConnectivitySystem = settings_connectivitySystem;
                    Settingstable.S_MaxUpload = settings_maxUpload;
                    Settingstable.S_MaxCharacters = settings_maxCharacters;
                    Settingstable.S_Message_seen = settings_message_seen;
                    Settingstable.S_Message_typing = settings_message_typing;
                    Settingstable.S_AllowedExtenstion = settings_allowedExtenstion;
                    Settingstable.S_Theme = settings_theme;
                    Settingstable.S_DefaulColor = settings_header_background;
                    Settingstable.S_Header_hover_border = settings_header_hover_border;
                    Settingstable.S_Header_color = settings_header_color;
                    Settingstable.S_Body_background = settings_body_background;
                    Settingstable.S_btn_color = settings_btn_color;
                    Settingstable.S_SecondryColor = settings_background_color;
                    Settingstable.S_btn_hover_color = settings_btn_hover_color;
                    Settingstable.S_btn_hover_background_color = settings_btn_hover_background_color;
                    Settingstable.Setting_Header_color = settings_s_header_color;
                    Settingstable.Setting_Header_background = settings_s_header_background;
                    Settingstable.Setting_Active_sidebar_color = settings_s_active_sidebar_color;
                    Settingstable.Setting_Active_sidebar_background = settings_s_active_sidebar_background;
                    Settingstable.Setting_Sidebar_background = settings_s_sidebar_background;
                    Settingstable.Setting_Sidebar_color = settings_s_sidebar_color;
                    Settingstable.S_Logo_extension = settings_logo_extension;
                    Settingstable.S_Background_extension = settings_background_extension;
                    Settingstable.S_Video_upload = settings_video_upload;
                    Settingstable.S_Audio_upload = settings_audio_upload;
                    Settingstable.S_Header_search_color = settings_header_search_color;
                    Settingstable.S_Header_button_shadow = settings_header_button_shadow;
                    Settingstable.S_btn_disabled = settings_btn_disabled;
                    Settingstable.S_User_registration = settings_user_registration;
                    Settingstable.S_Favicon_extension = settings_favicon_extension;
                    Settingstable.S_Chat_outgoing_background = settings_chat_outgoing_background;
                    Settingstable.S_Windows_app_version = settings_windows_app_version;
                    Settingstable.S_Credit_card = settings_credit_card;
                    Settingstable.S_Bitcoin = settings_bitcoin;
                    Settingstable.S_m_withdrawal = settings_m_withdrawal;
                    Settingstable.S_Affiliate_type = settings_affiliate_type;
                    Settingstable.S_Affiliate_system = settings_affiliate_system;
                    Settingstable.S_Classified = settings_classified;
                    Settingstable.S_Bucket_name = settings_bucket_name;
                    Settingstable.S_Region = settings_region;
                    Settingstable.S_Footer_background = settings_footer_background;
                    Settingstable.S_Is_utf8 = settings_is_utf8;
                    Settingstable.S_Alipay = settings_alipay;
                    Settingstable.S_Audio_chat = settings_audio_chat;
                    Settingstable.S_Sms_provider = settings_sms_provider;
                    // Settingstable.S_Updated_latest = settings_updated_latest;
                    Settingstable.S_Footer_background_2 = settings_footer_background_2;
                    Settingstable.S_Footer_background_n = settings_footer_background_n;
                    Settingstable.S_Blogs = settings_blogs;
                    Settingstable.S_Can_blogs = settings_can_blogs;
                    Settingstable.S_Push = settings_push;
                    Settingstable.S_Push_id = settings_push_id;
                    Settingstable.S_Push_key = settings_push_key;
                    Settingstable.S_Events = settings_events;
                    Settingstable.S_Forum = settings_forum;
                    Settingstable.S_Last_update = settings_last_update;
                    Settingstable.S_Movies = settings_movies;
                    Settingstable.S_Yndex_translation_api = settings_yandex_translation_api;
                    Settingstable.S_Update_db_15 = settings_update_db_15;
                    Settingstable.S_Ad_v_price = settings_ad_v_price;
                    Settingstable.S_Ad_c_price = settings_ad_c_price;
                    Settingstable.S_Emo_cdn = settings_emo_cdn;
                    Settingstable.S_User_ads = settings_user_ads;
                    Settingstable.S_User_status = settings_user_status;
                    Settingstable.S_Date_style = settings_date_style;
                    Settingstable.S_Stickers = settings_stickers;
                    Settingstable.S_Giphy_api = settings_giphy_api;
                    Settingstable.S_Find_friends = settings_find_friends;
                    Settingstable.S_Update_available = settings_update_available;
                    Settingstable.S_Logo_url = settings_logo_url;
                    Settingstable.S_User_messages = settings_user_messages;

                    //stye
                    Settingstable.S_NotificationDesktop = Settings.NotificationDesktop;
                    Settingstable.S_NotificationPlaysound = Settings.NotificationPlaysound;
          
                    Settingstable.S_BackgroundChats_images = Settings.BackgroundChats_images;
                    Settingstable.Lang_Resources = Settings.Lang_Resources;

                    SQLiteCommandSender.Insert_To_SettingsTable(Settingstable);
                    //}
                    //else
                    //{
                    //    StatusApiKey = "false";
                    //}
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Windows ##########################

        #region Windows

        // On Click Button Close
        private void Btn_Close_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Windows.Application.Current.Shutdown();
            Environment.Exit(0);

        }

        // On Click Button Back
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RightBigText.Content = LocalResources.label_RightBigText_Login;
                RightSmallText.Text = LocalResources.label_RightSmallText_Login;
                RightImage.ImageSource = new BitmapImage(
                    new Uri(
                        @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +
                        "/Images/Backgrounds/" + Settings.Register_Img, UriKind.Absolute));

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void LoadingCancelbutton_OnClick(object sender, RoutedEventArgs e)
        {
            IsCanceled = true;
        }

        #endregion



    }
}