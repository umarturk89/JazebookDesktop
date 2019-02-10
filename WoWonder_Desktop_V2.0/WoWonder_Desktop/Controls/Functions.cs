using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Controls
{
    public class Functions
    {
        //Cach folder Destinations
        public static string Main_Destination = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Settings.Application_Name + "\\";

        public static string Files_Destination = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Settings.Application_Name + "\\Users\\";

        public static Random random = new Random();

        public static ObservableCollection<Users> ConvertFromChatActivity(List<DataBase.ChatActivity> dataList)
        {
            try
            {
                var dd = new ObservableCollection<Users>();
                foreach (var credentials in dataList)
                {
                    Users csc = new Users();
                    csc.U_Id = credentials.U_Id;
                    csc.U_username = credentials.U_username;
                    csc.U_name = credentials.U_name;
                    csc.U_profile_picture = credentials.U_profile_picture;
                    csc.U_cover_picture = credentials.U_cover_picture;
                    csc.U_verified = credentials.U_verified;
                    csc.U_lastseen = credentials.U_lastseen;
                    csc.U_lastseenWithoutCut = credentials.U_lastseenWithoutCut;
                    csc.U_lastseen_time_text = credentials.U_lastseen_time_text;
                    csc.u_url = credentials.u_url;
                    csc.U_lastseen_unix_time = credentials.U_lastseen_unix_time;
                    csc.U_chat_color = credentials.U_chat_color;
                    csc.M_Id = credentials.M_Id;
                    csc.From_Id = credentials.From_Id;
                    csc.To_Id = credentials.To_Id;
                    csc.M_text = credentials.M_text;
                    csc.M_media = credentials.M_media;
                    csc.M_mediaFileName = credentials.M_mediaFileName;
                    csc.M_mediaFileNamese = credentials.M_mediaFileNamese;
                    csc.M_time = credentials.M_time;
                    csc.M_date_time = credentials.M_date_time;
                    csc.M_seen = credentials.M_seen;
                    csc.M_stickers = credentials.M_stickers;
                    csc.S_Color_onof = credentials.S_Color_onof;
                    csc.S_ImageProfile = credentials.S_ImageProfile;
                    csc.S_noProfile_color = credentials.S_noProfile_color;
                    csc.S_Message_FontWeight = credentials.S_Message_FontWeight;
                    csc.S_Message_color = credentials.S_Message_color;
                    csc.IsSeeniconcheck = credentials.IsSeeniconcheck;
                    csc.ChatColorcirclevisibilty = credentials.ChatColorcirclevisibilty;
                    csc.MediaIconvisibilty = credentials.MediaIconvisibilty;
                    csc.MediaIconImage = credentials.MediaIconImage;
                    csc.App_Main_Later = credentials.App_Main_Later;
                    csc.UsernameTwoLetters = credentials.UsernameTwoLetters;
                    dd.Add(csc);
                }
                return dd;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static ObservableCollection<UsersContact> ConvertFromUsersContact(List<DataBase.UsersContactTable> dataList)
        {
            try
            {
                var datauser = new ObservableCollection<UsersContact>();
                foreach (var credentials in dataList)
                {
                    UsersContact uc = new UsersContact();

                    uc.UC_Id = credentials.UC_Id;
                    uc.UC_username = credentials.UC_username;
                    uc.UC_name = credentials.UC_name;
                    uc.UC_profile_picture = credentials.UC_profile_picture;
                    uc.UC_cover_picture = credentials.UC_cover_picture;
                    uc.UC_verified = credentials.UC_verified;
                    uc.UC_lastseen = credentials.UC_lastseen;
                    uc.UC_lastseen_time_text = credentials.UC_lastseen_time_text;
                    uc.UC_lastseen_unix_time = credentials.UC_lastseen_unix_time;
                    uc.UC_url = credentials.UC_url;
                    uc.UC_user_platform = credentials.UC_user_platform;
                    uc.UC_Color_onof = credentials.UC_Color_onof;
                    uc.UC_App_Main_Later = credentials.UC_App_Main_Later;
                    uc.UC_chat_color = credentials.UC_chat_color;

                    //user_profile
                    uc.UC_email = credentials.UC_email;
                    uc.UC_first_name = credentials.UC_first_name;
                    uc.UC_last_name = credentials.UC_last_name;
                    uc.UC_relationship_id = credentials.UC_relationship_id;
                    uc.UC_address = credentials.UC_address;
                    uc.UC_working = credentials.UC_working;
                    uc.UC_working_link = credentials.UC_working_link;
                    uc.UC_about = credentials.UC_about;
                    uc.UC_school = credentials.UC_school;
                    uc.UC_gender = credentials.UC_gender;
                    uc.UC_birthday = credentials.UC_birthday;
                    uc.UC_website = credentials.UC_website;
                    uc.UC_facebook = credentials.UC_facebook;
                    uc.UC_google = credentials.UC_google;
                    uc.UC_twitter = credentials.UC_twitter;
                    uc.UC_linkedin = credentials.UC_linkedin;
                    uc.UC_youtube = credentials.UC_youtube;
                    uc.UC_vk = credentials.UC_vk;
                    uc.UC_instagram = credentials.UC_instagram;
                    uc.UC_language = credentials.UC_language;
                    uc.UC_ip_address = credentials.UC_ip_address;
                    uc.UC_follow_privacy = credentials.UC_follow_privacy;
                    uc.UC_post_privacy = credentials.UC_post_privacy;
                    uc.UC_message_privacy = credentials.UC_message_privacy;
                    uc.UC_confirm_followers = credentials.UC_confirm_followers;
                    uc.UC_show_activities_privacy = credentials.UC_show_activities_privacy;
                    uc.UC_birth_privacy = credentials.UC_birth_privacy;
                    uc.UC_visit_privacy = credentials.UC_visit_privacy;
                    uc.UC_showlastseen = credentials.UC_showlastseen;
                    uc.UC_status = credentials.UC_status;
                    uc.UC_active = credentials.UC_active;
                    uc.UC_admin = credentials.UC_admin;
                    uc.UC_registered = credentials.UC_registered;
                    uc.UC_phone_number = credentials.UC_phone_number;
                    uc.UC_is_pro = credentials.UC_is_pro;
                    uc.UC_pro_type = credentials.UC_pro_type;
                    uc.UC_joined = credentials.UC_joined;
                    uc.UC_timezone = credentials.UC_timezone;
                    uc.UC_referrer = credentials.UC_referrer;
                    uc.UC_balance = credentials.UC_balance;
                    uc.UC_paypal_email = credentials.UC_paypal_email;
                    uc.UC_notifications_sound = credentials.UC_notifications_sound;
                    uc.UC_order_posts_by = credentials.UC_order_posts_by;
                    uc.UC_social_login = credentials.UC_social_login;
                    uc.UC_device_id = credentials.UC_device_id;

                    datauser.Add(uc);
                }
                return datauser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public static ObservableCollection<Classes.Messages> ConvertFromMessages(List<DataBase.MessagesTable> dataList)
        {
            try
            {
                var mm = new ObservableCollection<Classes.Messages>();
                foreach (var credentials in dataList)
                {
                    Classes.Messages m = new Classes.Messages();
                    m.Mes_Id = credentials.Mes_Id;
                    m.Mes_From_Id = credentials.Mes_From_Id;
                    m.Mes_To_Id = credentials.Mes_To_Id;
                    m.Mes_Text = credentials.Mes_Text;
                    m.Mes_Media = credentials.Mes_Media;
                    m.Mes_MediaFileName = credentials.Mes_MediaFileName;
                    m.Mes_MediaFileNames = credentials.Mes_MediaFileNames;
                    m.Mes_Time = credentials.Mes_Time;
                    m.Mes_Seen = credentials.Mes_Seen;
                    m.Mes_Deleted_one = credentials.Mes_Deleted_one;
                    m.Mes_Deleted_two = credentials.Mes_Deleted_two;
                    m.Mes_Sent_push = credentials.Mes_Sent_push;
                    m.Mes_Notification_id = credentials.Mes_Notification_id;
                    m.Mes_Type_two = credentials.Mes_Type_two;
                    m.Mes_Time_text = credentials.Mes_Time_text;
                    m.Mes_Position = credentials.Mes_Position;
                    m.Mes_Type = credentials.Mes_Type;
                    m.Mes_File_size = credentials.Mes_File_size;
                    m.Mes_Stickers = credentials.Mes_Stickers;
                    m.Mes_User_avatar = credentials.Mes_User_avatar;
                    m.Progress_Value = credentials.Progress_Value;
                    m.sound_time = credentials.sound_time;
                    m.sound_slider_value = credentials.sound_slider_value;
                    m.Pause_Visibility = credentials.Pause_Visibility;
                    m.Play_Visibility = credentials.Play_Visibility;
                    m.Download_Visibility = credentials.Download_Visibility;
                    m.Progress_Visibility = credentials.Progress_Visibility;
                    m.Icon_File_Visibility = credentials.Icon_File_Visibility;
                    m.Hlink_Download_Visibility = credentials.Hlink_Download_Visibility;
                    m.Hlink_Open_Visibility = credentials.Hlink_Open_Visibility;
                    m.Img_user_message = credentials.Img_user_message;
                    m.Type_Icon_File = credentials.Type_Icon_File;
                    m.Color_box_message = credentials.Color_box_message;

                    mm.Add(m);
                }
                return mm;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        public static ObservableCollection<UsersContactProfile> ConvertUsersContactProfile(List<DataBase.UsersContactProfileTable> dataList)
        {
            try
            {
                var dd = new ObservableCollection<UsersContactProfile>();
                foreach (var credentials in dataList)
                {
                    UsersContactProfile ucp = new UsersContactProfile();
                    //users
                    ucp.UCP_Id = credentials.UCP_Id;
                    ucp.UCP_username = credentials.UCP_username;
                    ucp.UCP_name = credentials.UCP_name;
                    ucp.UCP_cover_picture = credentials.UCP_cover_picture;
                    ucp.UCP_profile_picture = credentials.UCP_profile_picture;
                    ucp.UCP_verified = credentials.UCP_verified;
                    ucp.UCP_lastseen = credentials.UCP_lastseen;
                    ucp.UCP_lastseen_time_text = credentials.UCP_lastseen_time_text;
                    ucp.UCP_lastseen_unix_time = credentials.UCP_lastseen_unix_time;
                    ucp.UCP_url = credentials.UCP_url;
                    ucp.UCP_user_platform = credentials.UCP_user_platform;
                    ucp.UCP_chat_color = credentials.UCP_chat_color;
                    ucp.UCP_Notifications_Message_user = credentials.UCP_Notifications_Message_user;
                    ucp.UCP_Notifications_Message_Sound_user = credentials.UCP_Notifications_Message_Sound_user;
                    //user_profile
                    ucp.UCP_email = credentials.UCP_email;
                    ucp.UCP_first_name = credentials.UCP_first_name;
                    ucp.UCP_last_name = credentials.UCP_last_name;
                    ucp.UCP_relationship_id = credentials.UCP_relationship_id;
                    ucp.UCP_address = credentials.UCP_address;
                    ucp.UCP_working = credentials.UCP_working;
                    ucp.UCP_working_link = credentials.UCP_working_link;
                    ucp.UCP_about = credentials.UCP_about;
                    ucp.UCP_school = credentials.UCP_school;
                    ucp.UCP_gender = credentials.UCP_gender;
                    ucp.UCP_birthday = credentials.UCP_birthday;
                    ucp.UCP_website = credentials.UCP_website;
                    ucp.UCP_facebook = credentials.UCP_facebook;
                    ucp.UCP_google = credentials.UCP_google;
                    ucp.UCP_twitter = credentials.UCP_twitter;
                    ucp.UCP_linkedin = credentials.UCP_linkedin;
                    ucp.UCP_youtube = credentials.UCP_youtube;
                    ucp.UCP_vk = credentials.UCP_vk;
                    ucp.UCP_instagram = credentials.UCP_instagram;
                    ucp.UCP_language = credentials.UCP_language;
                    ucp.UCP_ip_address = credentials.UCP_ip_address;
                    ucp.UCP_follow_privacy = credentials.UCP_follow_privacy;
                    ucp.UCP_post_privacy = credentials.UCP_post_privacy;
                    ucp.UCP_message_privacy = credentials.UCP_message_privacy;
                    ucp.UCP_confirm_followers = credentials.UCP_confirm_followers;
                    ucp.UCP_show_activities_privacy = credentials.UCP_show_activities_privacy;
                    ucp.UCP_birth_privacy = credentials.UCP_birth_privacy;
                    ucp.UCP_visit_privacy = credentials.UCP_visit_privacy;
                    ucp.UCP_showlastseen = credentials.UCP_showlastseen;
                    ucp.UCP_status = credentials.UCP_status;
                    ucp.UCP_active = credentials.UCP_active;
                    ucp.UCP_admin = credentials.UCP_admin;
                    ucp.UCP_registered = credentials.UCP_registered;
                    ucp.UCP_phone_number = credentials.UCP_phone_number;
                    ucp.UCP_is_pro = credentials.UCP_is_pro;
                    ucp.UCP_pro_type = credentials.UCP_pro_type;
                    ucp.UCP_joined = credentials.UCP_joined;
                    ucp.UCP_timezone = credentials.UCP_timezone;
                    ucp.UCP_referrer = credentials.UCP_referrer;
                    ucp.UCP_balance = credentials.UCP_balance;
                    ucp.UCP_paypal_email = credentials.UCP_paypal_email;
                    ucp.UCP_notifications_sound = credentials.UCP_notifications_sound;
                    ucp.UCP_order_posts_by = credentials.UCP_order_posts_by;
                    ucp.UCP_social_login = credentials.UCP_social_login;
                    ucp.UCP_device_id = credentials.UCP_device_id;

                    dd.Add(ucp);
                }
                return dd;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        //creat new Random String Session 
        public static string RandomString(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXdsdaawerthklmnbvcxer46gfdsYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //creat new Random Color
        public static string RandomColor()
        {
            try
            {
                string color = "";
                int b;

                Random r = new Random();
                b = r.Next(1, 8);
                switch (b)
                {
                    case 5:
                        {
                            color = "#314d74"; //dark blue
                        }
                        break;
                    case 1:
                        {

                            color = "#404040"; //dark gray
                        }
                        break;
                    case 2:
                        {
                            color = "#146c7c"; // nele
                        }
                        break;

                    case 4:
                        {
                            color = "#789c74"; //dark green
                        }
                        break;

                    case 6:
                        {
                            color = "#cc8237"; //brown
                        }
                        break;
                    case 7:
                        {
                            color = "#c37887"; //light red
                        }
                        break;
                    case 8:
                        {
                            color = "#f2e032"; //yellow
                        }
                        break;
                    case 9:
                        {
                            color = "#444"; //yellow
                        }
                        break;
                    case 3:
                        {
                            color = "#cc635c"; //red
                        }
                        break;
                }
                return color;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Settings.Main_Color;

            }
        }

        // Check For Internet Connection
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);
        public static bool CheckForInternetConnection()
        {
            try
            {

                int description;
                return InternetGetConnectedState(out description, 0);

            }
            catch (Exception e)
            {
                return false;
                Console.WriteLine(e);
            }
        }

        // Functions Get data User Profile and Insert data user in database
        public static void Get_User_Profile(JObject data)
        {
            try
            {
                if (data != null)
                {
                    JObject Alfa = JObject.FromObject(data);
                    var U_UserId = Alfa["user_id"].ToString();
                    var U_Username = Alfa["username"].ToString();
                    var U_Email = Alfa["email"].ToString();
                    var U_First_name = Alfa["first_name"].ToString();
                    var U_Last_name = Alfa["last_name"].ToString();
                    var U_Avatar = Alfa["avatar"].ToString();
                    var U_Cover = Alfa["cover"].ToString();
                    var U_Relationship_id = Alfa["relationship_id"].ToString();
                    var U_Address = Alfa["address"].ToString();
                    var U_Working = Alfa["working"].ToString();
                    var U_Working_link = Alfa["working_link"].ToString();
                    var U_About = Alfa["about"].ToString();
                    var U_School = Alfa["school"].ToString();
                    var U_Gender = Alfa["gender"].ToString();
                    var U_Birthday = Alfa["birthday"].ToString();
                    var U_Website = Alfa["website"].ToString();
                    var U_Facebook = Alfa["facebook"].ToString();
                    var U_Google = Alfa["username"].ToString();
                    var U_Twitter = Alfa["twitter"].ToString();
                    var U_Linkedin = Alfa["linkedin"].ToString();
                    var U_Youtube = Alfa["youtube"].ToString();
                    var U_Vk = Alfa["vk"].ToString();
                    var U_Instagram = Alfa["instagram"].ToString();
                    var U_Language = Alfa["language"].ToString();
                    var U_Ip_address = Alfa["ip_address"].ToString();
                    var U_Verified = Alfa["verified"].ToString();
                    var U_Lastseen = Alfa["lastseen"].ToString();
                    var U_Showlastseen = Alfa["showlastseen"].ToString();
                    var U_Status = Alfa["status"].ToString();
                    var U_Active = Alfa["active"].ToString();
                    var U_Admin = Alfa["admin"].ToString();
                    var U_Registered = Alfa["registered"].ToString();
                    var U_Phone_number = Alfa["phone_number"].ToString();
                    var U_Is_pro = Alfa["is_pro"].ToString();
                    var U_Pro_type = Alfa["pro_type"].ToString();
                    var U_Joined = Alfa["joined"].ToString();
                    var U_Timezone = Alfa["timezone"].ToString();
                    var U_Referrer = Alfa["referrer"].ToString();
                    var U_Balance = Alfa["balance"].ToString();
                    var U_Paypal_email = Alfa["paypal_email"].ToString();
                    var U_Notifications_sound = Alfa["notifications_sound"].ToString();
                    var U_Order_posts_by = Alfa["order_posts_by"].ToString();
                    var U_Social_loginn = Alfa["social_login"].ToString();
                    var U_Device_id = Alfa["device_id"].ToString();
                    var U_Url = Alfa["url"].ToString();
                    var U_Name = Alfa["name"].ToString();


                    string AvatarSplit = U_Avatar.Split('/').Last();


                    // Insert data user in database
                    DataBase.ProfilesTable ProfilesTable = new DataBase.ProfilesTable();
                    ProfilesTable.pm_UserId = U_UserId;
                    ProfilesTable.pm_Username = U_Username;
                    ProfilesTable.pm_Email = U_Email;
                    ProfilesTable.pm_First_name = U_First_name;
                    ProfilesTable.pm_Last_name = U_Last_name;
                    ProfilesTable.pm_Avatar = Get_image(UserDetails.User_id, AvatarSplit, U_Avatar);
                    ProfilesTable.pm_Cover = U_Cover;
                    ProfilesTable.pm_Relationship_id = U_Relationship_id;
                    ProfilesTable.pm_Address = U_Address;
                    ProfilesTable.pm_Working = U_Working;
                    ProfilesTable.pm_Working_link = U_Working_link;
                    ProfilesTable.pm_About = U_About;
                    ProfilesTable.pm_School = U_School;
                    ProfilesTable.pm_Gender = U_Gender;
                    ProfilesTable.pm_Birthday = U_Birthday;
                    ProfilesTable.pm_Website = U_Website;
                    ProfilesTable.pm_Facebook = U_Facebook;
                    ProfilesTable.pm_Google = U_Google;
                    ProfilesTable.pm_Twitter = U_Twitter;
                    ProfilesTable.pm_Linkedin = U_Linkedin;
                    ProfilesTable.pm_Youtube = U_Youtube;
                    ProfilesTable.pm_Vk = U_Vk;
                    ProfilesTable.pm_Instagram = U_Instagram;
                    ProfilesTable.pm_Language = U_Language;
                    ProfilesTable.pm_Ip_address = U_Ip_address;
                    ProfilesTable.pm_Verified = U_Verified;
                    ProfilesTable.pm_Lastseen = U_Lastseen;
                    ProfilesTable.pm_Showlastseen = U_Showlastseen;
                    ProfilesTable.pm_Status = U_Status;
                    ProfilesTable.pm_Active = U_Active;
                    ProfilesTable.pm_Admin = U_Admin;
                    ProfilesTable.pm_Registered = U_Registered;
                    ProfilesTable.pm_Phone_number = U_Phone_number;
                    ProfilesTable.pm_Is_pro = U_Is_pro;
                    ProfilesTable.pm_Pro_type = U_Pro_type;
                    ProfilesTable.pm_Joined = U_Joined;
                    ProfilesTable.pm_Timezone = U_Timezone;
                    ProfilesTable.pm_Referrer = U_Referrer;
                    ProfilesTable.pm_Balance = U_Balance;
                    ProfilesTable.pm_Paypal_email = U_Paypal_email;
                    ProfilesTable.pm_Notifications_sound = U_Notifications_sound;
                    ProfilesTable.pm_Order_posts_by = U_Order_posts_by;
                    ProfilesTable.pm_Social_login = U_Social_loginn;
                    ProfilesTable.pm_Device_id = U_Device_id;
                    ProfilesTable.pm_Url = U_Url;
                    ProfilesTable.pm_Name = U_Name;

                    SQLiteCommandSender.Insert_Or_Replace_To_ProfileTable(ProfilesTable);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        // Functions SubString Cut Of
        public static string SubStringCutOf(string s, int x)
        {
            try
            {
                String substring = s.Substring(0, x);
                return substring + "...";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return s;
            }
        }
        public static string HtmlDecodestring(string a)
        {
            try
            {
                string b = WebUtility.HtmlDecode(a);
                return b;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }

        //cut string name user : ex. ML
        public static string GetoLettersfromString(string key)
        {
            try
            {
                var string1 = key.Split(' ').First();
                var string2 = key.Split(' ').Last();

                String substring1 = string1.Substring(0, 1);
                String substring2 = string2.Substring(0, 1);

                var result = substring1 + substring2;
                return result.ToUpper();

            }
            catch (Exception e)
            {
                e.ToString();
                return "";
            }
        }

        // Functions Save Images
        public static void save_img(string userid, string filename, string url)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }
                WebClient webClient = new WebClient();
                if (!File.Exists(FolderDestination + filename))
                {
                    webClient.DownloadFileAsync(new Uri(url), FolderDestination + filename);
                }
            }
            catch (Exception e)
            {

                e.ToString();
            }
        }

        // Functions Get_image from folder
        public static string Get_image(string userid, string filename, string url)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }
                if (Directory.Exists(FolderDestination))
                {
                    // Get the file name from the path.
                    if (File.Exists(FolderDestination + filename))
                    {
                        return FolderDestination + filename;
                    }
                    else
                    {
                        Task.Run(
                            () => //This code runs on a new thread, control is returned to the caller on the UI thread.
                            {
                                save_img(userid, filename, url);
                            });
                        return url;
                    }
                }
                else
                {
                    return url;
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return url;
            }
        }

        // Functions Save Sounds
        public static void save_sound(string userid, string filename, string url, Classes.Messages selectedgroup)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\" + "sounds\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }

                WebClient webClient = new WebClient();
                if (!File.Exists(FolderDestination + filename))
                {
                    webClient.DownloadProgressChanged += (s, e) =>
                    {
                        selectedgroup.Progress_Value = e.ProgressPercentage;
                    };

                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        selectedgroup.Progress_Visibility = "Collapsed";
                        selectedgroup.Download_Visibility = "Collapsed";
                        selectedgroup.Play_Visibility = "Visible";
                        selectedgroup.Mes_Media = FolderDestination + filename;
                        //Update data  Messages in Table
                        SQLiteCommandSender.Insert_Or_Update_To_one_MessagesTable(selectedgroup);
                    };

                    webClient.DownloadFileAsync(new Uri(url), FolderDestination + filename);
                    selectedgroup.Progress_Visibility = "Visible";
                }
                else
                {
                    selectedgroup.Progress_Visibility = "Collapsed";
                    selectedgroup.Download_Visibility = "Collapsed";
                    selectedgroup.Play_Visibility = "Visible";
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        // Functions Get sound from folder
        public static string Get_sound(string userid, string filename)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\" + "sounds\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }
                if (Directory.Exists(FolderDestination))
                {
                    // Get the file name from the path.
                    if (File.Exists(FolderDestination + filename))
                    {
                        return FolderDestination + filename;
                    }
                    else
                    {
                        return "Not Found sound";
                    }
                }
                else
                {
                    return "Not Found sound";
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return "Not Found sound";
            }
        }

        // Functions Check File Extension */Audio, Image, Video\*
        public static string Check_FileExtension(string filename)
        {
            if (filename.EndsWith("mp3") || filename.EndsWith("MP3"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("aac") || filename.EndsWith("AAC"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("aiff") || filename.EndsWith("AIFF"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("amr") || filename.EndsWith("AMR"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("ape") || filename.EndsWith("APE"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("arf") || filename.EndsWith("ARF"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("asf") || filename.EndsWith("ASF"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("m4a") || filename.EndsWith("M4A"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("m4b") || filename.EndsWith("M4B"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("m4p") || filename.EndsWith("M4A"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("3ga") || filename.EndsWith("3GA"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("ogg") || filename.EndsWith("OGG"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("wav") || filename.EndsWith("WAV"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("wma") || filename.EndsWith("WMA"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("wpl") || filename.EndsWith("WPL"))
            {
                return "Audio";
            }
            else if (filename.EndsWith("ani") || filename.EndsWith("ANI"))
            {
                return "Image";
            }
            else if (filename.EndsWith("bmp") || filename.EndsWith("BMP"))
            {
                return "Image";
            }
            else if (filename.EndsWith("cal") || filename.EndsWith("CAL"))
            {
                return "Image";
            }
            else if (filename.EndsWith("fax") || filename.EndsWith("FAX"))
            {
                return "Image";
            }
            else if (filename.EndsWith("gif") || filename.EndsWith("GIF"))
            {
                return "Image";
            }
            else if (filename.EndsWith("img") || filename.EndsWith("IMG"))
            {
                return "Image";
            }
            else if (filename.EndsWith("jbg") || filename.EndsWith("JBG"))
            {
                return "Image";
            }
            else if (filename.EndsWith("jpeg") || filename.EndsWith("JPEG"))
            {
                return "Image";
            }
            else if (filename.EndsWith("jpe") || filename.EndsWith("JPE"))
            {
                return "Image";
            }
            else if (filename.EndsWith("jpg") || filename.EndsWith("JPG"))
            {
                return "Image";
            }
            else if (filename.EndsWith("mac") || filename.EndsWith("MAC"))
            {
                return "Image";
            }
            else if (filename.EndsWith("pbm") || filename.EndsWith("PBM"))
            {
                return "Image";
            }
            else if (filename.EndsWith("pcd") || filename.EndsWith("PCD"))
            {
                return "Image";
            }
            else if (filename.EndsWith("pcx") || filename.EndsWith("PCX"))
            {
                return "Image";
            }
            else if (filename.EndsWith("pct") || filename.EndsWith("PCT"))
            {
                return "Image";
            }
            else if (filename.EndsWith("pgm") || filename.EndsWith("PGM"))
            {
                return "Image";
            }
            else if (filename.EndsWith("png") || filename.EndsWith("PNG"))
            {
                return "Image";
            }
            else if (filename.EndsWith("ppm") || filename.EndsWith("PPM"))
            {
                return "Image";
            }
            else if (filename.EndsWith("psd") || filename.EndsWith("PSD"))
            {
                return "Image";
            }
            else if (filename.EndsWith("ras") || filename.EndsWith("RAS"))
            {
                return "Image";
            }
            else if (filename.EndsWith("tga") || filename.EndsWith("TGA"))
            {
                return "Image";
            }
            else if (filename.EndsWith("tiff") || filename.EndsWith("TIFF"))
            {
                return "Image";
            }
            else if (filename.EndsWith("wmf") || filename.EndsWith("WMF"))
            {
                return "Image";
            }
            else if (filename.EndsWith("avi") || filename.EndsWith("AVI"))
            {
                return "Video";
            }
            else if (filename.EndsWith("asf") || filename.EndsWith("ASF"))
            {
                return "Video";
            }
            else if (filename.EndsWith("mov") || filename.EndsWith("MOV"))
            {
                return "Video";
            }
            else if (filename.EndsWith("qt") || filename.EndsWith("QT"))
            {
                return "Video";
            }
            else if (filename.EndsWith("avchd") || filename.EndsWith("AVCHD"))
            {
                return "Video";
            }
            else if (filename.EndsWith("flv") || filename.EndsWith("FLV"))
            {
                return "Video";
            }
            else if (filename.EndsWith("swf") || filename.EndsWith("SWF"))
            {
                return "Video";
            }
            else if (filename.EndsWith("mpg") || filename.EndsWith("MPG"))
            {
                return "Video";
            }
            else if (filename.EndsWith("mpeg") || filename.EndsWith("MPEG"))
            {
                return "Video";
            }
            else if (filename.EndsWith("wmv") || filename.EndsWith("WMV"))
            {
                return "Video";
            }
            else if (filename.EndsWith("mpg-4") || filename.EndsWith("MPEG-4"))
            {
                return "Video";
            }
            else if (filename.EndsWith("mp4") || filename.EndsWith("MP4"))
            {
                return "Video";
            }
            else if (filename.EndsWith("h.264") || filename.EndsWith("H.264"))
            {
                return "Video";
            }
            else if (filename.EndsWith("divx") || filename.EndsWith("DivX"))
            {
                return "Video";
            }
            else if (filename.EndsWith("mkv") || filename.EndsWith("MKV"))
            {
                return "Video";
            }
            else if (filename.EndsWith("rar") || filename.EndsWith("RAR"))
            {
                return "File";
            }
            else if (filename.EndsWith("zip") || filename.EndsWith("ZIP"))
            {
                return "File";
            }
            else if (filename.EndsWith("txt") || filename.EndsWith("TXT"))
            {
                return "File";
            }
            else if (filename.EndsWith("docx") || filename.EndsWith("DOCX"))
            {
                return "File";
            }
            else if (filename.EndsWith("doc") || filename.EndsWith("DOC"))
            {
                return "File";
            }
            else if (filename.EndsWith("pdf") || filename.EndsWith("PDF"))
            {
                return "File";
            }
            else
            {
                return "Forbidden";
            }
        }

        // Functions Save or get from folder Images messages
        public static string Get_img_messages(string userid, string filename, string url, Classes.Messages selectedgroup)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\" + "images\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }
                // Get the file name from the path.
                if (File.Exists(FolderDestination + filename))
                {
                    return FolderDestination + filename;
                }
                else
                {
                    WebClient webClient = new WebClient();

                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        selectedgroup.Mes_Media = FolderDestination + filename;
                        SQLiteCommandSender.Insert_Or_Update_To_one_MessagesTable(selectedgroup);
                    };

                    webClient.DownloadFileAsync(new Uri(url), FolderDestination + filename);

                }
                return url;
            }
            catch (Exception e)
            {
                e.ToString();
                return url;
            }
        }

        // Functions open from folder Images messages
        public static void open_img(string userid, string filename, string url, Classes.Messages selectedgroup)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\" + "images\\";

                if (File.Exists(FolderDestination + filename))
                {
                    Process.Start(FolderDestination + filename);
                }
                else
                {
                    Get_img_messages(userid, filename, url, selectedgroup);
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        // Functions Save or get from folder sticker messages
        public static string Get_Sticker_messages(string filename, string url, Classes.Messages selectedgroup)
        {
            try
            {
                string FolderDestination = Main_Destination + "\\" + "sticker\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }
                // Get the file name from the path.
                if (File.Exists(FolderDestination + filename))
                {
                    return FolderDestination + filename;
                }
                else
                {
                    WebClient webClient = new WebClient();

                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        if (filename.Contains(".jpg"))
                        {
                            selectedgroup.Mes_Media = FolderDestination + filename;
                        }
                        else
                        {
                            selectedgroup.Mes_MediaFileName = FolderDestination + filename;
                        }
                        SQLiteCommandSender.Insert_Or_Update_To_one_MessagesTable(selectedgroup);
                    };

                    webClient.DownloadFileAsync(new Uri(url), FolderDestination + filename);
                }
                return url;
            }
            catch (Exception e)
            {
                e.ToString();
                return url;
            }
        }

        // Functions Save Video
        public static void save_Video(string userid, string filename, string url, Classes.Messages selectedgroup)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\" + "video\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }

                WebClient webClient = new WebClient();
                if (!File.Exists(FolderDestination + filename))
                {
                    webClient.DownloadProgressChanged += (s, e) =>
                    {
                        Task.Run(() =>
                        {
                            selectedgroup.Progress_Value = e.ProgressPercentage;
                        });
                    };

                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        selectedgroup.Progress_Visibility = "Collapsed";
                        selectedgroup.Download_Visibility = "Collapsed";
                        selectedgroup.Play_Visibility = "Visible";
                        selectedgroup.Mes_Media = FolderDestination + filename;
                        //Update data  Messages in Table
                        SQLiteCommandSender.Insert_Or_Update_To_one_MessagesTable(selectedgroup);
                    };

                    webClient.DownloadFileAsync(new Uri(url), FolderDestination + filename);
                    selectedgroup.Progress_Visibility = "Visible";
                }
                else
                {
                    selectedgroup.Progress_Visibility = "Collapsed";
                    selectedgroup.Download_Visibility = "Collapsed";
                    selectedgroup.Play_Visibility = "Visible";
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        // Functions Get vidoe from folder
        public static string Get_Video(string userid, string filename)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\" + "video\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }
                if (Directory.Exists(FolderDestination))
                {
                    // Get the file name from the path.
                    if (File.Exists(FolderDestination + filename))
                    {
                        return FolderDestination + filename;
                    }
                    else
                    {
                        return "Not Found vidoe";
                    }
                }
                else
                {
                    return "Not Found vidoe";
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return "Not Found vidoe";
            }
        }

        // Functions Rename vidoe
        public static string Rename_Video(string filename)
        {
            string FileName = filename.Split('.').Last();
            DateTime data = new DateTime();
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(DateTime.UtcNow)).TotalMilliseconds;
            string dataTime = Convert.ToString(unixTimestamp);
            string NewFileName = "V_" + RandomString(8) + "." + FileName;

            return NewFileName;
        }

        // Functions Save file
        public static void save_file(string userid, string filename, string url, Controls.Classes.Messages selectedgroup)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\" + "file\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }

                WebClient webClient = new WebClient();
                if (!File.Exists(FolderDestination + filename))
                {
                    webClient.DownloadProgressChanged += (s, e) =>
                    {
                        selectedgroup.Progress_Value = e.ProgressPercentage;
                    };

                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        selectedgroup.Progress_Visibility = "Collapsed";
                        selectedgroup.Download_Visibility = "Collapsed";
                        selectedgroup.Icon_File_Visibility = "Visible";
                        selectedgroup.Mes_Media = FolderDestination + filename;
                        //Update data  Messages in Table
                        SQLiteCommandSender.Insert_Or_Update_To_one_MessagesTable(selectedgroup);
                    };

                    webClient.DownloadFileAsync(new Uri(url), FolderDestination + filename);
                    selectedgroup.Progress_Visibility = "Visible";
                }
                else
                {
                    selectedgroup.Progress_Visibility = "Collapsed";
                    selectedgroup.Download_Visibility = "Collapsed";
                    selectedgroup.Icon_File_Visibility = "Visible";
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        // Functions Save or get from folder file messages
        public static string Get_file(string userid, string filename)
        {
            try
            {
                string FolderDestination = Files_Destination + userid + "\\" + "file\\";
                if (Directory.Exists(FolderDestination) == false)
                {
                    Directory.CreateDirectory(FolderDestination);
                }
                if (Directory.Exists(FolderDestination))
                {
                    // Get the file name from the path.
                    if (File.Exists(FolderDestination + filename))
                    {
                        return FolderDestination + filename;
                    }
                    else
                    {
                        return "Not Found file";
                    }
                }
                else
                {
                    return "Not Found file";
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return "Not Found file";
            }
        }

        // Functions  Delete data File  user where block
        public static void Delete_dataFile_user(string userid)
        {
            try
            {
                string FolderDestination = Files_Destination + userid;

                DirectoryInfo di = new DirectoryInfo(FolderDestination);

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        // Functions  Delete data File and Folder user and Messages Table
        public static void clearFolder()
        {
            try
            {
                string FolderDestination = Functions.Files_Destination;

                DirectoryInfo di = new DirectoryInfo(FolderDestination);

                string[] folder = Directory.GetDirectories(FolderDestination);

                foreach (string dir in folder)
                {
                    DirectoryInfo sub = new DirectoryInfo(dir);

                    foreach (DirectoryInfo s in sub.GetDirectories())
                    {
                        s.Delete(true);
                    }
                }

                SQLite_Entity.Connection.Query<DataBase.MessagesTable>(
                    "Delete FROM MessagesTable WHERE Mes_From_Id = " + UserDetails.User_id + " OR  Mes_To_Id = " +
                    UserDetails.User_id);
            }

            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        // Functions Get datatime today
        public static string Get_datatime()
        {
            DateTime today = DateTime.Now;
            TimeSpan duration = new TimeSpan(36, 0, 0, 0);
            DateTime answer = today.Add(duration);
            Console.WriteLine("{0:dddd}", answer);
            return answer.ToString();
        }

    }
}
