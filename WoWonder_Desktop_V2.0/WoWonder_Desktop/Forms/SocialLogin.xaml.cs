using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using CefSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WoWonderClient;
using WoWonderClient.Classes.Global;
using WoWonderClient.Classes.User;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for SocialLogin.xaml
    /// </summary>
    public partial class SocialLogin : Window
    {
        public static string HashSet = "";
        public static string Result = "false";
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public SocialLogin(string type)
        {
            InitializeComponent();
            this.Title = "Social Login (" + Settings.Application_Name + ")";

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

            // Get Type Social Login from page Login
            HashSet = Functions.RandomString(80);
            if (type == "Facebook")
            {
                SocialLoginbrowser.Address = WoWonderClient.Client.WebsiteUrl +
                                             "/app_api.php?type=user_login_with&provider=Facebook&hash=" + HashSet;
            }
            else if (type == "Twitter")
            {
                SocialLoginbrowser.Address = WoWonderClient.Client.WebsiteUrl +
                                             "/app_api.php?type=user_login_with&provider=Twitter&hash=" + HashSet;

            }
            else if (type == "Vk")
            {
                SocialLoginbrowser.Address = WoWonderClient.Client.WebsiteUrl +
                                             "/app_api.php?type=user_login_with&provider=Vkontakte&hash=" + HashSet;
            }
            else if (type == "Instagram")
            {
                SocialLoginbrowser.Address = WoWonderClient.Client.WebsiteUrl +
                                             "/app_api.php?type=user_login_with&provider=Instagram&hash=" + HashSet;
            }
            else if (type == "Google")
            {
                SocialLoginbrowser.Address = WoWonderClient.Client.WebsiteUrl +
                                             "/app_api.php?type=user_login_with&provider=Google&hash=" + HashSet;
            }
            // Timer Control
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 6);
            dispatcherTimer.Start();

            ModeDark_Window();
        }

        // check Hash id  available to website
        public async Task<string> checkHash()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response =await client.GetAsync(WoWonderClient.Client.WebsiteUrl +"/app_api.php?type=check_hash&hash_id=" + HashSet);
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    string apiStatus = data["api_status"].ToString();

                    if (apiStatus == "200")
                    {
                        JObject userdata = JObject.FromObject(data["user_data"]);
                        UserDetails.User_id = userdata["user_id"].ToString();
                        Current.AccessToken = userdata["session_id"].ToString();

                        // Insert data user in database
                        DataBase.LoginTable LoginData = new DataBase.LoginTable();
                        LoginData.Session = Current.AccessToken;
                        LoginData.UserId = UserDetails.User_id;
                        LoginData.Status = "Active";
                        SQLiteCommandSender.Insert_To_LoginTable(LoginData);

                        //Get Setting
                        var Data = await WoWonderClient.Current.GetSettings();

                        if (Data.Item1 == 200)
                        {
                            if (Data.Item2 is GetSiteSettingsObject.Config res)
                            {
                                DataBase.SettingsTable Settingstable = new DataBase.SettingsTable
                                {
                                    S_SiteName = res.SiteName,
                                    S_SiteTitle = res.SiteTitle,
                                    S_SiteKeywords = res.SiteKeywords,
                                    S_SiteDesc = res.SiteDesc,
                                    S_DefaultLang = res.DefualtLang,
                                    S_FileSharing = res.FileSharing,
                                    S_ChatSystem = res.ChatSystem,
                                    S_User_lastseen = res.UserLastseen,
                                    S_Age = res.Age,
                                    S_DeleteAccount = res.DeleteAccount,
                                    S_ConnectivitySystem = res.ConnectivitySystem,
                                    S_MaxUpload = res.MaxUpload,
                                    S_MaxCharacters = res.MaxCharacters,
                                    S_Message_seen = res.MessageSeen,
                                    S_Message_typing = res.MessageTyping,
                                    S_AllowedExtenstion = res.AllowedExtenstion,
                                    S_Theme = res.Theme,
                                    //S_DefaulColor = res.HeaderColor,  // ??
                                    S_Header_hover_border = res.HeaderHoverBorder,
                                    S_Header_color = res.HeaderColor,
                                    S_Body_background = res.BodyBackground,
                                    S_btn_color = res.BtnColor,
                                    //S_SecondryColor = res.SiteName,
                                    S_btn_hover_color = res.BtnHoverColor,
                                    S_btn_hover_background_color = res.BtnHoverBackgroundColor,
                                    Setting_Header_color = res.SettingHeaderColor,
                                    Setting_Header_background = res.SettingHeaderBackground,
                                    Setting_Active_sidebar_color = res.SettingActiveSidebarColor,
                                    Setting_Active_sidebar_background = res.SettingActiveSidebarBackground,
                                    Setting_Sidebar_background = res.SettingActiveSidebarBackground,
                                    Setting_Sidebar_color = res.SettingSidebarColor,
                                    S_Logo_extension = res.LogoExtension,
                                    S_Background_extension = res.BackgroundExtension,
                                    S_Video_upload = res.VideoUpload,
                                    S_Audio_upload = res.AudioUpload,
                                    S_Header_search_color = res.HeaderSearchColor,
                                    S_Header_button_shadow = res.HeaderButtonShadow,
                                    S_btn_disabled = res.BtnDisabled,
                                    S_User_registration = res.UserRegistration,
                                    S_Favicon_extension = res.FaviconExtension,
                                    S_Chat_outgoing_background = res.ChatOutgoingBackground,
                                    S_Windows_app_version = res.WindowsAppVersion,
                                    S_Credit_card = res.CreditCard,
                                    S_Bitcoin = res.Bitcoin,
                                    S_m_withdrawal = res.Withdrawal,
                                    S_Affiliate_type = res.AffiliateType,
                                    S_Affiliate_system = res.AffiliateSystem,
                                    S_Classified = res.Classified,
                                    S_Bucket_name = res.BucketName,
                                    S_Region = res.Region,
                                    S_Footer_background = res.footerBackground,
                                    S_Is_utf8 = res.IsUtf8,
                                    S_Alipay = res.Alipay,
                                    S_Audio_chat = res.AudioChat,
                                    S_Sms_provider = res.SmsProvider,
                                    // Settingstable.S_Updated_latest = settings_updated_latest;
                                    S_Footer_background_2 = res.footerBackground2,
                                    S_Footer_background_n = res.footerBackground_n,
                                    S_Blogs = res.Blogs,
                                    S_Can_blogs = res.CanBlogs,
                                    S_Push = res.Push,
                                    S_Push_id = res.pushId,
                                    S_Push_key = res.pushKey,
                                    S_Events = res.Events,
                                    S_Forum = res.Forum,
                                    S_Last_update = res.LastUpdate,
                                    S_Movies = res.Movies,
                                    S_Yndex_translation_api = res.YandexTranslationApi,
                                    S_Update_db_15 = res.UpdateDb15,
                                    S_Ad_v_price = res.adPrice_v,
                                    S_Ad_c_price = res.adPrice_c,
                                    S_Emo_cdn = res.EmoCdn,
                                    S_User_ads = res.UserAds,
                                    S_User_status = res.UserStatus,
                                    S_Date_style = res.DateStyle,
                                    S_Stickers = res.Stickers,
                                    S_Giphy_api = res.GiphyApi,
                                    S_Find_friends = res.FindFriends,
                                    //S_Update_available = res.ava,
                                    S_Logo_url = res.LogoUrl,
                                    //S_User_messages = res.SiteName,

                                    //stye
                                    S_NotificationDesktop = Settings.NotificationDesktop,
                                    S_NotificationPlaysound = Settings.NotificationPlaysound,
              
                                    S_BackgroundChats_images = Settings.BackgroundChats_images,
                                    Lang_Resources = Settings.Lang_Resources
                                };

                                SQLiteCommandSender.Insert_To_SettingsTable(Settingstable);
                            }
                        }
 
                        //Get User Profile
                        var data_Profile = await WoWonderClient.Requests.RequestsAsync.Get_User_Profile_Http(UserDetails.User_id);

                        if (data_Profile.Item1 == 200)
                        {
                            if (data_Profile.Item2 is GetUserDataObject.UserData userData)
                            {
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

                        SQLiteCommandSender.Insert_To_StickersTable();
                        Login_Window.DisplayWorkthrothPages = true;
                        // Open windows main
                        this.Hide();
                        MainWindow mw = new MainWindow();
                        mw.Show();

                        this.Close();
                    }
                }
            }
            catch
            {

            }
            return "true";
        }

        // hide ProgressBar and text Loading.. 
        private void Facbookbrowser_OnNavigated(object sender, NavigationEventArgs e)
        {

        }

        // run timer
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // code goes here
                checkHash().ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void SocialLoginbrowser_OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            try
            {
                App.Current.Dispatcher.Invoke((Action) delegate // <--- HERE
                {
                    if (SocialLoginbrowser.IsLoading == true)
                    {
                        try
                        {
                            Task.Run(() =>
                            {
                                Thread.Sleep(4000);
                                App.Current.Dispatcher.Invoke((Action) delegate // <--- HERE
                                {
                                    LoadingPanel.Visibility = Visibility.Collapsed;
                                });
                            });
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                    }
                });
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

                Panel.Background = new SolidColorBrush(DarkBackgroundColor);
                LoadingPanel.Background = new SolidColorBrush(DarkBackgroundColor);

                Lbl_Loading.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_connection.Foreground = new SolidColorBrush(WhiteBackgroundColor);

            }
        }


    }
}
