using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using WoWonder_Desktop.SQLite;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.Forms;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Point = System.Windows.Point;
using System.IO;
using WpfAnimatedGif;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Reflection;
using WoWonder_Desktop.language;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;
using WoWonderClient.Classes.Global;
using WoWonderClient.Classes.User;
using Button = System.Windows.Controls.Button;
using ListBox = System.Windows.Controls.ListBox;
using WoWonderClient;
using WoWonderClient.Requests;

namespace WoWonder_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables

        public static string EmojiTabStop = "yes";
        public int ChatActitvitySec = 1;
        public static string Profilename = "";
        public static string ProfilePicSourse = "";
        public static string ContentMsg = "";
        public static string search_key = "";
        public static string Search_Gifs_key = "";
        public static string Search_messages_key = "";
        public static string Follow_id = "";
        public bool internetconection = true;
        public string dictionary = "";
        public string IDuser = "";
        public string ID_To = "";
        public string ID_From = "";
        public string Messages_id = "";
        private static string FirstMessageid = "";
        public static string LastMessageid = "";
        public static string LoadmoremessgaCount = "";
        public static string ChatColor = "";
        public static string Img_user_message = "";
        public static string UserPofilURl = "";
        public static string FriendsPofilURl = "";
        public Classes.Messages _selectedgroup;
        public UsersContactProfile _SelectedUsersContactProfile;
        public UsersContact _SelectedUsersContact;
        public static string _SelectedType;
        public static string before_message_id = "0";
        private static string FileNameAttachment = "";
        public static string state_Check = "0";
        public static string Main_Call_Comming = "False";
        public static JToken Main_Video_call_user = "";
        public static string Main_Call_Comming_Form = "False";
        public static bool ModeDarkstlye = false;

        private Grid _GridWindow;
        private TextBlock _TitleApp;
        private Button _ButtonWindow_Min;
        private Button _ButtonWindow_Max;
        private Button _ButtonWindow_x;


        #endregion

        #region Lists Items Declaration

        public static ObservableCollection<Users> ListUsers = new ObservableCollection<Users>();
        public static ObservableCollection<UsersContact> ListUsersContact = new ObservableCollection<UsersContact>();

        public static ObservableCollection<UsersContactProfile> ListUsersProfile =
            new ObservableCollection<UsersContactProfile>();

        public static ObservableCollection<Classes.Messages>
            ListMessages = new ObservableCollection<Classes.Messages>();

        public static ObservableCollection<Search> ListSearsh = new ObservableCollection<Search>();
        public static ObservableCollection<UsersFriends> ListUsersFriends = new ObservableCollection<UsersFriends>();
        public static ObservableCollection<Classes.Get_Gifs> ListGifs = new ObservableCollection<Classes.Get_Gifs>();

        public static ObservableCollection<Classes.Notifications> ListNotifications =
            new ObservableCollection<Classes.Notifications>();

        public static ObservableCollection<Classes.Pro_users> ListProUsers =
            new ObservableCollection<Classes.Pro_users>();

        public static ObservableCollection<Classes.Trending_hashtag> ListHashtag =
            new ObservableCollection<Classes.Trending_hashtag>();

        public static ObservableCollection<Classes.SharedFile> ListSharedFiles =
            new ObservableCollection<Classes.SharedFile>();

        public static ObservableCollection<Classes.Friend> ListFriends = new ObservableCollection<Classes.Friend>();

        public static ObservableCollection<Classes.Call_Video>
            ListCall = new ObservableCollection<Classes.Call_Video>();

        #endregion

        #region Background Workers Declaration

        private static readonly TaskFactory _taskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static BackgroundWorker bgd_Worker_MessageUpdater = new BackgroundWorker();
        public static BackgroundWorker LoadMoreMessageWorker = new BackgroundWorker();


        #endregion

        #region Timer And MediaPlayer

        MediaPlayer mediaPlayer = new MediaPlayer();

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer_messages = new DispatcherTimer();

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                this.Title = Settings.Application_Name;

                if (Settings.WebException_Security)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                           | SecurityProtocolType.Tls11
                                                           | SecurityProtocolType.Tls12
                                                           | SecurityProtocolType.Ssl3;
                }

                getIcon();
                getsettingstable();
                GetDataMyProfile();
                My_Profile();
                get_data_Call();

                this.AllowDrop = true;
                this.DragEnter += new DragEventHandler(WinMin_DragEnter);
                this.Drop += new DragEventHandler(WinMin_Drop);

                SQLiteCommandSender.GetChatActivityList();
                ChatActivityList.ItemsSource = ListUsers;

                if (Settings.FlowDirection_RightToLeft)
                {
                    this.FlowDirection = FlowDirection.RightToLeft;
                }

                if (RightMainPanel.Visibility == Visibility.Visible)
                {
                    DropDownMenueOnMessageBox.Visibility = Visibility.Collapsed;
                    ProfileToggle.Visibility = Visibility.Collapsed;
                }

                if (!Settings.VideoCall)
                    VideoButton.Visibility = Visibility.Collapsed;
                

                ChatTitleChange.Visibility = Visibility.Collapsed;
                ChatSeen.Visibility = Visibility.Collapsed;
              

                //Check For Internet Connection

                //ChatActivity

                Chat_Work_Async();

                // Timer Control
                dispatcherTimer.Tick += ChatActitvity_Timer;
                dispatcherTimer.Interval = new TimeSpan(0, 0, Settings.RefreshChatActitvityPer);
                dispatcherTimer.Start();

                // timer messages
                timer_messages.Tick += Get_new_messges_Timer;
                timer_messages.Interval = new TimeSpan(0, 0, Settings.Update_Message_Receiver_INT);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //########################## Chat Actitvity User ##########################

        #region Chat Actitvity User

        // Functions Chat Users
        public void ChatUsers(List<GetUserListObject.User> All_User)
        {
            try
            {

                var ImageUpdate = false;
                foreach (var j in All_User)
                {

                    //Variables
                    String Name_App_Main_Leter = Settings.Application_Name.Substring(0, 1);
                    var isverified = "Visible";
                    var Color_onof = "#C0C0C0"; //silver
                    var Message_FontWeight = "Light";
                    var Message_color = "#7E7E7E";
                    string AvatarSplit = j.ProfilePicture.Split('/').Last();
                    var ImageProfile = "Visible";
                    var noProfile_color = Functions.RandomColor();
                    var Chekicon = "Collapsed";
                    var ChatColorcirclevisibilty = "Collapsed";
                    var MediaIconvisibilty = "Collapsed";
                    var MediaIconImage = "Image";
                    var U_lastseenWithoutCut = j.Lastseen;

                    //###############################################
                    /// <summary>
                    /// insert data Users Contact Profile in class and database
                    /// </summary>
                    var checkUsersProfile = ListUsersProfile.FirstOrDefault(a => a.UCP_Id == j.UserId);
                    if (checkUsersProfile != null)
                    {
                        if (checkUsersProfile.UCP_username != j.Username)
                        {
                            checkUsersProfile.UCP_username = j.Username;
                        }

                        if (checkUsersProfile.UCP_name != j.Name)
                        {
                            checkUsersProfile.UCP_name = j.Name;
                        }

                        if (checkUsersProfile.UCP_cover_picture != j.CoverPicture)
                        {
                            checkUsersProfile.UCP_cover_picture = j.CoverPicture;
                        }

                        if (checkUsersProfile.UCP_profile_picture !=
                            Functions.Get_image(j.UserId, AvatarSplit, j.ProfilePicture))
                        {
                            checkUsersProfile.UCP_profile_picture =
                                Functions.Get_image(j.UserId, AvatarSplit, j.ProfilePicture);
                        }

                        if (checkUsersProfile.UCP_verified != j.Verified)
                        {
                            checkUsersProfile.UCP_verified = j.Verified;
                        }

                        if (checkUsersProfile.UCP_lastseen != j.Lastseen)
                        {
                            checkUsersProfile.UCP_lastseen = j.Lastseen;
                        }

                        if (checkUsersProfile.UCP_lastseen_time_text != j.LastseenTimeText)
                        {
                            checkUsersProfile.UCP_lastseen_time_text = j.LastseenTimeText;
                        }

                        if (checkUsersProfile.UCP_lastseen_unix_time != j.LastseenUnixTime)
                        {
                            checkUsersProfile.UCP_lastseen_unix_time = j.LastseenUnixTime;
                        }

                        if (checkUsersProfile.UCP_url != j.Url)
                        {
                            checkUsersProfile.UCP_url = j.Url;
                        }

                        if (checkUsersProfile.UCP_user_platform != j.UserPlatform)
                        {
                            checkUsersProfile.UCP_user_platform = j.UserPlatform;
                        }

                        if (checkUsersProfile.UCP_chat_color != j.ChatColor)
                        {
                            checkUsersProfile.UCP_chat_color = j.ChatColor;
                        }

                        if (checkUsersProfile.UCP_email != j.UserProfile.Email)
                        {
                            checkUsersProfile.UCP_email = j.UserProfile.Email;
                        }

                        if (checkUsersProfile.UCP_first_name != j.UserProfile.FirstName)
                        {
                            checkUsersProfile.UCP_first_name = j.UserProfile.FirstName;
                        }

                        if (checkUsersProfile.UCP_last_name != j.UserProfile.LastName)
                        {
                            checkUsersProfile.UCP_last_name = j.UserProfile.LastName;
                        }

                        if (checkUsersProfile.UCP_relationship_id != j.UserProfile.RelationshipId)
                        {
                            checkUsersProfile.UCP_relationship_id = j.UserProfile.RelationshipId;
                        }

                        if (checkUsersProfile.UCP_address != j.UserProfile.Address)
                        {
                            checkUsersProfile.UCP_address = j.UserProfile.Address;
                        }

                        if (checkUsersProfile.UCP_working != j.UserProfile.Working)
                        {
                            checkUsersProfile.UCP_working = j.UserProfile.Working;
                        }

                        if (checkUsersProfile.UCP_working_link != j.UserProfile.WorkingLink)
                        {
                            checkUsersProfile.UCP_working_link = j.UserProfile.WorkingLink;
                        }

                        if (checkUsersProfile.UCP_about != j.UserProfile.About)
                        {
                            checkUsersProfile.UCP_about = j.UserProfile.About;
                        }

                        if (checkUsersProfile.UCP_school != j.UserProfile.School)
                        {
                            checkUsersProfile.UCP_school = j.UserProfile.School;
                        }

                        if (checkUsersProfile.UCP_gender != j.UserProfile.Gender)
                        {
                            checkUsersProfile.UCP_gender = j.UserProfile.Gender;
                        }

                        if (checkUsersProfile.UCP_birthday != j.UserProfile.Birthday)
                        {
                            checkUsersProfile.UCP_birthday = j.UserProfile.Birthday;
                        }

                        if (checkUsersProfile.UCP_website != j.UserProfile.Website)
                        {
                            checkUsersProfile.UCP_website = j.UserProfile.Website;
                        }

                        if (checkUsersProfile.UCP_facebook != j.UserProfile.Facebook)
                        {
                            checkUsersProfile.UCP_facebook = j.UserProfile.Facebook;
                        }

                        if (checkUsersProfile.UCP_google != j.UserProfile.Google)
                        {
                            checkUsersProfile.UCP_google = j.UserProfile.Google;
                        }

                        if (checkUsersProfile.UCP_twitter != j.UserProfile.Twitter)
                        {
                            checkUsersProfile.UCP_twitter = j.UserProfile.Twitter;
                        }

                        if (checkUsersProfile.UCP_linkedin != j.UserProfile.Linkedin)
                        {
                            checkUsersProfile.UCP_linkedin = j.UserProfile.Linkedin;
                        }

                        if (checkUsersProfile.UCP_youtube != j.UserProfile.Youtube)
                        {
                            checkUsersProfile.UCP_youtube = j.UserProfile.Youtube;
                        }

                        if (checkUsersProfile.UCP_vk != j.UserProfile.Vk)
                        {
                            checkUsersProfile.UCP_vk = j.UserProfile.Vk;
                        }

                        if (checkUsersProfile.UCP_instagram != j.UserProfile.Instagram)
                        {
                            checkUsersProfile.UCP_instagram = j.UserProfile.Instagram;
                        }

                        if (checkUsersProfile.UCP_language != j.UserProfile.Language)
                        {
                            checkUsersProfile.UCP_language = j.UserProfile.Language;
                        }

                        if (checkUsersProfile.UCP_ip_address != j.UserProfile.IpAddress)
                        {
                            checkUsersProfile.UCP_ip_address = j.UserProfile.IpAddress;
                        }

                        if (checkUsersProfile.UCP_follow_privacy != j.UserProfile.FollowPrivacy)
                        {
                            checkUsersProfile.UCP_follow_privacy = j.UserProfile.FollowPrivacy;
                        }

                        if (checkUsersProfile.UCP_post_privacy != j.UserProfile.PostPrivacy)
                        {
                            checkUsersProfile.UCP_post_privacy = j.UserProfile.PostPrivacy;
                        }

                        if (checkUsersProfile.UCP_message_privacy != j.UserProfile.MessagePrivacy)
                        {
                            checkUsersProfile.UCP_message_privacy = j.UserProfile.MessagePrivacy;
                        }

                        if (checkUsersProfile.UCP_confirm_followers != j.UserProfile.ConfirmFollowers)
                        {
                            checkUsersProfile.UCP_confirm_followers = j.UserProfile.ConfirmFollowers;
                        }

                        if (checkUsersProfile.UCP_show_activities_privacy != j.UserProfile.ShowActivitiesPrivacy)
                        {
                            checkUsersProfile.UCP_show_activities_privacy = j.UserProfile.ShowActivitiesPrivacy;
                        }

                        if (checkUsersProfile.UCP_birth_privacy != j.UserProfile.BirthPrivacy)
                        {
                            checkUsersProfile.UCP_birth_privacy = j.UserProfile.BirthPrivacy;
                        }

                        if (checkUsersProfile.UCP_visit_privacy != j.UserProfile.VisitPrivacy)
                        {
                            checkUsersProfile.UCP_visit_privacy = j.UserProfile.VisitPrivacy;
                        }

                        if (checkUsersProfile.UCP_showlastseen != j.UserProfile.Showlastseen)
                        {
                            checkUsersProfile.UCP_showlastseen = j.UserProfile.Showlastseen;
                        }

                        if (checkUsersProfile.UCP_status != j.UserProfile.Status)
                        {
                            checkUsersProfile.UCP_status = j.UserProfile.Status;
                        }

                        if (checkUsersProfile.UCP_active != j.UserProfile.Active)
                        {
                            checkUsersProfile.UCP_active = j.UserProfile.Active;
                        }

                        if (checkUsersProfile.UCP_admin != j.UserProfile.Admin)
                        {
                            checkUsersProfile.UCP_admin = j.UserProfile.Admin;
                        }

                        if (checkUsersProfile.UCP_registered != j.UserProfile.Registered)
                        {
                            checkUsersProfile.UCP_registered = j.UserProfile.Registered;
                        }

                        if (checkUsersProfile.UCP_phone_number != j.UserProfile.PhoneNumber)
                        {
                            checkUsersProfile.UCP_phone_number = j.UserProfile.PhoneNumber;
                        }

                        if (checkUsersProfile.UCP_is_pro != j.UserProfile.IsPro)
                        {
                            checkUsersProfile.UCP_is_pro = j.UserProfile.IsPro;
                        }

                        if (checkUsersProfile.UCP_pro_type != j.UserProfile.ProType)
                        {
                            checkUsersProfile.UCP_pro_type = j.UserProfile.ProType;
                        }

                        if (checkUsersProfile.UCP_joined != j.UserProfile.Joined)
                        {
                            checkUsersProfile.UCP_joined = j.UserProfile.Joined;
                        }

                        if (checkUsersProfile.UCP_timezone != j.UserProfile.Timezone)
                        {
                            checkUsersProfile.UCP_timezone = j.UserProfile.Timezone;
                        }

                        if (checkUsersProfile.UCP_referrer != j.UserProfile.Referrer)
                        {
                            checkUsersProfile.UCP_referrer = j.UserProfile.Referrer;
                        }

                        if (checkUsersProfile.UCP_balance != j.UserProfile.Balance)
                        {
                            checkUsersProfile.UCP_balance = j.UserProfile.Balance;
                        }

                        if (checkUsersProfile.UCP_paypal_email != j.UserProfile.PaypalEmail)
                        {
                            checkUsersProfile.UCP_paypal_email = j.UserProfile.PaypalEmail;
                        }

                        if (checkUsersProfile.UCP_notifications_sound != j.UserProfile.NotificationsSound)
                        {
                            checkUsersProfile.UCP_notifications_sound = j.UserProfile.NotificationsSound;
                        }

                        if (checkUsersProfile.UCP_order_posts_by != j.UserProfile.OrderPostsBy)
                        {
                            checkUsersProfile.UCP_order_posts_by = j.UserProfile.OrderPostsBy;
                        }

                        if (checkUsersProfile.UCP_social_login != j.UserProfile.SocialLogin)
                        {
                            checkUsersProfile.UCP_social_login = j.UserProfile.SocialLogin;
                        }

                        if (checkUsersProfile.UCP_device_id != j.UserProfile.DeviceId)
                        {
                            checkUsersProfile.UCP_device_id = j.UserProfile.DeviceId;
                        }
                    }
                    else
                    {
                        UsersContactProfile ucp = new UsersContactProfile();

                        //users
                        ucp.UCP_Id = j.UserId;
                        ucp.UCP_username = j.Username;
                        ucp.UCP_name = j.Name;
                        ucp.UCP_cover_picture = j.CoverPicture;
                        ucp.UCP_profile_picture = Functions.Get_image(j.UserId, AvatarSplit, j.ProfilePicture);
                        ucp.UCP_verified = j.Verified;
                        ucp.UCP_lastseen = j.Lastseen;
                        ucp.UCP_lastseen_time_text = j.LastseenTimeText;
                        ucp.UCP_lastseen_unix_time = j.LastseenUnixTime;
                        ucp.UCP_url = j.Url;
                        ucp.UCP_user_platform = j.UserPlatform;
                        ucp.UCP_chat_color = j.ChatColor;
                        ucp.UCP_Notifications_Message_Sound_user = "true";
                        ucp.UCP_Notifications_Message_user = "true";
                        //user_profile
                        ucp.UCP_email = j.UserProfile.Email;
                        ucp.UCP_first_name = j.UserProfile.FirstName;
                        ucp.UCP_last_name = j.UserProfile.LastName;
                        ucp.UCP_relationship_id = j.UserProfile.RelationshipId;
                        ucp.UCP_address = j.UserProfile.Address;
                        ucp.UCP_working = j.UserProfile.Working;
                        ucp.UCP_working_link = j.UserProfile.WorkingLink;
                        ucp.UCP_about = j.UserProfile.About;
                        ucp.UCP_school = j.UserProfile.School;
                        ucp.UCP_gender = j.UserProfile.Gender;
                        ucp.UCP_birthday = j.UserProfile.Birthday;
                        ucp.UCP_website = j.UserProfile.Website;
                        ucp.UCP_facebook = j.UserProfile.Facebook;
                        ucp.UCP_google = j.UserProfile.Google;
                        ucp.UCP_twitter = j.UserProfile.Twitter;
                        ucp.UCP_linkedin = j.UserProfile.Linkedin;
                        ucp.UCP_youtube = j.UserProfile.Youtube;
                        ucp.UCP_vk = j.UserProfile.Vk;
                        ucp.UCP_instagram = j.UserProfile.Instagram;
                        ucp.UCP_language = j.UserProfile.Language;
                        ucp.UCP_ip_address = j.UserProfile.IpAddress;
                        ucp.UCP_follow_privacy = j.UserProfile.FollowPrivacy;
                        ucp.UCP_post_privacy = j.UserProfile.PostPrivacy;
                        ucp.UCP_message_privacy = j.UserProfile.MessagePrivacy;
                        ucp.UCP_confirm_followers = j.UserProfile.ConfirmFollowers;
                        ucp.UCP_show_activities_privacy = j.UserProfile.ShowActivitiesPrivacy;
                        ucp.UCP_birth_privacy = j.UserProfile.BirthPrivacy;
                        ucp.UCP_visit_privacy = j.UserProfile.VisitPrivacy;
                        ucp.UCP_showlastseen = j.UserProfile.Showlastseen;
                        ucp.UCP_status = j.UserProfile.Status;
                        ucp.UCP_active = j.UserProfile.Active;
                        ucp.UCP_admin = j.UserProfile.Admin;
                        ucp.UCP_registered = j.UserProfile.Registered;
                        ucp.UCP_phone_number = j.UserProfile.PhoneNumber;
                        ucp.UCP_is_pro = j.UserProfile.IsPro;
                        ucp.UCP_pro_type = j.UserProfile.ProType;
                        ucp.UCP_joined = j.UserProfile.Joined;
                        ucp.UCP_timezone = j.UserProfile.Timezone;
                        ucp.UCP_referrer = j.UserProfile.Referrer;
                        ucp.UCP_balance = j.UserProfile.Balance;
                        ucp.UCP_paypal_email = j.UserProfile.PaypalEmail;
                        ucp.UCP_notifications_sound = j.UserProfile.NotificationsSound;
                        ucp.UCP_order_posts_by = j.UserProfile.OrderPostsBy;
                        ucp.UCP_social_login = j.UserProfile.SocialLogin;
                        ucp.UCP_device_id = j.UserProfile.DeviceId;

                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            var checkUsers = ListUsersProfile.FirstOrDefault(a => a.UCP_Id == j.UserId);
                            if (checkUsers == null)
                            {
                                ListUsersProfile.Add(ucp);
                            }
                        });
                    }

                    //###############################################
                    /// <summary>
                    /// insert data Users in class and database
                    /// </summary>

                    if (j.Lastseen == "on")
                    {
                        Color_onof = "#00cc00"; //green
                        Name_App_Main_Leter = "";
                        j.LastseenTimeText = "Online";
                    }

                    if (j.Verified == "0")
                    {
                        isverified = "Hidden";
                    }

                    if (AvatarSplit == "d-avatar.jpg")
                    {
                        ImageProfile = "Hidden";
                        noProfile_color = Functions.RandomColor();
                    }

                    if (j.LastMessage.Seen == "0")
                    {
                        Message_FontWeight = "ExtraBold";
                        Message_color = Settings.Main_Color;
                    }
                    else if (j.LastMessage.ToId == "")
                    {
                        Message_color = j.ChatColor;
                        j.LastMessage.Text = LocalResources.label_Changed_his_chat_color;
                        ChatColorcirclevisibilty = "Visible";
                    }

                    ///If message contains Media files 
                    if (j.LastMessage.Media.Contains("image"))
                    {
                        MediaIconImage = "Image";
                        MediaIconvisibilty = "Visible";
                        j.LastMessage.Text = LocalResources.label_Send_you_an_image_file;
                    }
                    else if (j.LastMessage.Media.Contains("video"))
                    {
                        MediaIconImage = "Beats";
                        MediaIconvisibilty = "Visible";
                        j.LastMessage.Text = LocalResources.label_Send_you_an_video_file;
                    }
                    else if (j.LastMessage.Media.Contains("sticker"))
                    {
                        MediaIconImage = "Face";
                        MediaIconvisibilty = "Visible";
                        j.LastMessage.Text = LocalResources.label_Send_you_an_sticker_file;
                    }
                    else if (j.LastMessage.Media.Contains("sounds"))
                    {
                        MediaIconImage = "MusicCircle";
                        MediaIconvisibilty = "Visible";
                        j.LastMessage.Text = LocalResources.label_Send_you_an_audio_file;
                    }
                    else if (j.LastMessage.Media.Contains("file"))
                    {
                        MediaIconImage = "FileChart";
                        MediaIconvisibilty = "Visible";
                        j.LastMessage.Text = LocalResources.label_Send_you_an_file;
                    }
                    //else if (j.LastMessage.Stickers.Contains("mp4"))
                    //{
                    //    MediaIconImage = "Gift";
                    //    MediaIconvisibilty = "Visible";
                    //    j.LastMessage.Text = LocalResources.label_Send_you_an_gif_image;
                    //}

                    if (j.LastseenTimeText.Contains("hours ago"))
                    {
                        j.LastseenTimeText = j.LastseenTimeText.Replace("hours ago", LocalResources.label_H);
                    }
                    else if (j.LastseenTimeText.Contains("days ago"))
                    {
                        j.LastseenTimeText = j.LastseenTimeText.Replace("days ago", LocalResources.label_D);
                    }
                    else if (j.LastseenTimeText.Contains("month ago"))
                    {
                        j.LastseenTimeText = j.LastseenTimeText.Replace("month ago", LocalResources.label_M);
                    }
                    else if (j.LastseenTimeText.Contains("months ago"))
                    {
                        j.LastseenTimeText = j.LastseenTimeText.Replace("months ago", LocalResources.label_Min);
                    }
                    else if (j.LastseenTimeText.Contains("day ago"))
                    {
                        j.LastseenTimeText = j.LastseenTimeText.Replace("day ago", LocalResources.label_D);
                    }
                    else if (j.LastseenTimeText.Contains("minutes ago"))
                    {
                        j.LastseenTimeText = j.LastseenTimeText.Replace("minutes ago", LocalResources.label_Min);
                    }
                    else if (j.LastseenTimeText.Contains("seconds ago"))
                    {
                        j.LastseenTimeText = j.LastseenTimeText.Replace("seconds ago", LocalResources.label_Sec);
                    }
                    else if (j.LastseenTimeText.Contains("hour ago"))
                    {
                        j.LastseenTimeText = j.LastseenTimeText.Replace("hour ago", LocalResources.label_H);
                    }

                    if (j.LastMessage.FromId != UserDetails.User_id)
                    {
                        if (j.LastMessage.Seen == "0")
                        {

                        }
                    }
                    else
                    {
                        if (j.LastMessage.Seen != "0")
                        {
                            Chekicon = "Visible";
                        }
                    }


                    var check = ListUsers.FirstOrDefault(a => a.U_Id == j.UserId);
                    if (check != null)
                    {
                        if (check.U_username != j.Username)
                        {
                            check.U_username = j.Username;
                        }

                        if (check.U_name != Functions.HtmlDecodestring(Functions.SubStringCutOf(j.Name, 15)))
                        {
                            check.U_name = Functions.HtmlDecodestring(Functions.SubStringCutOf(j.Name, 15));
                        }

                        check.U_profile_picture = Functions.Get_image(j.UserId, AvatarSplit, j.ProfilePicture);
                        if (check.U_profile_picture != Functions.Get_image(j.UserId, AvatarSplit, j.ProfilePicture) ||
                            ImageUpdate)
                        {
                            check.U_profile_picture = Functions.Get_image(j.UserId, AvatarSplit, j.ProfilePicture);
                            if (ImageUpdate)
                            {
                                ImageUpdate = false;
                            }
                            else
                            {
                                ImageUpdate = true;
                            }
                        }

                        if (check.U_verified != isverified)
                        {
                            check.U_verified = isverified;
                        }

                        if (check.U_lastseen != j.Lastseen)
                        {
                            check.U_lastseen = j.Lastseen;
                        }

                        if (check.U_lastseen_time_text != j.LastseenTimeText)
                        {
                            check.U_lastseen_time_text = j.LastseenTimeText;
                        }

                        if (check.U_lastseen_unix_time != j.LastseenUnixTime)
                        {
                            check.U_lastseen_unix_time = j.LastseenUnixTime;
                        }

                        if (check.U_chat_color != j.ChatColor)
                        {
                            check.U_chat_color = j.ChatColor;
                        }

                        if (check.U_lastseenWithoutCut != U_lastseenWithoutCut)
                        {
                            check.U_lastseenWithoutCut = U_lastseenWithoutCut;
                        }

                        if (check.u_url != j.Url)
                        {
                            check.u_url = j.Url;
                        }

                        if (check.M_Id != j.LastMessage.Id)
                        {
                            check.M_Id = j.LastMessage.Id;
                        }

                        if (check.From_Id != j.LastMessage.FromId)
                        {
                            check.From_Id = j.LastMessage.FromId;
                        }

                        if (check.To_Id != j.LastMessage.ToId)
                        {
                            check.To_Id = j.LastMessage.ToId;
                        }

                        if (check.M_text !=
                            Functions.HtmlDecodestring(Functions.SubStringCutOf(j.LastMessage.Text, 35)))
                        {
                            var index = ListUsers.IndexOf(ListUsers.Where(a => a.U_Id == j.UserId).FirstOrDefault());
                            if (index > -1)
                            {
                                check.M_text =
                                    Functions.HtmlDecodestring(Functions.SubStringCutOf(j.LastMessage.Text, 35));
                                Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                                {
                                    ListUsers.Move(index, 0);
                                    if (this.WindowState == WindowState.Minimized)
                                    {
                                        ///Chk_Desktop_Notifications user - profile
                                        if (Chk_Desktop_Notifications.IsChecked == true)
                                        {
                                            var s = SQLite_Entity.Connection
                                                .Table<DataBase.UsersContactProfileTable>()
                                                .FirstOrDefault(a => a.UCP_Id == j.UserId);
                                            if (s != null)
                                            {
                                                if (s.UCP_Notifications_Message_user == "true")
                                                {
                                                    MsgPopupWindow PopUp =
                                                        new MsgPopupWindow(check.M_text, check.U_username,
                                                            check.U_profile_picture, j.UserId);
                                                    PopUp.Activate();
                                                    PopUp.Show();
                                                    PopUp.Activate();

                                                    var workingAreaaa = SystemParameters.WorkArea;
                                                    var transform =
                                                        PresentationSource.FromVisual(PopUp)
                                                            .CompositionTarget.TransformFromDevice;
                                                    var corner =
                                                        transform.Transform(
                                                            new Point(workingAreaaa.Right, workingAreaaa.Bottom));

                                                    PopUp.Left = corner.X - PopUp.ActualWidth - 10;
                                                    PopUp.Top = corner.Y - PopUp.ActualHeight;
                                                }
                                            }
                                        }
                                    }
                                }));
                            }
                        }

                        if (check.M_media != j.LastMessage.Media)
                        {
                            check.M_media = j.LastMessage.Media;
                        }

                        if (check.M_mediaFileName != j.LastMessage.MediaFileName)
                        {
                            check.M_mediaFileName = j.LastMessage.MediaFileName;
                        }

                        if (check.M_time != j.LastMessage.Time)
                        {
                            check.M_time = j.LastMessage.Time;
                        }

                        if (check.M_mediaFileName != j.LastMessage.MediaFileName)
                        {
                            check.M_mediaFileName = j.LastMessage.MediaFileName;
                        }

                        if (check.M_seen != j.LastMessage.Seen)
                        {
                            check.M_seen = j.LastMessage.Seen;
                        }

                        if (check.M_date_time != j.LastMessage.DateTime)
                        {
                            check.M_date_time = j.LastMessage.DateTime;
                        }

                        //if (check.M_stickers != stickers)
                        //{
                        //    check.M_stickers = stickers;
                        //}
                        if (check.S_Color_onof != Color_onof)
                        {
                            check.S_Color_onof = Color_onof;
                        }

                        if (check.App_Main_Later != Name_App_Main_Leter)
                        {
                            check.App_Main_Later = Name_App_Main_Leter;
                        }

                        if (check.S_ImageProfile != ImageProfile)
                        {
                            check.S_ImageProfile = ImageProfile;
                        }

                        if (check.S_Message_FontWeight != Message_FontWeight)
                        {
                            check.S_Message_FontWeight = Message_FontWeight;
                        }

                        if (check.S_Message_color != Message_color)
                        {
                            check.S_Message_color = Message_color;
                        }

                        if (check.IsSeeniconcheck != Chekicon)
                        {
                            check.IsSeeniconcheck = Chekicon;
                        }

                        if (check.ChatColorcirclevisibilty != ChatColorcirclevisibilty)
                        {
                            check.ChatColorcirclevisibilty = ChatColorcirclevisibilty;
                        }

                        if (check.MediaIconImage != MediaIconImage)
                        {
                            check.MediaIconImage = MediaIconImage;
                        }

                        if (check.MediaIconvisibilty != MediaIconvisibilty)
                        {
                            check.MediaIconvisibilty = MediaIconvisibilty;
                        }

                        if (check.UsernameTwoLetters !=
                            Functions.GetoLettersfromString(
                                Functions.HtmlDecodestring(Functions.SubStringCutOf(j.Name, 15))))
                        {
                            check.UsernameTwoLetters =
                                Functions.GetoLettersfromString(
                                    Functions.HtmlDecodestring(Functions.SubStringCutOf(j.Name, 15)));
                        }
                    }
                    else
                    {
                        try
                        {
                            Users us = new Users();
                            us.U_Id = j.UserId;
                            us.U_username = j.Username;
                            us.U_name = Functions.HtmlDecodestring(Functions.SubStringCutOf(j.Name, 15));
                            us.U_cover_picture = j.CoverPicture;
                            us.U_profile_picture = Functions.Get_image(j.UserId, AvatarSplit, j.ProfilePicture);
                            us.U_verified = isverified;
                            us.U_lastseen = j.Lastseen;
                            us.U_lastseen_time_text = j.LastseenTimeText;
                            us.U_lastseen_unix_time = j.LastseenUnixTime;
                            us.U_chat_color = j.ChatColor;
                            us.U_lastseenWithoutCut = U_lastseenWithoutCut;
                            us.u_url = j.Url;
                            us.M_Id = j.LastMessage.Id;
                            us.From_Id = j.LastMessage.FromId;
                            us.To_Id = j.LastMessage.ToId;
                            us.M_text = Functions.HtmlDecodestring(Functions.SubStringCutOf(j.LastMessage.Text, 35));
                            us.M_media = j.LastMessage.Media;
                            us.M_mediaFileName = j.LastMessage.MediaFileName;
                            us.M_mediaFileNamese = j.LastMessage.MediaFileNames;
                            us.M_time = j.LastMessage.Time;
                            us.M_seen = j.LastMessage.Seen;
                            us.M_date_time = j.LastMessage.DateTime;

                            //Style
                            if (ModeDarkstlye)
                            {
                                us.S_Color_Background = "#232323";
                                us.S_Color_Foreground = "#efefef";
                            }
                            else
                            {
                                us.S_Color_Background = "#ffff";
                                us.S_Color_Foreground = "#444";
                            }

                            us.S_Color_onof = Color_onof;
                            us.App_Main_Later = Name_App_Main_Leter.ToString();
                            us.S_ImageProfile = "Collapsed";
                            us.S_noProfile_color = noProfile_color;
                            us.S_Message_FontWeight = Message_FontWeight;
                            us.S_Message_color = Message_color;
                            us.IsSeeniconcheck = Chekicon;
                            us.ChatColorcirclevisibilty = ChatColorcirclevisibilty;
                            us.MediaIconImage = MediaIconImage;
                            us.MediaIconvisibilty = MediaIconvisibilty;
                            us.UsernameTwoLetters = Functions.GetoLettersfromString(us.U_name);
                            //us.M_stickers = j.LastMessage.Stickers;

                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                ListUsers.Add(us);
                            });
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);

                        }

                    }
                }

                // Insert data user in database
                SQLiteCommandSender.Insert_Or_Replace_ChatActivity(ListUsers);
                SQLiteCommandSender.Insert_Or_Replace_UsersContactProfileTable(ListUsersProfile);


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }


        // Run background worker : Chat
        private async void Chat_Work_Async()
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    IProgress<int> progress = new Progress<int>(percentCompleted =>
                    {
                        ProgressBar_Search_ChatActivty.Value = percentCompleted;
                        Notifications_ProgressBar.Value = percentCompleted;
                    });

                    await Task.Run(async () =>
                    {
                        try
                        {
                            var data = await RequestsAsync.Chat_Activity_Http(UserDetails.User_id);

                            if (data.Item1 == 200)
                            {
                                if (data.Item2 is GetUserListObject result)
                                {

                                    //Get API data users
                                    if (result.Users != null)
                                        ChatUsers(result.Users);

                                    //Get API data notifications
                                    if (result.Notifications != null)
                                        NotificationsUsers(result.Notifications);

                                    //Get API data Pro Users
                                    if (result.ProUsers != null)
                                        Pro_Users(result.ProUsers);

                                    //Get API data Trending Hashtag
                                    if (result.trendingHashtag != null)
                                        Trending_hashtag(result.trendingHashtag);

                                    try
                                    {
                                        var Call_Comming = result.VideoCall;
                                        if (Call_Comming)
                                        {
                                            Main_Call_Comming = "True";
                                            //Main_Video_call_user = result.VideoCallUser;
                                        }
                                        else
                                        {
                                            Main_Call_Comming = "False";
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        Console.WriteLine(exception);
                                    }

                                }
                            }

                            if (UserContacts_list.Items.Count <= 100)
                            {
                                var response =
                                    await RequestsAsync.User_Contact_Http(UserDetails.User_id);

                                if (response.Item1 == 200)
                                {
                                    if (response.Item2 is GetUserContactsObject contacts)
                                    {
                                        UsersContact(contacts.Users);
                                    }
                                }

                            }

                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                    });

                    //if (ListUsers.Count == 0 && state_Check == "1")
                    //    

                    //else
                    //{
                    //    if (state_Check == "1")
                    //        No_Chat_Messages_Panel.Visibility = Visibility.Collapsed;
                    //}

                    if (ListUsers.Count == 0)
                        No_Chat_Messages_Panel.Visibility = Visibility.Visible;
                    else
                        No_Chat_Messages_Panel.Visibility = Visibility.Collapsed;


                    ChatActivityList.ItemsSource = ListUsers;
                    Notifications_list.ItemsSource = ListNotifications;
                    ProUsersList.ItemsSource = ListProUsers;
                    Hashtag_list.ItemsSource = ListHashtag;

                    ProgressBar_Search_ChatActivty.Visibility = Visibility.Collapsed;
                    ProgressBar_Search_ChatActivty.IsIndeterminate = false;

                    Notifications_ProgressBar.Visibility = Visibility.Collapsed;
                    Notifications_ProgressBar.IsIndeterminate = false;


                    //Completed background worker : Users Contact
                    UserContacts_list.ItemsSource = ListUsersContact;
                    ProgressBar_UserContacts.Visibility = Visibility.Collapsed;
                    ProgressBar_UserContacts.IsIndeterminate = false;

                    if (UserContacts_list.Items.Count <= 100)
                    {
                        User_Contacts_Async();
                    }

                    try
                    {


                        if (Main_Call_Comming == "True" && Main_Call_Comming_Form == "False")
                        {
                            var user_id = Main_Video_call_user["user_id"].ToString();
                            var avatar = Main_Video_call_user["avatar"].ToString();
                            var name = Main_Video_call_user["name"].ToString();
                            var CallID = Main_Video_call_user["call_id"].ToString();

                            Main_Call_Comming_Form = "True";
                            Recieve_Call_Window openReciever =
                                new Recieve_Call_Window(user_id, avatar, name, CallID, this);
                            openReciever.Left = -550;
                            openReciever.ShowDialog();
                        }

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }


                    //Run background worker: Users Contact

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }


        //Event Selection Changed in Chat Actitvity User - and Run background worker : Messages , friends Users 
        private void ChatActivityList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Users selectedGroup = (Users)ChatActivityList.SelectedItem;
                if (selectedGroup != null)
                {
                    Task.Run(() =>
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            if (selectedGroup.U_chat_color.Contains("rgb"))
                            {
                                try
                                {
                                    var regex = new Regex(@"([0-9]+)");
                                    string colorData = selectedGroup.U_chat_color;

                                    var matches = regex.Matches(colorData);

                                    var color_r = Convert.ToInt32(matches[0].ToString());
                                    var color_g = Convert.ToInt32(matches[1].ToString());
                                    var color_b = Convert.ToInt32(matches[2].ToString());

                                    ChatColor = HexFromRGB(color_r, color_g, color_b);
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                }
                            }
                            else
                            {
                                ChatColor = selectedGroup.U_chat_color;
                            }

                            Img_user_message = selectedGroup.U_profile_picture;
                            Btn_LoadmoreMessages.Content = LocalResources.label_Load_More_Messages;
                            IDuser = selectedGroup.U_Id;
                            ID_To = selectedGroup.To_Id;
                            ID_From = selectedGroup.From_Id;

                            //Convert
                            var UCP = ListUsersProfile.FirstOrDefault(p => p.UCP_Id == selectedGroup.U_Id);
                            if (UCP != null)
                            {
                                UsersContactProfile data = new UsersContactProfile();

                                data.UCP_Id = UCP.UCP_Id;
                                data.UCP_username = UCP.UCP_username;
                                data.UCP_name = UCP.UCP_name;
                                data.UCP_cover_picture = UCP.UCP_cover_picture;
                                data.UCP_profile_picture = UCP.UCP_profile_picture;
                                data.UCP_verified = UCP.UCP_verified;
                                data.UCP_lastseen = UCP.UCP_lastseen;
                                data.UCP_lastseen_time_text = UCP.UCP_lastseen_time_text;
                                data.UCP_lastseen_unix_time = UCP.UCP_lastseen_unix_time;
                                data.UCP_url = UCP.UCP_url;
                                data.UCP_user_platform = UCP.UCP_user_platform;
                                data.UCP_chat_color = UCP.UCP_chat_color;
                                data.UCP_Notifications_Message_user = UCP.UCP_Notifications_Message_user;
                                data.UCP_Notifications_Message_Sound_user = UCP.UCP_Notifications_Message_Sound_user;
                                data.UCP_email = UCP.UCP_email;
                                data.UCP_first_name = UCP.UCP_first_name;
                                data.UCP_last_name = UCP.UCP_last_name;
                                data.UCP_relationship_id = UCP.UCP_relationship_id;
                                data.UCP_address = UCP.UCP_address;
                                data.UCP_working = UCP.UCP_working;
                                data.UCP_working_link = UCP.UCP_working_link;
                                data.UCP_about = UCP.UCP_about;
                                data.UCP_school = UCP.UCP_school;
                                data.UCP_gender = UCP.UCP_gender;
                                data.UCP_birthday = UCP.UCP_birthday;
                                data.UCP_website = UCP.UCP_website;
                                data.UCP_facebook = UCP.UCP_facebook;
                                data.UCP_google = UCP.UCP_google;
                                data.UCP_twitter = UCP.UCP_twitter;
                                data.UCP_linkedin = UCP.UCP_linkedin;
                                data.UCP_youtube = UCP.UCP_youtube;
                                data.UCP_vk = UCP.UCP_vk;
                                data.UCP_instagram = UCP.UCP_instagram;
                                data.UCP_language = UCP.UCP_language;
                                data.UCP_ip_address = UCP.UCP_ip_address;
                                data.UCP_follow_privacy = UCP.UCP_follow_privacy;
                                data.UCP_post_privacy = UCP.UCP_post_privacy;
                                data.UCP_message_privacy = UCP.UCP_message_privacy;
                                data.UCP_confirm_followers = UCP.UCP_confirm_followers;
                                data.UCP_show_activities_privacy = UCP.UCP_show_activities_privacy;
                                data.UCP_birth_privacy = UCP.UCP_birth_privacy;
                                data.UCP_visit_privacy = UCP.UCP_visit_privacy;
                                data.UCP_showlastseen = UCP.UCP_showlastseen;
                                data.UCP_status = UCP.UCP_status;
                                data.UCP_active = UCP.UCP_active;
                                data.UCP_admin = UCP.UCP_admin;
                                data.UCP_registered = UCP.UCP_registered;
                                data.UCP_phone_number = UCP.UCP_phone_number;
                                data.UCP_is_pro = UCP.UCP_is_pro;
                                data.UCP_pro_type = UCP.UCP_pro_type;
                                data.UCP_joined = UCP.UCP_joined;
                                data.UCP_timezone = UCP.UCP_timezone;
                                data.UCP_referrer = UCP.UCP_referrer;
                                data.UCP_balance = UCP.UCP_balance;
                                data.UCP_paypal_email = UCP.UCP_paypal_email;
                                data.UCP_notifications_sound = UCP.UCP_notifications_sound;
                                data.UCP_order_posts_by = UCP.UCP_order_posts_by;
                                data.UCP_social_login = UCP.UCP_social_login;
                                data.UCP_device_id = UCP.UCP_device_id;

                                //get profile user */ When the conversation is selected, the data moves to the profile \*
                                User_RightSide_ProfileLoader(data);
                                _SelectedUsersContactProfile = data;
                                _SelectedType = "UsersContactProfile";

                            }

                            Get_SharedFiles(selectedGroup.U_Id);

                            if (RightMainPanel.Visibility == Visibility.Collapsed)
                            {
                                RightMainPanel.Visibility = Visibility.Visible;
                                ProfileToggle.Visibility = Visibility.Visible;
                                SendMessagepanel.Visibility = Visibility.Visible;
                                DropDownMenueOnMessageBox.Visibility = Visibility.Collapsed;

                            }
                            ChatTitleChange.Visibility = Visibility.Visible;
                            ChatSeen.Visibility = Visibility.Visible;

                            if (Settings.VideoCall)
                                VideoButton.Visibility = Visibility.Visible;

                            //View data and Colors in Chat Messgaes listBox
                            ConvertMessageColors(selectedGroup);
                            ChatTitleChange.Text = selectedGroup.U_name;
                            ChatSeen.Text = selectedGroup.U_lastseenWithoutCut;
                            if (selectedGroup.M_seen == "0" || selectedGroup.ChatColorcirclevisibilty == "Visible")
                            {
                                selectedGroup.S_Message_color = "#7E7E7E";
                                selectedGroup.S_Message_FontWeight = "Light";
                            }
                        });
                    });

                    //========== View data in Chat Messgaes listBox ===========
                    //Hide NoMessagePanel when it is clicking
                    NoMessagePanel.Visibility = Visibility.Collapsed;

                    if (mediaPlayer.CanPause)
                    {
                        mediaPlayer.Pause();
                        timer.Stop();
                    }
                    Task.Run(() =>
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            try
                            {
                                ListMessages.Clear();
                                before_message_id = "0";

                                if (uplouding.Visibility == Visibility.Visible)
                                {
                                    uplouding.Visibility = Visibility.Collapsed;
                                    uplouding.IsIndeterminate = false;
                                    NoMessagePanel.Visibility = Visibility.Hidden;
                                }

                                var x = SQLiteCommandSender.GetMessages_CredentialsList(selectedGroup.From_Id,
                                    selectedGroup.To_Id, before_message_id, ChatColor);
                                if (x == "1") //database.. Get Messages
                                {
                                    ChatMessgaeslistBox.ItemsSource = ListMessages;

                                    //Scroll Down >> 
                                    ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                                    ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);
                                }
                                else // or server.. Get Messages
                                {
                                    Message_ProgressBar.Visibility = Visibility.Visible;
                                    Message_ProgressBar.IsIndeterminate = true;

                                    Messages_Async();

                                }
                                timer_messages.Stop();
                                timer_messages.Start();
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        });
                    });
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Search Chat Activty Where username
        private void Txt_searchMessages_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var search = ListUsers.Where(a => a.U_username.Contains(Txt_searchMessages.Text)).ToList();
                ChatActivityList.ItemsSource = search;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Messages ##########################

        #region Messages

        /// <summary>
        /// The event  The event Run background worker : Messages in Selectionchanged in Chat Activity User in tab top #region Chat Actitvity User
        /// </summary>

        // Functions Messages
        public void Messages(List<GetUserMessagesObject.Message> messages)
        {
            try
            {
                if (messages != null)
                {
                    ListMessages = new ObservableCollection<Classes.Messages>();


                    foreach (var message in messages)
                    {
                        Classes.Messages m = new Classes.Messages();
                        //Variables
                        var Color_box_message = "";

                        if (message.Type == "right_text" || message.Type == "right_image" ||
                            message.Type == "right_audio" ||
                            message.Type == "right_video" || message.Type == "right_file" ||
                            message.Type == "right_sticker" ||
                            message.Type == "right_contact")
                        {
                            Color_box_message = ChatColor;
                        }

                        if (message.Type == "right_sticker" || message.Type == "left_sticker")
                        {
                            string text_name_sticker = message.Media.Split('_').Last();
                            message.Media = Functions.Get_Sticker_messages(text_name_sticker, message.Media, m);
                        }

                        if (message.Type == "right_video" || message.Type == "left_video")
                        {
                            message.MediaFileName = Functions.Rename_Video(message.MediaFileName);
                        }

                        if (message.Type == "right_gif" || message.Type == "left_gif")
                        {
                            var result = Path.ChangeExtension(message.Stickers, ".gif");
                            string[] stringSeparators = new string[] { "/" };
                            var name = result.Split(stringSeparators, StringSplitOptions.None);
                            var string_url = (name[2] + "/" + name[3]);
                            var string_name = name[4] + ".gif";
                            result = "https://" + string_url.Replace(string_url, "i.giphy.com/") + string_name;
                            message.MediaFileName = Functions.Get_Sticker_messages(string_name, result, m);
                        }

                        if (message.Type == "right_contact" || message.Type == "left_contact")
                        {
                            string[] stringSeparators = new string[] { "&quot;" };
                            var name = message.Text.Split(stringSeparators, StringSplitOptions.None);
                            var string_name = name[3];
                            var string_number = name[7];
                            message.Text = string_name + "\r\n" + string_number;
                        }

                        var Type_Icon_File = "File";
                        if (message.Type == "right_file" || message.Type == "left_file")
                        {
                            if (message.MediaFileName.EndsWith("rar") || message.MediaFileName.EndsWith("RAR") ||
                                message.MediaFileName.EndsWith("zip") || message.MediaFileName.EndsWith("ZIP"))
                            {
                                Type_Icon_File = "ZipBox";
                            }
                            else if (message.MediaFileName.EndsWith("txt") || message.MediaFileName.EndsWith("TXT"))
                            {
                                Type_Icon_File = "NoteText";
                            }
                            else if (message.MediaFileName.EndsWith("docx") || message.MediaFileName.EndsWith("DOCX"))
                            {
                                Type_Icon_File = "FileWord";
                            }
                            else if (message.MediaFileName.EndsWith("doc") || message.MediaFileName.EndsWith("DOC"))
                            {
                                Type_Icon_File = "FileWord";
                            }
                            else if (message.MediaFileName.EndsWith("pdf") || message.MediaFileName.EndsWith("PDF"))
                            {
                                Type_Icon_File = "FilePdf";
                            }

                            if (message.MediaFileName.Length > 25)
                            {
                                message.MediaFileName = Functions.SubStringCutOf(message.MediaFileName, 25) + "." +
                                                        message.MediaFileName.Split('.').Last();
                            }
                        }

                        var check_extension = Functions.Check_FileExtension(message.MediaFileName);
                        var Play_Visibility = "Collapsed";
                        var Pause_Visibility = "Collapsed";
                        var Progress_Visibility = "Collapsed";
                        var Download_Visibility = "Visible";
                        var Video_text_Visibility = "Visible";
                        var Icon_File_Visibility = "Collapsed";
                        var Hlink_Download_Visibility = "Visible";
                        var Hlink_Open_Visibility = "Collapsed";

                        if (check_extension == "Audio")
                        {
                            var checkforSound = Functions.Get_sound(IDuser, message.MediaFileName);
                            if (checkforSound != "Not Found sound")
                            {
                                Play_Visibility = "Visible";
                                Pause_Visibility = "Collapsed";
                                Progress_Visibility = "Collapsed";
                                Download_Visibility = "Collapsed";
                            }
                        }

                        else if (check_extension == "Video")
                        {
                            var checkforSound = Functions.Get_Video(IDuser, message.MediaFileName);
                            if (checkforSound != "Not Found vidoe")
                            {
                                Play_Visibility = "Visible";
                                Progress_Visibility = "Collapsed";
                                Download_Visibility = "Collapsed";
                            }
                        }
                        else if (check_extension == "File")
                        {
                            var checkforSound = Functions.Get_file(IDuser, message.MediaFileName);
                            if (checkforSound != "Not Found file")
                            {
                                Icon_File_Visibility = "Visible";
                                Progress_Visibility = "Collapsed";
                                Download_Visibility = "Collapsed";
                                Hlink_Download_Visibility = "Collapsed";
                                Hlink_Open_Visibility = "Visible";
                            }
                        }

                        m.Mes_Id = message.Id;
                        m.Mes_From_Id = message.FromId;
                        m.Mes_To_Id = message.ToId;
                        m.Mes_Text = message.Text;
                        m.Mes_Media = message.Media;
                        m.Mes_MediaFileName = message.MediaFileName;
                        m.Mes_MediaFileNames = message.MediaFileNames;
                        m.Mes_Time = message.Time;
                        m.Mes_Seen = message.Seen;
                        m.Mes_Deleted_one = message.DeletedOne;
                        m.Mes_Deleted_two = message.DeletedTwo;
                        m.Mes_Sent_push = message.SentPush;
                        m.Mes_Notification_id = message.NotificationId;
                        m.Mes_Type_two = message.TypeTwo;
                        m.Mes_Time_text = message.TimeText;
                        m.Mes_Position = message.Position;
                        m.Mes_Type = message.Type;
                        m.Mes_File_size = message.FileSize;
                        m.Mes_User_avatar = message.MessageUser.Avatar;
                        m.Mes_Stickers = message.Stickers;
                        //style
                        m.Color_box_message = Color_box_message;
                        m.Img_user_message = Img_user_message;
                        m.Progress_Visibility = Progress_Visibility;
                        m.Download_Visibility = Download_Visibility;
                        m.Play_Visibility = Play_Visibility;
                        m.Pause_Visibility = Pause_Visibility;
                        m.Icon_File_Visibility = Icon_File_Visibility;
                        m.Hlink_Download_Visibility = Hlink_Download_Visibility;
                        m.Hlink_Open_Visibility = Hlink_Open_Visibility;
                        m.Type_Icon_File = Type_Icon_File;
                        m.sound_slider_value = 0;
                        m.Progress_Value = 0;
                        m.sound_time = "";

                        if (check_extension == "Image")
                        {
                            message.MediaFileName =
                                Functions.Get_img_messages(IDuser, message.MediaFileName, message.Media, m);
                        }

                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            ListMessages.Add(m);
                        });
                    }

                    // Insert data user in database
                    SQLiteCommandSender.Insert_Or_Replace_Messages(ListMessages);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Run background worker : Messages

        private async void Messages_Async()
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    IProgress<int> progress = new Progress<int>(percentCompleted =>
                    {
                        Message_ProgressBar.Value = percentCompleted;
                        Message_ProgressBar.Visibility = Visibility.Visible;
                        Message_ProgressBar.IsIndeterminate = true;
                    });


                    await Task.Run(async () =>
                    {
                        var response = await RequestsAsync.Get_User_Messages_Web(UserDetails.User_id, IDuser);

                        if (response.Item1 == 200)
                        {
                            if (response.Item2 is GetUserMessagesObject result)
                            {
                                Messages(result.Messages); //begovsky change  
                            }
                        }
                    });

                    ChatMessgaeslistBox.ItemsSource = ListMessages;

                    //Scroll Down >> Elin Doughouz
                    ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                    ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);
                    Message_ProgressBar.Visibility = Visibility.Collapsed;
                    Message_ProgressBar.IsIndeterminate = false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        //Event Scroll Changed Hover : Hidden Or Visible 
        private void cbdg1_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                    () =>
                    {
                        double d = e.VerticalOffset;
                        if (d <= 5)
                        {
                            if (ChatMessgaeslistBox.Items.Count == 0)
                            {
                                Btn_LoadmoreMessages.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                Btn_LoadmoreMessages.Visibility = Visibility.Visible;
                            }
                        }
                        else
                        {
                            Btn_LoadmoreMessages.Visibility = Visibility.Hidden;
                        }
                    }));
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        ////////////////////// 
        /// message type Sound
        ////////////////////// 

        #region Sound

        //Click Button Download Sound in cash
        private void Btn_Sound_Download_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {

                    var mi = (Button)sender;
                    Messages_id = mi.CommandParameter.ToString();

                    var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);

                    if (selectedGroup != null)
                    {
                        if (selectedGroup.Download_Visibility == "Visible")
                        {
                            selectedGroup.Progress_Visibility = "Visible";
                            selectedGroup.Download_Visibility = "Collapsed";
                            selectedGroup.Play_Visibility = "Collapsed";
                            selectedGroup.Pause_Visibility = "Collapsed";

                            Task.Run(() =>
                            {
                                Functions.save_sound(IDuser, selectedGroup.Mes_MediaFileName, selectedGroup.Mes_Media,
                                    selectedGroup);
                            });
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erorr ");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Play Sound
        private void Btn_Play_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mi = (Button)sender;
                Messages_id = mi.CommandParameter.ToString();

                if (mediaPlayer.CanPause)
                {
                    mediaPlayer.Pause();
                    timer.Stop();
                }

                var RemovePause = ListMessages.Where(a => a.Pause_Visibility == "Visible");
                if (RemovePause.Count() > 0)
                {
                    foreach (var PauseIcon in RemovePause)
                    {
                        PauseIcon.Pause_Visibility = "Collapsed";
                        PauseIcon.Play_Visibility = "Visible";
                    }
                }

                var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);

                if (selectedGroup != null)
                {
                    if (selectedGroup.Play_Visibility == "Visible")
                    {
                        selectedGroup.Progress_Visibility = "Collapsed";
                        selectedGroup.Play_Visibility = "Collapsed";
                        selectedGroup.Pause_Visibility = "Visible";

                        var soundPath = Functions.Get_sound(IDuser, selectedGroup.Mes_MediaFileName);
                        if (soundPath != "Not Found sound")
                        {
                            _selectedgroup = selectedGroup;
                            if (selectedGroup.sound_slider_value > 0)
                            {

                                mediaPlayer = new MediaPlayer();
                                mediaPlayer.Open(new Uri(soundPath));
                                var dd = Convert.ToDouble(selectedGroup.sound_slider_value);
                                mediaPlayer.Position = TimeSpan.FromSeconds(dd);
                                mediaPlayer.Play();
                                timer.Start();
                            }
                            else
                            {
                                mediaPlayer = new MediaPlayer();
                                mediaPlayer.Open(new Uri(soundPath));
                                mediaPlayer.Play();

                                timer = new DispatcherTimer();
                                timer.Interval = TimeSpan.FromSeconds(1);
                                timer.Tick += timer_Tick;
                                timer.Start();
                            }
                        }
                        else
                        {
                            Task.Run(() =>
                            {
                                Functions.save_sound(IDuser, selectedGroup.Mes_MediaFileName, selectedGroup.Mes_Media,
                                    selectedGroup);
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

        //Click Button Pause Sound
        private void Btn_Pause_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mi = (Button)sender;
                Messages_id = mi.CommandParameter.ToString();

                var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);
                if (selectedGroup != null)
                {
                    if (selectedGroup.Pause_Visibility == "Visible")
                    {
                        if (mediaPlayer.CanPause)
                        {
                            mediaPlayer.Pause();
                            timer.Stop();
                        }

                        selectedGroup.Pause_Visibility = "Collapsed";
                        selectedGroup.Play_Visibility = "Visible";
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan))
                {
                    if (Convert.ToInt32(mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds) !=
                        _selectedgroup.sound_slider_value)
                    {
                        _selectedgroup.sound_slider_value = Convert.ToInt32(mediaPlayer.Position.TotalSeconds);

                        if (TimeSpan.FromSeconds(mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds).ToString(@"hh") ==
                            "00")
                        {
                            _selectedgroup.sound_time = TimeSpan.FromSeconds(_selectedgroup.sound_slider_value)
                                .ToString(@"mm\:ss");
                        }
                        else
                        {
                            _selectedgroup.sound_time = TimeSpan.FromSeconds(_selectedgroup.sound_slider_value)
                                .ToString(@"hh\:mm\:ss");
                        }
                    }
                    else
                    {
                        _selectedgroup.sound_slider_value = 0;
                        _selectedgroup.Pause_Visibility = "Collapsed";
                        _selectedgroup.Play_Visibility = "Visible";
                        mediaPlayer.Stop();
                        timer.Stop();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                var mi = (Slider)sender;
                mi.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Thumb_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            try
            {
                var mi = (Slider)sender;
                TimeSpan ts = new TimeSpan(0, 0, 0, (int)mi.Value);
                mediaPlayer.Position = ts;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            try
            {
                var mi = (Slider)sender;
                Thumb t = mi.Template.FindName("Thumb", mi) as Thumb;
                if (t != null)
                    Mouse.Capture(t);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        ////////////////////// 
        /// message type Image
        ////////////////////// 

        #region Image

        private void Btn_Open_img_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mi = (Button)sender;
                Messages_id = mi.CommandParameter.ToString();

                var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);
                if (selectedGroup != null)
                {
                    Functions.open_img(IDuser, selectedGroup.Mes_MediaFileName, selectedGroup.Mes_Media, selectedGroup);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Download Image in PC
        private void Btn_Save_img_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    var mi = (Button)sender;
                    Messages_id = mi.CommandParameter.ToString();

                    var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);
                    var pathdeck = Functions.Get_img_messages(IDuser, selectedGroup.Mes_MediaFileName,
                        selectedGroup.Mes_Media, selectedGroup);
                    if (pathdeck.Contains("http:"))
                    {
                        MessageBox.Show("This file is download the main time");
                    }

                    if (selectedGroup != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                        sfd.FileName = selectedGroup.Mes_MediaFileName;
                        ImageFormat format = ImageFormat.Png;
                        var sdf = sfd.ShowDialog();
                        if (sdf == true)
                        {
                            string ext = System.IO.Path.GetExtension(sfd.FileName);
                            switch (ext)
                            {
                                case ".jpg":
                                    format = ImageFormat.Jpeg;
                                    break;
                                case ".bmp":
                                    format = ImageFormat.Bmp;
                                    break;
                                case ".emf":
                                    format = ImageFormat.Emf;
                                    break;
                                case ".exif":
                                    format = ImageFormat.Exif;
                                    break;
                                case ".icon":
                                    format = ImageFormat.Icon;
                                    break;
                                case ".memoryBmp":
                                    format = ImageFormat.MemoryBmp;
                                    break;
                                case ".tiff":
                                    format = ImageFormat.Tiff;
                                    break;
                                case ".wmf":
                                    format = ImageFormat.Wmf;
                                    break;
                            }

                            File.Copy(pathdeck, sfd.FileName, true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erorr ");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        ////////////////////// 
        /// message type video
        ////////////////////// 

        #region video

        //Click Button Play video
        private void Btn_Play_video_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mi = (Button)sender;
                Messages_id = mi.CommandParameter.ToString();

                var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);
                if (selectedGroup != null)
                {
                    string pathdeck = Functions.Get_Video(IDuser, selectedGroup.Mes_MediaFileName);
                    Video_MediaPlayer_Window vmp = new Video_MediaPlayer_Window(pathdeck);
                    vmp.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Download video  in cash
        private void Btn_Download_video_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    var mi = (Button)sender;
                    Messages_id = mi.CommandParameter.ToString();

                    var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);

                    if (selectedGroup != null)
                    {
                        if (selectedGroup.Download_Visibility == "Visible")
                        {
                            selectedGroup.Progress_Visibility = "Visible";
                            selectedGroup.Download_Visibility = "Collapsed";
                            selectedGroup.Play_Visibility = "Collapsed";

                            Task.Run(() =>
                            {
                                Functions.save_Video(IDuser, selectedGroup.Mes_MediaFileName, selectedGroup.Mes_Media,
                                    selectedGroup);
                            });
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erorr ");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        /// message type file
        ////////////////////// 
        //Click Hyper link Download file
        private void Hyperlink_file_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    var mi = (Hyperlink)sender;
                    Messages_id = mi.CommandParameter.ToString();

                    var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);

                    if (selectedGroup != null)
                    {
                        if (selectedGroup.Download_Visibility == "Visible")
                        {
                            selectedGroup.Progress_Visibility = "Visible";
                            selectedGroup.Download_Visibility = "Collapsed";
                            selectedGroup.Icon_File_Visibility = "Collapsed";
                            selectedGroup.Hlink_Download_Visibility = "Collapsed";
                            selectedGroup.Hlink_Open_Visibility = "Visible";

                            Task.Run(() =>
                            {
                                Functions.save_file(IDuser, selectedGroup.Mes_MediaFileName, selectedGroup.Mes_Media,
                                    selectedGroup);
                            });
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Erorr ");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click  Hyper link Open file from deck PC
        private void Hyperlink_Openfile_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                var mi = (Hyperlink)sender;
                Messages_id = mi.CommandParameter.ToString();

                var selectedGroup = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);
                if (selectedGroup != null)
                {
                    string pathdeck = Functions.Get_file(IDuser, selectedGroup.Mes_MediaFileName);
                    Process.Start(new ProcessStartInfo(pathdeck));
                    e.Handled = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        ////////////////////// 
        /// message type gifs
        ////////////////////// 

        #region Gifs

        private void Gifs_Media_OnAnimationLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var b = sender as System.Windows.Controls.Image;
                if (b != null)
                {
                    DependencyObject item = b;
                    while (item is ListBoxItem == false)
                    {
                        item = VisualTreeHelper.GetParent(item);
                    }

                    var lbi = (ListBoxItem)item;
                    var data = (sender as FrameworkElement).DataContext as Classes.Messages;
                    if (data != null)
                    {
                        //data.G_Bar_load_gifs_Visibility = "Collapsed";
                        var controller = ImageBehavior.GetAnimationController(b);
                        if (controller != null)
                        {
                            controller.Pause();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        #endregion

        //########################## Messages Updater\Checker ##########################

        #region Messages Updater\Checker

        private static string TypingStatus = "Online";

        // Run background worker : Messages Updater\Checker
        private async Task Message_Updated_Async()
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    await Task.Run(async () =>
                    {
                        if (ListMessages.Count != 0)
                        {
                            var Updater = ListMessages.SingleOrDefault(d => d.Mes_Id == Messages_id);
                            if (Updater != null)
                            {
                                return;
                            }
                            else
                            {
                                LastMessageid = ListMessages.Last().Mes_Id;
                            }

                            var response = await RequestsAsync.Message_Update_Http(UserDetails.User_id,
                                IDuser, "0", LastMessageid);

                            if (response.Item1 == 200)
                            {
                                if (response.Item2 is MessageUpdateObject result)
                                {

                                    TypingStatus = result.Typing.ToString() == "1" ? "Typping" : "Normal";


                                    if (result.Messages.Count == 0)
                                    {
                                        LoadmoremessgaCount = "0";
                                        return;
                                    }
                                    else
                                    {
                                        LoadmoremessgaCount = "1";
                                        Classes.Messages m = new Classes.Messages();
                                        foreach (var message in result.Messages)
                                        {

                                            //Variables
                                            var Color_box_message = "";

                                            if (message.Type == "right_text" || message.Type == "right_image" ||
                                                message.Type == "right_audio" ||
                                                message.Type == "right_video" || message.Type == "right_file" ||
                                                message.Type == "right_sticker" ||
                                                message.Type == "right_contact")
                                            {
                                                Color_box_message = ChatColor;
                                            }

                                            if (message.Type == "right_sticker" || message.Type == "left_sticker")
                                            {
                                                string text_name_sticker = message.Media.Split('_').Last();
                                                message.Media =
                                                    Functions.Get_Sticker_messages(text_name_sticker, message.Media, m);
                                            }

                                            if (message.Type == "right_video" || message.Type == "left_video")
                                            {
                                                message.MediaFileName = Functions.Rename_Video(message.MediaFileName);
                                            }

                                            if (message.Type == "right_gif" || message.Type == "left_gif")
                                            {
                                                var resultGif = Path.ChangeExtension(message.Stickers, ".gif");
                                                string[] stringSeparators = new string[] { "/" };
                                                var name = resultGif.Split(stringSeparators, StringSplitOptions.None);
                                                var string_url = (name[2] + "/" + name[3]);
                                                var string_name = name[4] + ".gif";
                                                resultGif = "https://" +
                                                            string_url.Replace(string_url, "i.giphy.com/") +
                                                            string_name;
                                                message.MediaFileName =
                                                    Functions.Get_Sticker_messages(string_name, resultGif, m);
                                            }

                                            if (message.Type == "right_contact" || message.Type == "left_contact")
                                            {
                                                string[] stringSeparators = new string[] { "&quot;" };
                                                var name = message.Text.Split(stringSeparators,
                                                    StringSplitOptions.None);
                                                var string_name = name[3];
                                                var string_number = name[7];
                                                message.Text = string_name + "\r\n" + string_number;
                                            }

                                            var Type_Icon_File = "File";
                                            if (message.Type == "right_file" || message.Type == "left_file")
                                            {
                                                if (message.MediaFileName.EndsWith("rar") ||
                                                    message.MediaFileName.EndsWith("RAR") ||
                                                    message.MediaFileName.EndsWith("zip") ||
                                                    message.MediaFileName.EndsWith("ZIP"))
                                                {
                                                    Type_Icon_File = "ZipBox";
                                                }
                                                else if (message.MediaFileName.EndsWith("txt") ||
                                                         message.MediaFileName.EndsWith("TXT"))
                                                {
                                                    Type_Icon_File = "NoteText";
                                                }
                                                else if (message.MediaFileName.EndsWith("docx") ||
                                                         message.MediaFileName.EndsWith("DOCX"))
                                                {
                                                    Type_Icon_File = "FileWord";
                                                }
                                                else if (message.MediaFileName.EndsWith("doc") ||
                                                         message.MediaFileName.EndsWith("DOC"))
                                                {
                                                    Type_Icon_File = "FileWord";
                                                }
                                                else if (message.MediaFileName.EndsWith("pdf") ||
                                                         message.MediaFileName.EndsWith("PDF"))
                                                {
                                                    Type_Icon_File = "FilePdf";
                                                }

                                                if (message.MediaFileName.Length > 25)
                                                {
                                                    message.MediaFileName =
                                                        Functions.SubStringCutOf(message.MediaFileName, 25) + "." +
                                                        message.MediaFileName.Split('.').Last();
                                                }
                                            }

                                            var check_extension = Functions.Check_FileExtension(message.MediaFileName);
                                            var Play_Visibility = "Collapsed";
                                            var Pause_Visibility = "Collapsed";
                                            var Progress_Visibility = "Collapsed";
                                            var Download_Visibility = "Visible";
                                            var Video_text_Visibility = "Visible";
                                            var Icon_File_Visibility = "Collapsed";
                                            var Hlink_Download_Visibility = "Visible";
                                            var Hlink_Open_Visibility = "Collapsed";

                                            if (check_extension == "Audio")
                                            {
                                                var checkforSound = Functions.Get_sound(IDuser, message.MediaFileName);
                                                if (checkforSound != "Not Found sound")
                                                {
                                                    Play_Visibility = "Visible";
                                                    Pause_Visibility = "Collapsed";
                                                    Progress_Visibility = "Collapsed";
                                                    Download_Visibility = "Collapsed";
                                                }
                                            }

                                            else if (check_extension == "Video")
                                            {
                                                var checkforSound = Functions.Get_Video(IDuser, message.MediaFileName);
                                                if (checkforSound != "Not Found vidoe")
                                                {
                                                    Play_Visibility = "Visible";
                                                    Progress_Visibility = "Collapsed";
                                                    Download_Visibility = "Collapsed";
                                                }
                                            }
                                            else if (check_extension == "File")
                                            {
                                                var checkforSound = Functions.Get_file(IDuser, message.MediaFileName);
                                                if (checkforSound != "Not Found file")
                                                {
                                                    Icon_File_Visibility = "Visible";
                                                    Progress_Visibility = "Collapsed";
                                                    Download_Visibility = "Collapsed";
                                                    Hlink_Download_Visibility = "Collapsed";
                                                    Hlink_Open_Visibility = "Visible";
                                                }
                                            }

                                            m.Mes_Id = message.Id;
                                            m.Mes_From_Id = message.FromId;
                                            m.Mes_To_Id = message.ToId;
                                            m.Mes_Text = message.Text;
                                            m.Mes_Media = message.Media;
                                            m.Mes_MediaFileName = message.MediaFileName;
                                            m.Mes_MediaFileNames = message.MediaFileNames;
                                            m.Mes_Time = message.Time;
                                            m.Mes_Seen = message.Seen;
                                            m.Mes_Deleted_one = message.DeletedOne;
                                            m.Mes_Deleted_two = message.DeletedTwo;
                                            m.Mes_Sent_push = message.SentPush;
                                            m.Mes_Notification_id = message.NotificationId;
                                            m.Mes_Type_two = message.TypeTwo;
                                            m.Mes_Time_text = message.TimeText;
                                            m.Mes_Position = message.Position;
                                            m.Mes_Type = message.Type;
                                            m.Mes_File_size = message.FileSize;
                                            m.Mes_User_avatar = message.MessageUser.Avatar;
                                            m.Mes_Stickers = message.Stickers;
                                            //style
                                            m.Color_box_message = Color_box_message;
                                            m.Img_user_message = Img_user_message;
                                            m.Progress_Visibility = Progress_Visibility;
                                            m.Download_Visibility = Download_Visibility;
                                            m.Play_Visibility = Play_Visibility;
                                            m.Pause_Visibility = Pause_Visibility;
                                            m.Icon_File_Visibility = Icon_File_Visibility;
                                            m.Hlink_Download_Visibility = Hlink_Download_Visibility;
                                            m.Hlink_Open_Visibility = Hlink_Open_Visibility;
                                            m.Type_Icon_File = Type_Icon_File;
                                            m.sound_slider_value = 0;
                                            m.Progress_Value = 0;
                                            m.sound_time = "";

                                            if (check_extension == "Image")
                                            {
                                                message.MediaFileName =
                                                    Functions.Get_img_messages(IDuser, message.MediaFileName,
                                                        message.Media, m);
                                            }

                                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                            {

                                                ListMessages.Add(m);
                                                var index = ListUsers.IndexOf(ListUsers.Where(a => a.U_Id == IDuser)
                                                    .FirstOrDefault());
                                                if (index > -1)
                                                {
                                                    ListUsers.Move(index, 0);
                                                }

                                            });

                                            // Insert data user in database
                                            SQLiteCommandSender.Insert_Or_Replace_Messages(ListMessages);
                                        }
                                    }
                                }
                            }
                        }

                    });

                    var bw2 = new BackgroundWorker();
                    bw2.DoWork += (o, args) => AddNewMessagetoCash();
                    bw2.RunWorkerAsync();
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(async () =>
                    {

                        try
                        {
                            if (TypingStatus == "Typping")
                            {
                                ChatSeen.Text = LocalResources.label_Typping;
                            }
                            else
                            {
                                if (ListUsers.FirstOrDefault(a => a.U_Id == IDuser).U_lastseen != "on")
                                {

                                    ChatSeen.Text = LocalResources.label_last_seen + " " +
                                                    ListUsers.FirstOrDefault(a => a.U_Id == IDuser)
                                                        .U_lastseen_time_text;
                                }
                                else
                                {
                                    ChatSeen.Text = LocalResources.label_Online;
                                }

                            }

                            if (LoadmoremessgaCount == "0")
                            {
                                if (ChatMessgaeslistBox.SelectedItem != null)
                                    LastMessageid = (ChatMessgaeslistBox.SelectedItem as Classes.Messages).Mes_Id;

                                await Message_Updated_Async().ConfigureAwait(false); ;

                                // ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                                // ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);

                                return;
                            }
                            else
                            {
                                ChatMessgaeslistBox.ItemsSource = ListMessages;

                                if (ChatMessgaeslistBox.SelectedItem != null)
                                    LastMessageid = ListMessages.Last().Mes_Id;

                                await Message_Updated_Async().ConfigureAwait(false); ;

                                ListBoxAutomationPeer svAutomation =
                                    (ListBoxAutomationPeer)ScrollViewerAutomationPeer.CreatePeerForElement(
                                        ChatMessgaeslistBox);

                                ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                                ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }
                    }));

                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }


        // Functions Add New Message to list and database
        public void AddNewMessagetoCash()
        {
            try
            {
                if (Functions.CheckForInternetConnection() == false)
                {
                    return;
                }

                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["user_id"] = UserDetails.User_id;
                    values["user_profile_id"] = IDuser;
                    values["s"] = Current.AccessToken;
                    values["before_message_id"] = "0";
                    values["after_message_id"] = "0";

                    var ChatusersListresponse =
                        client.UploadValues(WoWonderClient.Client.WebsiteUrl + "/app_api.php?type=get_user_messages",
                            values);
                    var ChatusersListresponseString = Encoding.Default.GetString(ChatusersListresponse);
                    var dictChatusersList =
                        new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(ChatusersListresponseString);
                    string ApiStatus = dictChatusersList["api_status"].ToString();

                    if (ApiStatus == "200")
                    {
                        var s = new JavaScriptSerializer();
                        var gg = dictChatusersList["messages"];
                        var messages = JObject.Parse(ChatusersListresponseString).SelectToken("messages").ToString();
                        JArray ChatMessages = JArray.Parse(messages);
                        if (ChatMessages.Count() == 0)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        #endregion

        //########################## Messages sent\Attach file ##########################

        #region Messages sent\Attach file

        #region sent Attach file 

        public string size_file = "";

        private async void Btn_Attach_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                // Set filter for file extension and default file extension by default its select from 
                dlg.DefaultExt = "All files";

                try
                {
                    string Extenstions = SQLiteCommandSender.GetUsersSettings().S_AllowedExtenstion.Replace(",", ";*.");
                    dlg.Filter = "All files (Documents,Images,Media,Archive)|" + Extenstions + "";
                }
                catch
                {
                    dlg.Filter =
                        "Documents (.txt;*.pdf)|*.txt;*.pdf|Image files (*.png;*.jpg;*.gif;*.ico;*.jpeg)|*.png;*.jpg;*.gif;*.ico;*.jpeg|Media files (*.mp4;*.mp3;*.avi;*.3gp;*.mp2;*.wmv;*.mkv;*.mpg;*.flv;*.wav)|*.mp4;*.mp3;*.avi;*.3gp;*.mp2;*.wmv;*.mkv;*.mpg;*.flv;*.wav|Archive files (.rar;*.zip;*.iso;*.tar;*.gz)|.rar;*.zip;*.iso;*.tar;*.gz|All files (*.*)|*.*";
                }

                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    if (internetconection)
                    {
                        var index = ListUsers.IndexOf(ListUsers.Where(a => a.U_Id == IDuser).FirstOrDefault());
                        if (index > -1)
                        {
                            ListUsers.Move(index, 0);
                        }

                        // Open document
                        string filename = dlg.FileName;
                        var FileName_Media = filename.Split('\\').Last();

                        var size = new FileInfo(dlg.FileName).Length;
                        double total_size = size / 1024.0F / 1024.0F;
                        size_file = total_size.ToString("0.### KB");

                        var filecopy = Functions.Files_Destination + IDuser;
                        var check_extension = Functions.Check_FileExtension(filename);

                        var copyedfile = "";

                        if (check_extension == "File")
                        {
                            copyedfile = filecopy + "\\" + "file\\" + FileName_Media;
                            if (!File.Exists(copyedfile))
                            {
                                File.Copy(filename, copyedfile, true);
                            }
                        }

                        if (check_extension == "Image")
                        {
                            copyedfile = filecopy + "\\" + "images\\" + FileName_Media;
                            if (!File.Exists(copyedfile))
                            {
                                File.Copy(filename, copyedfile, true);
                            }
                        }

                        if (check_extension == "Video")
                        {
                            copyedfile = filecopy + "\\" + "video\\" + FileName_Media;
                            if (!File.Exists(copyedfile))
                            {
                                File.Copy(filename, copyedfile, true);
                            }
                        }

                        if (check_extension == "Audio")
                        {
                            copyedfile = filecopy + "\\" + "sounds\\" + FileName_Media;
                            if (!File.Exists(copyedfile))
                            {
                                File.Copy(filename, copyedfile, true);
                            }
                        }

                        var f = filename.Split(new char[] { '.' });
                        var allowedExtenstion = SQLiteCommandSender.GetUsersSettings().S_AllowedExtenstion
                            .Split(new char[] { ',' });
                        if (allowedExtenstion.Contains(f.Last()))
                        {
                            try
                            {
                                uplouding.Visibility = Visibility.Visible;
                                uplouding.IsIndeterminate = true;
                                NoMessagePanel.Visibility = Visibility.Hidden;


                                var response = await RequestsAsync.InsertNewMessage(UserDetails.User_id, IDuser, "",
                                    time2, copyedfile);

                                if (response.Item1 == 200)
                                {
                                    if (response.Item2 is InsertNewMessageObject res)
                                    {

                                        MethodTosendAttchmentMesssage(filename);

                                        UpdateMessageID(res);
                                            
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
                            MessageBox.Show("The selected file extenstion is forbidden !", "Error", MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(LocalResources.label_Please_check_your_internet);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private static string TextMesage = "";
        private static Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        string time2 = unixTimestamp.ToString();

        private void ScrollAndUpdateChatbox()
        {
            try
            {
                MessageBoxText.Document.Blocks.Clear();
                ChatMessgaeslistBox.ItemsSource = ListMessages;

                Btn_LoadmoreMessages.Visibility = Visibility.Hidden;
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                {
                    ListBoxAutomationPeer svAutomation =
                        (ListBoxAutomationPeer)ScrollViewerAutomationPeer.CreatePeerForElement(ChatMessgaeslistBox);

                    //Scroll Down >> 
                    ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                    ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);

                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Update Message
        private void UpdateMessageID(InsertNewMessageObject result)
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {

                    foreach (var message in result.Messages)
                    {
                        var msg = ListMessages.LastOrDefault();


                        //Variables
                        var Color_box_message = "";

                        if (message.Type == "right_text" || message.Type == "right_image" ||
                            message.Type == "right_audio" ||
                            message.Type == "right_video" || message.Type == "right_file" ||
                            message.Type == "right_sticker" ||
                            message.Type == "right_contact")
                        {
                            Color_box_message = ChatColor;
                        }

                        if (message.Type == "right_sticker" || message.Type == "left_sticker")
                        {
                            string text_name_sticker = message.Media.Split('_').Last();
                            message.Media = Functions.Get_Sticker_messages(text_name_sticker, message.Media, msg);
                        }

                        if (message.Type == "right_video" || message.Type == "left_video")
                        {
                            // msg_mediaFileName = Functions.Rename_Video(msg_mediaFileName);
                        }

                        if (message.Type == "right_gif" || message.Type == "left_gif")
                        {
                            var resultGif = Path.ChangeExtension(message.Stickers, ".gif");
                            string[] stringSeparators = new string[] { "/" };
                            var name = resultGif.Split(stringSeparators, StringSplitOptions.None);
                            var string_url = (name[2] + "/" + name[3]);
                            var string_name = name[4] + ".gif";
                            resultGif = "https://" + string_url.Replace(string_url, "i.giphy.com/") + string_name;
                            message.MediaFileName = Functions.Get_Sticker_messages(string_name, resultGif, msg);
                        }

                        if (message.Type == "right_contact" || message.Type == "left_contact")
                        {
                            string[] stringSeparators = new string[] { "&quot;" };
                            var name = message.Text.Split(stringSeparators, StringSplitOptions.None);
                            var string_name = name[3];
                            var string_number = name[7];
                            message.Text = string_name + "\r\n" + string_number;
                        }

                        var Type_Icon_File = "File";
                        if (message.Type == "right_file" || message.Type == "left_file")
                        {
                            if (message.MediaFileName.EndsWith("rar") || message.MediaFileName.EndsWith("RAR") ||
                                message.MediaFileName.EndsWith("zip") || message.MediaFileName.EndsWith("ZIP"))
                            {
                                Type_Icon_File = "ZipBox";
                            }
                            else if (message.MediaFileName.EndsWith("txt") || message.MediaFileName.EndsWith("TXT"))
                            {
                                Type_Icon_File = "NoteText";
                            }
                            else if (message.MediaFileName.EndsWith("docx") || message.MediaFileName.EndsWith("DOCX"))
                            {
                                Type_Icon_File = "FileWord";
                            }
                            else if (message.MediaFileName.EndsWith("doc") || message.MediaFileName.EndsWith("DOC"))
                            {
                                Type_Icon_File = "FileWord";
                            }
                            else if (message.MediaFileName.EndsWith("pdf") || message.MediaFileName.EndsWith("PDF"))
                            {
                                Type_Icon_File = "FilePdf";
                            }

                            if (message.MediaFileName.Length > 25)
                            {
                                message.MediaFileName = Functions.SubStringCutOf(message.MediaFileName, 25) + "." +
                                                        message.MediaFileName.Split('.').Last();
                            }
                        }

                        var check_extension = Functions.Check_FileExtension(message.MediaFileName);
                        var Play_Visibility = "Collapsed";
                        var Pause_Visibility = "Collapsed";
                        var Progress_Visibility = "Collapsed";
                        var Download_Visibility = "Visible";
                        var Video_text_Visibility = "Visible";
                        var Icon_File_Visibility = "Collapsed";
                        var Hlink_Download_Visibility = "Visible";
                        var Hlink_Open_Visibility = "Collapsed";

                        if (check_extension == "Audio")
                        {
                            var checkforSound = Functions.Get_sound(IDuser, message.MediaFileName);
                            if (checkforSound != "Not Found sound")
                            {
                                Play_Visibility = "Visible";
                                Pause_Visibility = "Collapsed";
                                Progress_Visibility = "Collapsed";
                                Download_Visibility = "Collapsed";
                            }
                        }

                        else if (check_extension == "Video")
                        {
                            var checkforSound = Functions.Get_Video(IDuser, message.MediaFileName);
                            if (checkforSound != "Not Found vidoe")
                            {
                                Play_Visibility = "Visible";
                                Progress_Visibility = "Collapsed";
                                Download_Visibility = "Collapsed";
                            }
                        }
                        else if (check_extension == "File")
                        {
                            var checkforSound = Functions.Get_file(IDuser, message.MediaFileName);
                            if (checkforSound != "Not Found file")
                            {
                                Icon_File_Visibility = "Visible";
                                Progress_Visibility = "Collapsed";
                                Download_Visibility = "Collapsed";
                                Hlink_Download_Visibility = "Collapsed";
                                Hlink_Open_Visibility = "Visible";
                            }
                        }

                        if (msg != null)
                        {
                            msg.Mes_Id = message.Id;
                            msg.Mes_From_Id = message.FromId;
                            msg.Mes_To_Id = message.ToId;
                            msg.Mes_Text = message.Text;
                            msg.Mes_Media = message.Media;
                            msg.Mes_MediaFileName = message.MediaFileName;
                            msg.Mes_MediaFileNames = message.MediaFileNames;
                            msg.Mes_Time = message.Time;
                            msg.Mes_Seen = message.Seen;
                            msg.Mes_Deleted_one = message.DeletedOne;
                            msg.Mes_Deleted_two = message.DeletedTwo;
                            msg.Mes_Sent_push = message.SentPush;
                            msg.Mes_Notification_id = message.NotificationId;
                            msg.Mes_Type_two = message.TypeTwo;
                            msg.Mes_Time_text = message.TimeText;
                            msg.Mes_Position = message.Position;
                            msg.Mes_Type = message.Type;
                            msg.Mes_File_size = message.FileSize.ToString();
                            msg.Mes_Sent_Time = message.TimeText;
                            msg.Mes_Stickers = message.Stickers;

                            //style
                            msg.Color_box_message = Color_box_message;
                            msg.Img_user_message = Img_user_message;
                            msg.Progress_Visibility = Progress_Visibility;
                            msg.Download_Visibility = Download_Visibility;
                            msg.Play_Visibility = Play_Visibility;
                            msg.Pause_Visibility = Pause_Visibility;
                            msg.Icon_File_Visibility = Icon_File_Visibility;
                            msg.Hlink_Download_Visibility = Hlink_Download_Visibility;
                            msg.Hlink_Open_Visibility = Hlink_Open_Visibility;
                            msg.Type_Icon_File = Type_Icon_File;
                            msg.sound_slider_value = 0;
                            msg.Progress_Value = 0;
                            msg.sound_time = "";

                            if (check_extension == "Image")
                            {
                                msg.Mes_MediaFileName =
                                    Functions.Get_img_messages(IDuser, msg.Mes_MediaFileName, msg.Mes_Media, msg);
                            }

                            updater(msg);
                        }
                       
                    }


                }
                else
                {
                    MessageBox.Show("Cannot send Message , check your Internet Connection !", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateLastMessgaeControl()
        {
            try
            {
                ChatMessgaeslistBox.ItemsSource = ListMessages;
                uplouding.Visibility = Visibility.Hidden;
                uplouding.IsIndeterminate = false;
                Btn_LoadmoreMessages.Visibility = Visibility.Hidden;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void updater(Classes.Messages m)
        {
            try
            {
                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    uplouding.Visibility = Visibility.Collapsed;
                    uplouding.IsIndeterminate = false;
                    NoMessagePanel.Visibility = Visibility.Hidden;
                });
                var Updater = ListMessages.Where(d => d.Mes_Id == m.Mes_Sent_Time).FirstOrDefault();
                if (Updater != null)
                {
                    Updater.Mes_Sent_Time = m.Mes_Sent_Time;
                    Updater.Mes_Id = m.Mes_Id;
                    Updater.Mes_From_Id = m.Mes_From_Id;
                    Updater.Mes_To_Id = m.Mes_To_Id;
                    Updater.Mes_Text = m.Mes_Text;

                    if (m.Mes_Type != "right_gif")
                    {
                        Updater.Mes_Media = m.Mes_Media;
                        Updater.Mes_MediaFileName = m.Mes_MediaFileName;
                    }

                    Updater.Mes_MediaFileName = m.Mes_MediaFileName;
                    Updater.Mes_MediaFileNames = m.Mes_MediaFileNames;
                    Updater.Mes_Time = m.Mes_Time;
                    Updater.Mes_Seen = m.Mes_Seen;
                    Updater.Mes_Deleted_one = m.Mes_Deleted_one;
                    Updater.Mes_Deleted_two = m.Mes_Deleted_two;
                    Updater.Mes_Sent_push = m.Mes_Sent_push;
                    Updater.Mes_Notification_id = m.Mes_Notification_id;
                    Updater.Mes_Type_two = m.Mes_Type_two;
                    Updater.Mes_Time_text = m.Mes_Time_text;
                    Updater.Mes_Position = m.Mes_Position;
                    Updater.Mes_Type = m.Mes_Type;
                    Updater.Mes_File_size = size_file;
                    Updater.Mes_Stickers = m.Mes_Stickers;
                    Updater.Mes_Sent_Time = m.Mes_Sent_Time;
                    //style
                    Updater.Color_box_message = m.Color_box_message;
                    Updater.Img_user_message = m.Img_user_message;
                    Updater.Progress_Visibility = m.Progress_Visibility;
                    Updater.Download_Visibility = m.Download_Visibility;
                    Updater.Play_Visibility = m.Play_Visibility;
                    Updater.Pause_Visibility = m.Pause_Visibility;
                    Updater.Icon_File_Visibility = m.Icon_File_Visibility;
                    Updater.Hlink_Download_Visibility = m.Hlink_Download_Visibility;
                    Updater.Hlink_Open_Visibility = m.Hlink_Open_Visibility;
                    Updater.Type_Icon_File = m.Type_Icon_File;
                    Updater.sound_slider_value = 0;
                    Updater.Progress_Value = 0;
                    Updater.sound_time = "";

                    SQLiteCommandSender.Insert_Or_Update_To_one_MessagesTable(Updater);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        #region Send Button >> MessageBoxText

        private void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    TextRange textRange = new TextRange(MessageBoxText.Document.ContentStart,
                        MessageBoxText.Document.ContentEnd);
                    var TextMsg = textRange.Text;

                    if (TextMsg == "" || TextMsg == " " ||
                        TextMsg == "Write your Message\r\n" ||
                        ChatTitleChange.Text == ""
                    )
                    {
                        return;
                    }
                    else
                    {
                        var x = ListUsers.IndexOf(ListUsers.Where(a => a.U_Id == IDuser).FirstOrDefault());
                        if (x > -1)
                        {
                            ListUsers.Move(x, 0);
                        }

                        StringWriter myWriter = new StringWriter();
                        HttpUtility.HtmlDecode(textRange.Text, myWriter);
                        TextMsg = myWriter.ToString();
                        TextMesage = TextMsg;
                        var Updater2 = ListUsers.Where(d => d.U_Id == IDuser).FirstOrDefault();
                        if (Updater2 != null)
                        {
                            Updater2.M_text = TextMsg;
                            ChatActivityList.Items.Refresh();
                        }

                        NoMessagePanel.Visibility = Visibility.Hidden;
                        MethodTosendMesssage(TextMsg);
                    }
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private void MessageBoxText_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void MessageBoxText_OnGotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextRange textRange = new TextRange(MessageBoxText.Document.ContentStart,
                    MessageBoxText.Document.ContentEnd);
                if (textRange.Text == LocalResources.label_MessageBoxText + "\r\n")
                {
                    textRange.Text = "";
                    if (Btn_SwitchDarkMode.IsChecked == true)
                    {
                        MessageBoxText.Foreground = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        MessageBoxText.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
            }

            catch
            {
            }
        }

        private void MessageBoxText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextRange textRange = new TextRange(MessageBoxText.Document.ContentStart,
                    MessageBoxText.Document.ContentEnd);
                if (textRange.Text.Length >= 1 && textRange.Text.Length <= 2)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += (o, args) => TypingEvent("Typping");
                    bw.RunWorkerAsync();
                }
                else if (textRange.Text == "")
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += (o, args) => TypingEvent("removing");
                    bw.RunWorkerAsync();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void TypingEvent(string Status)
        {
            try
            {
                if (internetconection)
                {
                    using (var client = new WebClient())
                    {
                        var values = new NameValueCollection();
                        values["user_id"] = UserDetails.User_id;
                        values["recipient_id"] = IDuser;
                        values["s"] = Current.AccessToken;
                        if (Status == "Typping")
                        {
                            var ChatusersListresponse =
                                client.UploadValues(
                                    WoWonderClient.Client.WebsiteUrl + "/app_api.php?type=register_typing", values);
                            var ChatusersListresponseString = Encoding.Default.GetString(ChatusersListresponse);
                            var dictChatusersList =
                                new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(
                                    ChatusersListresponseString);
                            string ApiStatus = dictChatusersList["api_status"].ToString();

                            if (ApiStatus == "200")
                            {
                                return;
                            }
                        }
                        else if (Status == "removing")
                        {
                            var ChatusersListresponse =
                                client.UploadValues(
                                    WoWonderClient.Client.WebsiteUrl + "/app_api.php?type=remove_typing", values);
                            var ChatusersListresponseString = Encoding.Default.GetString(ChatusersListresponse);
                            var dictChatusersList =
                                new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(
                                    ChatusersListresponseString);
                            string ApiStatus = dictChatusersList["api_status"].ToString();

                            if (ApiStatus == "200")
                            {
                                return;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void MessageBoxText_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
                {
                    string textRange = new TextRange(MessageBoxText.Document.ContentStart,
                        MessageBoxText.Document.ContentEnd).Text;
                    var TextMsg = textRange;
                    var Spaces = Regex.Matches(textRange, "\r\n").Count;

                    if (Spaces < 3)
                    {
                        TextMsg = textRange.Replace("\r\n", "");
                    }

                    if (TextMsg == "" || TextMsg == "Write your Message\r\n" || ChatTitleChange.Text == "")
                    {
                        return;
                    }
                    else
                    {
                        var x = ListUsers.IndexOf(ListUsers.Where(a => a.U_Id == IDuser).FirstOrDefault());
                        if (x > -1)
                        {
                            ListUsers.Move(x, 0);
                        }

                        StringWriter myWriter = new StringWriter();
                        HttpUtility.HtmlDecode(TextMsg, myWriter);
                        TextMsg = myWriter.ToString();
                        TextMesage = TextMsg;
                        var Updater2 = ListUsers.Where(d => d.U_Id == IDuser).FirstOrDefault();
                        if (Updater2 != null)
                        {
                            Updater2.M_text = TextMsg;
                        }

                        NoMessagePanel.Visibility = Visibility.Hidden;

                        MethodTosendMesssage(TextMsg);

                        MessageBoxText.Document.Blocks.Clear();
                    }

                    e.Handled = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        #region Drop

        private void WinMin_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effects = DragDropEffects.Copy;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        List<string> chekFile = new List<string>();

        private async void WinMin_Drop(object sender, DragEventArgs e)
        {
            try
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files != null && files.Length != 0)
                {
                    if (files.Length == 1)
                    {
                        // do what you want
                        foreach (var fils in files)
                        {
                            var FileName_Media = fils.Split('\\').Last();

                            var size = new FileInfo(fils).Length;
                            double total_size = size / 1024.0F / 1024.0F;
                            size_file = total_size.ToString("0.### KB");

                            var filecopy = Functions.Files_Destination + IDuser;
                            var check_extension = Functions.Check_FileExtension(fils);
                            var copyedfile = "";
                            if (check_extension == "File")
                            {
                                copyedfile = filecopy + "\\" + "file\\" + FileName_Media;
                                if (!File.Exists(copyedfile))
                                {
                                    File.Copy(fils, copyedfile, true);
                                }
                            }

                            if (check_extension == "Image")
                            {
                                copyedfile = filecopy + "\\" + "images\\" + FileName_Media;
                                if (!File.Exists(copyedfile))
                                {
                                    File.Copy(fils, copyedfile, true);
                                }
                            }

                            if (check_extension == "Video")
                            {
                                copyedfile = filecopy + "\\" + "video\\" + FileName_Media;
                                if (!File.Exists(copyedfile))
                                {
                                    File.Copy(fils, copyedfile, true);
                                }
                            }

                            if (check_extension == "Audio")
                            {
                                copyedfile = filecopy + "\\" + "sounds\\" + FileName_Media;
                                if (!File.Exists(copyedfile))
                                {
                                    File.Copy(fils, copyedfile, true);
                                }
                            }

                            var f = copyedfile.Split(new char[] { '.' });
                            var allowedExtenstion = SQLiteCommandSender.GetUsersSettings()
                                .S_AllowedExtenstion.Split(new char[] { ',' });
                            if (allowedExtenstion.Contains(f.Last()))
                            {
                                try
                                {
                                    if (internetconection)
                                    {
                                        var x = ListUsers.IndexOf(ListUsers.Where(a => a.U_Id == IDuser)
                                            .FirstOrDefault());
                                        if (x > -1)
                                        {
                                            ListUsers.Move(x, 0);
                                        }

                                        uplouding.Visibility = Visibility.Visible;
                                        uplouding.IsIndeterminate = true;
                                        NoMessagePanel.Visibility = Visibility.Hidden;
                                        if (!chekFile.Contains(fils))
                                        {
                                            MethodTosendAttchmentMesssage(fils);

                                            var response = await RequestsAsync.InsertNewMessage(
                                                UserDetails.User_id, IDuser, "", time2, copyedfile);

                                            if (response.Item1 == 200)
                                            {
                                                if (response.Item2 is InsertNewMessageObject res)
                                                {
                                                    MethodTosendAttchmentMesssage(copyedfile);
                                                    UpdateMessageID(res);
                                                }
                                            }

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
                                MessageBox.Show("The selected file extenstion is forbidden !", "Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                            }

                            chekFile.Add(fils);
                        }

                        if (uplouding.Visibility == Visibility.Visible)
                        {
                            uplouding.Visibility = Visibility.Collapsed;
                            uplouding.IsIndeterminate = false;
                            NoMessagePanel.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        // sent messeges file
        private void MethodTosendAttchmentMesssage(string FileNameAttachment)
        {
            try
            {
                unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                time2 = unixTimestamp.ToString();
                var time = DateTime.Now.ToString("hh:mm");

                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                {
                    var check_extension = Functions.Check_FileExtension(FileNameAttachment);

                    var FileName_Media = FileNameAttachment.Split('\\').Last();
                    Classes.Messages m = new Classes.Messages();


                    var Play_Visibility = "Collapsed";
                    var Pause_Visibility = "Collapsed";
                    var Progress_Visibility = "Collapsed";
                    var Download_Visibility = "Visible";
                    var Video_text_Visibility = "Visible";
                    var Icon_File_Visibility = "Collapsed";
                    var Hlink_Download_Visibility = "Visible";
                    var Hlink_Open_Visibility = "Collapsed";


                    var Type_Icon_File = "File";
                    if (m.Mes_Type == "right_file")
                    {
                        if (FileNameAttachment.EndsWith("rar") || FileNameAttachment.EndsWith("RAR") ||
                            FileNameAttachment.EndsWith("zip") || FileNameAttachment.EndsWith("ZIP"))
                        {
                            Type_Icon_File = "ZipBox";
                        }
                        else if (FileNameAttachment.EndsWith("txt") || FileNameAttachment.EndsWith("TXT"))
                        {
                            Type_Icon_File = "NoteText";
                        }
                        else if (FileNameAttachment.EndsWith("docx") || FileNameAttachment.EndsWith("DOCX"))
                        {
                            Type_Icon_File = "FileWord";
                        }
                        else if (FileNameAttachment.EndsWith("doc") || FileNameAttachment.EndsWith("DOC"))
                        {
                            Type_Icon_File = "FileWord";
                        }
                        else if (FileNameAttachment.EndsWith("pdf") || FileNameAttachment.EndsWith("PDF"))
                        {
                            Type_Icon_File = "FilePdf";
                        }

                        if (FileNameAttachment.Length > 25)
                        {
                            FileNameAttachment = Functions.SubStringCutOf(FileNameAttachment, 25) + "." +
                                                 FileNameAttachment.Split('.').Last();
                        }
                    }

                    if (check_extension == "Image")
                    {
                        m.Mes_Type = "right_image";
                        m.Color_box_message = ChatColor;
                    }

                    if (check_extension == "Audio")
                    {
                        m.Mes_Position = "right";
                        m.Mes_Type = "right_audio";

                        m.Play_Visibility = "Visible";
                        m.Pause_Visibility = "Collapsed";
                        m.Progress_Visibility = "Collapsed";
                        m.Download_Visibility = "Collapsed";
                    }

                    else if (check_extension == "Video")
                    {
                        m.Mes_Position = "right";
                        m.Mes_Type = "right_video";

                        m.Play_Visibility = "Visible";
                        m.Progress_Visibility = "Collapsed";
                        m.Download_Visibility = "Collapsed";
                    }
                    else if (check_extension == "File")
                    {
                        m.Mes_Type = "right_file";

                        m.Icon_File_Visibility = "Visible";
                        m.Progress_Visibility = "Collapsed";
                        m.Download_Visibility = "Collapsed";
                        m.Hlink_Download_Visibility = "Collapsed";
                        m.Hlink_Open_Visibility = "Visible";
                    }

                    m.Mes_Id = time2;
                    m.Mes_From_Id = IDuser;
                    m.Mes_To_Id = UserDetails.User_id;
                    m.Mes_Media = FileNameAttachment;
                    m.Mes_MediaFileName = FileName_Media;
                    m.Mes_File_size = size_file;
                    m.Mes_Time = time;
                    m.Mes_Time_text = time;
                    m.Mes_Position = "right";
                    //style
                    m.Color_box_message = ChatColor;
                    m.Img_user_message = Img_user_message;
                    m.Progress_Visibility = Progress_Visibility;
                    m.Download_Visibility = Download_Visibility;
                    m.Play_Visibility = Play_Visibility;
                    m.Pause_Visibility = Pause_Visibility;
                    m.Icon_File_Visibility = Icon_File_Visibility;
                    m.Hlink_Download_Visibility = Hlink_Download_Visibility;
                    m.Hlink_Open_Visibility = Hlink_Open_Visibility;
                    m.Type_Icon_File = Type_Icon_File;
                    m.sound_slider_value = 0;
                    m.Progress_Value = 0;
                    m.sound_time = "";

                    var check = ListMessages.FirstOrDefault(a => a.Mes_Id == m.Mes_Id);
                    if (check == null)
                    {
                        ListMessages.Add(m);
                    }

                    ChatMessgaeslistBox.ItemsSource = ListMessages;
                });

                ScrollAndUpdateChatbox();

            }
            catch (Exception)
            {
                ScrollAndUpdateChatbox();
            }
        }

        // sent messeges text
        private async void MethodTosendMesssage(string TextMsg)
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    time2 = unixTimestamp.ToString();
                    var time = DateTime.Now.ToString("hh:mm");

                    var Widthresult = "";
                    var VisibiltyEvent = "";

                    App.Current.Dispatcher.Invoke((Action)async delegate // <--- HERE
                    {
                        var check = ListMessages.FirstOrDefault(a => a.Mes_Id == Messages_id);
                        if (check == null)
                        {
                            ListMessages.Add(new Classes.Messages()
                            {
                                Mes_Id = time2,
                                Mes_From_Id = IDuser,
                                Mes_To_Id = UserDetails.User_id,
                                Mes_Text = TextMsg,
                                Mes_Time = time,
                                Mes_Time_text = time,
                                Mes_Position = "right",
                                Mes_Type = "right_text",
                                Color_box_message = ChatColor,
                            });


                            var response =
                                await RequestsAsync.InsertNewMessage(UserDetails.User_id, IDuser, TextMsg, time2, "");

                            if (response.Item1 == 200)
                            {
                                if (response.Item2 is InsertNewMessageObject result)
                                {
                                    var lastMessage = ListMessages.LastOrDefault();
                                    if(lastMessage!=null)
                                        UpdateMessageID(result); //lastMessage.Mes_Id = result.Messages[0].Id;


                                }

                            }

                        }

                    });

                    ScrollAndUpdateChatbox();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        //########################## LoadMore Message ##########################

        #region LoadMore Message

        //Click Button LoadMore Message
        private void Btn_LoadmoreMessages_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {

                FirstMessageid = ListMessages.FirstOrDefault().Mes_Id;

                //  var s =   SQLiteCommandSender.GetMessages_CredentialsList(ID_From, ID_To, FirstMessageid,ChatColor);
                if (Functions.CheckForInternetConnection() == false)
                {
                    return;
                }

                if (Btn_LoadmoreMessages.Content.ToString() == LocalResources.label_Load_More_Messages)
                {
                    ChatMessgaeslistBox.SelectedIndex = 0;

                    if (LoadMoreMessageWorker.WorkerSupportsCancellation == true)
                    {
                        LoadMoreMessageWorker.CancelAsync();
                    }

                    Load_More_Messages_Async();

                    Btn_LoadmoreMessages.Content = LocalResources.label_Loading;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private async void Load_More_Messages_Async()
        {

            await Task.Run(async () =>
            {
                var response = await RequestsAsync.Message_Update_Http(UserDetails.User_id,
                    IDuser, "0", LastMessageid);

                if (response.Item1 == 200)
                {
                    if (response.Item2 is MessageUpdateObject result)
                    {
                        if (result.Messages.Count == 0)
                        {
                            LoadmoremessgaCount = "0";
                        }
                        else
                        {
                            LoadmoremessgaCount = "1";
                        }

                        foreach (var message in result.Messages)
                        {
                            Classes.Messages m = new Classes.Messages();

                            //Variables
                            var Color_box_message = "";

                            if (message.Type == "right_text" || message.Type == "right_image" ||
                                message.Type == "right_audio" ||
                                message.Type == "right_video" || message.Type == "right_file" ||
                                message.Type == "right_sticker" ||
                                message.Type == "right_contact")
                            {
                                Color_box_message = ChatColor;
                            }

                            if (message.Type == "right_sticker" || message.Type == "left_sticker")
                            {
                                string text_name_sticker = message.Media.Split('_').Last();
                                message.Media = Functions.Get_Sticker_messages(text_name_sticker, message.Media, m);
                            }

                            if (message.Type == "right_video" || message.Type == "left_video")
                            {
                                message.MediaFileName = Functions.Rename_Video(message.MediaFileName);
                            }

                            if (message.Type == "right_gif" || message.Type == "left_gif")
                            {
                                var resultGif = Path.ChangeExtension(message.Stickers, ".gif");
                                string[] stringSeparators = new string[] { "/" };
                                var name = resultGif.Split(stringSeparators, StringSplitOptions.None);
                                var string_url = (name[2] + "/" + name[3]);
                                var string_name = name[4] + ".gif";
                                resultGif = "https://" + string_url.Replace(string_url, "i.giphy.com/") + string_name;
                                message.MediaFileName = Functions.Get_Sticker_messages(string_name, resultGif, m);
                            }

                            if (message.Type == "right_contact" || message.Type == "left_contact")
                            {
                                string[] stringSeparators = new string[] { "&quot;" };
                                var name = message.Text.Split(stringSeparators, StringSplitOptions.None);
                                var string_name = name[3];
                                var string_number = name[7];
                                message.Text = string_name + "\r\n" + string_number;
                            }

                            var Type_Icon_File = "File";
                            if (message.Type == "right_file" || message.Type == "left_file")
                            {
                                if (message.MediaFileName.EndsWith("rar") ||
                                    message.MediaFileName.EndsWith("RAR") ||
                                    message.MediaFileName.EndsWith("zip") || message.MediaFileName.EndsWith("ZIP"))
                                {
                                    Type_Icon_File = "ZipBox";
                                }
                                else if (message.MediaFileName.EndsWith("txt") ||
                                         message.MediaFileName.EndsWith("TXT"))
                                {
                                    Type_Icon_File = "NoteText";
                                }
                                else if (message.MediaFileName.EndsWith("docx") ||
                                         message.MediaFileName.EndsWith("DOCX"))
                                {
                                    Type_Icon_File = "FileWord";
                                }
                                else if (message.MediaFileName.EndsWith("doc") ||
                                         message.MediaFileName.EndsWith("DOC"))
                                {
                                    Type_Icon_File = "FileWord";
                                }
                                else if (message.MediaFileName.EndsWith("pdf") ||
                                         message.MediaFileName.EndsWith("PDF"))
                                {
                                    Type_Icon_File = "FilePdf";
                                }

                                if (message.MediaFileName.Length > 25)
                                {
                                    message.MediaFileName =
                                        Functions.SubStringCutOf(message.MediaFileName, 25) + "." +
                                        message.MediaFileName.Split('.').Last();
                                }
                            }

                            var check_extension = Functions.Check_FileExtension(message.MediaFileName);
                            var Play_Visibility = "Collapsed";
                            var Pause_Visibility = "Collapsed";
                            var Progress_Visibility = "Collapsed";
                            var Download_Visibility = "Visible";
                            var Video_text_Visibility = "Visible";
                            var Icon_File_Visibility = "Collapsed";
                            var Hlink_Download_Visibility = "Visible";
                            var Hlink_Open_Visibility = "Collapsed";

                            if (check_extension == "Audio")
                            {
                                var checkforSound = Functions.Get_sound(IDuser, message.MediaFileName);
                                if (checkforSound != "Not Found sound")
                                {
                                    Play_Visibility = "Visible";
                                    Pause_Visibility = "Collapsed";
                                    Progress_Visibility = "Collapsed";
                                    Download_Visibility = "Collapsed";
                                }
                            }

                            else if (check_extension == "Video")
                            {
                                var checkforSound = Functions.Get_Video(IDuser, message.MediaFileName);
                                if (checkforSound != "Not Found vidoe")
                                {
                                    Play_Visibility = "Visible";
                                    Progress_Visibility = "Collapsed";
                                    Download_Visibility = "Collapsed";
                                }
                            }
                            else if (check_extension == "File")
                            {
                                var checkforSound = Functions.Get_file(IDuser, message.MediaFileName);
                                if (checkforSound != "Not Found file")
                                {
                                    Icon_File_Visibility = "Visible";
                                    Progress_Visibility = "Collapsed";
                                    Download_Visibility = "Collapsed";
                                    Hlink_Download_Visibility = "Collapsed";
                                    Hlink_Open_Visibility = "Visible";
                                }
                            }

                            m.Mes_Id = message.Id;
                            m.Mes_From_Id = message.FromId;
                            m.Mes_To_Id = message.ToId;
                            m.Mes_Text = message.Text;
                            m.Mes_Media = message.Media;
                            m.Mes_MediaFileName = message.MediaFileName;
                            m.Mes_MediaFileNames = message.MediaFileNames;
                            m.Mes_Time = message.Time;
                            m.Mes_Seen = message.Seen;
                            m.Mes_Deleted_one = message.DeletedOne;
                            m.Mes_Deleted_two = message.DeletedTwo;
                            m.Mes_Sent_push = message.SentPush;
                            m.Mes_Notification_id = message.NotificationId;
                            m.Mes_Type_two = message.TypeTwo;
                            m.Mes_Time_text = message.TimeText;
                            m.Mes_Position = message.Position;
                            m.Mes_Type = message.Type;
                            m.Mes_File_size = message.FileSize;
                            m.Mes_User_avatar = message.MessageUser.Avatar;
                            m.Mes_Stickers = message.Stickers;
                            //style
                            m.Color_box_message = Color_box_message;
                            m.Img_user_message = Img_user_message;
                            m.Progress_Visibility = Progress_Visibility;
                            m.Download_Visibility = Download_Visibility;
                            m.Play_Visibility = Play_Visibility;
                            m.Pause_Visibility = Pause_Visibility;
                            m.Icon_File_Visibility = Icon_File_Visibility;
                            m.Hlink_Download_Visibility = Hlink_Download_Visibility;
                            m.Hlink_Open_Visibility = Hlink_Open_Visibility;
                            m.Type_Icon_File = Type_Icon_File;
                            m.sound_slider_value = 0;
                            m.Progress_Value = 0;
                            m.sound_time = "";

                            if (check_extension == "Image")
                            {
                                message.MediaFileName =
                                    Functions.Get_img_messages(IDuser, message.MediaFileName, message.Media, m);
                            }

                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                ListMessages.Insert(0, m);
                            });
                        }
                    }
                }
            });

            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                if (LoadMoreMessageWorker.WorkerSupportsCancellation == true)
                {
                    LoadMoreMessageWorker.CancelAsync();
                }

                if (LoadmoremessgaCount == "0")
                {
                    Btn_LoadmoreMessages.Content = LocalResources.label_No_More_Messages;
                }
                else
                {
                    Btn_LoadmoreMessages.Content = LocalResources.label_Load_More_Messages;
                    ChatMessgaeslistBox.ItemsSource = ListMessages;
                    ChatMessgaeslistBox.Items.Refresh();
                }
            }));

        }


        //########################## Users Contact ##########################

        #region Call_Video

        private void SendVideoCallButton_OnClick(object sender, RoutedEventArgs e)
        {
            Send_Video_Call SendCall = new Send_Video_Call(ProfileUserName.Content.ToString(), IDuser,
                AvatarofUser.Source, this);
            SendCall.ShowDialog();
        }

        public async void get_data_Call()
        {
            try
            {
                await Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        var call = SQLiteCommandSender.get_data_CallVideo();
                        if (call.Count == 0)
                        {
                            No_call.Visibility = Visibility.Visible;
                            Grid_No_call.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            ListCall = SQLiteCommandSender.get_data_CallVideo();
                            Calls_list.ItemsSource = ListCall;
                            No_call.Visibility = Visibility.Collapsed;
                            Grid_No_call.Visibility = Visibility.Collapsed;
                        }
                    });
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_delete_call_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mi = (Button)sender;
                string Call_id = mi.CommandParameter.ToString();

                SQLiteCommandSender.remove_data_CallVideo(Call_id);

                var delete_calls = ListCall.FirstOrDefault(a => a.Call_Video_Call_id == Call_id);
                if (delete_calls != null)
                {
                    ListCall.Remove(delete_calls);
                    Calls_list.ItemsSource = ListCall;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Selection Changed null is SelectedItem Calls_list
        private void Calls_list_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Calls_list.SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Mouse Move null is SelectedItem Calls_list
        private void Calls_list_OnMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Calls_list.SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Users Contact ##########################

        #region UsersContact

        // Functions UsersContact
        public void UsersContact(List<GetUserContactsObject.User> users)
        {
            try
            {
                if (internetconection)
                {
                    if (users != null)
                    {
                        foreach (var user in users)
                        {

                            //Variables
                            string AvatarSplit = user.ProfilePicture.Split('/').Last();
                            String Name_App_Main_Leter = Settings.Application_Name.Substring(0, 1);
                            var Is_user_platform = "Web";
                            var null_UserPlatform = "Visible";
                            var Color_onof = "#C0C0C0"; //silver

                            if (user.UserPlatform == "web")
                            {
                                Is_user_platform = "Web";
                            }
                            else if (user.UserPlatform == "phone")
                            {
                                Is_user_platform = "CellphoneAndroid";
                            }
                            else if (user.UserPlatform == "null")
                            {
                                null_UserPlatform = "Collapsed";
                            }

                            var check = ListUsersContact.FirstOrDefault(a => a.UC_Id == user.UserId);
                            if (check != null)
                            {

                                if (user.Lastseen == "on")
                                {
                                    Color_onof = "#00cc00"; //green
                                    Name_App_Main_Leter = "";
                                    user.LastseenTimeText = "Online";

                                    var x = ListUsersContact.IndexOf(check);
                                    if (x >= -1)
                                    {
                                        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                                            new Action(() => { ListUsersContact.Move(x, 0); }));
                                    }
                                }

                                if (check.UC_username != user.Username)
                                {
                                    check.UC_username = user.Username;
                                }

                                if (check.UC_name !=
                                    Functions.HtmlDecodestring(Functions.SubStringCutOf(user.Name, 15)))
                                {
                                    check.UC_name = Functions.HtmlDecodestring(Functions.SubStringCutOf(user.Name, 15));
                                }

                                if (check.UC_profile_picture !=
                                    Functions.Get_image(user.UserId, AvatarSplit, user.ProfilePicture))
                                {
                                    check.UC_profile_picture =
                                        Functions.Get_image(user.UserId, AvatarSplit, user.ProfilePicture);
                                }

                                if (check.UC_cover_picture != user.CoverPicture)
                                {
                                    check.UC_cover_picture = user.CoverPicture;
                                }

                                if (check.UC_verified != user.Verified)
                                {
                                    check.UC_verified = user.Verified;
                                }

                                if (check.UC_lastseen != user.Lastseen)
                                {
                                    check.UC_lastseen = user.Lastseen;
                                }

                                if (check.UC_lastseen_time_text != user.LastseenTimeText)
                                {
                                    check.UC_lastseen_time_text = user.LastseenTimeText;
                                }

                                if (check.UC_lastseen_unix_time != user.LastseenUnixTime)
                                {
                                    check.UC_lastseen_unix_time = user.LastseenUnixTime;
                                }

                                if (check.UC_url != user.Url)
                                {
                                    check.UC_url = user.Url;
                                }

                                if (check.UC_user_platform != Is_user_platform)
                                {
                                    check.UC_user_platform = Is_user_platform;
                                }

                                if (check.UC_null_UserPlatform != null_UserPlatform)
                                {
                                    check.UC_null_UserPlatform = null_UserPlatform;
                                }

                                if (check.UC_Color_onof != Color_onof)
                                {
                                    check.UC_Color_onof = Color_onof;
                                }

                                if (check.UC_App_Main_Later != Name_App_Main_Leter)
                                {
                                    check.UC_App_Main_Later = Name_App_Main_Leter;
                                }
                            }
                            else
                            {
                                if (user.Lastseen == "on")
                                {
                                    Color_onof = "#00cc00"; //green
                                    Name_App_Main_Leter = "";
                                    user.LastseenUnixTime = "Online";

                                    var x = ListUsersContact.IndexOf(check);
                                    if (x > -1)
                                    {
                                        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                                        {
                                            if (ListUsersContact.Count > 0)
                                                ListUsersContact.Move(x, 0);
                                        }));
                                    }
                                }

                                UsersContact uc = new UsersContact();
                                uc.UC_Id = user.UserId;
                                uc.UC_username = user.Username;
                                uc.UC_name = Functions.HtmlDecodestring(Functions.SubStringCutOf(user.Name, 15));
                                uc.UC_profile_picture =
                                    Functions.Get_image(user.UserId, AvatarSplit, user.ProfilePicture);
                                uc.UC_cover_picture = user.CoverPicture;
                                uc.UC_verified = user.Verified;
                                uc.UC_lastseen = user.Lastseen;
                                uc.UC_lastseen_time_text = user.LastseenUnixTime;
                                uc.UC_lastseen_unix_time = user.LastseenUnixTime;
                                uc.UC_url = user.Url;
                                uc.UC_user_platform = Is_user_platform;
                                uc.UC_null_UserPlatform = null_UserPlatform;
                                uc.UC_Color_onof = Color_onof;
                                uc.UC_App_Main_Later = Name_App_Main_Leter;
                                uc.UC_chat_color = Settings.Main_Color;

                                //user_profile
                                uc.UC_email = user.UserProfile.Email;
                                uc.UC_first_name = user.UserProfile.FirstName;
                                uc.UC_last_name = user.UserProfile.LastName;
                                uc.UC_relationship_id = user.UserProfile.RelationshipId;
                                uc.UC_address = user.UserProfile.Address;
                                uc.UC_working = user.UserProfile.Working;
                                uc.UC_working_link = user.UserProfile.WorkingLink;
                                uc.UC_about = user.UserProfile.About;
                                uc.UC_school = user.UserProfile.School;
                                uc.UC_gender = user.UserProfile.Gender;
                                uc.UC_birthday = user.UserProfile.Birthday;
                                uc.UC_website = user.UserProfile.Website;
                                uc.UC_facebook = user.UserProfile.Facebook;
                                uc.UC_google = user.UserProfile.Google;
                                uc.UC_twitter = user.UserProfile.Twitter;
                                uc.UC_linkedin = user.UserProfile.Linkedin;
                                uc.UC_youtube = user.UserProfile.Youtube;
                                uc.UC_vk = user.UserProfile.Vk;
                                uc.UC_instagram = user.UserProfile.Instagram;
                                uc.UC_language = user.UserProfile.Language;
                                uc.UC_ip_address = user.UserProfile.IpAddress;
                                uc.UC_follow_privacy = user.UserProfile.FollowPrivacy;
                                uc.UC_post_privacy = user.UserProfile.PostPrivacy;
                                uc.UC_message_privacy = user.UserProfile.MessagePrivacy;
                                uc.UC_confirm_followers = user.UserProfile.ConfirmFollowers;
                                uc.UC_show_activities_privacy = user.UserProfile.ShowActivitiesPrivacy;
                                uc.UC_birth_privacy = user.UserProfile.BirthPrivacy;
                                uc.UC_visit_privacy = user.UserProfile.VisitPrivacy;
                                uc.UC_showlastseen = user.UserProfile.Showlastseen;
                                uc.UC_status = user.UserProfile.Status;
                                uc.UC_active = user.UserProfile.Active;
                                uc.UC_admin = user.UserProfile.Admin;
                                uc.UC_registered = user.UserProfile.Registered;
                                uc.UC_phone_number = user.UserProfile.PhoneNumber;
                                uc.UC_is_pro = user.UserProfile.IsPro;
                                uc.UC_pro_type = user.UserProfile.ProType;
                                uc.UC_joined = user.UserProfile.Joined;
                                uc.UC_timezone = user.UserProfile.Timezone;
                                uc.UC_referrer = user.UserProfile.Referrer;
                                uc.UC_balance = user.UserProfile.Balance;
                                uc.UC_paypal_email = user.UserProfile.PaypalEmail;
                                uc.UC_notifications_sound = user.UserProfile.NotificationsSound;
                                uc.UC_order_posts_by = user.UserProfile.OrderPostsBy;
                                uc.UC_social_login = user.UserProfile.SocialLogin;
                                uc.UC_device_id = user.UserProfile.DeviceId;

                                if (ModeDarkstlye)
                                {
                                    uc.S_Color_Background = "#232323";
                                    uc.S_Color_Foreground = "#efefef";
                                }
                                else
                                {
                                    uc.S_Color_Background = "#ffff";
                                    uc.S_Color_Foreground = "#444";
                                }

                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                {
                                    ListUsersContact.Add(uc);
                                });
                            }
                        }

                        // Insert data user in database
                        SQLiteCommandSender.Insert_Or_Replace_UsersContactTable(ListUsersContact);

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        // Run background worker : Users Contact

        private async void User_Contacts_Async()
        {
            IProgress<int> progress = new Progress<int>(percentCompleted =>
            {
                ProgressBar_UserContacts.Value = percentCompleted;
            });


            await Task.Run(async () =>
            {
                try
                {
                    if (internetconection)
                    {

                        var response = await RequestsAsync.User_Contact_Http(UserDetails.User_id);

                        if (response.Item1 == 200)
                        {
                            if (response.Item2 is GetUserContactsObject contacts)
                            {
                                UsersContact(contacts.Users);
                            }

                        }

                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });

            this.Dispatcher.Invoke(() =>
            {
                if (ListUsersContact.Count == 0 && state_Check == "1")
                {
                    No_No_Users_Contact.Visibility = Visibility.Visible;
                    No_Users_ContactGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    if (state_Check == "1")
                    {
                        No_No_Users_Contact.Visibility = Visibility.Collapsed;
                        No_Users_ContactGrid.Visibility = Visibility.Collapsed;
                    }
                }

                UserContacts_list.ItemsSource = ListUsersContact;
            });




        }


        //Event Selection Changed in Users Contact - and Run background worker : Messages , friends Users
        private void UserContacts_list_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UsersContact selectedGroup = (UsersContact)UserContacts_list.SelectedItem;

                if (selectedGroup != null)
                {
                    //Setting up visualisation
                    ChatColor = selectedGroup.UC_chat_color;
                    Img_user_message = selectedGroup.UC_profile_picture;
                    ChatTitleChange.Text = selectedGroup.UC_name;
                    ChatSeen.Text = selectedGroup.UC_lastseen_time_text;

                    Btn_LoadmoreMessages.Content = LocalResources.label_Load_More_Messages;
                    var ChatPanelColor = (Color)ColorConverter.ConvertFromString(Settings.Main_Color);
                    var ChatForegroundColor = (Color)ColorConverter.ConvertFromString("#ffff");

                    if (Settings.Change_ChatPanelColor)
                    {
                        ChatInfoPanel.Background = new SolidColorBrush(ChatPanelColor);
                    }

                    ProfileToggle.Background = new SolidColorBrush(ChatPanelColor);
                    ProfileToggle.Foreground = new SolidColorBrush(ChatForegroundColor);

                    DropDownMenueOnMessageBox.Foreground = new SolidColorBrush(ChatForegroundColor);
                    ChatTitleChange.Foreground = new SolidColorBrush(ChatForegroundColor);
                    ChatSeen.Foreground = new SolidColorBrush(ChatForegroundColor);

                    //Profile 
                    ProfileUserName.Content = selectedGroup.UC_name;
                    ProfileLastSeen.Content = selectedGroup.UC_lastseen_time_text;
                    UserPofilURl = selectedGroup.UC_url;
                    AvatarofUser.Source = new BitmapImage(new Uri(selectedGroup.UC_profile_picture));

                    ProfileUserAbout.Content = string.IsNullOrEmpty(selectedGroup.UC_about)
                        ? (object)(LocalResources.label_About + "(" + Settings.Application_Name + ")")
                        : selectedGroup.UC_about;

                    ////////////////////////////

                    _SelectedUsersContact = selectedGroup;
                    _SelectedType = "UsersContact";

                    //Media
                    Get_SharedFiles(selectedGroup.UC_Id);

                    if (RightMainPanel.Visibility == Visibility.Collapsed)
                    {
                        RightMainPanel.Visibility = Visibility.Visible;
                        ProfileToggle.Visibility = Visibility.Visible;
                        DropDownMenueOnMessageBox.Visibility = Visibility.Collapsed;
                    }

                    ChatTitleChange.Visibility = Visibility.Visible;
                    ChatSeen.Visibility = Visibility.Visible;

                    if (Settings.VideoCall)
                        VideoButton.Visibility = Visibility.Visible;

                    SendMessagepanel.Visibility = Visibility.Visible;


                    //========== View data in Chat Messgaes listBox ===========
                    //Hide NoMessagePanel when it is clicking
                    NoMessagePanel.Visibility = Visibility.Collapsed;

                    ID_To = UserDetails.User_id;
                    ID_From = IDuser = selectedGroup.UC_Id;

                    if (mediaPlayer.CanPause)
                    {
                        mediaPlayer.Pause();
                        timer.Stop();
                    }

                    Task.Run(() =>
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            try
                            {
                                ListMessages.Clear();
                                before_message_id = "0";
                                var x = SQLiteCommandSender.GetMessages_CredentialsList(ID_From,
                                    ID_To, before_message_id, ChatColor);
                                if (x == "1")
                                {
                                    ChatMessgaeslistBox.ItemsSource = ListMessages;

                                    //Scroll Down >> 
                                    ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                                    ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);
                                }
                                else
                                {
                                    if (internetconection)
                                    {
                                        Message_ProgressBar.Visibility = Visibility.Visible;
                                        Message_ProgressBar.IsIndeterminate = true;

                                        Messages_Async();
                                    }
                                }

                                timer_messages.Stop();
                                timer_messages.Start();
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        });
                    });

                    //========== View data in images friends User listBox ===========

                    //friends
                    //ListFriends.Clear();

                    //ProgressBar_Friends.Visibility = Visibility.Visible;
                    //ProgressBar_Friends.IsIndeterminate = true;

                    //bgd_Worker_friends = new BackgroundWorker();
                    //bgd_Worker_friends.DoWork += bgd_Worker_friends_DoWork;
                    //bgd_Worker_friends.ProgressChanged += bgd_Worker_friends_ProgressChanged;
                    //bgd_Worker_friends.RunWorkerCompleted += bgd_Worker_friends_RunWorkerCompleted;
                    //bgd_Worker_friends.WorkerSupportsCancellation = true;
                    //bgd_Worker_friends.WorkerReportsProgress = true;
                    //bgd_Worker_friends.RunWorkerAsync();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Search Users Contact Where username
        private void Txt_Search_BoxContacts_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                UserContacts_list.ItemsSource = ListUsersContact.Where(a =>
                        a.UC_name.IndexOf(Txt_Search_BoxContacts.Text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .ToList();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void Search(string Txt_Search)
        {
            try
            {
                var s = ListUsersContact
                    .Where(a => a.UC_name.IndexOf(Txt_Search, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // click Button Refresh Users Contact
        private void Btn_Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //UsersContact
                ListUsersContact.Clear();
                ProgressBar_UserContacts.Visibility = Visibility.Visible;
                ProgressBar_UserContacts.IsIndeterminate = true;

                User_Contacts_Async();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## User Data my Profile ##########################

        #region User Data MyProfile

        //view data My Profile
        public void GetDataMyProfile()
        {
            try
            {
                var data = MemoryVariables.UsersProfileList.FirstOrDefault(a => a.pm_UserId == UserDetails.User_id);

                if (data != null)
                {
                    AvatarImageofUser.Source = new BitmapImage(new Uri(data.pm_Avatar));

                    if (data.pm_First_name == "" && data.pm_Last_name == "")
                    {
                        UsernameLogin.Content = data.pm_Username;
                    }
                    else
                    {
                        UsernameLogin.Content = data.pm_First_name + " " + data.pm_Last_name;
                    }

                    if (data.pm_Username == "")
                        Lbl_Username.Text = LocalResources.label_Empty;
                    else
                        Lbl_Username.Text = data.pm_Username;

                    if (data.pm_Email == "")
                        Lbl_Email.Text = LocalResources.label_Empty;
                    else
                        Lbl_Email.Text = data.pm_Email;

                    if (data.pm_Birthday == "")
                        Lbl_Birthday.Text = LocalResources.label_Empty;
                    else
                        Lbl_Birthday.Text = data.pm_Birthday;

                    if (data.pm_Phone_number == "")
                        Lbl_Phone_number.Text = LocalResources.label_Empty;
                    else
                        Lbl_Phone_number.Text = data.pm_Phone_number;

                    if (data.pm_Website == "")
                        Lbl_Website.Text = LocalResources.label_Empty;
                    else
                        Lbl_Website.Text = data.pm_Website;

                    if (data.pm_Address == "")
                        Lbl_Address.Text = LocalResources.label_Empty;
                    else
                        Lbl_Address.Text = data.pm_Address;

                    if (data.pm_School == "")
                        Lbl_School.Text = LocalResources.label_Empty;
                    else
                        Lbl_School.Text = data.pm_School;

                    if (UseLayoutRounding)
                        Lbl_Gender.Text = LocalResources.label_Empty;
                    else
                        Lbl_Gender.Text = data.pm_Gender;

                    if (data.pm_Facebook == "")
                        Lbl_Facebook.Text = LocalResources.label_Empty;
                    else
                        Lbl_Facebook.Text = data.pm_Facebook;

                    if (data.pm_Google == "")
                        Lbl_Google.Text = LocalResources.label_Empty;
                    else
                        Lbl_Google.Text = data.pm_Google;

                    if (data.pm_Twitter == "")
                        Lbl_Twitter.Text = LocalResources.label_Empty;
                    else
                        Lbl_Twitter.Text = data.pm_Twitter;

                    if (data.pm_Youtube == "")
                        Lbl_Youtube.Text = LocalResources.label_Empty;
                    else
                        Lbl_Youtube.Text = data.pm_Youtube;

                    if (data.pm_Linkedin == "")
                        Lbl_Linkedin.Text = LocalResources.label_Empty;
                    else
                        Lbl_Linkedin.Text = data.pm_Linkedin;

                    if (data.pm_Instagram == "")
                        Lbl_Instagram.Text = LocalResources.label_Empty;
                    else
                        Lbl_Instagram.Text = data.pm_Instagram;

                    if (data.pm_Vk == "")
                        Lbl_Vk.Text = LocalResources.label_Empty;
                    else
                        Lbl_Vk.Text = data.pm_Vk;
                }

                if (Functions.CheckForInternetConnection())
                {
                    LastseenUser.Content = LocalResources.label_Online;
                }
                else
                {
                    LastseenUser.Content = LocalResources.label_offline;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Click Button Open Window : Edit My Profile
        private void Btn_EditProfile_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Edit_MyProfile_Window Form = new Edit_MyProfile_Window(this);
                Form.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private async void Btn_EditProfileImage_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create OpenFileDialog 
                OpenFileDialog open = new OpenFileDialog();

                // Set filter for file extension and default file extension 
                open.DefaultExt = ".png";
                open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";

                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = open.ShowDialog();

                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    if (internetconection)
                    {
                        // Open document and image file path
                        string filename = open.FileName;

                        // display image in picture box  
                        AvatarImageofUser.Source = new BitmapImage(new Uri(filename));
                        try
                        {
                            System.Drawing.Image image = System.Drawing.Image.FromFile(filename);

                            var ms = new MemoryStream();
                            image.Save(ms, ImageFormat.Jpeg);
                            var bytes = ms.ToArray();
                            var imageMemoryStream = new MemoryStream(bytes);
                            System.Drawing.Image imgfromStream = System.Drawing.Image.FromStream(imageMemoryStream);

                            var imgStream = new MemoryStream();
                            string MimeTipe = Classes.MimeType.GetMimeType(bytes, filename);
                            using (System.Drawing.Image imagess = System.Drawing.Image.FromFile(filename))
                            {
                                imagess.Save(imgStream, ImageFormat.Jpeg);
                            }

                            imgStream.Seek(0, SeekOrigin.Begin); // it does not work without this 

                            var response = await RequestsAsync.Set_profile_picture_Http(UserDetails.User_id, filename,
                                MimeTipe, imgStream);

                            if (response.Item1 == 200)
                            {
                                var DataRow = SQLite_Entity.Connection.Table<DataBase.ProfilesTable>()
                                    .FirstOrDefault(a => a.pm_UserId == UserDetails.User_id);

                                string ImageFile = response.Item2["avatar"].ToString();
                                string AvatarSplit = open.FileName.Split('\\').Last();
                                if (DataRow != null)
                                {
                                    DataRow.pm_Avatar = ImageFile;
                                    SQLiteCommandSender.Insert_Or_Replace_To_ProfileTable(DataRow);

                                    Functions.Get_image(UserDetails.User_id, AvatarSplit, DataRow.pm_Avatar);
                                }
                            }

                        }
                        catch (Exception es)
                        {
                            es.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show(LocalResources.label_Please_check_your_internet);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //view data My Profile from DataBase.ProfilesTable
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
                        AvatarImageofUser.Source = new BitmapImage(new Uri(data.pm_Avatar));

                        if (data.pm_First_name == "" && data.pm_Last_name == "")
                        {
                            UsernameLogin.Content = data.pm_Username;
                        }
                        else
                        {
                            UsernameLogin.Content = data.pm_First_name + " " + data.pm_Last_name;
                        }

                        if (data.pm_Username == "")
                            Lbl_Username.Text = LocalResources.label_Empty;
                        else
                            Lbl_Username.Text = data.pm_Username;

                        if (data.pm_Email == "")
                            Lbl_Email.Text = LocalResources.label_Empty;
                        else
                            Lbl_Email.Text = data.pm_Email;

                        if (data.pm_Birthday == "")
                            Lbl_Birthday.Text = LocalResources.label_Empty;
                        else
                            Lbl_Birthday.Text = data.pm_Birthday;

                        if (data.pm_Phone_number == "")
                            Lbl_Phone_number.Text = LocalResources.label_Empty;
                        else
                            Lbl_Phone_number.Text = data.pm_Phone_number;

                        if (data.pm_Website == "")
                            Lbl_Website.Text = LocalResources.label_Empty;
                        else
                            Lbl_Website.Text = data.pm_Website;

                        if (data.pm_Address == "")
                            Lbl_Address.Text = LocalResources.label_Empty;
                        else
                            Lbl_Address.Text = data.pm_Address;

                        if (data.pm_School == "")
                            Lbl_School.Text = LocalResources.label_Empty;
                        else
                            Lbl_School.Text = data.pm_School;

                        if (UseLayoutRounding)
                            Lbl_Gender.Text = LocalResources.label_Empty;
                        else
                            Lbl_Gender.Text = data.pm_Gender;

                        if (data.pm_Facebook == "")
                            Lbl_Facebook.Text = LocalResources.label_Empty;
                        else
                            Lbl_Facebook.Text = data.pm_Facebook;

                        if (data.pm_Google == "")
                            Lbl_Google.Text = LocalResources.label_Empty;
                        else
                            Lbl_Google.Text = data.pm_Google;

                        if (data.pm_Twitter == "")
                            Lbl_Twitter.Text = LocalResources.label_Empty;
                        else
                            Lbl_Twitter.Text = data.pm_Twitter;

                        if (data.pm_Youtube == "")
                            Lbl_Youtube.Text = LocalResources.label_Empty;
                        else
                            Lbl_Youtube.Text = data.pm_Youtube;

                        if (data.pm_Linkedin == "")
                            Lbl_Linkedin.Text = LocalResources.label_Empty;
                        else
                            Lbl_Linkedin.Text = data.pm_Linkedin;

                        if (data.pm_Instagram == "")
                            Lbl_Instagram.Text = LocalResources.label_Empty;
                        else
                            Lbl_Instagram.Text = data.pm_Instagram;

                        if (data.pm_Vk == "")
                            Lbl_Vk.Text = LocalResources.label_Empty;
                        else
                            Lbl_Vk.Text = data.pm_Vk;
                    }
                }

                if (Functions.CheckForInternetConnection())
                {
                    LastseenUser.Content = LocalResources.label_Online;
                }
                else
                {
                    LastseenUser.Content = LocalResources.label_offline;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        //########################## Users Data Profile ##########################

        #region Users Data Profile

        /// <summary>
        /// The event Selectionchanged in Chat Activity User in tab top #region Chat Actitvity User
        /// When the conversation is selected, the data moves to the profile
        /// </summary>

        //Get data Profile user in Right Side "UserName, LastSeen, About, Avatar"
        public async void User_RightSide_ProfileLoader(UsersContactProfile ucp)
        {
            try
            {
                await Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        //profile 
                        ProfileUserName.Content = ucp.UCP_name;
                        ProfileLastSeen.Content = ucp.UCP_lastseen_time_text;
                        UserPofilURl = ucp.UCP_url;

                        if (ucp.UCP_about == "")
                            ProfileUserAbout.Content =
                                LocalResources.label_ProfileUserAbout + "(" + Settings.Application_Name + ")";
                        else
                            ProfileUserAbout.Content = ucp.UCP_about;

                        if (ucp.UCP_address == "")
                            Lbl_location.Content = LocalResources.label_location_not_available;
                        else
                            Lbl_location.Content = ucp.UCP_address;

                        try
                        {

                            AvatarofUser.Source = new BitmapImage(new Uri(ucp.UCP_profile_picture));

                            //MessageBox.Show("Width: " + img.Width + ", Height: " + img.Height);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            AvatarofUser.Source = new BitmapImage(new Uri(Settings.avatar_Img));
                        }
                    });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Get Shared Files Profile user in Right Side "images, Media, file"
        public async void Get_SharedFiles(string userID)
        {
            try
            {
                await Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {

                        if (ListSharedFiles.Count() > 0)
                        {
                            ListSharedFiles.Clear();
                        }

                        var ImagePath = Functions.Files_Destination + userID + "\\images";
                        var SoundsPath = Functions.Files_Destination + userID + "\\sounds";
                        var VideoPath = Functions.Files_Destination + userID + "\\video";
                        var OutherPath = Functions.Files_Destination + userID + "\\file";

                        //Check for folder if exists
                        if (Directory.Exists(ImagePath) == false)
                            Directory.CreateDirectory(ImagePath);

                        if (Directory.Exists(SoundsPath) == false)
                            Directory.CreateDirectory(SoundsPath);

                        if (Directory.Exists(VideoPath) == false)
                            Directory.CreateDirectory(VideoPath);

                        if (Directory.Exists(OutherPath) == false)
                            Directory.CreateDirectory(OutherPath);

                        var ImageFiles = new DirectoryInfo(ImagePath + "\\").GetFiles()
                            .OrderByDescending(f => f.LastWriteTime);
                        var SoundsFiles = new DirectoryInfo(SoundsPath + "\\").GetFiles()
                            .OrderByDescending(f => f.LastWriteTime);
                        var VideoFiles = new DirectoryInfo(VideoPath + "\\").GetFiles()
                            .OrderByDescending(f => f.LastWriteTime);
                        var OutherFiles = new DirectoryInfo(OutherPath + "\\").GetFiles()
                            .OrderByDescending(f => f.LastWriteTime);


                        if (ImageFiles.Count() > 0)
                        {
                            foreach (var dir in ImageFiles)
                            {
                                Classes.SharedFile File = new Classes.SharedFile();
                                File.File_Type = "Image";
                                File.File_Name = dir.Name;
                                File.File_Date = dir.LastWriteTime.Millisecond.ToString();
                                File.FilePath = dir.FullName;
                                File.ImageURL = dir.FullName;
                                File.FileExtension = dir.Extension;

                                File.EmptyLabelVisibility = "Collapsed";
                                File.ImageFrameVisibility = "Visible";
                                File.VoiceFrameVisibility = "Collapsed";
                                File.VideoFrameVisibility = "Collapsed";
                                File.FileFrameVisibility = "Collapsed";
                                ListSharedFiles.Add(File);
                            }
                        }

                        if (SoundsFiles.Count() > 0)
                        {
                            foreach (var dir in SoundsFiles)
                            {
                                Classes.SharedFile File = new Classes.SharedFile();
                                File.File_Type = "Media";
                                File.File_Name = dir.Name;
                                File.FilePath = dir.FullName;
                                File.FileExtension = dir.Extension;
                                File.File_Date = dir.LastWriteTime.Millisecond.ToString();
                                File.EmptyLabelVisibility = "Collapsed";
                                File.ImageFrameVisibility = "Collapsed";
                                File.VoiceFrameVisibility = "Visible";
                                File.VideoFrameVisibility = "Collapsed";
                                File.FileFrameVisibility = "Collapsed";
                                ListSharedFiles.Add(File);
                            }
                        }

                        if (VideoFiles.Count() > 0)
                        {
                            foreach (var dir in VideoFiles)
                            {
                                Classes.SharedFile File = new Classes.SharedFile();
                                File.File_Type = "Media";
                                File.File_Name = dir.Name;
                                File.FilePath = dir.FullName;
                                File.FileExtension = dir.Extension;
                                File.File_Date = dir.LastWriteTime.Millisecond.ToString();
                                File.EmptyLabelVisibility = "Collapsed";
                                File.ImageFrameVisibility = "Collapsed";
                                File.VoiceFrameVisibility = "Collapsed";
                                File.VideoFrameVisibility = "Visible";
                                File.FileFrameVisibility = "Collapsed";
                                ListSharedFiles.Add(File);
                            }
                        }

                        if (OutherFiles.Count() > 0)
                        {
                            foreach (var dir in OutherFiles)
                            {
                                Classes.SharedFile File = new Classes.SharedFile();
                                File.File_Type = "File";
                                File.File_Name = dir.Name;
                                File.FilePath = dir.FullName;
                                File.FileExtension = dir.Extension;
                                File.File_Date = dir.LastWriteTime.Millisecond.ToString();
                                File.EmptyLabelVisibility = "Collapsed";
                                File.ImageFrameVisibility = "Collapsed";
                                File.VoiceFrameVisibility = "Collapsed";
                                File.VideoFrameVisibility = "Collapsed";
                                File.FileFrameVisibility = "Visible";

                                ListSharedFiles.Add(File);
                            }
                        }

                        if (ListSharedFiles.Count > 0)
                        {
                            var OrderByDateList = ListSharedFiles.OrderBy(T => T.File_Date);

                            var ChooseJustTreeItems = new ObservableCollection<Classes.SharedFile>();
                            int CountItem = 0;
                            foreach (var Item in OrderByDateList)
                            {
                                if (CountItem > 2)
                                {
                                    break;
                                }
                                else
                                {
                                    ChooseJustTreeItems.Add(Item);
                                    CountItem++;
                                }
                            }

                            if (SharedFilesButton.Visibility != Visibility.Visible)
                            {
                                SharedFilesButton.Visibility = Visibility.Visible;
                            }

                            FilesListview.ItemsSource = ChooseJustTreeItems;
                        }
                        else
                        {
                            Classes.SharedFile File = new Classes.SharedFile();

                            File.EmptyLabelVisibility = "Visible";
                            File.ImageFrameVisibility = "Collapsed";
                            File.VoiceFrameVisibility = "Collapsed";
                            File.VideoFrameVisibility = "Collapsed";
                            File.FileFrameVisibility = "Collapsed";

                            ListSharedFiles.Add(File);
                            if (SharedFilesButton.Visibility != Visibility.Collapsed)
                            {
                                SharedFilesButton.Visibility = Visibility.Collapsed;
                            }

                            FilesListview.ItemsSource = ListSharedFiles;
                        }

                    });
                });
            }
            catch
            {

            }
        }

        //Open Shared Files from deck PC
        private void FilesListview_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.SharedFile selectedGroup = (Classes.SharedFile)FilesListview.SelectedItem;

                if (selectedGroup != null)
                {

                    if (selectedGroup.File_Type == "Image")
                    {
                        Process.Start(selectedGroup.FilePath);
                    }
                    else if (selectedGroup.File_Type == "File")
                    {
                        Process.Start(selectedGroup.FilePath);
                    }
                    else if (selectedGroup.File_Type == "Media")
                    {
                        Process.Start(selectedGroup.FilePath);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Open Window : Shared Files 
        private void SharedFilesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SharedFilesWindow SharedFileForm =
                    new SharedFilesWindow(_SelectedUsersContactProfile, _SelectedUsersContact, _SelectedType);
                SharedFileForm.Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Open Window : User Profile information
        private void ViewProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserProfileinfo_Window UserProfileinfo = new UserProfileinfo_Window(_SelectedUsersContactProfile,
                    _SelectedUsersContact, _SelectedType);
                UserProfileinfo.Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Open profile user on Browser
        private void ProfileViewLinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    Process.Start(UserPofilURl);
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch
            {

            }
        }

        #endregion

        //########################## friends Users ##########################

        #region Friends

        /// <summary>
        /// The event Run background worker : friends in Selectionchanged in Chat Activity User in tab top #region Chat Actitvity User
        /// </summary>

        // Functions friends
        public void friends(JArray All_friends, System.ComponentModel.DoWorkEventArgs e)
        {
            //try
            //{
            //    if (internetconection)
            //    {
            //        if (All_friends != null)
            //        {
            //            if (bgd_Worker_friends.CancellationPending == true)
            //            {
            //                e.Cancel = true;
            //                App.Current.Dispatcher.Invoke((Action) delegate // <--- HERE
            //                {
            //                    ProgressBar_Friends.Visibility = Visibility.Collapsed;
            //                    ProgressBar_Friends.IsIndeterminate = false;
            //                });
            //            }

            //            foreach (var j in All_friends)
            //            {
            //                JObject Alfa = JObject.FromObject(j);

            //                var user_id = Alfa["user_id"].ToString();
            //                var username = Alfa["username"].ToString();
            //                var email = Alfa["email"].ToString();
            //                var first_name = Alfa["first_name"].ToString();
            //                var avatar = Alfa["avatar"].ToString();
            //                var cover = Alfa["cover"].ToString();
            //                var relationship_id = Alfa["relationship_id"].ToString();
            //                var address = Alfa["address"].ToString();
            //                var working = Alfa["working"].ToString();
            //                var working_link = Alfa["working_link"].ToString();
            //                var about = Alfa["about"].ToString();
            //                var school = Alfa["school"].ToString();
            //                var gender = Alfa["gender"].ToString();
            //                var birthday = Alfa["birthday"].ToString();
            //                var website = Alfa["website"].ToString();
            //                var facebook = Alfa["facebook"].ToString();
            //                var google = Alfa["google"].ToString();
            //                var twitter = Alfa["twitter"].ToString();
            //                var linkedin = Alfa["linkedin"].ToString();
            //                var youtube = Alfa["youtube"].ToString();
            //                var vk = Alfa["vk"].ToString();
            //                var instagram = Alfa["instagram"].ToString();
            //                var language = Alfa["language"].ToString();
            //                var ip_address = Alfa["ip_address"].ToString();
            //                var follow_privacy = Alfa["follow_privacy"].ToString();
            //                var post_privacy = Alfa["post_privacy"].ToString();
            //                var message_privacy = Alfa["message_privacy"].ToString();
            //                var confirm_followers = Alfa["confirm_followers"].ToString();
            //                var show_activities_privacy = Alfa["show_activities_privacy"].ToString();
            //                var birth_privacy = Alfa["birth_privacy"].ToString();
            //                var visit_privacy = Alfa["visit_privacy"].ToString();
            //                var verified = Alfa["verified"].ToString();
            //                var lastseen = Alfa["lastseen"].ToString();
            //                var showlastseen = Alfa["showlastseen"].ToString();
            //                var status = Alfa["status"].ToString();
            //                var active = Alfa["active"].ToString();
            //                var admin = Alfa["admin"].ToString();
            //                var registered = Alfa["registered"].ToString();
            //                var phone_number = Alfa["phone_number"].ToString();
            //                var is_pro = Alfa["is_pro"].ToString();
            //                var pro_type = Alfa["pro_type"].ToString();
            //                var joined = Alfa["joined"].ToString();
            //                var timezone = Alfa["timezone"].ToString();
            //                var referrer = Alfa["referrer"].ToString();
            //                var balance = Alfa["balance"].ToString();
            //                var paypal_email = Alfa["paypal_email"].ToString();
            //                var notifications_sound = Alfa["notifications_sound"].ToString();
            //                var order_posts_by = Alfa["order_posts_by"].ToString();
            //                var social_login = Alfa["social_login"].ToString();
            //                var device_id = Alfa["device_id"].ToString();
            //                var url = Alfa["url"].ToString();
            //                var name = Alfa["name"].ToString();

            //                string AvatarSplit = avatar.Split('/').Last();

            //                Friend f = new Friend();

            //                f.F_user_id = user_id;
            //                f.F_username = username;
            //                f.F_email = email;
            //                f.F_first_name = first_name;
            //                f.F_last_name = lastseen;
            //                f.F_avatar = Functions.Get_image(user_id, AvatarSplit, avatar);
            //                f.F_cover = cover;
            //                f.F_relationship_id = relationship_id;
            //                f.F_address = address;
            //                f.F_working = working;
            //                f.F_working_link = working_link;
            //                f.F_about = about;
            //                f.F_school = school;
            //                f.F_gender = gender;
            //                f.F_birthday = birthday;
            //                f.F_website = website;
            //                f.F_facebook = facebook;
            //                f.F_google = google;
            //                f.F_twitter = twitter;
            //                f.F_linkedin = linkedin;
            //                f.F_youtube = youtube;
            //                f.F_vk = vk;
            //                f.F_instagram = instagram;
            //                f.F_language = language;
            //                f.F_ip_address = ip_address;
            //                f.F_follow_privacy = follow_privacy;
            //                f.F_post_privacy = post_privacy;
            //                f.F_message_privacy = message_privacy;
            //                f.F_confirm_followers = confirm_followers;
            //                f.F_show_activities_privacy = show_activities_privacy;
            //                f.F_birth_privacy = birth_privacy;
            //                f.F_visit_privacy = visit_privacy;
            //                f.F_verified = verified;
            //                f.F_lastseen = lastseen;
            //                f.F_showlastseen = showlastseen;
            //                f.F_status = status;
            //                f.F_active = active;
            //                f.F_admin = admin;
            //                f.F_registered = registered;
            //                f.F_phone_number = phone_number;
            //                f.F_is_pro = is_pro;
            //                f.F_pro_type = pro_type;
            //                f.F_joined = joined;
            //                f.F_timezone = timezone;
            //                f.F_referrer = referrer;
            //                f.F_balance = balance;
            //                f.F_paypal_email = paypal_email;
            //                f.F_notifications_sound = notifications_sound;
            //                f.F_order_posts_by = order_posts_by;
            //                f.F_social_login = social_login;
            //                f.F_device_id = device_id;
            //                f.F_url = url;
            //                f.F_name = name;

            //                App.Current.Dispatcher.Invoke((Action) delegate // <--- HERE
            //                {
            //                    ListFriends.Add(f);
            //                });
            //            }
            //            e.Cancel = true;
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}
        }

        // Run background worker : friends
        void bgd_Worker_friends_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //try
            //{
            //    if (internetconection)
            //    {
            //        if (Settings.Use_WebClient)
            //        {
            //            var All_friends = API_Request.Get_user_posts_Web(IDuser);
            //            friends(All_friends, e);
            //        }
            //        else
            //        {
            //            var All_friends = API_Request.Get_user_posts_Http(IDuser);
            //            friends(All_friends, e);
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}
        }

        void bgd_Worker_friends_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //try
            //{
            //    ProgressBar_Friends.Value = e.ProgressPercentage;
            //    ProgressBar_Friends.Visibility = Visibility.Visible;
            //    ProgressBar_Friends.IsIndeterminate = true;
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}
        }

        void bgd_Worker_friends_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //try
            //{
            //    FriendsListview.ItemsSource = ListFriends;
            //    if (bgd_Worker_friends.WorkerSupportsCancellation == true)
            //    {
            //        bgd_Worker_friends.CancelAsync();
            //        ProgressBar_Friends.Visibility = Visibility.Collapsed;
            //        ProgressBar_Friends.IsIndeterminate = false;
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}
        }

        //Event Selection Changed in friends Users
        private void FriendsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (internetconection)
            //    {
            //        Friend selectedGroup = (Friend)FriendsListview.SelectedItem;
            //        if (selectedGroup != null)
            //        {
            //            Process.Start(selectedGroup.F_url);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show(LocalResources.label_Please_check_your_internet);
            //    }
            //}
            //catch
            //{

            //}
        }

        #endregion

        //########################## Search User ##########################

        #region Search User

        // Functions Search
        public void SearchedUsers(List<GetSearchObject.User> users)
        {
            try
            {
                if (internetconection)
                {
                    ListSearsh = new ObservableCollection<Search>();

                    if (users != null)
                    {
                        if (users.Count > 0)
                        {
                            foreach (var user in users)
                            {

                                //Variables
                                var follow = "follow";
                                var color = Settings.Main_Color; //Wownder Default Brush
                                var gender = "GenderMale";
                                var Gender_color = "#0000ff"; //blue
                                var text_color_following = "#ffff";

                                if (Classes.GetSettingUser.Setting_ConnectivitySystem == "1")
                                {
                                    follow = "Add Friend";
                                }

                                if (user.IsFollowing != "no")
                                {
                                    if (Classes.GetSettingUser.Setting_ConnectivitySystem == "1")
                                    {
                                        follow = "Requested";
                                    }
                                    else
                                    {
                                        follow = "unfollow";
                                    }

                                    color = "#efefef";
                                    text_color_following = "#444";
                                }

                                if (user.Gender != "male")
                                {
                                    gender = "GenderFemale";
                                    Gender_color = "#cc5490"; //pink
                                }

                                Search sa = new Search
                                {
                                    user_Id = user.UserId,
                                    Search_Username = user.Username,
                                    Search_Name = Functions.SubStringCutOf(user.Name, 12),
                                    Search_Profile_picture = user.ProfilePicture, //BEGOVSKY CHANGE
                                    Search_Cover_picture = user.CoverPicture,
                                    Search_Verified = user.Verified,
                                    Search_Lastseen = user.Lastseen,
                                    Search_Gender = gender,
                                    Search_Lastseen_time_text = user.LastseenUnixTime, //BEGOVSKY CHANGE
                                    Search_Url = user.Url,
                                    Search_Is_following = user.IsFollowing,
                                    Search_text_following = follow,
                                    Search_color_follow = color,
                                    Search_color_Gender = Gender_color,
                                    Search_text_color_following = text_color_following
                                };

                                if (ModeDarkstlye)
                                {
                                    sa.S_Color_Background = "#232323";
                                    sa.S_Color_Foreground = "#efefef";
                                }
                                else
                                {
                                    sa.S_Color_Background = "#ffff";
                                    sa.S_Color_Foreground = "#444";
                                }

                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                {
                                    ListSearsh.Add(sa);
                                });
                            }
                        }
                    }


                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Run background worker : Search
        private async void Search_Async()
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    IProgress<int> progress = new Progress<int>(percentCompleted =>
                    {
                        ProgressBar_Search_User.Value = percentCompleted;
                    });


                    await Task.Run(async () =>
                    {
                        var response = await RequestsAsync.Search_Users_Web(UserDetails.User_id, search_key);

                        if (response.Item1 == 200)
                        {
                            if (response.Item2 is GetSearchObject result)
                            {
                                if (ModeDarkstlye)
                                {
                                    var list = result.Users.Select(user => new Search()
                                    {
                                        user_Id = user.UserId,
                                        Search_Verified = user.Verified,
                                        Search_Lastseen = user.Lastseen,
                                        Search_Username = user.Username,
                                        Search_Name = user.Name,
                                        Search_Profile_picture = user.ProfilePicture,
                                        Search_Cover_picture = user.CoverPicture,
                                        Search_Gender = user.Gender,
                                        Search_Lastseen_time_text = user.LastseenUnixTime,
                                        Search_Url = user.Url,
                                        Search_Is_following = user.IsFollowing,
                                        //Search_color_follow = user.,
                                        //Search_color_Gender = user.Details.ge;
                                        //Search_text_color_following = user.Cha;
                                        S_Color_Background = "#232323",
                                        S_Color_Foreground = "#efefef"

                                    }).ToList();

                                    ListSearsh = new ObservableCollection<Search>(list);
                                }
                                else
                                {
                                    var list = result.Users.Select(user => new Search()
                                    {
                                        user_Id = user.UserId,
                                        Search_Verified = user.Verified,
                                        Search_Lastseen = user.Lastseen,
                                        Search_Username = user.Username,
                                        Search_Name = user.Name,
                                        Search_Profile_picture = user.ProfilePicture,
                                        Search_Cover_picture = user.CoverPicture,
                                        Search_Gender = user.Gender,
                                        Search_Lastseen_time_text = user.LastseenUnixTime,
                                        Search_Url = user.Url,
                                        Search_Is_following = user.IsFollowing,
                                        //Search_color_follow = user.,
                                        //Search_color_Gender = Gender_color;
                                        //Search_text_color_following = text_color_following;
                                        S_Color_Background = "#ffff",
                                        S_Color_Foreground = "#444"

                                    }).ToList();

                                    ListSearsh = new ObservableCollection<Search>(list);
                                }

                                SearchedUsers(result.Users);
                            }
                        }
                    });

                    SearshUser_list.ItemsSource = ListSearsh;

                    ProgressBar_Search_User.Visibility = Visibility.Collapsed;
                    ProgressBar_Search_User.IsIndeterminate = false;

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        // Get value search key
        private void Txt_SearchBoxOnline_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        // click Button Search User
        private void Btn_SearshUser_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    ProgressBar_Search_User.Visibility = Visibility.Visible;
                    ProgressBar_Search_User.IsIndeterminate = true;
                    No_Search_User_Panel.Visibility = Visibility.Collapsed;
                    No_Search_User_Grid.Visibility = Visibility.Collapsed;

                    Search_Async();
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }

                if (ListSearsh.Count == 0)
                {
                    No_Search_User_Panel.Visibility = Visibility.Visible;
                    No_Search_User_Grid.Visibility = Visibility.Visible;
                }
                else
                {
                    No_Search_User_Panel.Visibility = Visibility.Collapsed;
                    No_Search_User_Grid.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Enter Search User
        private void Txt_SearchBoxOnline_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                search_key = Txt_SearchBoxOnline.Text;

                if (e.Key == Key.Enter)
                {
                    if (internetconection)
                    {
                        ProgressBar_Search_User.Visibility = Visibility.Visible;
                        ProgressBar_Search_User.IsIndeterminate = true;
                        No_Search_User_Panel.Visibility = Visibility.Collapsed;
                        No_Search_User_Grid.Visibility = Visibility.Collapsed;

                        // your event handler here
                        e.Handled = true;

                        Search_Async();
                    }
                    else
                    {
                        MessageBox.Show(LocalResources.label_Please_check_your_internet);
                    }

                    if (ListSearsh.Count == 0)
                    {
                        No_Search_User_Panel.Visibility = Visibility.Visible;
                        No_Search_User_Grid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        No_Search_User_Panel.Visibility = Visibility.Collapsed;
                        No_Search_User_Grid.Visibility = Visibility.Collapsed;

                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Selection Changed null is SelectedItem SearshUser_list
        private void SearshUser_list_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SearshUser_list.SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Mouse Move null is SelectedItem SearshUser_list
        private void SearshUser_list_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                SearshUser_list.SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Follow User ##########################

        #region Following User

        // Run background worker : Follow

        // click Button Follow
        private void btn_Follow_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    var mi = (ListBox)sender;
                    var originalSource = e.OriginalSource as Button;

                    Follow_id = originalSource.CommandParameter.ToString();

                    RequestsAsync.Follow_Users_Http(UserDetails.User_id, Follow_id).ConfigureAwait(false);

                    var selectedGroup = ListSearsh.FirstOrDefault(a => a.user_Id == Follow_id);
                    if (selectedGroup != null)
                    {
                        if (selectedGroup.Search_Is_following == "yes")
                        {
                            if (Classes.GetSettingUser.Setting_ConnectivitySystem == "1")
                            {
                                selectedGroup.Search_text_following = "Add Friend";
                            }
                            else
                            {
                                selectedGroup.Search_text_following = "follow";
                            }

                            selectedGroup.Search_Is_following = "no";
                            selectedGroup.Search_color_follow = Settings.Main_Color; //Wownder Default Brush
                            selectedGroup.Search_text_color_following = "#ffff";
                        }
                        else
                        {
                            if (Classes.GetSettingUser.Setting_ConnectivitySystem == "1")
                            {
                                selectedGroup.Search_text_following = "Requested";
                            }
                            else
                            {
                                selectedGroup.Search_text_following = "unfollow";
                            }

                            selectedGroup.Search_Is_following = "yes";
                            selectedGroup.Search_color_follow = "#efefef"; //salver
                            selectedGroup.Search_text_color_following = "#444";
                        }
                    }
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Search Gifs ##########################

        #region Search Gifs

        // Functions Gifs
        public void Gifs(List<GetGifsObject.Data> gifs)
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    ListGifs = new ObservableCollection<Classes.Get_Gifs>();

                    if (gifs.Count > 0)
                    {
                        foreach (var gif in gifs)
                        {
                            Classes.Get_Gifs g = new Classes.Get_Gifs();

                            //Variables
                            var Bar_load_gifs_Visibility = "Visible";

                            g.G_id = gif.Id;
                            g.G_fixed_height_small_width = gif.Images.FixedHeightSmall.Width;
                            g.G_fixed_height_small_height = gif.Images.FixedHeightSmall.Height;
                            g.G_fixed_height_small_url =
                                new Uri(gif.Images.FixedHeightSmall.Url, UriKind.Absolute).ToString();
                            g.G_original_url = gif.Images.Original.Url;
                            g.G_fixed_height_small_mp4 = gif.Images.FixedHeightSmall.Mp4;
                            g.G_fixed_height_small_webp = gif.Images.FixedHeightSmall.Webp;

                            //style
                            g.G_Bar_load_gifs_Visibility = Bar_load_gifs_Visibility;
                            g.G_btn_ExitGifs_remove = "Collapsed";

                            App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                            {
                                ListGifs.Add(g);
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

        // Run background worker : Gifs

        private async void Gifs_Async()
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {

                    IProgress<int> progress = new Progress<int>(percentCompleted =>
                    {
                        ProgressBar_Search_Gifs.Value = percentCompleted;
                    });


                    await Task.Run(async () =>
                    {
                        var response = await RequestsAsync.Search_Gifs_Web(Search_Gifs_key);

                        if (response.Item1 == 200)
                        {
                            if (response.Item2 is List<GetGifsObject.Data> gifs)
                            {
                                Gifs(gifs); //begovsky maybe change maybe not
                            }

                        }

                    });

                    GifsListview.ItemsSource = ListGifs;


                    ProgressBar_Search_Gifs.Visibility = Visibility.Collapsed;
                    ProgressBar_Search_Gifs.IsIndeterminate = false;

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        //Event show or hide Progress Bar load gifs
        private void Gifs_MediaElement_OnAnimationLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var b = sender as System.Windows.Controls.Image;
                if (b != null)
                {
                    DependencyObject item = b;
                    while (item is ListBoxItem == false)
                    {
                        item = VisualTreeHelper.GetParent(item);
                    }

                    var lbi = (ListBoxItem)item;
                    var data = (sender as FrameworkElement).DataContext as Classes.Get_Gifs;
                    if (data != null)
                    {
                        data.G_Bar_load_gifs_Visibility = "Collapsed";
                        var controller = ImageBehavior.GetAnimationController(b);
                        if (controller != null)
                        {
                            controller.Pause();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event On Mouse Enter Play gifs 
        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var b = sender as System.Windows.Controls.Image;
                if (b != null)
                {
                    var controller = ImageBehavior.GetAnimationController(b);
                    if (controller != null)
                    {
                        controller.Play();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event  On Mouse Leave Pause gifs
        private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var b = sender as System.Windows.Controls.Image;
                if (b != null)
                {
                    var controller = ImageBehavior.GetAnimationController(b);
                    if (controller != null)
                    {
                        controller.Pause();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Enter Search Gifs
        private void Txt_searchGifs_OnKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    Search_Gifs_key = Txt_searchGifs.Text;

                    if (e.Key == Key.Enter)
                    {
                        ListGifs.Clear();
                        ProgressBar_Search_Gifs.Visibility = Visibility.Visible;
                        ProgressBar_Search_Gifs.IsIndeterminate = true;

                        // your event handler here
                        e.Handled = true;

                        Gifs_Async();
                    }
                }

                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button Search Gifs
        private void Btn_searchGifs_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    ProgressBar_Search_Gifs.Visibility = Visibility.Visible;
                    ProgressBar_Search_Gifs.IsIndeterminate = true;

                    Gifs_Async();
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Button Sent Gifs and Insert data user in database
        private void GifsListview_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    Classes.Get_Gifs selectedGroup = (Classes.Get_Gifs)GifsListview.SelectedItem;

                    if (selectedGroup != null)
                    {
                        var time = DateTime.Now.ToString("hh:mm");
                        ListMessages.Add(new Classes.Messages()
                        {
                            Mes_Type = "right_gif",
                            Mes_Id = time2,
                            Mes_Time_text = time,
                            Mes_MediaFileName = selectedGroup.G_fixed_height_small_url, // url - view
                            Mes_Media = selectedGroup.G_fixed_height_small_mp4, //Media - sent
                        });

                        Task.Factory.StartNew(() =>
                        {
                            SendMessageTask("", "", "", selectedGroup.G_id, selectedGroup.G_fixed_height_small_mp4)
                                .ConfigureAwait(false);
                        });

                        var lastItem = ChatMessgaeslistBox.ItemsSource.OfType<object>().Last();

                        ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                        ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);

                        var scan_gifs = SQLite_Entity.Connection.Table<DataBase.GifsTable>()
                            .FirstOrDefault(a => a.G_id == selectedGroup.G_id);

                        if (scan_gifs == null)
                        {
                            // Insert data user in database
                            DataBase.GifsTable GifeData = new DataBase.GifsTable();
                            GifeData.G_id = selectedGroup.G_id;
                            GifeData.G_fixed_height_small_width = selectedGroup.G_fixed_height_small_width;
                            GifeData.G_fixed_height_small_height = selectedGroup.G_fixed_height_small_height;
                            GifeData.G_fixed_height_small_url = selectedGroup.G_fixed_height_small_url;
                            GifeData.G_original_url = selectedGroup.G_original_url;
                            GifeData.G_fixed_height_small_mp4 = selectedGroup.G_fixed_height_small_mp4;
                            GifeData.G_fixed_height_small_webp = selectedGroup.G_fixed_height_small_webp;
                            GifeData.G_btn_ExitGifs_remove = "Visible";
                            GifeData.G_Bar_load_gifs_Visibility = selectedGroup.G_Bar_load_gifs_Visibility;

                            SQLiteCommandSender.Insert_To_GifsTable(GifeData);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Get data image gif from Gifs Table
        private void Txt_searchGifs_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Txt_searchGifs.Text == "")
                {
                    ListGifs.Clear();
                    var data = SQLiteCommandSender.Get_To_GifsTable();
                    ListGifs = data;
                    GifsListview.ItemsSource = ListGifs;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button Search Gifs remove data image gif from Gifs Table
        private void Btn_ExitGifs_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mi = (Button)sender;
                string gifs_id = mi.CommandParameter.ToString();

                var delete_gifs = ListGifs.FirstOrDefault(a => a.G_id == gifs_id);
                if (delete_gifs != null)
                {
                    SQLiteCommandSender.removeGifsTable(gifs_id);
                    ListGifs.Remove(delete_gifs);
                    GifsListview.ItemsSource = ListGifs;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Stickers ##########################

        #region Stickers

        //Click Button Open Window : Setting_Stickers
        private void Button_S_Setting_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Setting_Stickers_Window settingStickers = new Setting_Stickers_Window(this);
                settingStickers.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Functions Get Stickers
        public void GetStickers()
        {
            try
            {
                var data_s = SQLiteCommandSender.Get_From_StickersTable();
                if (data_s != null)
                {
                    foreach (var item in data_s)
                    {
                        if (item.S_name == "Spoiled_Rabbit")
                        {
                            if (item.S_Visibility == "Visible")
                            {
                                Grid_Spoiled_Rabbit.Visibility = Visibility.Visible;
                                Btn_S_rappit.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Grid_Spoiled_Rabbit.Visibility = Visibility.Collapsed;
                                Btn_S_rappit.Visibility = Visibility.Collapsed;
                            }

                        }
                        else if (item.S_name == "Water_Drop")
                        {
                            if (item.S_Visibility == "Visible")
                            {
                                Grid_Water_Drop.Visibility = Visibility.Visible;
                                Btn_S_Water_Drop.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Grid_Water_Drop.Visibility = Visibility.Collapsed;
                                Btn_S_Water_Drop.Visibility = Visibility.Collapsed;
                            }
                        }
                        else if (item.S_name == "Monster")
                        {
                            if (item.S_Visibility == "Visible")
                            {
                                Grid_Monstor.Visibility = Visibility.Visible;
                                Btn_S_Monster.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Grid_Monstor.Visibility = Visibility.Collapsed;
                                Btn_S_Monster.Visibility = Visibility.Collapsed;
                            }
                        }
                        else if (item.S_name == "NINJA_Nyankko")
                        {
                            if (item.S_Visibility == "Visible")
                            {
                                Grid_NINJA_Nyankko.Visibility = Visibility.Visible;
                                Btn_S_NINJA_Nyankko.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Grid_NINJA_Nyankko.Visibility = Visibility.Collapsed;
                                Btn_S_NINJA_Nyankko.Visibility = Visibility.Collapsed;
                            }
                        }
                        else if (item.S_name == "So_Much_Love")
                        {
                            if (item.S_Visibility == "Visible")
                            {
                                Grid_So_Much_Love.Visibility = Visibility.Visible;
                                Btn_S_So_Much_Love.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Grid_So_Much_Love.Visibility = Visibility.Collapsed;
                                Btn_S_So_Much_Love.Visibility = Visibility.Collapsed;
                            }
                        }
                        else if (item.S_name == "Sukkara_chan")
                        {
                            if (item.S_Visibility == "Visible")
                            {
                                Grid_Sukkara_chan.Visibility = Visibility.Visible;
                                Btn_S_Sukkara_chan.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Grid_Sukkara_chan.Visibility = Visibility.Collapsed;
                                Btn_S_Sukkara_chan.Visibility = Visibility.Collapsed;
                            }
                        }
                        else if (item.S_name == "Flower_Hijab")
                        {

                            if (item.S_Visibility == "Visible")
                            {
                                Grid_Flower_Hijab.Visibility = Visibility.Visible;
                                Btn_S_Flower_Hijab.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Grid_Flower_Hijab.Visibility = Visibility.Collapsed;
                                Btn_S_Flower_Hijab.Visibility = Visibility.Collapsed;
                            }
                        }
                        else if (item.S_name == "Trendy_boy")
                        {
                            if (item.S_Visibility == "Visible")
                            {
                                Grid_Trendy_boy.Visibility = Visibility.Visible;
                                Btn_S_Trendy_boy.Visibility = Visibility.Visible;

                            }
                            else
                            {
                                Grid_Trendy_boy.Visibility = Visibility.Collapsed;
                                Btn_S_Trendy_boy.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete Spoiled Rabbit Sticker
        private void SpoiledRabbitStickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid_Spoiled_Rabbit.Visibility = Visibility.Collapsed;
                Btn_S_rappit.Visibility = Visibility.Collapsed;
                SQLiteCommandSender.Update_To_StickersTable("Spoiled_Rabbit", "Collapsed");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete Water Sticker
        private void CloseWaterStickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid_Water_Drop.Visibility = Visibility.Collapsed;
                Btn_S_Water_Drop.Visibility = Visibility.Collapsed;
                SQLiteCommandSender.Update_To_StickersTable("Water_Drop", "Collapsed");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete Monsters Sticker
        private void CloseMonstersStickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid_Monstor.Visibility = Visibility.Collapsed;
                Btn_S_Monster.Visibility = Visibility.Collapsed;
                SQLiteCommandSender.Update_To_StickersTable("Monster", "Collapsed");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete NINJA Nyankko Sticker
        private void CloseNINJA_NyankkoStickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid_NINJA_Nyankko.Visibility = Visibility.Collapsed;
                Btn_S_NINJA_Nyankko.Visibility = Visibility.Collapsed;
                SQLiteCommandSender.Update_To_StickersTable("NINJA_Nyankko", "Collapsed");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete Sukkara chan Sticker
        private void Close_Sukkara_chanStickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid_Flower_Hijab.Visibility = Visibility.Collapsed;
                Btn_S_Sukkara_chan.Visibility = Visibility.Collapsed;
                SQLiteCommandSender.Update_To_StickersTable("Sukkara_chan", "Collapsed");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete So Much Love Sticker
        private void CloseSo_Much_LoveoStickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid_So_Much_Love.Visibility = Visibility.Collapsed;
                Btn_S_So_Much_Love.Visibility = Visibility.Collapsed;
                SQLiteCommandSender.Update_To_StickersTable("So_Much_Love", "Collapsed");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete Flower Hijab Sticker
        private void Close_Flower_Hijab_StickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid_Flower_Hijab.Visibility = Visibility.Collapsed;
                Btn_S_Flower_Hijab.Visibility = Visibility.Collapsed;
                SQLiteCommandSender.Update_To_StickersTable("Flower_Hijab", "Collapsed");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete Trendy boy Sticker
        private void Close_Trendy_boy_StickerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Grid_Trendy_boy.Visibility = Visibility.Collapsed;
                Btn_S_Trendy_boy.Visibility = Visibility.Collapsed;
                SQLiteCommandSender.Update_To_StickersTable("Trendy_boy", "Collapsed");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Functions  Get Vertical - ScrollViewer
        private double GetVerticalOffset(FrameworkElement child, ScrollViewer scrollViewer)
        {
            try
            {
                // Ensure the control is scrolled into view in the ScrollViewer. 
                GeneralTransform focusedVisualTransform = child.TransformToVisual(scrollViewer);
                Point topLeft = focusedVisualTransform.Transform(new Point(child.Margin.Left, child.Margin.Top));
                Rect rectangle = new Rect(topLeft, child.RenderSize);
                double newOffset = scrollViewer.VerticalOffset + (rectangle.Top);
                return newOffset < 0 ? 0 : newOffset; // no use returning negative offset
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return 0;
            }
        }

        private void Btn_S_rappit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = Grid_Spoiled_Rabbit as FrameworkElement;
                if (element != null)
                {
                    StickersScrollViewer.ScrollToVerticalOffset(GetVerticalOffset(element, StickersScrollViewer));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_S_Water_Drop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = Grid_Water_Drop as FrameworkElement;
                if (element != null)
                {
                    StickersScrollViewer.ScrollToVerticalOffset(GetVerticalOffset(element, StickersScrollViewer));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_S_Monster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = Grid_Monstor as FrameworkElement;
                if (element != null)
                {
                    StickersScrollViewer.ScrollToVerticalOffset(GetVerticalOffset(element, StickersScrollViewer));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_S_NINJA_Nyankko_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = Grid_NINJA_Nyankko as FrameworkElement;
                if (element != null)
                {
                    StickersScrollViewer.ScrollToVerticalOffset(GetVerticalOffset(element, StickersScrollViewer));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_S_So_Much_Love_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = Grid_So_Much_Love as FrameworkElement;
                if (element != null)
                {
                    StickersScrollViewer.ScrollToVerticalOffset(GetVerticalOffset(element, StickersScrollViewer));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_S_Sukkara_chan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = Grid_Sukkara_chan as FrameworkElement;
                if (element != null)
                {
                    StickersScrollViewer.ScrollToVerticalOffset(GetVerticalOffset(element, StickersScrollViewer));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_S_Flower_Hijab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = Grid_Flower_Hijab as FrameworkElement;
                if (element != null)
                {
                    StickersScrollViewer.ScrollToVerticalOffset(GetVerticalOffset(element, StickersScrollViewer));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Btn_S_Trendy_boy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrameworkElement element = Grid_Trendy_boy as FrameworkElement;
                if (element != null)
                {
                    StickersScrollViewer.ScrollToVerticalOffset(GetVerticalOffset(element, StickersScrollViewer));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button moving to Left
        private void Btn_S_left_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Horizontal_ScrollViewer.ScrollToLeftEnd();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button moving to Righ
        private void Btn_S_Right_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Horizontal_ScrollViewer.ScrollToRightEnd();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button Send Messages type Sticker
        private void Sticker_OnClick(object sender, RoutedEventArgs e)
        {
            if (internetconection)
            {
                var b = sender as Button;
                if (b != null)
                {
                    var ff = b.Name;
                    var number = b.Name.Replace("Sticker", "");
                    var time = DateTime.Now.ToString("hh:mm");

                    var Stick = @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name +
                                ";component/" + "/Images/Stickers/" + b.Name + ".png";

                    ListMessages.Add(new Classes.Messages()
                    {
                        Mes_Type = "right_sticker",
                        Mes_Id = time2,
                        Mes_Time_text = time,

                        Mes_MediaFileName = Stickers.GetStickerImage(number), // url
                        Mes_Media = Stick, //Media
                    });

                    Task.Factory.StartNew(() =>
                    {
                        SendMessageTask("", "", Stickers.GetStickerImage(number), "Sticker" + number, "")
                            .ConfigureAwait(false);
                    });

                    var lastItem = ChatMessgaeslistBox.ItemsSource.OfType<object>().Last();

                    ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                    ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show(LocalResources.label_Please_check_your_internet);
            }
        }

        // Run Api :  Send Messages type Sticker / Gifs / Contact
        public async Task SendMessageTask(string Text, string Contact, string sticker, string stickerID, string GifUrl)
        {
            try
            {
                if (internetconection)
                {

                    var Time = unixTimestamp.ToString();
                    var respond = await RequestsAsync.Send_User_Message_Web(UserDetails.User_id, IDuser, Time, Text,
                        Contact, sticker, stickerID,
                        GifUrl);
                    if (respond.Item1 == 200)
                    {
                        UpdateMessageID(respond.Item2);
                    }

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        #endregion

        //########################## Emoji Icon ##########################

        #region Emoji

        #region Emoji Icon Click

        private void NormalSmile_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" :)");
            MessageBoxText.Focus();

        }

        private void LaughingEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" (<");
            MessageBoxText.Focus();

        }

        private void HappyFaceEmoji_Click(object sender, RoutedEventArgs e)
        {

            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" **)");
            MessageBoxText.Focus();
        }

        private void CrazyEmoji_Click(object sender, RoutedEventArgs e)
        {

            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" :p");
            MessageBoxText.Focus();


        }

        private void ChesekyEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" :_p");
            MessageBoxText.Focus();


        }

        private void CoolEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" B)");
            MessageBoxText.Focus();

        }

        private void CringeEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" :D");
            MessageBoxText.Focus();


        }

        private void CossolEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" /_)");
            MessageBoxText.Focus();


        }

        private void AngelEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" 0)");
            MessageBoxText.Focus();

        }

        private void CryingEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" :_(");
            MessageBoxText.Focus();

        }

        private void SadfaceEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun("  :(");
            MessageBoxText.Focus();


        }

        private void KissingEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun("  :*");
            MessageBoxText.Focus();


        }

        private void HeartEmoji_Click(object sender, RoutedEventArgs e)
        {

            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun("  <3");
            MessageBoxText.Focus();


        }

        private void BreakingHeartEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" </3");
            MessageBoxText.Focus();


        }

        private void HeartEyesEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" *_*");
            MessageBoxText.Focus();


        }

        private void StarEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" <5");
            MessageBoxText.Focus();


        }

        private void SurprisedEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" :o");
            MessageBoxText.Focus();


        }

        private void ScreamEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" :0");
            MessageBoxText.Focus();
        }

        private void PainedFaceEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" o(");
            MessageBoxText.Focus();
        }

        private void DissatisfiedEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" -_(");
            MessageBoxText.Focus();


        }

        private void AngryEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" x(");
            MessageBoxText.Focus();


        }

        private void AngryFace_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun("  X(");
            MessageBoxText.Focus();


        }

        private void FaceWithStraightMouthEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" -_-");
            MessageBoxText.Focus();


        }

        private void PuzzledEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun(" :-/");
            MessageBoxText.Focus();


        }

        private void StraightFacedEmoji_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun("  :|");
            MessageBoxText.Focus();

        }

        private void HeavyExclamation_Click(object sender, RoutedEventArgs e)
        {
            string textRange = new TextRange(MessageBoxText.Document.ContentStart, MessageBoxText.Document.ContentEnd)
                .Text;
            if (textRange.Contains("Write your Message"))
            {
                MessageBoxText.Document.Blocks.Clear();
            }

            MessageBoxText.CaretPosition =
                MessageBoxText.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            MessageBoxText.CaretPosition.InsertTextInRun("  !_");
            MessageBoxText.Focus();


        }

        #endregion

        #region Emoji Icon Events Click/Mouse

        private void SmileButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(Settings.Main_Color);
            EmojiSmileofSmileButton.Foreground = new SolidColorBrush(color);
            imogiPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void imogiPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Btn_SwitchDarkMode.IsChecked == true)
            {
                EmojiSmileofSmileButton.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                Color color = (Color)ColorConverter.ConvertFromString(Settings.Secondery_Color);
                EmojiSmileofSmileButton.Foreground = new SolidColorBrush(color);
            }

            imogiPanel.Visibility = Visibility.Hidden;
        }

        private void imogiPanel_MouseMove(object sender, MouseEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(Settings.Main_Color);
            EmojiSmileofSmileButton.Foreground = new SolidColorBrush(color);
            imogiPanel.Visibility = Visibility.Visible;
        }

        private void imogiPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(Settings.Main_Color);
            EmojiSmileofSmileButton.Foreground = new SolidColorBrush(color);
        }

        private void SmileButton_Click(object sender, RoutedEventArgs e)
        {


            //>> Next Update

            //imogiPanel.Visibility = System.Windows.Visibility.Hidden;

            //if (imogiPanel.Children.Contains(EmojiTabcontrol))
            //{
            //    GifsListview.ItemsSource = null;

            //    RightPanel.Visibility = System.Windows.Visibility.Visible;
            //    UserAboutSection.Visibility = System.Windows.Visibility.Collapsed;
            //    UserFriendsSection.Visibility = System.Windows.Visibility.Collapsed;
            //    UserMediaSection.Visibility = System.Windows.Visibility.Collapsed;
            //    UserDeleteChatSection.Visibility = System.Windows.Visibility.Collapsed;
            //    imogiPanel.Children.Remove(EmojiTabcontrol);
            //    EmojiTabcontrol.Margin = new Thickness(0, 0, 0, 0);

            //    RightPanel.Children.Add(EmojiTabcontrol);
            //    EmojiTabStop = "no";
            //    RightPanelScrollViewer.ScrollToEnd();

            //}
            //else
            //{
            //    RightPanel.Visibility = System.Windows.Visibility.Collapsed;
            //    UserAboutSection.Visibility = System.Windows.Visibility.Visible;
            //    UserFriendsSection.Visibility = System.Windows.Visibility.Visible;
            //    UserMediaSection.Visibility = System.Windows.Visibility.Visible;
            //    UserDeleteChatSection.Visibility = System.Windows.Visibility.Visible;
            //    RightPanel.Children.Remove(EmojiTabcontrol);
            //    EmojiTabcontrol.Margin = new Thickness(0, 0, 0, 0);
            //    imogiPanel.Children.Add(EmojiTabcontrol);

            //}

        }


        #endregion

        private void SmileButton_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Color color = (Color)ColorConverter.ConvertFromString(Settings.Main_Color);

                if (RightPanel.Children.Contains(EmojiTabcontrol))
                {
                    EmojiSmileofSmileButton.Foreground = new SolidColorBrush(color);
                    imogiPanel.Visibility = Visibility.Hidden;

                }
                else if (EmojiTabStop == "yes")
                {
                    EmojiSmileofSmileButton.Foreground = new SolidColorBrush(color);
                    imogiPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    Color color2 = (Color)ColorConverter.ConvertFromString(Settings.Secondery_Color);
                    EmojiSmileofSmileButton.Foreground = new SolidColorBrush(color2);
                    EmojiTabStop = "yes";
                    imogiPanel.Visibility = Visibility.Hidden;
                    return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Search Mesages ##########################

        #region Search Mesages

        public T FindDescendant<T>(DependencyObject obj) where T : DependencyObject
        {
            // Check if this object is the specified type
            if (obj is T)
                return obj as T;

            // Check for children
            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            if (childrenCount < 1)
                return null;

            // First check all the children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    return child as T;
            }

            // Then check the childrens children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = FindDescendant<T>(VisualTreeHelper.GetChild(obj, i));
                if (child != null && child is T)
                    return child as T;
            }

            return null;
        }

        List<int> CountResult = new List<int>();

        // click Button Search Mesages
        private void SearchMsgButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                CountResult = new List<int>();
                var search = ListMessages.Where(a => a.Mes_Text.ToLower().Contains(Search_messages_key.ToLower()));
                var dd = ChatMessgaeslistBox.Items;
                CountSearch.Content = search.Count() + " Maches";

                foreach (var Item in search)
                {
                    var currentSelectedListBoxItem =
                        this.ChatMessgaeslistBox.ItemContainerGenerator.ContainerFromItem(Item) as ListBoxItem;
                    int curIndex = ChatMessgaeslistBox.Items.IndexOf(Item);

                    Classes.Messages selectedGroup = (Classes.Messages)Item;
                    if (selectedGroup.Mes_Type == "right_text" || selectedGroup.Mes_Type == "left_text")
                    {
                        if (currentSelectedListBoxItem == null)
                            return;

                        // Iterate whole listbox tree and search for this items
                        RichTextBox nameBox = FindDescendant<RichTextBox>(currentSelectedListBoxItem);

                        if (nameBox != null)
                        {

                            TextRange textRange =
                                new TextRange(nameBox.Document.ContentStart, nameBox.Document.ContentEnd);

                            textRange.ApplyPropertyValue(TextElement.BackgroundProperty,
                                System.Windows.Media.Brushes.Transparent);

                            if (selectedGroup.Mes_Type == "right_text")
                            {
                                textRange.ApplyPropertyValue(TextElement.ForegroundProperty,
                                    System.Windows.Media.Brushes.White);
                            }
                            else
                            {
                                textRange.ApplyPropertyValue(TextElement.ForegroundProperty,
                                    System.Windows.Media.Brushes.Black);
                            }


                            IEnumerable<TextRange> wordRanges = GetAllWordRanges(nameBox.Document);
                            foreach (TextRange wordRange in wordRanges)
                            {
                                if (wordRange.Text.ToLower() == Search_messages_key.ToLower() ||
                                    wordRange.Text.ToUpper() == Search_messages_key.ToUpper() ||
                                    wordRange.Text == Search_messages_key)
                                {

                                    wordRange.ApplyPropertyValue(TextElement.BackgroundProperty,
                                        System.Windows.Media.Brushes.LightYellow);
                                    wordRange.ApplyPropertyValue(TextElement.ForegroundProperty,
                                        System.Windows.Media.Brushes.Black);

                                    var Cheker = CountResult.Contains(curIndex);
                                    if (Cheker == false)
                                    {
                                        CountResult.Add(curIndex);
                                    }

                                }
                            }
                        }
                    }
                }

            }

            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }
        }


        public static IEnumerable<TextRange> GetAllWordRanges(FlowDocument document)
        {
            string pattern = @"[^\W\d](\w|[-']{1,2}(?=\w))*";
            TextPointer pointer = document.ContentStart;
            while (pointer != null)
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
                    MatchCollection matches = Regex.Matches(textRun, pattern);
                    foreach (Match match in matches)
                    {
                        int startIndex = match.Index;
                        int length = match.Length;
                        TextPointer start = pointer.GetPositionAtOffset(startIndex);
                        TextPointer end = start.GetPositionAtOffset(length);
                        yield return new TextRange(start, end);
                    }
                }

                pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
            }
        }

        //Event Enter Search Mesages
        private void MSG_searchMessages_OnKeyUp(object sender, KeyEventArgs e)
        {
            Search_messages_key = MSG_searchMessages.Text;
        }

        List<int> CountResult_After = new List<int>();

        private void SearchMsgDownButton_OnClick(object sender, RoutedEventArgs e)
        {

            int Alfa = CountResult.Count;
            if (Alfa == 0)
            {
                MessageBox.Show("No search result found");
            }

            int index = CountResult.LastOrDefault();

            //Next version on update >>>>>>>>>>

            //var currentSelectedListBoxItem = this.ChatMessgaeslistBox.ItemContainerGenerator.ContainerFromIndex(index) as ListBoxItem;
            //RichTextBox nameBox = FindDescendant<RichTextBox>(currentSelectedListBoxItem);

            //if (nameBox != null)
            //{
            //    IEnumerable<TextRange> wordRanges = GetAllWordRanges(nameBox.Document);
            //    foreach (TextRange wordRange in wordRanges)
            //    {
            //        if (wordRange.Text.ToLower() == Search_messages_key.ToLower() || wordRange.Text.ToUpper() == Search_messages_key.ToUpper() || wordRange.Text == Search_messages_key)
            //        {

            //            wordRange.ApplyPropertyValue(TextElement.BackgroundProperty,
            //                System.Windows.Media.Brushes.DarkRed);
            //            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty,
            //                System.Windows.Media.Brushes.White);

            //        }
            //    }
            //}

            ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.Items[index]);
            CountResult.Remove(index);
            if (!CountResult_After.Contains(index))
            {
                CountResult_After.Add(index);
            }

        }

        private void SearchMsgUpButton_OnClick(object sender, RoutedEventArgs e)
        {
            int Alfa = CountResult_After.Count;
            int index = CountResult_After.FirstOrDefault();

            if (Alfa == 0)
            {
                MessageBox.Show(LocalResources.Label2_No_search_result_found);
            }

            ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.Items[index]);
            CountResult.Add(index);
            CountResult_After.Remove(index);

        }

        //Click Button show textbox Search Mesages
        private void SearchMesageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SearchMessagePanel.Visibility == Visibility.Collapsed)
                {
                    SearchMessagePanel.Visibility = Visibility.Visible;
                }
                else
                {
                    SearchMessagePanel.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Close textbox Search Mesages
        private void CloseMsgUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchMessagePanel.Visibility = Visibility.Collapsed;
                CountSearch.Content = "";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Pick Color ##########################

        #region Pick Color

        //Click Button Open Window : Pick Color
        private void ChangeColorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PickColorWindow ColorWin = new PickColorWindow(IDuser, this);
                ColorWin.Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Notifications ##########################

        #region Notifications

        // Functions Notifications
        public void NotificationsUsers(List<GetUserListObject.Notification> notifications)
        {
            try
            {
                if (internetconection)
                {


                    if (notifications != null)
                    {

                        foreach (var notification in notifications)
                        {

                            var username = notification.Notifier.Username;
                            var avatar = notification.Notifier.Avatar;

                            string AvatarSplit = avatar.Split('/').Last();
                            var check = ListNotifications.FirstOrDefault(a => a.N_id == notification.Id);
                            if (check != null)
                            {

                            }
                            else
                            {
                                Classes.Notifications n = new Classes.Notifications
                                {
                                    N_id = notification.Id,
                                    N_notifier_id = notification.NotifierId,
                                    N_recipient_id = notification.RecipientId,
                                    N_post_id = notification.PostId,
                                    N_page_id = notification.PageId,
                                    N_group_id = notification.GroupId,
                                    N_event_id = notification.EventId,
                                    N_thread_id = notification.ThreadId,
                                    N_seen_pop = notification.SeenPop,
                                    N_type = notification.Type,
                                    N_type2 = notification.Type2,
                                    N_text = notification.Text,
                                    N_url = notification.Url,
                                    N_seen = notification.Seen,
                                    N_time = notification.Time,
                                    N_type_text = notification.TypeText,
                                    N_icon = notification.Icon,
                                    N_time_text_string = notification.TimeTextString,
                                    N_time_text = notification.TimeText,
                                    N_username = username,
                                    N_avatar = Functions.Get_image(notification.NotifierId, AvatarSplit, avatar),
                                    N_Type_icon = GetIconFontAwesom(notification.Type),
                                    N_Color_icon = GetColorFontAwesom(notification.Type)
                                };

                                if (notification.Type == "reaction")
                                    n = ReactionNotification(n);
                                else if (notification.Type == "poke")
                                    n.N_type_text = notification.Type;
                                else if (notification.Type == "viewed_story")
                                    n.N_type_text = notification.Type.Replace("_", " ");

                                if (ModeDarkstlye)
                                {
                                    n.S_Color_Background = "#232323";
                                    n.S_Color_Foreground = "#efefef";
                                }
                                else
                                {
                                    n.S_Color_Background = "#ffff";
                                    n.S_Color_Foreground = "#444";
                                }

                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                {
                                    ListNotifications.Add(n);
                                });

                                Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                                {
                                    if (this.WindowState == WindowState.Minimized)
                                    {
                                        if (ListNotifications.Count() > 0)
                                        {
                                            if (Chk_Desktop_Notifications.IsChecked == true)
                                            {
                                                MsgPopupWindow PopUp = new MsgPopupWindow(notification.TypeText,
                                                    username, avatar, notification.NotifierId);

                                                PopUp.Activate();
                                                PopUp.Show();
                                                PopUp.Activate();

                                                var workingAreaaa = SystemParameters.WorkArea;
                                                var transform =
                                                    PresentationSource.FromVisual(PopUp)
                                                        .CompositionTarget.TransformFromDevice;
                                                var corner =
                                                    transform.Transform(
                                                        new Point(workingAreaaa.Right, workingAreaaa.Bottom));

                                                PopUp.Left = corner.X - PopUp.ActualWidth - 10;
                                                PopUp.Top = corner.Y - PopUp.ActualHeight;
                                            }
                                        }
                                    }
                                }));
                            }
                        }

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private Classes.Notifications ReactionNotification(Classes.Notifications notification)
        {
            notification.N_type_text = notification.N_type + " " + notification.N_type2;

            switch (notification.N_type2)
            {
                case "Like":
                    notification.N_Type_icon = "ThumbUp";
                    notification.N_Color_icon = "#FCC414";
                    break;
                case "Love":
                    notification.N_Type_icon = "Heart";
                    notification.N_Color_icon = "#E82722";
                    break;
                case "HaHa":
                    notification.N_Type_icon = "Emoticon";
                    notification.N_Color_icon = "#FCC414";
                    break;
                case "WoW":
                    notification.N_Type_icon = "EmoticonTongue";
                    notification.N_Color_icon = "#000000";
                    break;
                case "Sad":
                    notification.N_Type_icon = "EmoticonSad";
                    notification.N_Color_icon = "#FCC414";
                    break;
                case "Angry":
                    notification.N_Type_icon = "EmoticonNeutral";
                    notification.N_Color_icon = "#FCC414";
                    break;

                default:
                    notification.N_Type_icon = "Blur";
                    break;
            }

            return notification;

        }

        public static string GetIconFontAwesom(string Type)
        {
            try
            {
                var Type_icon = "";

                if (Type == "following")
                {
                    Type_icon = "AccountMultiplePlus";
                    return Type_icon;
                }
                else if (Type == "comment" || Type == "comment_reply" || Type == "also_replied")
                {
                    Type_icon = "Comment";
                    return Type_icon;
                }
                else if (Type == "liked_post" || Type == "liked_comment" || Type == "liked_reply_comment" ||
                         Type == "liked_page")
                {
                    Type_icon = "ThumbUp";
                    return Type_icon;
                }
                else if (Type == "wondered_post" || Type == "wondered_comment" || Type == "wondered_reply_comment" ||
                         Type == "exclamation-circle")
                {
                    Type_icon = "Information";
                    return Type_icon;
                }
                else if (Type == "comment_mention" || Type == "comment_reply_mention")
                {
                    Type_icon = "Tag";
                    return Type_icon;
                }
                else if (Type == "post_mention")
                {
                    Type_icon = "Bullseye";
                    return Type_icon;
                }
                else if (Type == "share_post")
                {
                    Type_icon = "Share";
                    return Type_icon;
                }
                else if (Type == "profile_wall_post")
                {
                    Type_icon = "FormatListBulleted";
                    return Type_icon;
                }
                else if (Type == "visited_profile")
                {

                    Type_icon = "Eye";
                    return Type_icon;
                }
                else if (Type == "joined_group" || Type == "accepted_invite" || Type == "accepted_request" ||
                         Type == "accepted_join_request")
                {

                    Type_icon = "Check";
                    return Type_icon;
                }
                else if (Type == "invited_page")
                {

                    Type_icon = "Flag";
                    return Type_icon;
                }

                else if (Type == "added_you_to_group")
                {

                    Type_icon = "Adjust";
                    return Type_icon;
                }
                else if (Type == "requested_to_join_group")
                {

                    Type_icon = "Clock";
                    return Type_icon;
                }
                else if (Type == "thumbs-down")
                {
                    Type_icon = "ThumbDown";
                    return Type_icon;
                }
                else if (Type == "poke")
                {
                    Type_icon = "HandPointingRight";
                    return Type_icon;
                }
                else if (Type == "viewed_story")
                {
                    Type_icon = "Image";
                    return Type_icon;
                }

                else
                {
                    Type_icon = "ArrowRightDropCircleOutline";
                    return Type_icon;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return "";
            }
        }

        public static string GetColorFontAwesom(string Type)
        {
            try
            {
                var Icon_Color_FO = "#444";

                if (ModeDarkstlye)
                {
                    Icon_Color_FO = "#ffff";
                    return Icon_Color_FO;
                }
                else
                {
                    if (Type == "following")
                    {
                        Icon_Color_FO = "#444";
                        return Icon_Color_FO;
                    }
                    else if (Type == "comment" || Type == "comment_reply" || Type == "also_replied")
                    {
                        Icon_Color_FO = Settings.Main_Color;
                        return Icon_Color_FO;
                    }
                    else if (Type == "liked_post" || Type == "liked_comment" || Type == "liked_reply_comment")
                    {
                        Icon_Color_FO = Settings.Main_Color;
                        return Icon_Color_FO;
                    }
                    else if (Type == "wondered_post" || Type == "wondered_comment" ||
                             Type == "wondered_reply_comment" ||
                             Type == "exclamation-circle")
                    {
                        Icon_Color_FO = "#FFA500";
                        return Icon_Color_FO;
                    }
                    else if (Type == "comment_mention" || Type == "comment_reply_mention")
                    {
                        Icon_Color_FO = "#B20000";
                        return Icon_Color_FO;
                    }
                    else if (Type == "post_mention")
                    {
                        Icon_Color_FO = "#B20000";
                        return Icon_Color_FO;
                    }
                    else if (Type == "share_post")
                    {
                        Icon_Color_FO = "#2F2FFF";
                        return Icon_Color_FO;
                    }
                    else if (Type == "profile_wall_post")
                    {
                        Icon_Color_FO = "#444";
                        return Icon_Color_FO;
                    }
                    else if (Type == "visited_profile")
                    {
                        Icon_Color_FO = "#328432";
                        return Icon_Color_FO;
                    }
                    else if (Type == "liked_page")
                    {
                        Icon_Color_FO = "#2F2FFF";
                        return Icon_Color_FO;
                    }
                    else if (Type == "joined_group" || Type == "accepted_invite" || Type == "accepted_request")
                    {
                        Icon_Color_FO = "#2F2FFF";
                        return Icon_Color_FO;
                    }
                    else if (Type == "invited_page")
                    {
                        Icon_Color_FO = "#B20000";
                        return Icon_Color_FO;
                    }
                    else if (Type == "accepted_join_request")
                    {
                        Icon_Color_FO = "#2F2FFF";
                        return Icon_Color_FO;
                    }
                    else if (Type == "added_you_to_group")
                    {
                        Icon_Color_FO = "#444";
                        return Icon_Color_FO;
                    }
                    else if (Type == "requested_to_join_group")
                    {
                        Icon_Color_FO = Settings.Main_Color;
                        return Icon_Color_FO;
                    }
                    else if (Type == "thumbs-down")
                    {
                        Icon_Color_FO = Settings.Main_Color;
                        return Icon_Color_FO;
                    }
                    else if (Type == "poke")
                    {
                        Icon_Color_FO = "#FCC414";
                        return Icon_Color_FO;
                    }
                    else
                    {
                        Icon_Color_FO = "#444";
                        return Icon_Color_FO;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return "";
            }
        }

        private void Notifications_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.Notifications selectedGroup = (Classes.Notifications)Notifications_list.SelectedItem;

                if (selectedGroup != null)
                {
                    Process.Start(selectedGroup.N_url);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Open Window : Notifications Control
        private void NotificationsControlButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NotificationsControlWindow NotificationsControlWindow = new NotificationsControlWindow(IDuser);
                NotificationsControlWindow.Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Pro Users ##########################

        #region Pro Users

        // Functions Pro Users
        public void Pro_Users(List<GetUserListObject.ProUser> proUsers)
        {
            try
            {
                if (internetconection)
                {


                    if (proUsers != null)
                    {

                        foreach (var proUser in proUsers)
                        {

                            var check = ListProUsers.FirstOrDefault(a => a.P_user_id == proUser.UserId);
                            if (check != null)
                            {

                            }
                            else
                            {
                                Classes.Pro_users p = new Classes.Pro_users();
                                p.P_user_id = proUser.UserId;
                                p.P_username = proUser.Username;
                                p.P_first_name = proUser.FirstName;
                                p.P_last_name = proUser.LastName;
                                p.P_avatar = proUser.Avatar;
                                p.P_url = proUser.Url;

                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                {
                                    ListProUsers.Add(p);
                                });
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //Event Selection Changed in Pro Users
        private void ProUsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    Classes.Pro_users selectedGroup = (Classes.Pro_users)ProUsersList.SelectedItem;

                    if (selectedGroup != null)
                    {
                        Process.Start(selectedGroup.P_url);
                    }
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Trending Hashtags ##########################

        #region Trending Hashtags

        // Functions Trending hashtag
        public void Trending_hashtag(List<GetUserListObject.TrendingHashtag> trendingHashtags)
        {
            try
            {
                if (internetconection)
                {
                    if (trendingHashtags != null)
                    {
                        foreach (var trendingHashtag in trendingHashtags)
                        {

                            var check = ListHashtag.FirstOrDefault(a => a.T_id == trendingHashtag.Id);
                            if (check != null)
                            {

                            }
                            else
                            {
                                Classes.Trending_hashtag t = new Classes.Trending_hashtag();
                                t.T_id = trendingHashtag.Id;
                                t.T_hash = trendingHashtag.Hash;
                                t.T_tag = "#" + trendingHashtag.Tag;
                                t.T_last_trend_time = trendingHashtag.LastTrendTime;
                                t.T_trend_use_num = trendingHashtag.TrendUseNum;
                                t.T_url = trendingHashtag.Url;

                                if (ModeDarkstlye)
                                {
                                    t.S_Color_Background = "#232323";
                                    t.S_Color_Foreground = "#efefef";
                                }
                                else
                                {
                                    t.S_Color_Background = "#ffff";
                                    t.S_Color_Foreground = "#444";
                                }

                                App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                {
                                    ListHashtag.Add(t);
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Selection Changed in Hashtag ist
        private void Hashtag_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    Classes.Trending_hashtag selectedGroup = (Classes.Trending_hashtag)Hashtag_list.SelectedItem;

                    if (selectedGroup != null)
                    {
                        Process.Start(selectedGroup.T_url);
                    }
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Block User  ##########################

        #region Block User

        //Click Button Open Window : BlockedUsers
        private void Btn_Block_User_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UsersBlocked_Window b = new UsersBlocked_Window(IDuser, ID_From, ID_To, this);
                b.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Delete Messages ##########################

        #region Delete Messages

        // Functions Delete Messages


        // Run background worker :  delete messages
        private async void DeleteMessages_Async()
        {
            try
            {
                if (internetconection)
                {
                    await Task.Run(async () =>
                    {
                        var response = await RequestsAsync.Delete_Conversation(UserDetails.User_id, IDuser);

                        if (response.Item1 == 200)
                        {
                            if (response.Item2 is DeleteConversationObject result)
                            {

                                SQLiteCommandSender.removeUser_All_Table(IDuser, ID_From, ID_To);

                                Functions.Delete_dataFile_user(IDuser);

                                var delete = ListUsers.FirstOrDefault(a => a.U_Id == IDuser);
                                if (delete != null)
                                {
                                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                    {
                                        ListUsers.Remove(delete);
                                        ListMessages.Clear();
                                        ListSharedFiles.Clear();
                                        ListUsersProfile.Clear();

                                        //Scroll Top >> 
                                        ChatActivityList.SelectedIndex = 0;

                                        ChatActivityList.ScrollIntoView(ChatActivityList.SelectedItem);
                                        RightMainPanel.Visibility = Visibility.Collapsed;
                                    });
                                }

                            }
                        }
                    });

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }


        }


        //Click Button Delete Messages
        private void Btn_delete_chat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    DeleteMessages_Async();
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## TOP Header ##########################

        #region TOP Header

        //Click ListBoxItem View Profile using URL
        private void View_Profile_ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    ListBoxItem lbi = e.Source as ListBoxItem;
                    if (lbi != null)
                    {
                        Process.Start(UserPofilURl);
                    }
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch
            {

            }
        }

        //Click ListBoxItem Block User
        private void Block_User_ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBoxItem lbi = e.Source as ListBoxItem;
                if (lbi != null)
                {
                    if (ChatActivityList.SelectedItem != null)
                    {
                        UsersBlocked_Window b = new UsersBlocked_Window(IDuser, ID_From, ID_To, this);
                        b.ShowDialog();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click ListBoxItem Delete Messages
        private void Delete_conversation_ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    ListBoxItem lbi = e.Source as ListBoxItem;
                    if (lbi != null)
                    {
                        DeleteMessages_Async();
                    }
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Profile Toggle
        private void ProfileToggle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RightMainPanel.Visibility == Visibility.Collapsed)
                {
                    RightMainPanel.Visibility = Visibility.Visible;
                    DropDownMenueOnMessageBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    RightMainPanel.Visibility = Visibility.Collapsed;
                    DropDownMenueOnMessageBox.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Settings ##########################

        #region Settings

        //Functions Get user data setting 
        private void getsettingstable()
        {
            try
            {
                state_Check = "0";
                var setting = SQLiteCommandSender.GetUsersSettings();
                if (setting != null)
                {
                    Chk_Desktop_Notifications.IsChecked = Convert.ToBoolean(setting.S_NotificationDesktop);
                    Chk_Play_sound.IsChecked = Convert.ToBoolean(setting.S_NotificationPlaysound);
                    Background_Chat.Source = new BitmapImage(new Uri(setting.S_BackgroundChats_images));
                    ImageBrush_chat.ImageSource = new BitmapImage(new Uri(setting.S_BackgroundChats_images));
                    Btn_SwitchDarkMode.IsChecked = Convert.ToBoolean(setting.DarkMode);
                }

                Settings.NotificationDesktop = setting.S_NotificationDesktop;
                Settings.NotificationPlaysound = setting.S_NotificationPlaysound;
                Settings.Lang_Resources = setting.Lang_Resources;


                if (setting.Lang_Resources == "")
                {
                    Sel_language.SelectedIndex = 0;
                }

                if (setting.Lang_Resources == "en-US")
                {
                    Sel_language.SelectedIndex = 0;
                }
                else if (setting.Lang_Resources == "ar")
                {
                    Sel_language.SelectedIndex = 1;
                }
                else if (setting.Lang_Resources == "de-DE")
                {
                    Sel_language.SelectedIndex = 2;
                }
                else if (setting.Lang_Resources == "nl-NL")
                {
                    Sel_language.SelectedIndex = 3;
                }
                else if (setting.Lang_Resources == "fr-FR")
                {
                    Sel_language.SelectedIndex = 4;
                }
                else if (setting.Lang_Resources == "it-IT")
                {
                    Sel_language.SelectedIndex = 5;
                }
                else if (setting.Lang_Resources == "pt-BR")
                {
                    Sel_language.SelectedIndex = 6;
                }
                else if (setting.Lang_Resources == "ru-RU")
                {
                    Sel_language.SelectedIndex = 7;
                }
                else if (setting.Lang_Resources == "id-ID")
                {
                    Sel_language.SelectedIndex = 8;
                }
                else if (setting.Lang_Resources == "es-ES")
                {
                    Sel_language.SelectedIndex = 9;
                }
                else if (setting.Lang_Resources == "tr-TR")
                {
                    Sel_language.SelectedIndex = 10;
                }

                if (setting.DarkMode == "true")
                {
                    ModeDark_MainWindow();
                    ModeDarkstlye = true;
                }

                state_Check = "1";
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Checked Desktop Notifications true
        private void Chk_Desktop_Notifications_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (state_Check == "1")
                {
                    var s = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                    s.S_NotificationDesktop = "true";
                    Settings.NotificationDesktop = "true";
                    SQLiteCommandSender.Update_SettingsTable(s);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Checked Desktop Notifications false
        private void Chk_Desktop_Notifications_OnUnchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (state_Check == "1")
                {
                    var s = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                    s.S_NotificationDesktop = "false";
                    Settings.NotificationDesktop = "false";
                    SQLiteCommandSender.Update_SettingsTable(s);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Checked Play sound true
        private void Chk_Play_sound_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (state_Check == "1")
                {
                    var s = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                    s.S_NotificationPlaysound = "true";
                    Settings.NotificationPlaysound = "true";
                    SQLiteCommandSender.Update_SettingsTable(s);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Checked Play sound false
        private void Chk_Play_sound_OnUnchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (state_Check == "1")
                {
                    var s = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                    s.S_NotificationPlaysound = "false";
                    Settings.NotificationPlaysound = "false";
                    SQLiteCommandSender.Update_SettingsTable(s);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Selection Connaction language
        private void Sel_language_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (state_Check == "1")
                {
                    string culture = ((ComboBoxItem)this.Sel_language.SelectedValue).Tag as string;
                    Change_language(culture);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Functions Change language
        public void Change_language(string culture)
        {
            try
            {
                LocalResources.Culture = new CultureInfo(culture);
                ////
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

                //Update DataBase
                var s = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                s.Lang_Resources = culture;
                Settings.Lang_Resources = culture;
                SQLiteCommandSender.Update_SettingsTable(s);

                System.Windows.Forms.Application.Restart();
                Application.Current.Shutdown();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Checked Dark Mode >> On
        private void Btn_SwitchDarkMode_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (state_Check == "1")
                {
                    ModeDark_MainWindow();
                    var s = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                    s.DarkMode = "true";
                    ModeDarkstlye = true;
                    SQLiteCommandSender.Update_SettingsTable(s);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Unchecked Dark Mode >> Off
        private void Btn_SwitchDarkMode_OnUnchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (state_Check == "1")
                {
                    ModeLigth_MainWindow();
                    var s = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                    s.DarkMode = "false";
                    ModeDarkstlye = false;
                    SQLiteCommandSender.Update_SettingsTable(s);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Create OpenFileDialog Choose image Background Chat
        private void Lnk_Choose_file_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create OpenFileDialog 
                OpenFileDialog open = new OpenFileDialog();

                // Set filter for file extension and default file extension 
                open.DefaultExt = ".png";
                open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";

                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = open.ShowDialog();

                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document and image file path
                    string filename = open.FileName;

                    // display image in picture box  
                    Background_Chat.Source = new BitmapImage(new Uri(filename));
                    ImageBrush_chat.ImageSource = new BitmapImage(new Uri(filename));

                    var s = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                    s.S_BackgroundChats_images = filename;
                    Settings.BackgroundChats_images = filename;
                    SQLiteCommandSender.Update_SettingsTable(s);
                }
                else
                {
                    // Background_Chat.Source = new BitmapImage(new Uri("/Images/Backgrounds/emoji-background.png")); 
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Link Open Window : Setting_Stickers
        private void Lnk_sticker_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Setting_Stickers_Window settingStickers = new Setting_Stickers_Window(this);
                settingStickers.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Link Open Window :  Manage local strong
        private void Lnk_Manage_local_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Managelocalstrong_Window mls = new Managelocalstrong_Window();
                mls.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Link Open Window : About
        private void Lnk_About_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //About_Window about = new About_Window();
                //about.ShowDialog();
                var urlcontact = WoWonderClient.Client.WebsiteUrl + "/terms/about-us";
                Process.Start(urlcontact);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Link Open browser : url page Contact Us
        private void Lnk_Contact_Us_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (internetconection)
                {
                    var urlcontact = WoWonderClient.Client.WebsiteUrl + "/contact-us";
                    Process.Start(urlcontact);
                }
                else
                {
                    MessageBox.Show(LocalResources.label_Please_check_your_internet);
                }
            }
            catch
            {

            }
        }

        //Click Button logout 
        private void Logout_Button_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {

                if (internetconection)
                {
                    SQLite_Entity.DropAll();
                    Task.Run(() =>
                    {
                        Functions.clearFolder();
                        RequestsAsync.User_Logout_Http(UserDetails.User_id).ConfigureAwait(false);
                    });
                }


                System.Windows.Forms.Application.Restart();
                Application.Current.Shutdown();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Functions ##########################

        #region Functions

        public void ConvertMessageColors(Users Useract)
        {
            try
            {
                if (Useract.U_chat_color != Settings.Main_Color)
                {
                    if (Useract.U_chat_color.Contains("rgb"))
                    {
                        try
                        {
                            var regex = new Regex(@"([0-9]+)");
                            string colorData = Useract.U_chat_color;

                            var matches = regex.Matches(colorData);

                            var color_r = Convert.ToInt32(matches[0].ToString());
                            var color_g = Convert.ToInt32(matches[1].ToString());
                            var color_b = Convert.ToInt32(matches[2].ToString());

                            Useract.U_chat_color = HexFromRGB(color_r, color_g, color_b);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                        }
                    }

                    var ChatPanelColor = (Color)ColorConverter.ConvertFromString(Useract.U_chat_color);

                    if (Settings.Change_ChatPanelColor)
                    {
                        ChatInfoPanel.Background = new SolidColorBrush(ChatPanelColor);
                    }

                    var ChatForegroundColor = (Color)ColorConverter.ConvertFromString("#ffff");

                    ProfileToggle.Background = new SolidColorBrush(ChatPanelColor);
                    ProfileToggle.Foreground = new SolidColorBrush(ChatForegroundColor);
                    DropDownMenueOnMessageBox.Foreground = new SolidColorBrush(ChatForegroundColor);
                    ChatTitleChange.Foreground = new SolidColorBrush(ChatForegroundColor);
                    ChatSeen.Foreground = new SolidColorBrush(ChatForegroundColor);
                }
                else
                {
                    var ChatPanelColor = (Color)ColorConverter.ConvertFromString(Settings.Main_Color);
                    var ChatForegroundColor = (Color)ColorConverter.ConvertFromString("#ffff");

                    if (Settings.Change_ChatPanelColor)
                    {
                        ChatInfoPanel.Background = new SolidColorBrush(ChatPanelColor);
                    }

                    ChatInfoPanel.Background = new SolidColorBrush(ChatPanelColor);
                    ProfileToggle.Background = new SolidColorBrush(ChatPanelColor);
                    ProfileToggle.Foreground = new SolidColorBrush(ChatForegroundColor);
                    DropDownMenueOnMessageBox.Foreground = new SolidColorBrush(ChatForegroundColor);
                    ChatTitleChange.Foreground = new SolidColorBrush(ChatForegroundColor);
                    ChatSeen.Foreground = new SolidColorBrush(ChatForegroundColor);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Functions convert color RGB to HEX
        private string HexFromRGB(int r, int g, int b)
        {
            var hex = ColorTranslator.FromHtml(String.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).Name.Remove(0, 2);
            return "#" + hex;
        }

        // Timer updated data every 8 seconds
        private void ChatActitvity_Timer(object sender, EventArgs e)
        {
            try
            {
                Task.Run(() => //This code runs on a new thread, control is returned to the caller on the UI thread.
                {
                    internetconection = Functions.CheckForInternetConnection();
                });
                // code goes here
                if (internetconection)
                {
                    LastseenUser.Content = LocalResources.label_Online;

                    //Chat Activity and Users Contact
                    Chat_Work_Async();


                    if (OfflineModeStackpanel.Visibility == Visibility.Visible)
                    {
                        OfflineModeStackpanel.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    foreach (var item in ListUsers)
                    {
                        item.S_Color_onof = "#C0C0C0"; //silver
                        item.U_lastseen_time_text = "";

                        item.App_Main_Later = Settings.Application_Name.Substring(0, 1);
                        ChatSeen.Text = item.U_lastseenWithoutCut;
                    }

                    foreach (var item in ListUsersContact)
                    {
                        item.UC_Color_onof = "#C0C0C0"; //silver
                        item.UC_lastseen_time_text = "";
                        item.UC_App_Main_Later = Settings.Application_Name.Substring(0, 1);
                    }

                    LastseenUser.Content = LocalResources.label_offline;
                    ProgressBar_Search_ChatActivty.Visibility = Visibility.Collapsed;
                    ProgressBar_Search_ChatActivty.IsIndeterminate = false;

                    Notifications_ProgressBar.Visibility = Visibility.Collapsed;
                    Notifications_ProgressBar.IsIndeterminate = false;

                    ProgressBar_UserContacts.Visibility = Visibility.Collapsed;
                    ProgressBar_UserContacts.IsIndeterminate = false;

                    OfflineModeStackpanel.Visibility = Visibility.Visible;

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // Timer updated messges every 5 seconds 
        private void Get_new_messges_Timer(object sender, EventArgs e)
        {
            try
            {
                // code goes here
                if (Functions.CheckForInternetConnection())
                {
                    bgd_Worker_MessageUpdater = new BackgroundWorker();
                    bgd_Worker_MessageUpdater.DoWork += Bgd_Worker_MessageUpdater_DoWork;
                    bgd_Worker_MessageUpdater.ProgressChanged += Bgd_Worker_MessageUpdater_ProgressChanged;
                    bgd_Worker_MessageUpdater.RunWorkerCompleted += Bgd_Worker_MessageUpdater_RunWorkerCompleted;
                    bgd_Worker_MessageUpdater.WorkerSupportsCancellation = true;
                    bgd_Worker_MessageUpdater.WorkerReportsProgress = true;
                    bgd_Worker_MessageUpdater.RunWorkerAsync();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Message Update
        private void Bgd_Worker_MessageUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Functions.CheckForInternetConnection())
                {
                    if (ListMessages.Count != 0)
                    {
                        var Updater = ListMessages.SingleOrDefault(d => d.Mes_Id == Messages_id);
                        if (Updater != null)
                        {
                            return;
                        }
                        else
                        {
                            LastMessageid = ListMessages.Last().Mes_Id;
                        }

                        var response = AsyncHelper.RunSync(() => RequestsAsync.Message_Update_Http(UserDetails.User_id, IDuser, "0", LastMessageid));

                        if (response.Item1 == 200)
                        {
                            if (response.Item2 is MessageUpdateObject result)
                            {

                                TypingStatus = result.Typing.ToString() == "1" ? "Typping" : "Normal";


                                if (result.Messages.Count == 0)
                                {
                                    LoadmoremessgaCount = "0";
                                    return;
                                }
                                else
                                {
                                    LoadmoremessgaCount = "1";
                                    Classes.Messages m = new Classes.Messages();
                                    foreach (var message in result.Messages)
                                    {

                                        //Variables
                                        var Color_box_message = "";

                                        if (message.Type == "right_text" || message.Type == "right_image" ||
                                            message.Type == "right_audio" ||
                                            message.Type == "right_video" || message.Type == "right_file" ||
                                            message.Type == "right_sticker" ||
                                            message.Type == "right_contact")
                                        {
                                            Color_box_message = ChatColor;
                                        }

                                        if (message.Type == "right_sticker" || message.Type == "left_sticker")
                                        {
                                            string text_name_sticker = message.Media.Split('_').Last();
                                            message.Media =
                                                Functions.Get_Sticker_messages(text_name_sticker, message.Media, m);
                                        }

                                        if (message.Type == "right_video" || message.Type == "left_video")
                                        {
                                            message.MediaFileName = Functions.Rename_Video(message.MediaFileName);
                                        }

                                        if (message.Type == "right_gif" || message.Type == "left_gif")
                                        {
                                            var resultGif = Path.ChangeExtension(message.Stickers, ".gif");
                                            string[] stringSeparators = new string[] { "/" };
                                            var name = resultGif.Split(stringSeparators, StringSplitOptions.None);
                                            var string_url = (name[2] + "/" + name[3]);
                                            var string_name = name[4] + ".gif";
                                            resultGif = "https://" +
                                                        string_url.Replace(string_url, "i.giphy.com/") +
                                                        string_name;
                                            message.MediaFileName =
                                                Functions.Get_Sticker_messages(string_name, resultGif, m);
                                        }

                                        if (message.Type == "right_contact" || message.Type == "left_contact")
                                        {
                                            string[] stringSeparators = new string[] { "&quot;" };
                                            var name = message.Text.Split(stringSeparators,
                                                StringSplitOptions.None);
                                            var string_name = name[3];
                                            var string_number = name[7];
                                            message.Text = string_name + "\r\n" + string_number;
                                        }

                                        var Type_Icon_File = "File";
                                        if (message.Type == "right_file" || message.Type == "left_file")
                                        {
                                            if (message.MediaFileName.EndsWith("rar") ||
                                                message.MediaFileName.EndsWith("RAR") ||
                                                message.MediaFileName.EndsWith("zip") ||
                                                message.MediaFileName.EndsWith("ZIP"))
                                            {
                                                Type_Icon_File = "ZipBox";
                                            }
                                            else if (message.MediaFileName.EndsWith("txt") ||
                                                     message.MediaFileName.EndsWith("TXT"))
                                            {
                                                Type_Icon_File = "NoteText";
                                            }
                                            else if (message.MediaFileName.EndsWith("docx") ||
                                                     message.MediaFileName.EndsWith("DOCX"))
                                            {
                                                Type_Icon_File = "FileWord";
                                            }
                                            else if (message.MediaFileName.EndsWith("doc") ||
                                                     message.MediaFileName.EndsWith("DOC"))
                                            {
                                                Type_Icon_File = "FileWord";
                                            }
                                            else if (message.MediaFileName.EndsWith("pdf") ||
                                                     message.MediaFileName.EndsWith("PDF"))
                                            {
                                                Type_Icon_File = "FilePdf";
                                            }

                                            if (message.MediaFileName.Length > 25)
                                            {
                                                message.MediaFileName =
                                                    Functions.SubStringCutOf(message.MediaFileName, 25) + "." +
                                                    message.MediaFileName.Split('.').Last();
                                            }
                                        }

                                        var check_extension = Functions.Check_FileExtension(message.MediaFileName);
                                        var Play_Visibility = "Collapsed";
                                        var Pause_Visibility = "Collapsed";
                                        var Progress_Visibility = "Collapsed";
                                        var Download_Visibility = "Visible";
                                        var Video_text_Visibility = "Visible";
                                        var Icon_File_Visibility = "Collapsed";
                                        var Hlink_Download_Visibility = "Visible";
                                        var Hlink_Open_Visibility = "Collapsed";

                                        if (check_extension == "Audio")
                                        {
                                            var checkforSound = Functions.Get_sound(IDuser, message.MediaFileName);
                                            if (checkforSound != "Not Found sound")
                                            {
                                                Play_Visibility = "Visible";
                                                Pause_Visibility = "Collapsed";
                                                Progress_Visibility = "Collapsed";
                                                Download_Visibility = "Collapsed";
                                            }
                                        }

                                        else if (check_extension == "Video")
                                        {
                                            var checkforSound = Functions.Get_Video(IDuser, message.MediaFileName);
                                            if (checkforSound != "Not Found vidoe")
                                            {
                                                Play_Visibility = "Visible";
                                                Progress_Visibility = "Collapsed";
                                                Download_Visibility = "Collapsed";
                                            }
                                        }
                                        else if (check_extension == "File")
                                        {
                                            var checkforSound = Functions.Get_file(IDuser, message.MediaFileName);
                                            if (checkforSound != "Not Found file")
                                            {
                                                Icon_File_Visibility = "Visible";
                                                Progress_Visibility = "Collapsed";
                                                Download_Visibility = "Collapsed";
                                                Hlink_Download_Visibility = "Collapsed";
                                                Hlink_Open_Visibility = "Visible";
                                            }
                                        }

                                        m.Mes_Id = message.Id;
                                        m.Mes_From_Id = message.FromId;
                                        m.Mes_To_Id = message.ToId;
                                        m.Mes_Text = message.Text;
                                        m.Mes_Media = message.Media;
                                        m.Mes_MediaFileName = message.MediaFileName;
                                        m.Mes_MediaFileNames = message.MediaFileNames;
                                        m.Mes_Time = message.Time;
                                        m.Mes_Seen = message.Seen;
                                        m.Mes_Deleted_one = message.DeletedOne;
                                        m.Mes_Deleted_two = message.DeletedTwo;
                                        m.Mes_Sent_push = message.SentPush;
                                        m.Mes_Notification_id = message.NotificationId;
                                        m.Mes_Type_two = message.TypeTwo;
                                        m.Mes_Time_text = message.TimeText;
                                        m.Mes_Position = message.Position;
                                        m.Mes_Type = message.Type;
                                        m.Mes_File_size = message.FileSize;
                                        m.Mes_User_avatar = message.MessageUser.Avatar;
                                        m.Mes_Stickers = message.Stickers;
                                        //style
                                        m.Color_box_message = Color_box_message;
                                        m.Img_user_message = Img_user_message;
                                        m.Progress_Visibility = Progress_Visibility;
                                        m.Download_Visibility = Download_Visibility;
                                        m.Play_Visibility = Play_Visibility;
                                        m.Pause_Visibility = Pause_Visibility;
                                        m.Icon_File_Visibility = Icon_File_Visibility;
                                        m.Hlink_Download_Visibility = Hlink_Download_Visibility;
                                        m.Hlink_Open_Visibility = Hlink_Open_Visibility;
                                        m.Type_Icon_File = Type_Icon_File;
                                        m.sound_slider_value = 0;
                                        m.Progress_Value = 0;
                                        m.sound_time = "";

                                        if (check_extension == "Image")
                                        {
                                            message.MediaFileName =
                                                Functions.Get_img_messages(IDuser, message.MediaFileName,
                                                    message.Media, m);
                                        }

                                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                                        {
                                            var check = ListMessages.FirstOrDefault(a => a.Mes_Id == message.Id);
                                            if (check == null)
                                            {
                                                ListMessages.Add(m);
                                                var index = ListUsers.IndexOf(ListUsers.Where(a => a.U_Id == IDuser)
                                                    .FirstOrDefault());
                                                if (index > -1)
                                                {
                                                    ListUsers.Move(index, 0);
                                                }
                                            }
                                        });

                                        // Insert data user in database
                                        SQLiteCommandSender.Insert_Or_Replace_Messages(ListMessages);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Bgd_Worker_MessageUpdater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void Bgd_Worker_MessageUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                var bw2 = new BackgroundWorker();
                bw2.DoWork += (o, args) => AddNewMessagetoCash();
                bw2.RunWorkerAsync();
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                {
                    if (bgd_Worker_MessageUpdater.WorkerSupportsCancellation == true)
                    {
                        bgd_Worker_MessageUpdater.CancelAsync();
                    }
                    try
                    {
                        if (TypingStatus == "Typping")
                        {
                            ChatSeen.Text = LocalResources.label_Typping;
                        }
                        else
                        {
                            if (ListUsers.FirstOrDefault(a => a.U_Id == IDuser).U_lastseen != "on")
                            {

                                ChatSeen.Text = LocalResources.label_last_seen + " " + ListUsers.FirstOrDefault(a => a.U_Id == IDuser).U_lastseen_time_text;
                            }
                            else
                            {
                                ChatSeen.Text = LocalResources.label_Online;
                            }

                        }
                        if (LoadmoremessgaCount == "0")
                        {
                            if (ChatMessgaeslistBox.SelectedItem != null)
                                LastMessageid = (ChatMessgaeslistBox.SelectedItem as Classes.Messages).Mes_Id;

                            bgd_Worker_MessageUpdater.RunWorkerAsync();

                            // ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                            // ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);

                            return;
                        }
                        else
                        {
                            ChatMessgaeslistBox.ItemsSource = ListMessages;

                            if (ChatMessgaeslistBox.SelectedItem != null)
                                LastMessageid = ListMessages.Last().Mes_Id;

                            if (bgd_Worker_MessageUpdater.WorkerSupportsCancellation == true)
                            {
                                bgd_Worker_MessageUpdater.CancelAsync();
                            }

                            bgd_Worker_MessageUpdater.RunWorkerAsync();

                            ListBoxAutomationPeer svAutomation = (ListBoxAutomationPeer)ScrollViewerAutomationPeer.CreatePeerForElement(ChatMessgaeslistBox);

                            ChatMessgaeslistBox.SelectedIndex = ChatMessgaeslistBox.Items.Count - 1;
                            ChatMessgaeslistBox.ScrollIntoView(ChatMessgaeslistBox.SelectedItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        private void WinMin_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login_Window.DisplayWorkthrothPages)
                {
                    WelcomePage ff = new WelcomePage();
                    ff.Activate();
                    ff.ShowDialog();
                    ff.Activate();
                }

                RightMainPanel.Visibility = Visibility.Collapsed;
                SendMessagepanel.Visibility = Visibility.Collapsed;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public async void getIcon()
        {
            try
            {
                await Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                    {
                        GifsListview.ItemsSource = SQLiteCommandSender.Get_To_GifsTable();
                        GetStickers();
                    });
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void ModeDark_MainWindow()
        {
            try
            {
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);
                var SelverBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.LigthBackground_Color);

                this.Background = new SolidColorBrush(DarkBackgroundColor);

                TabControl.Background = new SolidColorBrush(DarkBackgroundColor);
                TabControl.BorderBrush = new SolidColorBrush(DarkBackgroundColor);

                // MessagesTab >>> 
                MessagesTab_DockPanel.Background = new SolidColorBrush(SelverBackgroundColor);
                Txt_searchMessages.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                ChatActivityList.Background = new SolidColorBrush(DarkBackgroundColor);
                Magnify_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                // CallsTab >>>
                CallsTab_DockPanel.Background = new SolidColorBrush(DarkBackgroundColor);
                Calls_list.Background = new SolidColorBrush(DarkBackgroundColor);
                Grid_No_call.Background = new SolidColorBrush(DarkBackgroundColor);
                No_call.Background = new SolidColorBrush(DarkBackgroundColor);

                // ContactsTab >>>
                ContactsTab_DockPanel.Background = new SolidColorBrush(DarkBackgroundColor);
                SearchPanel.Background = new SolidColorBrush(SelverBackgroundColor);
                Txt_Search_BoxContacts.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                UserContacts_list.Background = new SolidColorBrush(DarkBackgroundColor);
                Btn_Refresh_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                MagnifyIcon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                No_No_Users_Contact.Background = new SolidColorBrush(DarkBackgroundColor);
                No_Users_ContactGrid.Background = new SolidColorBrush(DarkBackgroundColor);

                // ProfileTab >>>
                ProfileTab_Grid.Background = new SolidColorBrush(DarkBackgroundColor);
                UsernameLogin.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                LastseenUser.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                ProfileTab_ScrollViewer.Background = new SolidColorBrush(DarkBackgroundColor);
                Icon_Username.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Username.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Icon_Email.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Email.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Icon_Birthday.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Birthday.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Icon_Phone_number.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Phone_number.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Icon_Website.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Website.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Icon_Address.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Address.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Icon_School.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_School.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Icon_Gender.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Gender.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Facebook.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Google.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Twitter.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Youtube.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Linkedin.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Instagram.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Vk.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                // SearchTab >>> 
                SearchTab_DockPanel.Background = new SolidColorBrush(DarkBackgroundColor);
                SearshUserPanel.Background = new SolidColorBrush(SelverBackgroundColor);
                Txt_SearchBoxOnline.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                btn_SearshUser.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                SearshUser_list.Background = new SolidColorBrush(DarkBackgroundColor);
                No_Search_User_Panel.Background = new SolidColorBrush(DarkBackgroundColor);

                // NotificationsTab >>>
                NotificationsTab_Grid.Background = new SolidColorBrush(SelverBackgroundColor);
                NotificationsTab_DockPanel.Background = new SolidColorBrush(DarkBackgroundColor);
                Title_Notifications.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Notifications.Background = new SolidColorBrush(DarkBackgroundColor);
                Notifications_list.Background = new SolidColorBrush(DarkBackgroundColor);

                proUsers.Background = new SolidColorBrush(DarkBackgroundColor);
                Title_proUsers.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_proUsers.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                ProUsersList.Background = new SolidColorBrush(DarkBackgroundColor);

                HashTag.Background = new SolidColorBrush(DarkBackgroundColor);
                Title_Trending_HashTags.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Trending_HashTags.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Hashtag_list.Background = new SolidColorBrush(DarkBackgroundColor);
                Hashtag_list_Border.Background = new SolidColorBrush(DarkBackgroundColor);

                // SettingsTab >> 
                SettingsTab_Grid.Background = new SolidColorBrush(SelverBackgroundColor);
                SettingsTab_DockPanel.Background = new SolidColorBrush(DarkBackgroundColor);
                S_Settings_Notifications.Background = new SolidColorBrush(DarkBackgroundColor);
                Title_S_Notifications.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Settings_Notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Chk_Desktop_Notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Chk_Play_sound.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                S_General_Notifications.Background = new SolidColorBrush(DarkBackgroundColor);
                Title_S_General_Notifications.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Settings_General.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Change_language.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Sel_language.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_en.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_ar.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_de.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_nl.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_fr.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_it.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_br.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_ru.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_id.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_es.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Item_tr.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                Lbl_Night_Mode.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Chk_Desktop_Notifications.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                S_Chat_Settings.Background = new SolidColorBrush(DarkBackgroundColor);
                Title_S_Chat_Settings.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Settings_Chat.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Image_size.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                S_Advanced_Settings.Background = new SolidColorBrush(DarkBackgroundColor);
                Title_Advanced_Settings.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Settings_Advanced.Foreground = new SolidColorBrush(WhiteBackgroundColor);


                // Search Message Panel >>>
                SearchMessagePanel.Background = new SolidColorBrush(DarkBackgroundColor);
                SearchMessagePanel.BorderBrush = new SolidColorBrush(DarkBackgroundColor);
                SearchMsgUpButton.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                SearchMsgDownButton.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                IconsearchMessages.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                MSG_searchMessages.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                CountSearch.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                IconCloseSearch.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                // Right Main Panel >>>
                RightMain_StackPanel.Background = new SolidColorBrush(SelverBackgroundColor);
                Profile_Border.Background = new SolidColorBrush(DarkBackgroundColor);
                ProfileUserName.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                ViewProfileButton.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                Title_Options.Background = new SolidColorBrush(DarkBackgroundColor);
                Options_Grid.Background = new SolidColorBrush(SelverBackgroundColor);
                Lbl_Options.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                SearchMesageButton.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                ChangeColorButton.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                NotificationsControlButton.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Btn_Block_User.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                UserFriendsSection.Background = new SolidColorBrush(SelverBackgroundColor);
                Title_Userlocation.Background = new SolidColorBrush(DarkBackgroundColor);
                UserlocationSection.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                location_Border.Background = new SolidColorBrush(SelverBackgroundColor);
                Lbl_location.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                UserAboutSection.Background = new SolidColorBrush(SelverBackgroundColor);
                Title_About.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_About.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                About_Border.Background = new SolidColorBrush(SelverBackgroundColor);
                ProfileUserAbout.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                UserMediaSection.Background = new SolidColorBrush(SelverBackgroundColor);
                Title_UserMediaSection.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Media_And_Files.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                FilesListview.Background = new SolidColorBrush(DarkBackgroundColor);

                UserDeleteChatSection.Background = new SolidColorBrush(DarkBackgroundColor);
                Btn_delete_chat.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                RightPanel.Background = new SolidColorBrush(DarkBackgroundColor);

                // Chat Text Box >>>
                DockPanelLeft_Grid.Background = new SolidColorBrush(DarkBackgroundColor);
                SendMessagepanel.Background = new SolidColorBrush(DarkBackgroundColor);
                SendMessagepanel.BorderBrush = new SolidColorBrush(DarkBackgroundColor);
                MessageBoxText.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                MessageBoxText.Background = new SolidColorBrush(DarkBackgroundColor);
                EmojiSmileofSmileButton.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Btn_LoadmoreMessages.Background = new SolidColorBrush(DarkBackgroundColor);

                // imogiPanel >>>
                imogiPanel.Background = new SolidColorBrush(DarkBackgroundColor);

                fontweight.Background = new SolidColorBrush(DarkBackgroundColor);
                fontweight_Panel.Background = new SolidColorBrush(DarkBackgroundColor);
                fontsize.Background = new SolidColorBrush(DarkBackgroundColor);

                Stickers_Grid.Background = new SolidColorBrush(DarkBackgroundColor);
                SpoiledRabbit_Label.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Cutewaterdrop_Label.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Monstor_Label.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                NINJA_Nyankko_Label.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                So_Much_Love_Label.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                SukkaraChan_Label.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Flower_Hijab_Label.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Trendy_boy_Label.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                gift_Grid.Background = new SolidColorBrush(DarkBackgroundColor);
                searchGifs_Icon.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Txt_searchGifs.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                // ListBoxItem >> ChatActivityList
                if (ListUsers.Count > 0)
                {
                    foreach (var Items in ListUsers)
                    {
                        Items.S_Color_Background = "#232323";
                        Items.S_Color_Foreground = "#efefef";
                    }
                }

                // ListBoxItem >> ListUsersContact
                if (ListUsersContact.Count > 0)
                {
                    foreach (var Items in ListUsersContact)
                    {
                        Items.S_Color_Background = "#232323";
                        Items.S_Color_Foreground = "#efefef";
                    }
                }

                // ListBoxItem >> ListCall
                if (ListCall.Count > 0)
                {
                    foreach (var Items in ListCall)
                    {
                        Items.S_Color_Background = "#232323";
                        Items.S_Color_Foreground = "#efefef";
                    }
                }

                // ListBoxItem >> ListSearsh
                if (ListSearsh.Count > 0)
                {
                    foreach (var Items in ListSearsh)
                    {
                        Items.S_Color_Background = "#232323";
                        Items.S_Color_Foreground = "#efefef";
                    }
                }

                // ListBoxItem >> ListNotifications
                if (ListNotifications.Count > 0)
                {
                    foreach (var Items in ListNotifications)
                    {
                        Items.S_Color_Background = "#232323";
                        Items.S_Color_Foreground = "#efefef";
                    }
                }

                // ListBoxItem >> ListHashtag
                if (ListHashtag.Count > 0)
                {
                    foreach (var Items in ListHashtag)
                    {
                        Items.S_Color_Background = "#232323";
                        Items.S_Color_Foreground = "#efefef";
                    }
                }

                _GridWindow.Background = new SolidColorBrush(DarkBackgroundColor);
                _TitleApp.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                _ButtonWindow_Min.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                _ButtonWindow_Max.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                _ButtonWindow_x.Foreground = new SolidColorBrush(WhiteBackgroundColor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ModeLigth_MainWindow()
        {
            try
            {
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackgroundColor);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackgroundColor);
                var SelverBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.LigthBackgroundColor);

                this.Background = new SolidColorBrush(WhiteBackgroundColor);

                TabControl.Background = new SolidColorBrush(WhiteBackgroundColor);
                TabControl.BorderBrush = new SolidColorBrush(WhiteBackgroundColor);

                // MessagesTab >>> 
                MessagesTab_DockPanel.Background = new SolidColorBrush(SelverBackgroundColor);
                Txt_searchMessages.Foreground = new SolidColorBrush(DarkBackgroundColor);
                ChatActivityList.Background = new SolidColorBrush(WhiteBackgroundColor);
                Magnify_Icon.Foreground = new SolidColorBrush(DarkBackgroundColor);

                // CallsTab >>>
                CallsTab_DockPanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                Calls_list.Background = new SolidColorBrush(WhiteBackgroundColor);
                Grid_No_call.Background = new SolidColorBrush(WhiteBackgroundColor);
                No_call.Background = new SolidColorBrush(WhiteBackgroundColor);

                // ContactsTab >>>
                ContactsTab_DockPanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                SearchPanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                Txt_Search_BoxContacts.Foreground = new SolidColorBrush(DarkBackgroundColor);
                UserContacts_list.Background = new SolidColorBrush(WhiteBackgroundColor);
                Btn_Refresh_Icon.Foreground = new SolidColorBrush(DarkBackgroundColor);
                MagnifyIcon.Foreground = new SolidColorBrush(DarkBackgroundColor);
                No_No_Users_Contact.Background = new SolidColorBrush(WhiteBackgroundColor);
                No_Users_ContactGrid.Background = new SolidColorBrush(WhiteBackgroundColor);

                // ProfileTab >>>
                ProfileTab_Grid.Background = new SolidColorBrush(WhiteBackgroundColor);
                UsernameLogin.Foreground = new SolidColorBrush(DarkBackgroundColor);
                LastseenUser.Foreground = new SolidColorBrush(DarkBackgroundColor);
                ProfileTab_ScrollViewer.Background = new SolidColorBrush(WhiteBackgroundColor);
                Icon_Username.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Username.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Icon_Email.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Email.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Icon_Birthday.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Birthday.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Icon_Phone_number.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Phone_number.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Icon_Website.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Website.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Icon_Address.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Address.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Icon_School.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_School.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Icon_Gender.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Gender.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Facebook.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Google.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Twitter.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Youtube.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Linkedin.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Instagram.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Vk.Foreground = new SolidColorBrush(DarkBackgroundColor);

                // SearchTab >>> 
                SearchTab_DockPanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                SearshUserPanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                Txt_SearchBoxOnline.Foreground = new SolidColorBrush(DarkBackgroundColor);
                btn_SearshUser.Foreground = new SolidColorBrush(DarkBackgroundColor);
                SearshUser_list.Background = new SolidColorBrush(WhiteBackgroundColor);
                No_Search_User_Panel.Background = new SolidColorBrush(WhiteBackgroundColor);

                // NotificationsTab >>>
                NotificationsTab_Grid.Background = new SolidColorBrush(SelverBackgroundColor);
                NotificationsTab_DockPanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                Title_Notifications.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Notifications.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Notifications.Background = new SolidColorBrush(WhiteBackgroundColor);
                Notifications_list.Background = new SolidColorBrush(WhiteBackgroundColor);

                proUsers.Background = new SolidColorBrush(WhiteBackgroundColor);
                Title_proUsers.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_proUsers.Foreground = new SolidColorBrush(DarkBackgroundColor);
                ProUsersList.Background = new SolidColorBrush(WhiteBackgroundColor);

                HashTag.Background = new SolidColorBrush(WhiteBackgroundColor);
                Title_Trending_HashTags.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Trending_HashTags.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Hashtag_list.Background = new SolidColorBrush(WhiteBackgroundColor);
                Hashtag_list_Border.Background = new SolidColorBrush(WhiteBackgroundColor);

                // SettingsTab >> 
                SettingsTab_Grid.Background = new SolidColorBrush(SelverBackgroundColor);
                SettingsTab_DockPanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                S_Settings_Notifications.Background = new SolidColorBrush(WhiteBackgroundColor);
                Title_S_Notifications.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Settings_Notifications.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Chk_Desktop_Notifications.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Chk_Play_sound.Foreground = new SolidColorBrush(DarkBackgroundColor);

                S_General_Notifications.Background = new SolidColorBrush(WhiteBackgroundColor);
                Title_S_General_Notifications.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Settings_General.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Change_language.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Sel_language.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_en.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_ar.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_de.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_nl.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_fr.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_it.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_br.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_ru.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_id.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_es.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Item_tr.Foreground = new SolidColorBrush(DarkBackgroundColor);

                Lbl_Night_Mode.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Chk_Desktop_Notifications.Foreground = new SolidColorBrush(DarkBackgroundColor);

                S_Chat_Settings.Background = new SolidColorBrush(WhiteBackgroundColor);
                Title_S_Chat_Settings.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Settings_Chat.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Image_size.Foreground = new SolidColorBrush(DarkBackgroundColor);

                S_Advanced_Settings.Background = new SolidColorBrush(WhiteBackgroundColor);
                Title_Advanced_Settings.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Settings_Advanced.Foreground = new SolidColorBrush(DarkBackgroundColor);


                // Search Message Panel >>>
                SearchMessagePanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                SearchMessagePanel.BorderBrush = new SolidColorBrush(WhiteBackgroundColor);
                SearchMsgUpButton.Foreground = new SolidColorBrush(DarkBackgroundColor);
                SearchMsgDownButton.Foreground = new SolidColorBrush(DarkBackgroundColor);
                IconsearchMessages.Foreground = new SolidColorBrush(DarkBackgroundColor);
                MSG_searchMessages.Foreground = new SolidColorBrush(DarkBackgroundColor);
                CountSearch.Foreground = new SolidColorBrush(DarkBackgroundColor);
                IconCloseSearch.Foreground = new SolidColorBrush(DarkBackgroundColor);

                // Right Main Panel >>>
                RightMain_StackPanel.Background = new SolidColorBrush(SelverBackgroundColor);
                Profile_Border.Background = new SolidColorBrush(WhiteBackgroundColor);
                ProfileUserName.Foreground = new SolidColorBrush(DarkBackgroundColor);
                ViewProfileButton.Foreground = new SolidColorBrush(DarkBackgroundColor);

                Title_Options.Background = new SolidColorBrush(WhiteBackgroundColor);
                Options_Grid.Background = new SolidColorBrush(SelverBackgroundColor);
                Lbl_Options.Foreground = new SolidColorBrush(DarkBackgroundColor);
                SearchMesageButton.Foreground = new SolidColorBrush(DarkBackgroundColor);
                ChangeColorButton.Foreground = new SolidColorBrush(DarkBackgroundColor);
                NotificationsControlButton.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Btn_Block_User.Foreground = new SolidColorBrush(DarkBackgroundColor);

                UserFriendsSection.Background = new SolidColorBrush(SelverBackgroundColor);
                Title_Userlocation.Background = new SolidColorBrush(WhiteBackgroundColor);
                UserlocationSection.Foreground = new SolidColorBrush(DarkBackgroundColor);
                location_Border.Background = new SolidColorBrush(SelverBackgroundColor);
                Lbl_location.Foreground = new SolidColorBrush(DarkBackgroundColor);

                UserAboutSection.Background = new SolidColorBrush(SelverBackgroundColor);
                Title_About.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_About.Foreground = new SolidColorBrush(DarkBackgroundColor);
                About_Border.Background = new SolidColorBrush(SelverBackgroundColor);
                ProfileUserAbout.Foreground = new SolidColorBrush(DarkBackgroundColor);

                UserMediaSection.Background = new SolidColorBrush(SelverBackgroundColor);
                Title_UserMediaSection.Background = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_Media_And_Files.Foreground = new SolidColorBrush(DarkBackgroundColor);
                FilesListview.Background = new SolidColorBrush(WhiteBackgroundColor);

                UserDeleteChatSection.Background = new SolidColorBrush(WhiteBackgroundColor);
                Btn_delete_chat.Foreground = new SolidColorBrush(DarkBackgroundColor);
                RightPanel.Background = new SolidColorBrush(WhiteBackgroundColor);

                // Chat Text Box >>>
                DockPanelLeft_Grid.Background = new SolidColorBrush(WhiteBackgroundColor);
                SendMessagepanel.Background = new SolidColorBrush(WhiteBackgroundColor);
                SendMessagepanel.BorderBrush = new SolidColorBrush(WhiteBackgroundColor);
                MessageBoxText.Foreground = new SolidColorBrush(DarkBackgroundColor);
                MessageBoxText.Background = new SolidColorBrush(WhiteBackgroundColor);
                EmojiSmileofSmileButton.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Btn_LoadmoreMessages.Background = new SolidColorBrush(WhiteBackgroundColor);

                // imogiPanel >>>
                imogiPanel.Background = new SolidColorBrush(WhiteBackgroundColor);

                fontweight.Background = new SolidColorBrush(WhiteBackgroundColor);
                fontweight_Panel.Background = new SolidColorBrush(WhiteBackgroundColor);
                fontsize.Background = new SolidColorBrush(WhiteBackgroundColor);

                Stickers_Grid.Background = new SolidColorBrush(WhiteBackgroundColor);
                SpoiledRabbit_Label.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Cutewaterdrop_Label.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Monstor_Label.Foreground = new SolidColorBrush(DarkBackgroundColor);
                NINJA_Nyankko_Label.Foreground = new SolidColorBrush(DarkBackgroundColor);
                So_Much_Love_Label.Foreground = new SolidColorBrush(DarkBackgroundColor);
                SukkaraChan_Label.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Flower_Hijab_Label.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Trendy_boy_Label.Foreground = new SolidColorBrush(DarkBackgroundColor);

                gift_Grid.Background = new SolidColorBrush(WhiteBackgroundColor);
                searchGifs_Icon.Foreground = new SolidColorBrush(DarkBackgroundColor);
                Txt_searchGifs.Foreground = new SolidColorBrush(DarkBackgroundColor);

                // ListBoxItem >> ChatActivityList
                if (ListUsers.Count > 0)
                {
                    foreach (var Items in ListUsers)
                    {
                        Items.S_Color_Background = "#ffff";
                        Items.S_Color_Foreground = "#444";
                    }
                }

                // ListBoxItem >> ListUsersContact
                if (ListUsersContact.Count > 0)
                {
                    foreach (var Items in ListUsersContact)
                    {
                        Items.S_Color_Background = "#ffff";
                        Items.S_Color_Foreground = "#444";
                    }
                }

                // ListBoxItem >> ListCall
                if (ListCall.Count > 0)
                {
                    foreach (var Items in ListCall)
                    {
                        Items.S_Color_Background = "#ffff";
                        Items.S_Color_Foreground = "#444";
                    }
                }

                // ListBoxItem >> ListSearsh
                if (ListSearsh.Count > 0)
                {
                    foreach (var Items in ListSearsh)
                    {
                        Items.S_Color_Background = "#ffff";
                        Items.S_Color_Foreground = "#444";
                    }
                }

                // ListBoxItem >> ListNotifications
                if (ListNotifications.Count > 0)
                {
                    foreach (var Items in ListNotifications)
                    {
                        Items.S_Color_Background = "#ffff";
                        Items.S_Color_Foreground = "#444";
                    }
                }

                // ListBoxItem >> ListHashtag
                if (ListHashtag.Count > 0)
                {
                    foreach (var Items in ListHashtag)
                    {
                        Items.S_Color_Background = "#ffff";
                        Items.S_Color_Foreground = "#444";
                    }
                }

                _GridWindow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffff"));
                _TitleApp.Foreground = new SolidColorBrush(DarkBackgroundColor);
                _ButtonWindow_Min.Foreground = new SolidColorBrush(DarkBackgroundColor);
                _ButtonWindow_Max.Foreground = new SolidColorBrush(DarkBackgroundColor);
                _ButtonWindow_x.Foreground = new SolidColorBrush(DarkBackgroundColor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

        //########################## Windows ##########################

        #region windows

        private void Btn_Minimize_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Btn_Maximize_OnClick(object sender, RoutedEventArgs e)
        {
            if (Width != 1350)
            {
                //WindowState = WindowState.Normal;
                Hide();
                Width = 1350;
                Height = 750;
                Left = 0;
                Top = 0;
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Show();
            }
            else
            {
                Hide();
                //WindowState = WindowState.Maximized;
                Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width + 30;
                Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height + 20;
                Left = -5;
                Top = -10;
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Show();
            }
        }

        private void Btn_Close_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Windows.Application.Current.Shutdown();
            Environment.Exit(0);
        }


        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var GridWindow = sender as Grid;
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

                _GridWindow = GridWindow;
                if (ModeDarkstlye)
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

                _TitleApp = TitleApp;
                if (ModeDarkstlye)
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
                var ButtonWindow_Minimize = sender as Button;
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

                _ButtonWindow_Min = ButtonWindow_Minimize;
                if (ModeDarkstlye)
                {
                    ButtonWindow_Minimize.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                }
                else
                {
                    ButtonWindow_Minimize.Foreground = new SolidColorBrush(DarkBackgroundColor);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        private void btn_Maximize_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var ButtonWindow_Maximize = sender as Button;
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

                _ButtonWindow_Max = ButtonWindow_Maximize;
                if (ModeDarkstlye)
                {
                    ButtonWindow_Maximize.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                }
                else
                {
                    ButtonWindow_Maximize.Foreground = new SolidColorBrush(DarkBackgroundColor);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void btn_Close_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var ButtonWindow_Close = sender as Button;
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

                _ButtonWindow_x = ButtonWindow_Close;
                if (ModeDarkstlye)
                {
                    ButtonWindow_Close.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                }
                else
                {
                    ButtonWindow_Close.Foreground = new SolidColorBrush(DarkBackgroundColor);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        private void RecourdButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {

        }


        //########################## End Application WoWonder Desktop Version 1.5 ##########################

    }

    public static class AsyncHelper
    {
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        public static void RunSync(Func<Task> func)
            => _taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
    }
}
#endregion