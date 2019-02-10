using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using WoWonder_Desktop.Controls;

namespace WoWonder_Desktop.SQLite
{
    public class SQLiteCommandSender
    {
        public DataBase.LoginTable GetLoginCredentials(string status)
        {
            try
            {
                var ss = SQLite_Entity.Connection.Table<DataBase.LoginTable>().FirstOrDefault(a => a.Status == status);
                if (ss != null)
                {
                    return ss;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        //Insert data To Login Table
        public static void Insert_To_LoginTable(DataBase.LoginTable credentials)
        {
            try
            {
                SQLite_Entity.Connection.Insert(credentials);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Update data To Login Table
        public void Update_To_LoginTable(DataBase.LoginTable credentials)
        {
            try
            {
                SQLite_Entity.Connection.Update(credentials);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static DataBase.LoginTable Select_From_LoginTable_By_ID(string userId)
        {
            try
            {
                return SQLite_Entity.Connection.Table<DataBase.LoginTable>().FirstOrDefault(c => c.UserId == userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        //Insert data To Settings Table
        public static void Insert_To_SettingsTable(DataBase.SettingsTable credentials)
        {
            try
            {
                SQLite_Entity.Connection.Insert(credentials);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Update data To Settings Table
        public static void Update_SettingsTable(DataBase.SettingsTable credentials)
        {
            try
            {
                var data = SQLite_Entity.Connection.Table<DataBase.SettingsTable>()
                    .FirstOrDefault();
                if (data != null)
                {
                    if (data.S_SiteName != credentials.S_SiteName)
                    {
                        data.S_SiteName = credentials.S_SiteName;
                    }
                    if (data.S_SiteTitle != credentials.S_SiteTitle)
                    {
                        data.S_SiteTitle = credentials.S_SiteTitle;
                    }
                    if (data.S_SiteKeywords != credentials.S_SiteKeywords)
                    {
                        data.S_SiteKeywords = credentials.S_SiteKeywords;
                    }
                    if (data.S_SiteDesc != credentials.S_SiteDesc)
                    {
                        data.S_SiteDesc = credentials.S_SiteDesc;
                    }
                    if (data.S_DefaultLang != credentials.S_DefaultLang)
                    {
                        data.S_DefaultLang = credentials.S_DefaultLang;
                    }
                    if (data.S_FileSharing != credentials.S_FileSharing)
                    {
                        data.S_FileSharing = credentials.S_FileSharing;
                    }
                    if (data.S_ChatSystem != credentials.S_ChatSystem)
                    {
                        data.S_ChatSystem = credentials.S_ChatSystem;
                    }
                    if (data.S_User_lastseen != credentials.S_User_lastseen)
                    {
                        data.S_User_lastseen = credentials.S_User_lastseen;
                    }
                    if (data.S_Age != credentials.S_Age)
                    {
                        data.S_Age = credentials.S_Age;
                    }
                    if (data.S_DeleteAccount != credentials.S_DeleteAccount)
                    {
                        data.S_DeleteAccount = credentials.S_DeleteAccount;
                    }
                    if (data.S_DefaultLang != credentials.S_DefaultLang)
                    {
                        data.S_DefaultLang = credentials.S_DefaultLang;
                    }
                    if (data.S_ConnectivitySystem != credentials.S_ConnectivitySystem)
                    {
                        data.S_ConnectivitySystem = credentials.S_ConnectivitySystem;
                    }
                    if (data.S_MaxUpload != credentials.S_MaxUpload)
                    {
                        data.S_MaxUpload = credentials.S_MaxUpload;
                    }
                    if (data.S_MaxCharacters != credentials.S_MaxCharacters)
                    {
                        data.S_MaxCharacters = credentials.S_MaxCharacters;
                    }
                    if (data.S_Message_seen != credentials.S_Message_seen)
                    {
                        data.S_Message_seen = credentials.S_Message_seen;
                    }
                    if (data.S_Message_typing != credentials.S_Message_typing)
                    {
                        data.S_Message_typing = credentials.S_Message_typing;
                    }
                    if (data.S_AllowedExtenstion != credentials.S_AllowedExtenstion)
                    {
                        data.S_AllowedExtenstion = credentials.S_AllowedExtenstion;
                    }
                    if (data.S_Theme != credentials.S_Theme)
                    {
                        data.S_Theme = credentials.S_Theme;
                    }
                    if (data.S_DefaulColor != credentials.S_DefaulColor)
                    {
                        data.S_DefaulColor = credentials.S_DefaulColor;
                    }
                    if (data.S_Header_hover_border != credentials.S_Header_hover_border)
                    {
                        data.S_Header_hover_border = credentials.S_Header_hover_border;
                    }
                    if (data.S_Header_color != credentials.S_Header_color)
                    {
                        data.S_Header_color = credentials.S_Header_color;
                    }
                    if (data.S_Body_background != credentials.S_Body_background)
                    {
                        data.S_Body_background = credentials.S_Body_background;
                    }
                    if (data.S_btn_color != credentials.S_btn_color)
                    {
                        data.S_btn_color = credentials.S_btn_color;
                    }
                    if (data.S_SecondryColor != credentials.S_SecondryColor)
                    {
                        data.S_SecondryColor = credentials.S_SecondryColor;
                    }
                    if (data.S_btn_hover_color != credentials.S_btn_hover_color)
                    {
                        data.S_btn_hover_color = credentials.S_btn_hover_color;
                    }
                    if (data.S_btn_hover_background_color != credentials.S_btn_hover_background_color)
                    {
                        data.S_btn_hover_background_color = credentials.S_btn_hover_background_color;
                    }
                    if (data.Setting_Header_color != credentials.Setting_Header_color)
                    {
                        data.Setting_Header_color = credentials.Setting_Header_color;
                    }
                    if (data.Setting_Header_background != credentials.Setting_Header_background)
                    {
                        data.Setting_Header_background = credentials.Setting_Header_background;
                    }
                    if (data.Setting_Active_sidebar_color != credentials.Setting_Active_sidebar_color)
                    {
                        data.Setting_Active_sidebar_color = credentials.Setting_Active_sidebar_color;
                    }
                    if (data.Setting_Active_sidebar_background != credentials.Setting_Active_sidebar_background)
                    {
                        data.Setting_Active_sidebar_background = credentials.Setting_Active_sidebar_background;
                    }
                    if (data.Setting_Sidebar_background != credentials.Setting_Sidebar_background)
                    {
                        data.Setting_Sidebar_background = credentials.Setting_Sidebar_background;
                    }
                    if (data.Setting_Sidebar_color != credentials.Setting_Sidebar_color)
                    {
                        data.Setting_Sidebar_color = credentials.Setting_Sidebar_color;
                    }
                    if (data.S_Logo_extension != credentials.S_Logo_extension)
                    {
                        data.S_Logo_extension = credentials.S_Logo_extension;
                    }
                    if (data.S_Background_extension != credentials.S_Background_extension)
                    {
                        data.S_Background_extension = credentials.S_Background_extension;
                    }
                    if (data.S_Video_upload != credentials.S_Video_upload)
                    {
                        data.S_Video_upload = credentials.S_Video_upload;
                    }
                    if (data.S_Audio_upload != credentials.S_Audio_upload)
                    {
                        data.S_Audio_upload = credentials.S_Audio_upload;
                    }
                    if (data.S_Header_search_color != credentials.S_Header_search_color)
                    {
                        data.S_Header_search_color = credentials.S_Header_search_color;
                    }
                    if (data.S_Header_button_shadow != credentials.S_Header_button_shadow)
                    {
                        data.S_Header_button_shadow = credentials.S_Header_button_shadow;
                    }
                    if (data.S_btn_disabled != credentials.S_btn_disabled)
                    {
                        data.S_btn_disabled = credentials.S_btn_disabled;
                    }
                    if (data.S_User_registration != credentials.S_User_registration)
                    {
                        data.S_User_registration = credentials.S_User_registration;
                    }
                    if (data.S_Favicon_extension != credentials.S_Favicon_extension)
                    {
                        data.S_Favicon_extension = credentials.S_Favicon_extension;
                    }
                    if (data.S_Chat_outgoing_background != credentials.S_Chat_outgoing_background)
                    {
                        data.S_Chat_outgoing_background = credentials.S_Chat_outgoing_background;
                    }
                    if (data.S_Windows_app_version != credentials.S_Windows_app_version)
                    {
                        data.S_Windows_app_version = credentials.S_Windows_app_version;
                    }
                    if (data.S_Credit_card != credentials.S_Credit_card)
                    {
                        data.S_Credit_card = credentials.S_Credit_card;
                    }
                    if (data.S_Bitcoin != credentials.S_Bitcoin)
                    {
                        data.S_Bitcoin = credentials.S_Bitcoin;
                    }
                    if (data.S_m_withdrawal != credentials.S_m_withdrawal)
                    {
                        data.S_m_withdrawal = credentials.S_m_withdrawal;
                    }
                    if (data.S_Affiliate_type != credentials.S_Affiliate_type)
                    {
                        data.S_Affiliate_type = credentials.S_Affiliate_type;
                    }
                    if (data.S_Affiliate_system != credentials.S_Affiliate_system)
                    {
                        data.S_Affiliate_system = credentials.S_Affiliate_system;
                    }
                    if (data.S_Classified != credentials.S_Classified)
                    {
                        data.S_Classified = credentials.S_Classified;
                    }
                    if (data.S_Bucket_name != credentials.S_Bucket_name)
                    {
                        data.S_Bucket_name = credentials.S_Bucket_name;
                    }
                    if (data.S_Region != credentials.S_Region)
                    {
                        data.S_Region = credentials.S_Region;
                    }
                    if (data.S_Footer_background != credentials.S_Footer_background)
                    {
                        data.S_Footer_background = credentials.S_Footer_background;
                    }
                    if (data.S_Is_utf8 != credentials.S_Is_utf8)
                    {
                        data.S_Is_utf8 = credentials.S_Is_utf8;
                    }
                    if (data.S_Alipay != credentials.S_Alipay)
                    {
                        data.S_Alipay = credentials.S_Alipay;
                    }
                    if (data.S_Audio_chat != credentials.S_Audio_chat)
                    {
                        data.S_Audio_chat = credentials.S_Audio_chat;
                    }
                    if (data.S_Sms_provider != credentials.S_Sms_provider)
                    {
                        data.S_Sms_provider = credentials.S_Sms_provider;
                    }
                    if (data.S_Updated_latest != credentials.S_Updated_latest)
                    {
                        data.S_Updated_latest = credentials.S_Updated_latest;
                    }
                    if (data.S_Footer_background_2 != credentials.S_Footer_background_2)
                    {
                        data.S_Footer_background_2 = credentials.S_Footer_background_2;
                    }
                    if (data.S_Footer_background_n != credentials.S_Footer_background_n)
                    {
                        data.S_Footer_background_n = credentials.S_Footer_background_n;
                    }
                    if (data.S_Can_blogs != credentials.S_Can_blogs)
                    {
                        data.S_Can_blogs = credentials.S_Can_blogs;
                    }
                    if (data.S_Push != credentials.S_Push)
                    {
                        data.S_Push = credentials.S_Push;
                    }
                    if (data.S_Push_id != credentials.S_Push_id)
                    {
                        data.S_Push_id = credentials.S_Push_id;
                    }
                    if (data.S_Push_key != credentials.S_Push_key)
                    {
                        data.S_Push_key = credentials.S_Push_key;
                    }
                    if (data.S_Events != credentials.S_Events)
                    {
                        data.S_Events = credentials.S_Events;
                    }
                    if (data.S_Forum != credentials.S_Forum)
                    {
                        data.S_Forum = credentials.S_Forum;
                    }
                    if (data.S_Last_update != credentials.S_Last_update)
                    {
                        data.S_Last_update = credentials.S_Last_update;
                    }
                    if (data.S_Movies != credentials.S_Movies)
                    {
                        data.S_Movies = credentials.S_Movies;
                    }
                    if (data.S_Yndex_translation_api != credentials.S_Yndex_translation_api)
                    {
                        data.S_Yndex_translation_api = credentials.S_Yndex_translation_api;
                    }
                    if (data.S_Update_db_15 != credentials.S_Update_db_15)
                    {
                        data.S_Update_db_15 = credentials.S_Update_db_15;
                    }
                    if (data.S_Ad_v_price != credentials.S_Ad_v_price)
                    {
                        data.S_Ad_v_price = credentials.S_Ad_v_price;
                    }
                    if (data.S_Ad_c_price != credentials.S_Ad_c_price)
                    {
                        data.S_Ad_c_price = credentials.S_Ad_c_price;
                    }
                    if (data.S_Emo_cdn != credentials.S_Emo_cdn)
                    {
                        data.S_Emo_cdn = credentials.S_Emo_cdn;
                    }
                    if (data.S_User_ads != credentials.S_User_ads)
                    {
                        data.S_User_ads = credentials.S_User_ads;
                    }
                    if (data.S_User_status != credentials.S_User_status)
                    {
                        data.S_User_status = credentials.S_User_status;
                    }
                    if (data.S_Date_style != credentials.S_Date_style)
                    {
                        data.S_Date_style = credentials.S_Date_style;
                    }
                    if (data.S_Stickers != credentials.S_Stickers)
                    {
                        data.S_Stickers = credentials.S_Stickers;
                    }
                    if (data.S_Giphy_api != credentials.S_Giphy_api)
                    {
                        data.S_Giphy_api = credentials.S_Giphy_api;
                    }
                    if (data.S_Find_friends != credentials.S_Find_friends)
                    {
                        data.S_Find_friends = credentials.S_Find_friends;
                    }
                    if (data.S_Update_available != credentials.S_Update_available)
                    {
                        data.S_Update_available = credentials.S_Update_available;
                    }
                    if (data.S_Logo_url != credentials.S_Logo_url)
                    {
                        data.S_Logo_url = credentials.S_Logo_url;
                    }
                    if (data.S_User_messages != credentials.S_User_messages)
                    {
                        data.S_User_messages = credentials.S_User_messages;
                    }

                    //stye
                    if (data.S_NotificationDesktop != credentials.S_NotificationDesktop)
                    {
                        data.S_NotificationDesktop = credentials.S_NotificationDesktop;
                    }
                    if (data.S_NotificationPlaysound != credentials.S_NotificationPlaysound)
                    {
                        data.S_NotificationPlaysound = credentials.S_NotificationPlaysound;
                    }
                    if (data.WebClient != credentials.WebClient)
                    {
                        data.WebClient = credentials.WebClient;
                    }
                    if (data.S_BackgroundChats_images != credentials.S_BackgroundChats_images)
                    {
                        data.S_BackgroundChats_images = credentials.S_BackgroundChats_images;
                    }
                    if (data.Lang_Resources != credentials.Lang_Resources)
                    {
                        data.Lang_Resources = credentials.Lang_Resources;
                    }
                    if (data.DarkMode != credentials.DarkMode)
                    {
                        data.DarkMode = credentials.DarkMode;
                    }
                    SQLite_Entity.Connection.Update(credentials);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Get user data setting 
        public static DataBase.SettingsTable GetUsersSettings()
        {
            try
            {
                var data = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().First();
                if (data != null)
                {
                    Classes.GetSettingUser.Setting_SiteName = data.S_SiteName;
                    Classes.GetSettingUser.Setting_SiteTitle = data.S_SiteTitle;
                    Classes.GetSettingUser.Setting_SiteKeywords = data.S_SiteKeywords;
                    Classes.GetSettingUser.Setting_SiteDesc = data.S_SiteDesc;
                    Classes.GetSettingUser.Setting_DefaultLang = data.S_DefaultLang;
                    Classes.GetSettingUser.Setting_FileSharing = data.S_FileSharing;
                    Classes.GetSettingUser.Setting_ChatSystem = data.S_ChatSystem;
                    Classes.GetSettingUser.Setting_User_lastseen = data.S_User_lastseen;
                    Classes.GetSettingUser.Setting_Age = data.S_Age;
                    Classes.GetSettingUser.Setting_DeleteAccount = data.S_DeleteAccount;
                    Classes.GetSettingUser.Setting_ConnectivitySystem = data.S_ConnectivitySystem;
                    Classes.GetSettingUser.Setting_MaxUpload = data.S_MaxUpload;
                    Classes.GetSettingUser.Setting_MaxCharacters = data.S_MaxCharacters;
                    Classes.GetSettingUser.Setting_Message_seen = data.S_Message_seen;
                    Classes.GetSettingUser.Setting_Message_typing = data.S_Message_typing;
                    Classes.GetSettingUser.Setting_AllowedExtenstion = data.S_AllowedExtenstion;
                    Classes.GetSettingUser.Setting_Theme = data.S_Theme;
                    Classes.GetSettingUser.Setting_DefaulColor = data.S_DefaulColor;
                    Classes.GetSettingUser.Setting_Header_hover_border = data.S_Header_hover_border;
                    Classes.GetSettingUser.Setting_Header_color = data.S_Header_color;
                    Classes.GetSettingUser.Setting_Body_background = data.S_Body_background;
                    Classes.GetSettingUser.Setting_btn_color = data.S_btn_color;
                    Classes.GetSettingUser.Setting_SecondryColor = data.S_SecondryColor;
                    Classes.GetSettingUser.Setting_btn_hover_color = data.S_btn_hover_color;
                    Classes.GetSettingUser.Setting_btn_hover_background_color = data.S_btn_hover_background_color;
                    Classes.GetSettingUser.Setting_Header_color = data.Setting_Header_color;
                    Classes.GetSettingUser.setting_Header_background = data.Setting_Header_background;
                    Classes.GetSettingUser.setting_Active_sidebar_color = data.Setting_Active_sidebar_color;
                    Classes.GetSettingUser.setting_Active_sidebar_background = data.Setting_Active_sidebar_background;
                    Classes.GetSettingUser.setting_Sidebar_background = data.Setting_Sidebar_background;
                    Classes.GetSettingUser.setting_Sidebar_color = data.Setting_Sidebar_color;
                    Classes.GetSettingUser.Setting_Logo_extension = data.S_Logo_extension;
                    Classes.GetSettingUser.Setting_Background_extension = data.S_Background_extension;
                    Classes.GetSettingUser.Setting_Video_upload = data.S_Video_upload;
                    Classes.GetSettingUser.Setting_Audio_upload = data.S_Audio_upload;
                    Classes.GetSettingUser.Setting_Header_search_color = data.S_Header_search_color;
                    Classes.GetSettingUser.Setting_Header_button_shadow = data.S_Header_button_shadow;
                    Classes.GetSettingUser.Setting_btn_disabled = data.S_btn_disabled;
                    Classes.GetSettingUser.Setting_User_registration = data.S_User_registration;
                    Classes.GetSettingUser.Setting_Favicon_extension = data.S_Favicon_extension;
                    Classes.GetSettingUser.Setting_Chat_outgoing_background = data.S_Chat_outgoing_background;
                    Classes.GetSettingUser.Setting_Windows_app_version = data.S_Windows_app_version;
                    Classes.GetSettingUser.Setting_Credit_card = data.S_Credit_card;
                    Classes.GetSettingUser.Setting_Bitcoin = data.S_Bitcoin;
                    Classes.GetSettingUser.Setting_m_withdrawal = data.S_m_withdrawal;
                    Classes.GetSettingUser.Setting_Affiliate_type = data.S_Affiliate_type;
                    Classes.GetSettingUser.Setting_Affiliate_system = data.S_Affiliate_system;
                    Classes.GetSettingUser.Setting_Classified = data.S_Classified;
                    Classes.GetSettingUser.Setting_Bucket_name = data.S_Bucket_name;
                    Classes.GetSettingUser.Setting_Region = data.S_Region;
                    Classes.GetSettingUser.Setting_Footer_background = data.S_Footer_background;
                    Classes.GetSettingUser.Setting_Is_utf8 = data.S_Is_utf8;
                    Classes.GetSettingUser.Setting_Alipay = data.S_Alipay;
                    Classes.GetSettingUser.Setting_Audio_chat = data.S_Audio_chat;
                    Classes.GetSettingUser.Setting_Sms_provider = data.S_Sms_provider;
                    Classes.GetSettingUser.Setting_Updated_latest = data.S_Updated_latest;
                    Classes.GetSettingUser.Setting_Footer_background_2 = data.S_Footer_background_2;
                    Classes.GetSettingUser.Setting_Footer_background_n = data.S_Footer_background_n;
                    Classes.GetSettingUser.Setting_Blogs = data.S_Blogs;
                    Classes.GetSettingUser.Setting_Can_blogs = data.S_Can_blogs;
                    Classes.GetSettingUser.Setting_Push = data.S_Push;
                    Classes.GetSettingUser.Setting_Push_id = data.S_Push_id;
                    Classes.GetSettingUser.Setting_Push_key = data.S_Push_key;
                    Classes.GetSettingUser.Setting_Events = data.S_Events;
                    Classes.GetSettingUser.Setting_Forum = data.S_Forum;
                    Classes.GetSettingUser.Setting_Last_update = data.S_Last_update;
                    Classes.GetSettingUser.Setting_Movies = data.S_Movies;
                    Classes.GetSettingUser.Setting_Yndex_translation_api = data.S_Yndex_translation_api;
                    Classes.GetSettingUser.Setting_Update_db_15 = data.S_Update_db_15;
                    Classes.GetSettingUser.Setting_Ad_v_price = data.S_Ad_v_price;
                    Classes.GetSettingUser.Setting_Ad_c_price = data.S_Ad_c_price;
                    Classes.GetSettingUser.Setting_Emo_cdn = data.S_Emo_cdn;
                    Classes.GetSettingUser.Setting_User_ads = data.S_User_ads;
                    Classes.GetSettingUser.Setting_User_status = data.S_User_status;
                    Classes.GetSettingUser.Setting_Date_style = data.S_Date_style;
                    Classes.GetSettingUser.Setting_Stickers = data.S_Stickers;
                    Classes.GetSettingUser.Setting_Giphy_api = data.S_Giphy_api;
                    Classes.GetSettingUser.Setting_Find_friends = data.S_Find_friends;
                    Classes.GetSettingUser.Setting_Update_available = data.S_Update_available;
                    Classes.GetSettingUser.Setting_Logo_url = data.S_Logo_url;
                    Classes.GetSettingUser.Setting_User_messages = data.S_User_messages;

                    //stye
                    Classes.GetSettingUser.Setting_NotificationDesktop = Settings.NotificationDesktop;
                    Classes.GetSettingUser.Setting_NotificationPlaysound = Settings.NotificationPlaysound;
                    Classes.GetSettingUser.Setting_ConnationType = data.WebClient;
                    Classes.GetSettingUser.Lang_Resources = data.Lang_Resources;
                    Classes.GetSettingUser.Setting_BackgroundChats_images = data.S_BackgroundChats_images;
                    Classes.GetSettingUser.Setting_BackgroundChats_images = data.S_BackgroundChats_images;
                    Classes.GetSettingUser.DarkMode = data.DarkMode;
                    
                    return data;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            return null;
        }

        // Get Profile info 
        public static DataBase.ProfilesTable Select_From_ProfileTable_By_ID(string userId)
        {
            var profile = SQLite_Entity.Connection.Table<DataBase.ProfilesTable>()
                .FirstOrDefault(a => a.pm_UserId == userId);
            if (MemoryVariables.UsersProfileList.FirstOrDefault(a => a.pm_UserId == userId) == null)
            {
                MemoryVariables.Add_To_user_profile_List(profile);
            }
            return profile;
        }

        //Insert data To Profile Table
        public static void Insert_Or_Replace_To_ProfileTable(DataBase.ProfilesTable credentials)
        {
            var Result = SQLite_Entity.Connection.Table<DataBase.ProfilesTable>()
                .FirstOrDefault(a => a.pm_UserId == credentials.pm_UserId);
            string AvatarSplit = credentials.pm_Avatar.Split('/').Last();

            if (Result == null)
            {
                SQLite_Entity.Connection.Insert(credentials);
            }
            else
            {
                if (Result.pm_Username != credentials.pm_Username || Result.pm_Email != credentials.pm_Email ||
                    Result.pm_First_name != credentials.pm_First_name ||
                    Result.pm_Last_name != credentials.pm_Last_name ||
                    Result.pm_Avatar != Functions.Get_image(UserDetails.User_id, AvatarSplit, credentials.pm_Avatar)
                    || Result.pm_Relationship_id != credentials.pm_Relationship_id ||
                    Result.pm_Address != credentials.pm_Address ||
                    Result.pm_Working != credentials.pm_Working ||
                    Result.pm_Working_link != credentials.pm_Working_link ||
                    Result.pm_About != credentials.pm_About
                    || Result.pm_School != credentials.pm_School || Result.pm_Gender != credentials.pm_Gender ||
                    Result.pm_Birthday != credentials.pm_Birthday || Result.pm_Website != credentials.pm_Website ||
                    Result.pm_Facebook != credentials.pm_Facebook
                    || Result.pm_Google != credentials.pm_Google || Result.pm_Twitter != credentials.pm_Twitter ||
                    Result.pm_Linkedin != credentials.pm_Linkedin || Result.pm_Youtube != credentials.pm_Youtube ||
                    Result.pm_Vk != credentials.pm_Vk || Result.pm_Instagram != credentials.pm_Instagram
                    || Result.pm_Language != credentials.pm_Language ||
                    Result.pm_Ip_address != credentials.pm_Ip_address ||
                    Result.pm_Verified != credentials.pm_Verified || Result.pm_Lastseen != credentials.pm_Lastseen ||
                    Result.pm_Showlastseen != credentials.pm_Showlastseen
                    || Result.pm_Status != credentials.pm_Status || Result.pm_Active != credentials.pm_Active ||
                    Result.pm_Admin != credentials.pm_Admin || Result.pm_Registered != credentials.pm_Registered ||
                    Result.pm_Phone_number != credentials.pm_Phone_number || Result.pm_Is_pro != credentials.pm_Is_pro
                    || Result.pm_Pro_type != credentials.pm_Pro_type || Result.pm_Joined != credentials.pm_Joined ||
                    Result.pm_Timezone != credentials.pm_Timezone || Result.pm_Referrer != credentials.pm_Referrer ||
                    Result.pm_Balance != credentials.pm_Balance || Result.pm_Paypal_email != credentials.pm_Paypal_email
                    || Result.pm_Notifications_sound != credentials.pm_Notifications_sound ||
                    Result.pm_Order_posts_by != credentials.pm_Order_posts_by ||
                    Result.pm_Social_login != credentials.pm_Social_login ||
                    Result.pm_Device_id != credentials.pm_Device_id ||
                    Result.pm_Url != credentials.pm_Url
                    || Result.pm_Name != credentials.pm_Name
                )
                {
                    SQLite_Entity.Connection.Update(credentials);
                }
            }
        }

        //Insert Or Update or Delete data To Chat Activity Table
        public static void Insert_Or_Replace_ChatActivity(ObservableCollection<Users> credentialsList)
        {
            try
            {
                List<DataBase.ChatActivity> ListOfDatabaseforInsert = new List<DataBase.ChatActivity>();
                List<DataBase.ChatActivity> ListOfDatabaseForUpdate = new List<DataBase.ChatActivity>();
                // get data from database
                var Result = SQLite_Entity.Connection.Table<DataBase.ChatActivity>().ToList();
                var ConvertedClass = Functions.ConvertFromChatActivity(Result);

                foreach (var credentials in credentialsList)
                {
                    DataBase.ChatActivity csc = new DataBase.ChatActivity();
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

                    var DataCheck = ConvertedClass.FirstOrDefault(a => a.U_Id == credentials.U_Id);

                    if (DataCheck != null)
                    {
                        var CheckbeforUpdate = Result.FirstOrDefault(a => a.U_Id == DataCheck.U_Id);
                        if (
                            CheckbeforUpdate.U_username != credentials.U_username ||
                            CheckbeforUpdate.U_name != credentials.U_name ||
                            CheckbeforUpdate.U_profile_picture != credentials.U_profile_picture ||
                            CheckbeforUpdate.U_cover_picture != credentials.U_cover_picture ||
                            CheckbeforUpdate.U_verified != credentials.U_verified ||
                            CheckbeforUpdate.U_lastseen != credentials.U_lastseen ||
                            CheckbeforUpdate.U_lastseenWithoutCut != credentials.U_lastseenWithoutCut ||
                            CheckbeforUpdate.U_lastseen_time_text != credentials.U_lastseen_time_text ||
                            CheckbeforUpdate.u_url != credentials.u_url ||
                            CheckbeforUpdate.U_lastseen_unix_time != credentials.U_lastseen_unix_time ||
                            CheckbeforUpdate.U_chat_color != credentials.U_chat_color ||
                            CheckbeforUpdate.M_Id != credentials.M_Id ||
                            CheckbeforUpdate.From_Id != credentials.From_Id ||
                            CheckbeforUpdate.To_Id != credentials.To_Id ||
                            CheckbeforUpdate.M_text != credentials.M_text ||
                            CheckbeforUpdate.M_media != credentials.M_media ||
                            CheckbeforUpdate.M_mediaFileName != credentials.M_mediaFileName ||
                            CheckbeforUpdate.M_mediaFileNamese != credentials.M_mediaFileNamese ||
                            CheckbeforUpdate.M_time != credentials.M_time ||
                            CheckbeforUpdate.M_date_time != credentials.M_date_time ||
                            CheckbeforUpdate.M_seen != credentials.M_seen ||
                            CheckbeforUpdate.M_stickers != credentials.M_stickers ||
                            CheckbeforUpdate.S_Color_onof != credentials.S_Color_onof ||
                            CheckbeforUpdate.S_ImageProfile != credentials.S_ImageProfile ||
                            CheckbeforUpdate.S_noProfile_color != credentials.S_noProfile_color ||
                            CheckbeforUpdate.S_Message_FontWeight != credentials.S_Message_FontWeight ||
                            CheckbeforUpdate.S_Message_color != credentials.S_Message_color ||
                            CheckbeforUpdate.IsSeeniconcheck != credentials.IsSeeniconcheck ||
                            CheckbeforUpdate.ChatColorcirclevisibilty != credentials.ChatColorcirclevisibilty ||
                            CheckbeforUpdate.MediaIconvisibilty != credentials.MediaIconvisibilty ||
                            CheckbeforUpdate.MediaIconImage != credentials.MediaIconImage ||
                            CheckbeforUpdate.App_Main_Later != credentials.App_Main_Later ||
                            CheckbeforUpdate.UsernameTwoLetters != credentials.UsernameTwoLetters 
                        )
                        {
                            CheckbeforUpdate.U_Id = credentials.U_Id;
                            CheckbeforUpdate.U_username = credentials.U_username;
                            CheckbeforUpdate.U_name = credentials.U_name;
                            CheckbeforUpdate.U_profile_picture = credentials.U_profile_picture;
                            CheckbeforUpdate.U_cover_picture = credentials.U_cover_picture;
                            CheckbeforUpdate.U_verified = credentials.U_verified;
                            CheckbeforUpdate.U_lastseen = credentials.U_lastseen;
                            CheckbeforUpdate.U_lastseenWithoutCut = credentials.U_lastseenWithoutCut;
                            CheckbeforUpdate.U_lastseen_time_text = credentials.U_lastseen_time_text;
                            CheckbeforUpdate.u_url = credentials.u_url;
                            CheckbeforUpdate.U_lastseen_unix_time = credentials.U_lastseen_unix_time;
                            CheckbeforUpdate.U_chat_color = credentials.U_chat_color;
                            CheckbeforUpdate.M_Id = credentials.M_Id;
                            CheckbeforUpdate.From_Id = credentials.From_Id;
                            CheckbeforUpdate.To_Id = credentials.To_Id;
                            CheckbeforUpdate.M_text = credentials.M_text;
                            CheckbeforUpdate.M_media = credentials.M_media;
                            CheckbeforUpdate.M_mediaFileName = credentials.M_mediaFileName;
                            CheckbeforUpdate.M_mediaFileNamese = credentials.M_mediaFileNamese;
                            CheckbeforUpdate.M_time = credentials.M_time;
                            CheckbeforUpdate.M_date_time = credentials.M_date_time;
                            CheckbeforUpdate.M_seen = credentials.M_seen;
                            CheckbeforUpdate.M_stickers = credentials.M_stickers;
                            CheckbeforUpdate.S_Color_onof = credentials.S_Color_onof;
                            CheckbeforUpdate.S_ImageProfile = credentials.S_ImageProfile;
                            CheckbeforUpdate.S_noProfile_color = credentials.S_noProfile_color;
                            CheckbeforUpdate.S_Message_FontWeight = credentials.S_Message_FontWeight;
                            CheckbeforUpdate.S_Message_color = credentials.S_Message_color;
                            CheckbeforUpdate.IsSeeniconcheck = credentials.IsSeeniconcheck;
                            CheckbeforUpdate.ChatColorcirclevisibilty = credentials.ChatColorcirclevisibilty;
                            CheckbeforUpdate.MediaIconvisibilty = credentials.MediaIconvisibilty;
                            CheckbeforUpdate.MediaIconImage = credentials.MediaIconImage;
                            CheckbeforUpdate.App_Main_Later = credentials.App_Main_Later;
                            CheckbeforUpdate.UsernameTwoLetters = credentials.UsernameTwoLetters;

                            ListOfDatabaseForUpdate.Add(CheckbeforUpdate);
                        }
                    }
                    else
                    {
                        ListOfDatabaseforInsert.Add(csc);
                    }
                }
                var Deletelist = ConvertedClass.Where(c => !credentialsList.Select(fc => fc.U_Id).Contains(c.U_Id))
                    .ToList();
                foreach (var row in Deletelist)
                {
                    var Delete = SQLite_Entity.Connection.Table<DataBase.ChatActivity>()
                        .FirstOrDefault(a => a.U_Id == row.U_Id);
                    SQLite_Entity.Connection.Delete(Delete);
                }

                if (ListOfDatabaseforInsert.Count > 0 || ListOfDatabaseForUpdate.Count > 0)
                {
                    SQLite_Entity.Connection.BeginTransaction();
                    if (ListOfDatabaseforInsert.Count > 0)
                    {
                        SQLite_Entity.Connection.InsertAll(ListOfDatabaseforInsert);
                    }
                    else if (ListOfDatabaseForUpdate.Count > 0)
                    {
                        SQLite_Entity.Connection.UpdateAll(ListOfDatabaseForUpdate);
                    }

                    SQLite_Entity.Connection.Commit();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        //Update one data user in Users Contact Table
        public static void Update_one_UsersContactTable(DataBase.UsersContactProfileTable credentials)
        {
            try
            {
                var data = SQLite_Entity.Connection.Table<DataBase.UsersContactProfileTable>().FirstOrDefault(a => a.UCP_Id == credentials.UCP_Id);
                if (data != null)
                {
                    data.UCP_Id = credentials.UCP_Id;
                    data.UCP_username = credentials.UCP_username;
                    data.UCP_name = credentials.UCP_name;
                    data.UCP_cover_picture = credentials.UCP_cover_picture;
                    data.UCP_profile_picture = credentials.UCP_profile_picture;
                    data.UCP_verified = credentials.UCP_verified;
                    data.UCP_lastseen = credentials.UCP_lastseen;
                    data.UCP_lastseen_time_text = credentials.UCP_lastseen_time_text;
                    data.UCP_lastseen_unix_time = credentials.UCP_lastseen_unix_time;
                    data.UCP_url = credentials.UCP_url;
                    data.UCP_user_platform = credentials.UCP_user_platform;
                    data.UCP_chat_color = credentials.UCP_chat_color;
                    data.UCP_Notifications_Message_user = credentials.UCP_Notifications_Message_user;
                    data.UCP_Notifications_Message_Sound_user = credentials.UCP_Notifications_Message_Sound_user;
                    //user_profile
                    data.UCP_email = credentials.UCP_email;
                    data.UCP_first_name = credentials.UCP_first_name;
                    data.UCP_last_name = credentials.UCP_last_name;
                    data.UCP_relationship_id = credentials.UCP_relationship_id;
                    data.UCP_address = credentials.UCP_address;
                    data.UCP_working = credentials.UCP_working;
                    data.UCP_working_link = credentials.UCP_working_link;
                    data.UCP_about = credentials.UCP_about;
                    data.UCP_school = credentials.UCP_school;
                    data.UCP_gender = credentials.UCP_gender;
                    data.UCP_birthday = credentials.UCP_birthday;
                    data.UCP_website = credentials.UCP_website;
                    data.UCP_facebook = credentials.UCP_facebook;
                    data.UCP_google = credentials.UCP_google;
                    data.UCP_twitter = credentials.UCP_twitter;
                    data.UCP_linkedin = credentials.UCP_linkedin;
                    data.UCP_youtube = credentials.UCP_youtube;
                    data.UCP_vk = credentials.UCP_vk;
                    data.UCP_instagram = credentials.UCP_instagram;
                    data.UCP_language = credentials.UCP_language;
                    data.UCP_ip_address = credentials.UCP_ip_address;
                    data.UCP_follow_privacy = credentials.UCP_follow_privacy;
                    data.UCP_post_privacy = credentials.UCP_post_privacy;
                    data.UCP_message_privacy = credentials.UCP_message_privacy;
                    data.UCP_confirm_followers = credentials.UCP_confirm_followers;
                    data.UCP_show_activities_privacy = credentials.UCP_show_activities_privacy;
                    data.UCP_birth_privacy = credentials.UCP_birth_privacy;
                    data.UCP_visit_privacy = credentials.UCP_visit_privacy;
                    data.UCP_showlastseen = credentials.UCP_showlastseen;
                    data.UCP_status = credentials.UCP_status;
                    data.UCP_active = credentials.UCP_active;
                    data.UCP_admin = credentials.UCP_admin;
                    data.UCP_registered = credentials.UCP_registered;
                    data.UCP_phone_number = credentials.UCP_phone_number;
                    data.UCP_is_pro = credentials.UCP_is_pro;
                    data.UCP_pro_type = credentials.UCP_pro_type;
                    data.UCP_joined = credentials.UCP_joined;
                    data.UCP_timezone = credentials.UCP_timezone;
                    data.UCP_referrer = credentials.UCP_referrer;
                    data.UCP_balance = credentials.UCP_balance;
                    data.UCP_paypal_email = credentials.UCP_paypal_email;
                    data.UCP_notifications_sound = credentials.UCP_notifications_sound;
                    data.UCP_order_posts_by = credentials.UCP_order_posts_by;
                    data.UCP_social_login = credentials.UCP_social_login;
                    data.UCP_device_id = credentials.UCP_device_id;

                    SQLite_Entity.Connection.Update(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Get data To Chat Activity List
        public static void GetChatActivityList()
        {
            try
            {
                var Result = SQLite_Entity.Connection.Table<DataBase.ChatActivity>().ToList();
                var data = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                foreach (var item in Result)
                {
                    Users us = new Users();
                    us.U_Id = item.U_Id;
                    us.U_username = item.U_username;
                    us.U_name = Functions.HtmlDecodestring(Functions.SubStringCutOf(item.U_name, 15));
                    us.U_cover_picture = item.U_cover_picture;

                    string AvatarSplit = item.U_profile_picture.Split('/').Last();

                    us.U_profile_picture = Functions.Get_image(item.U_Id, AvatarSplit, item.U_profile_picture);
                    us.U_verified = item.U_verified;
                    us.U_lastseen = item.U_lastseen;
                    us.U_lastseen_time_text = "";
                    us.U_lastseen_unix_time = item.U_lastseen_unix_time;
                    us.U_chat_color = item.U_chat_color;
                    us.U_lastseenWithoutCut = item.U_lastseenWithoutCut;
                    us.u_url = item.u_url;
                    us.M_Id = item.M_Id;
                    us.From_Id = item.From_Id;
                    us.To_Id = item.To_Id;
                    us.M_text = Functions.HtmlDecodestring(Functions.SubStringCutOf(item.M_text, 35));
                    us.M_media = item.M_media;
                    us.M_mediaFileName = item.M_mediaFileName;
                    us.M_mediaFileNamese = item.M_mediaFileNamese;
                    us.M_time = item.M_time;
                    us.M_seen = item.M_seen;
                    us.M_date_time = item.M_date_time;
                    us.M_stickers = item.M_stickers;
                    //style
                    us.S_Color_onof = "#C0C0C0"; //silver
                    us.App_Main_Later = Settings.Application_Name.Substring(0, 1);
                    us.S_ImageProfile = item.S_ImageProfile;
                    us.S_noProfile_color = item.S_noProfile_color;
                    us.S_Message_FontWeight = item.S_Message_FontWeight;
                    us.S_Message_color = item.S_Message_color;
                    us.IsSeeniconcheck = item.IsSeeniconcheck;
                    us.ChatColorcirclevisibilty = item.ChatColorcirclevisibilty;
                    us.MediaIconImage = item.MediaIconImage;
                    us.MediaIconvisibilty = item.MediaIconvisibilty;
                    us.UsernameTwoLetters = Functions.GetoLettersfromString(item.U_name);

                    if (MainWindow.ModeDarkstlye)
                    {
                        us.S_Color_Background = "#232323";
                        us.S_Color_Foreground = "#ffff";
                    }
                    else
                    {
                        us.S_Color_Background = "#ffff";
                        us.S_Color_Foreground = "#444";
                    }
                  
                    MainWindow.ListUsers.Add(us);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Insert data To Users Contact Table
        public static void Insert_Or_Replace_UsersContactTable(ObservableCollection<UsersContact> credentialsList)
        {
            try
            {
                List<DataBase.UsersContactTable> listOfDatabaseforInsert = new List<DataBase.UsersContactTable>();
                List<DataBase.UsersContactTable> listOfDatabaseForUpdate = new List<DataBase.UsersContactTable>();
                // get data from database
                var result = SQLite_Entity.Connection.Table<DataBase.UsersContactTable>().ToList();
                var Convertedclass = Functions.ConvertFromUsersContact(result);

                foreach (var credentials in credentialsList)
                {
                    DataBase.UsersContactTable uc = new DataBase.UsersContactTable();

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


                    var DataCheck = Convertedclass.FirstOrDefault(a => a.UC_Id == credentials.UC_Id);

                    if (DataCheck != null)
                    {
                        var CheckbeforUpdate = result.FirstOrDefault(a => a.UC_Id == DataCheck.UC_Id);
                        if (
                            CheckbeforUpdate.UC_username != credentials.UC_username ||
                            CheckbeforUpdate.UC_name != credentials.UC_name ||
                            CheckbeforUpdate.UC_profile_picture != credentials.UC_profile_picture ||
                            CheckbeforUpdate.UC_cover_picture != credentials.UC_cover_picture ||
                            CheckbeforUpdate.UC_verified != credentials.UC_verified ||
                            CheckbeforUpdate.UC_lastseen != credentials.UC_lastseen ||
                            CheckbeforUpdate.UC_lastseen_time_text != credentials.UC_lastseen_time_text ||
                            CheckbeforUpdate.UC_lastseen_unix_time != credentials.UC_lastseen_unix_time ||
                            CheckbeforUpdate.UC_url != credentials.UC_url ||
                            CheckbeforUpdate.UC_user_platform != credentials.UC_user_platform ||
                            CheckbeforUpdate.UC_Color_onof != credentials.UC_Color_onof ||
                            CheckbeforUpdate.UC_App_Main_Later != credentials.UC_App_Main_Later ||
                            CheckbeforUpdate.UC_chat_color != credentials.UC_chat_color ||
                            CheckbeforUpdate.UC_email != credentials.UC_email ||
                            CheckbeforUpdate.UC_first_name != credentials.UC_first_name ||
                            CheckbeforUpdate.UC_last_name != credentials.UC_last_name ||
                            CheckbeforUpdate.UC_relationship_id != credentials.UC_relationship_id ||
                            CheckbeforUpdate.UC_address != credentials.UC_address ||
                            CheckbeforUpdate.UC_working != credentials.UC_working ||
                            CheckbeforUpdate.UC_working_link != credentials.UC_working_link ||
                            CheckbeforUpdate.UC_about != credentials.UC_about ||
                            CheckbeforUpdate.UC_school != credentials.UC_school ||
                            CheckbeforUpdate.UC_gender != credentials.UC_gender ||
                            CheckbeforUpdate.UC_birthday != credentials.UC_birthday ||
                            CheckbeforUpdate.UC_website != credentials.UC_website ||
                            CheckbeforUpdate.UC_facebook != credentials.UC_facebook ||
                            CheckbeforUpdate.UC_google != credentials.UC_google ||
                            CheckbeforUpdate.UC_twitter != credentials.UC_twitter ||
                            CheckbeforUpdate.UC_linkedin != credentials.UC_linkedin ||
                            CheckbeforUpdate.UC_youtube != credentials.UC_youtube ||
                            CheckbeforUpdate.UC_vk != credentials.UC_vk ||
                            CheckbeforUpdate.UC_instagram != credentials.UC_instagram ||
                            CheckbeforUpdate.UC_language != credentials.UC_language ||
                            CheckbeforUpdate.UC_ip_address != credentials.UC_ip_address ||
                            CheckbeforUpdate.UC_follow_privacy != credentials.UC_follow_privacy ||
                            CheckbeforUpdate.UC_post_privacy != credentials.UC_post_privacy ||
                            CheckbeforUpdate.UC_message_privacy != credentials.UC_message_privacy ||
                            CheckbeforUpdate.UC_confirm_followers != credentials.UC_confirm_followers ||
                            CheckbeforUpdate.UC_show_activities_privacy != credentials.UC_show_activities_privacy ||
                            CheckbeforUpdate.UC_birth_privacy != credentials.UC_birth_privacy ||
                            CheckbeforUpdate.UC_visit_privacy != credentials.UC_visit_privacy ||
                            CheckbeforUpdate.UC_showlastseen != credentials.UC_showlastseen ||
                            CheckbeforUpdate.UC_status != credentials.UC_status ||
                            CheckbeforUpdate.UC_active != credentials.UC_active ||
                            CheckbeforUpdate.UC_admin != credentials.UC_admin ||
                            CheckbeforUpdate.UC_registered != credentials.UC_registered ||
                            CheckbeforUpdate.UC_phone_number != credentials.UC_phone_number ||
                            CheckbeforUpdate.UC_is_pro != credentials.UC_is_pro ||
                            CheckbeforUpdate.UC_pro_type != credentials.UC_pro_type ||
                            CheckbeforUpdate.UC_joined != credentials.UC_joined ||
                            CheckbeforUpdate.UC_timezone != credentials.UC_timezone ||
                            CheckbeforUpdate.UC_referrer != credentials.UC_referrer ||
                            CheckbeforUpdate.UC_balance != credentials.UC_balance ||
                            CheckbeforUpdate.UC_paypal_email != credentials.UC_paypal_email ||
                            CheckbeforUpdate.UC_notifications_sound != credentials.UC_notifications_sound ||
                            CheckbeforUpdate.UC_order_posts_by != credentials.UC_order_posts_by ||
                            CheckbeforUpdate.UC_social_login != credentials.UC_social_login ||
                            CheckbeforUpdate.UC_device_id != credentials.UC_device_id
                        )
                        {
                            CheckbeforUpdate.UC_Id = credentials.UC_Id;
                            CheckbeforUpdate.UC_username = credentials.UC_username;
                            CheckbeforUpdate.UC_name = credentials.UC_name;
                            CheckbeforUpdate.UC_profile_picture = credentials.UC_profile_picture;
                            CheckbeforUpdate.UC_cover_picture = credentials.UC_cover_picture;
                            CheckbeforUpdate.UC_verified = credentials.UC_verified;
                            CheckbeforUpdate.UC_lastseen = credentials.UC_lastseen;
                            CheckbeforUpdate.UC_lastseen_time_text = credentials.UC_lastseen_time_text;
                            CheckbeforUpdate.UC_lastseen_unix_time = credentials.UC_lastseen_unix_time;
                            CheckbeforUpdate.UC_url = credentials.UC_url;
                            CheckbeforUpdate.UC_user_platform = credentials.UC_user_platform;
                            CheckbeforUpdate.UC_Color_onof = credentials.UC_Color_onof;
                            CheckbeforUpdate.UC_App_Main_Later = credentials.UC_App_Main_Later;
                            CheckbeforUpdate.UC_chat_color = credentials.UC_chat_color;

                            //user_profile
                            CheckbeforUpdate.UC_email = credentials.UC_email;
                            CheckbeforUpdate.UC_first_name = credentials.UC_first_name;
                            CheckbeforUpdate.UC_last_name = credentials.UC_last_name;
                            CheckbeforUpdate.UC_relationship_id = credentials.UC_relationship_id;
                            CheckbeforUpdate.UC_address = credentials.UC_address;
                            CheckbeforUpdate.UC_working = credentials.UC_working;
                            CheckbeforUpdate.UC_working_link = credentials.UC_working_link;
                            CheckbeforUpdate.UC_about = credentials.UC_about;
                            CheckbeforUpdate.UC_school = credentials.UC_school;
                            CheckbeforUpdate.UC_gender = credentials.UC_gender;
                            CheckbeforUpdate.UC_birthday = credentials.UC_birthday;
                            CheckbeforUpdate.UC_website = credentials.UC_website;
                            CheckbeforUpdate.UC_facebook = credentials.UC_facebook;
                            CheckbeforUpdate.UC_google = credentials.UC_google;
                            CheckbeforUpdate.UC_twitter = credentials.UC_twitter;
                            CheckbeforUpdate.UC_linkedin = credentials.UC_linkedin;
                            CheckbeforUpdate.UC_youtube = credentials.UC_youtube;
                            CheckbeforUpdate.UC_vk = credentials.UC_vk;
                            CheckbeforUpdate.UC_instagram = credentials.UC_instagram;
                            CheckbeforUpdate.UC_language = credentials.UC_language;
                            CheckbeforUpdate.UC_ip_address = credentials.UC_ip_address;
                            CheckbeforUpdate.UC_follow_privacy = credentials.UC_follow_privacy;
                            CheckbeforUpdate.UC_post_privacy = credentials.UC_post_privacy;
                            CheckbeforUpdate.UC_message_privacy = credentials.UC_message_privacy;
                            CheckbeforUpdate.UC_confirm_followers = credentials.UC_confirm_followers;
                            CheckbeforUpdate.UC_show_activities_privacy = credentials.UC_show_activities_privacy;
                            CheckbeforUpdate.UC_birth_privacy = credentials.UC_birth_privacy;
                            CheckbeforUpdate.UC_visit_privacy = credentials.UC_visit_privacy;
                            CheckbeforUpdate.UC_showlastseen = credentials.UC_showlastseen;
                            CheckbeforUpdate.UC_status = credentials.UC_status;
                            CheckbeforUpdate.UC_active = credentials.UC_active;
                            CheckbeforUpdate.UC_admin = credentials.UC_admin;
                            CheckbeforUpdate.UC_registered = credentials.UC_registered;
                            CheckbeforUpdate.UC_phone_number = credentials.UC_phone_number;
                            CheckbeforUpdate.UC_is_pro = credentials.UC_is_pro;
                            CheckbeforUpdate.UC_pro_type = credentials.UC_pro_type;
                            CheckbeforUpdate.UC_joined = credentials.UC_joined;
                            CheckbeforUpdate.UC_timezone = credentials.UC_timezone;
                            CheckbeforUpdate.UC_referrer = credentials.UC_referrer;
                            CheckbeforUpdate.UC_balance = credentials.UC_balance;
                            CheckbeforUpdate.UC_paypal_email = credentials.UC_paypal_email;
                            CheckbeforUpdate.UC_notifications_sound = credentials.UC_notifications_sound;
                            CheckbeforUpdate.UC_order_posts_by = credentials.UC_order_posts_by;
                            CheckbeforUpdate.UC_social_login = credentials.UC_social_login;
                            CheckbeforUpdate.UC_device_id = credentials.UC_device_id;

                            listOfDatabaseForUpdate.Add(CheckbeforUpdate);
                        }
                    }
                    else
                    {
                        listOfDatabaseforInsert.Add(uc);
                    }
                }
                var deletelist = Convertedclass.Where(c => !credentialsList.Select(fc => fc.UC_Id).Contains(c.UC_Id))
                    .ToList();
                foreach (var row in deletelist)
                {
                    var delete = SQLite_Entity.Connection.Table<DataBase.UsersContactTable>()
                        .FirstOrDefault(a => a.UC_Id == row.UC_Id);
                    SQLite_Entity.Connection.Delete(delete);
                }

                if (listOfDatabaseforInsert.Count > 0 || listOfDatabaseForUpdate.Count > 0)
                {
                    SQLite_Entity.Connection.BeginTransaction();
                    if (listOfDatabaseforInsert.Count > 0)
                    {
                        SQLite_Entity.Connection.InsertAll(listOfDatabaseforInsert);
                    }
                    else if (listOfDatabaseForUpdate.Count > 0)
                    {
                        SQLite_Entity.Connection.UpdateAll(listOfDatabaseForUpdate);
                    }
                    SQLite_Entity.Connection.Commit();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        //Insert Or Update data To Messages Table
        public static void Insert_Or_Replace_Messages(ObservableCollection<Classes.Messages> credentialsList)
        {
            try
            {
                List<DataBase.MessagesTable> ListOfDatabaseforInsert = new List<DataBase.MessagesTable>();
                List<DataBase.MessagesTable> ListOfDatabaseForUpdate = new List<DataBase.MessagesTable>();
                // get data from database
                var Result = SQLite_Entity.Connection.Table<DataBase.MessagesTable>().ToList();
                var ConvertedClass = Functions.ConvertFromMessages(Result);

                foreach (var credentials in credentialsList)
                {
                    DataBase.MessagesTable m = new DataBase.MessagesTable();
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

                    var DataCheck = ConvertedClass.FirstOrDefault(a => a.Mes_Id == credentials.Mes_Id);

                    if (DataCheck != null)
                    {
                        var CheckbeforUpdate = Result.FirstOrDefault(a => a.Mes_Id == DataCheck.Mes_Id);
                        if (
                            CheckbeforUpdate.Mes_Id != credentials.Mes_Id ||
                            CheckbeforUpdate.Mes_From_Id != credentials.Mes_From_Id ||
                            CheckbeforUpdate.Mes_To_Id != credentials.Mes_To_Id ||
                            CheckbeforUpdate.Mes_Text != credentials.Mes_Text ||
                            CheckbeforUpdate.Mes_Media != credentials.Mes_Media ||
                            CheckbeforUpdate.Mes_MediaFileName != credentials.Mes_MediaFileName ||
                            CheckbeforUpdate.Mes_MediaFileNames != credentials.Mes_MediaFileNames ||
                            CheckbeforUpdate.Mes_Time != credentials.Mes_Time ||
                            CheckbeforUpdate.Mes_Seen != credentials.Mes_Seen ||
                            CheckbeforUpdate.Mes_Deleted_one != credentials.Mes_Deleted_one ||
                            CheckbeforUpdate.Mes_Deleted_two != credentials.Mes_Deleted_two ||
                            CheckbeforUpdate.Mes_Sent_push != credentials.Mes_Sent_push ||
                            CheckbeforUpdate.Mes_Notification_id != credentials.Mes_Notification_id ||
                            CheckbeforUpdate.Mes_Type_two != credentials.Mes_Type_two ||
                            CheckbeforUpdate.Mes_Time_text != credentials.Mes_Time_text ||
                            CheckbeforUpdate.Mes_Position != credentials.Mes_Position ||
                            CheckbeforUpdate.Mes_Type != credentials.Mes_Type ||
                            CheckbeforUpdate.Mes_File_size != credentials.Mes_File_size ||
                            CheckbeforUpdate.Mes_Stickers != credentials.Mes_Stickers ||
                            CheckbeforUpdate.Mes_User_avatar != credentials.Mes_User_avatar ||
                            CheckbeforUpdate.Progress_Value != credentials.Progress_Value ||
                            CheckbeforUpdate.sound_time != credentials.sound_time ||
                            CheckbeforUpdate.sound_slider_value != credentials.sound_slider_value ||
                            CheckbeforUpdate.Pause_Visibility != credentials.Pause_Visibility ||
                            CheckbeforUpdate.Play_Visibility != credentials.Play_Visibility ||
                            CheckbeforUpdate.Download_Visibility != credentials.Download_Visibility ||
                            CheckbeforUpdate.Progress_Visibility != credentials.Progress_Visibility ||
                            CheckbeforUpdate.Icon_File_Visibility != credentials.Icon_File_Visibility ||
                            CheckbeforUpdate.Hlink_Download_Visibility != credentials.Hlink_Download_Visibility ||
                            CheckbeforUpdate.Hlink_Open_Visibility != credentials.Hlink_Open_Visibility ||
                            CheckbeforUpdate.Img_user_message != credentials.Img_user_message ||
                            CheckbeforUpdate.Type_Icon_File != credentials.Type_Icon_File ||
                            CheckbeforUpdate.Color_box_message != credentials.Color_box_message
                        )
                        {
                            CheckbeforUpdate.Mes_Id = credentials.Mes_Id;
                            CheckbeforUpdate.Mes_From_Id = credentials.Mes_From_Id;
                            CheckbeforUpdate.Mes_To_Id = credentials.Mes_To_Id;
                            CheckbeforUpdate.Mes_Text = credentials.Mes_Text;
                            CheckbeforUpdate.Mes_Media = credentials.Mes_Media;
                            CheckbeforUpdate.Mes_MediaFileName = credentials.Mes_MediaFileName;
                            CheckbeforUpdate.Mes_MediaFileNames = credentials.Mes_MediaFileNames;
                            CheckbeforUpdate.Mes_Time = credentials.Mes_Time;
                            CheckbeforUpdate.Mes_Seen = credentials.Mes_Seen;
                            CheckbeforUpdate.Mes_Deleted_one = credentials.Mes_Deleted_one;
                            CheckbeforUpdate.Mes_Deleted_two = credentials.Mes_Deleted_two;
                            CheckbeforUpdate.Mes_Sent_push = credentials.Mes_Sent_push;
                            CheckbeforUpdate.Mes_Notification_id = credentials.Mes_Notification_id;
                            CheckbeforUpdate.Mes_Type_two = credentials.Mes_Type_two;
                            CheckbeforUpdate.Mes_Time_text = credentials.Mes_Time_text;
                            CheckbeforUpdate.Mes_Position = credentials.Mes_Position;
                            CheckbeforUpdate.Mes_Type = credentials.Mes_Type;
                            CheckbeforUpdate.Mes_File_size = credentials.Mes_File_size;
                            CheckbeforUpdate.Mes_Stickers = credentials.Mes_Stickers;
                            CheckbeforUpdate.Mes_User_avatar = credentials.Mes_User_avatar;
                            CheckbeforUpdate.Progress_Value = credentials.Progress_Value;
                            CheckbeforUpdate.sound_time = credentials.sound_time;
                            CheckbeforUpdate.sound_slider_value = credentials.sound_slider_value;
                            CheckbeforUpdate.Pause_Visibility = credentials.Pause_Visibility;
                            CheckbeforUpdate.Play_Visibility = credentials.Play_Visibility;
                            CheckbeforUpdate.Download_Visibility = credentials.Download_Visibility;
                            CheckbeforUpdate.Progress_Visibility = credentials.Progress_Visibility;
                            CheckbeforUpdate.Icon_File_Visibility = credentials.Icon_File_Visibility;
                            CheckbeforUpdate.Hlink_Download_Visibility = credentials.Hlink_Download_Visibility;
                            CheckbeforUpdate.Hlink_Open_Visibility = credentials.Hlink_Open_Visibility;
                            CheckbeforUpdate.Img_user_message = credentials.Img_user_message;
                            CheckbeforUpdate.Type_Icon_File = credentials.Type_Icon_File;
                            CheckbeforUpdate.Color_box_message = credentials.Color_box_message;

                            ListOfDatabaseForUpdate.Add(CheckbeforUpdate);
                        }
                    }
                    else
                    {
                        ListOfDatabaseforInsert.Add(m);
                    }
                }

                if (ListOfDatabaseforInsert.Count > 0 || ListOfDatabaseForUpdate.Count > 0)
                {
                    SQLite_Entity.Connection.BeginTransaction();
                    if (ListOfDatabaseforInsert.Count > 0)
                    {
                        SQLite_Entity.Connection.InsertAll(ListOfDatabaseforInsert);
                    }
                    else if (ListOfDatabaseForUpdate.Count > 0)
                    {
                        SQLite_Entity.Connection.UpdateAll(ListOfDatabaseForUpdate);
                    }

                    SQLite_Entity.Connection.Commit();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        //Update one Messages Table
        public static void Insert_Or_Update_To_one_MessagesTable(Classes.Messages credentials)
        {
            try
            {
                var data = SQLite_Entity.Connection.Table<DataBase.MessagesTable>()
                    .FirstOrDefault(a => a.Mes_Id == credentials.Mes_Id);

                if (data != null)
                {
                    data.Mes_Id = credentials.Mes_Id;
                    data.Mes_From_Id = credentials.Mes_From_Id;
                    data.Mes_To_Id = credentials.Mes_To_Id;
                    data.Mes_Text = credentials.Mes_Text;
                    data.Mes_Media = credentials.Mes_Media;
                    data.Mes_MediaFileName = credentials.Mes_MediaFileName;
                    data.Mes_MediaFileNames = credentials.Mes_MediaFileNames;
                    data.Mes_Time = credentials.Mes_Time;
                    data.Mes_Seen = credentials.Mes_Seen;
                    data.Mes_Deleted_one = credentials.Mes_Deleted_one;
                    data.Mes_Deleted_two = credentials.Mes_Deleted_two;
                    data.Mes_Sent_push = credentials.Mes_Sent_push;
                    data.Mes_Notification_id = credentials.Mes_Notification_id;
                    data.Mes_Type_two = credentials.Mes_Type_two;
                    data.Mes_Time_text = credentials.Mes_Time_text;
                    data.Mes_Position = credentials.Mes_Position;
                    data.Mes_Type = credentials.Mes_Type;
                    data.Mes_File_size = credentials.Mes_File_size;
                    data.Mes_Stickers = credentials.Mes_Stickers;
                    data.Mes_User_avatar = credentials.Mes_User_avatar;
                    data.Progress_Value = credentials.Progress_Value;
                    data.sound_time = credentials.sound_time;
                    data.sound_slider_value = credentials.sound_slider_value;
                    data.Pause_Visibility = credentials.Pause_Visibility;
                    data.Play_Visibility = credentials.Play_Visibility;
                    data.Download_Visibility = credentials.Download_Visibility;
                    data.Progress_Visibility = credentials.Progress_Visibility;
                    data.Icon_File_Visibility = credentials.Icon_File_Visibility;
                    data.Hlink_Download_Visibility = credentials.Hlink_Download_Visibility;
                    data.Hlink_Open_Visibility = credentials.Hlink_Open_Visibility;
                    data.Img_user_message = credentials.Img_user_message;
                    data.Type_Icon_File = credentials.Type_Icon_File;
                    data.Color_box_message = credentials.Color_box_message;

                    SQLite_Entity.Connection.Update(data);
                }
                else
                {
                    DataBase.MessagesTable mdb = new DataBase.MessagesTable();
                    mdb.Mes_Id = credentials.Mes_Id;
                    mdb.Mes_From_Id = credentials.Mes_From_Id;
                    mdb.Mes_To_Id = credentials.Mes_To_Id;
                    mdb.Mes_Text = credentials.Mes_Text;
                    mdb.Mes_Media = credentials.Mes_Media;
                    mdb.Mes_MediaFileName = credentials.Mes_MediaFileName;
                    mdb.Mes_MediaFileNames = credentials.Mes_MediaFileNames;
                    mdb.Mes_Time = credentials.Mes_Time;
                    mdb.Mes_Seen = credentials.Mes_Seen;
                    mdb.Mes_Deleted_one = credentials.Mes_Deleted_one;
                    mdb.Mes_Deleted_two = credentials.Mes_Deleted_two;
                    mdb.Mes_Sent_push = credentials.Mes_Sent_push;
                    mdb.Mes_Notification_id = credentials.Mes_Notification_id;
                    mdb.Mes_Type_two = credentials.Mes_Type_two;
                    mdb.Mes_Time_text = credentials.Mes_Time_text;
                    mdb.Mes_Position = credentials.Mes_Position;
                    mdb.Mes_Stickers = credentials.Mes_Stickers;
                    mdb.Mes_Type = credentials.Mes_Type;
                    mdb.Mes_File_size = credentials.Mes_File_size;
                    mdb.Mes_User_avatar = credentials.Mes_User_avatar;
                    mdb.Progress_Value = credentials.Progress_Value;
                    mdb.sound_time = credentials.sound_time;
                    mdb.sound_slider_value = credentials.sound_slider_value;
                    mdb.Pause_Visibility = credentials.Pause_Visibility;
                    mdb.Play_Visibility = credentials.Play_Visibility;
                    mdb.Download_Visibility = credentials.Download_Visibility;
                    mdb.Progress_Visibility = credentials.Progress_Visibility;
                    mdb.Icon_File_Visibility = credentials.Icon_File_Visibility;
                    mdb.Hlink_Download_Visibility = credentials.Hlink_Download_Visibility;
                    mdb.Hlink_Open_Visibility = credentials.Hlink_Open_Visibility;
                    mdb.Img_user_message = credentials.Img_user_message;
                    mdb.Type_Icon_File = credentials.Type_Icon_File;
                    mdb.Color_box_message = credentials.Color_box_message;

                    //Insert  one Messages Table
                    SQLite_Entity.Connection.Insert(mdb);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Get data To Messages
        public static string GetMessages_CredentialsList(string from_id, string to_id, string before_message_id,string ChatColor)
        {
            try
            {
                var before_q = "";
                if (before_message_id != "0")
                {
                    before_q = "AND Mes_Id < " + before_message_id + " AND Mes_Id <> " + before_message_id + " ";
                }

                var query = SQLite_Entity.Connection.Query<DataBase.MessagesTable>
                (
                    "SELECT * FROM MessagesTable WHERE ((Mes_From_Id =" + from_id + " and Mes_To_Id=" + to_id + ") OR (Mes_From_Id =" + to_id + " and Mes_To_Id=" + from_id + ")) " + before_q);

                var query_limit_from = query.Count - 35;
                if (query_limit_from < 1)
                {
                    query_limit_from = 0;
                }

                var Query = SQLite_Entity.Connection.Query<DataBase.MessagesTable>
                (
                    "SELECT * FROM MessagesTable " +
                    "WHERE ((Mes_From_Id = " + from_id + " AND Mes_To_Id = " + to_id + ") " +
                    "OR  (Mes_From_Id = " + to_id + " AND Mes_To_Id = " + from_id + "))" + before_q +
                    "ORDER BY Mes_Id ASC LIMIT " + query_limit_from + ", 35"
                );
                if (Query.Count > 0)
                {
                    foreach (var item in Query)
                    {
                        Classes.Messages m = new Classes.Messages();
                        m.Mes_Id = item.Mes_Id;
                        m.Mes_From_Id = item.Mes_From_Id;
                        m.Mes_To_Id = item.Mes_To_Id;
                        m.Mes_Text = item.Mes_Text;
                        m.Mes_Media = item.Mes_Media;
                        m.Mes_MediaFileName = item.Mes_MediaFileName;
                        m.Mes_MediaFileNames = item.Mes_MediaFileNames;
                        m.Mes_Time = item.Mes_Time;
                        m.Mes_Seen = item.Mes_Seen;
                        m.Mes_Deleted_one = item.Mes_Deleted_one;
                        m.Mes_Deleted_two = item.Mes_Deleted_two;
                        m.Mes_Sent_push = item.Mes_Sent_push;
                        m.Mes_Notification_id = item.Mes_Notification_id;
                        m.Mes_Type_two = item.Mes_Type_two;
                        m.Mes_Time_text = item.Mes_Time_text;
                        m.Mes_Position = item.Mes_Position;
                        m.Mes_Type = item.Mes_Type;
                        m.Mes_File_size = item.Mes_File_size;
                        m.Mes_Stickers = item.Mes_Stickers;
                        m.Mes_User_avatar = item.Mes_User_avatar;
                        m.Progress_Value = item.Progress_Value;
                        m.sound_time = item.sound_time;
                        m.sound_slider_value = item.sound_slider_value;
                        m.Pause_Visibility = item.Pause_Visibility;
                        m.Play_Visibility = item.Play_Visibility;
                        m.Download_Visibility = item.Download_Visibility;
                        m.Progress_Visibility = item.Progress_Visibility;
                        m.Icon_File_Visibility = item.Icon_File_Visibility;
                        m.Hlink_Download_Visibility = item.Hlink_Download_Visibility;
                        m.Hlink_Open_Visibility = item.Hlink_Open_Visibility;
                        m.Img_user_message = item.Img_user_message;
                        m.Type_Icon_File = item.Type_Icon_File;
                        m.Color_box_message = ChatColor;

                        if (before_message_id == "0")
                        {
                            MainWindow.ListMessages.Add(m);
                        }
                        else
                        {
                            MainWindow.ListMessages.Insert(0, m);
                        }
                    }
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "0";
            }
        }

        //Insert data To Gifs Table
        public static void Insert_To_GifsTable(DataBase.GifsTable credentials)
        {
            try
            {
                SQLite_Entity.Connection.Insert(credentials);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Get  data To Gifs Table
        public static ObservableCollection<Classes.Get_Gifs> Get_To_GifsTable()
        {
            try
            {
                var listifs = new ObservableCollection<Classes.Get_Gifs>();
                var data = SQLite_Entity.Connection.Table<DataBase.GifsTable>().ToList();

                foreach (var item in data)
                {
                    Classes.Get_Gifs g = new Classes.Get_Gifs();
                    g.G_id = item.G_id;
                    g.G_fixed_height_small_width = item.G_fixed_height_small_width;
                    g.G_fixed_height_small_height = item.G_fixed_height_small_height;
                    g.G_fixed_height_small_url = item.G_fixed_height_small_url;
                    g.G_original_url = item.G_original_url;
                    g.G_Bar_load_gifs_Visibility = item.G_Bar_load_gifs_Visibility;
                    g.G_btn_ExitGifs_remove = item.G_btn_ExitGifs_remove;

                    listifs.Add(g);
                }
                return listifs;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        //Remove data To Gifs Table
        public static void removeGifsTable(string Gifs_id)
        {
            try
            {
                var gifs = SQLite_Entity.Connection.Table<DataBase.GifsTable>().FirstOrDefault(a => a.G_id == Gifs_id);
                if (gifs != null)
                {
                    SQLite_Entity.Connection.Delete(gifs);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Insert data To Stickers Table
        public static void Insert_To_StickersTable()
        {
            try
            {
                var Stickers_List = new ObservableCollection<DataBase.StickersTable>();

                var data = SQLite_Entity.Connection.Table<DataBase.StickersTable>().Count();
                if (data == 0)
                {
                    DataBase.StickersTable s1 = new DataBase.StickersTable();
                    s1.S_name = "Spoiled_Rabbit";
                    s1.S_Visibility = "Visible";
                    s1.S_image = @"\Images\Stickers\Sticker4.png";
                    s1.S_cuont = "15";
                    Stickers_List.Add(s1);

                    DataBase.StickersTable s2 = new DataBase.StickersTable();
                    s2.S_name = "Water_Drop";
                    s2.S_Visibility = "Visible";
                    s2.S_image = @"\Images\Stickers\Sticker16.png";
                    s2.S_cuont = "16";
                    Stickers_List.Add(s2);

                    DataBase.StickersTable s3 = new DataBase.StickersTable();
                    s3.S_name = "Monster";
                    s3.S_Visibility = "Visible";
                    s3.S_image = @"\Images\Stickers\Sticker32.png";
                    s3.S_cuont = "17";
                    Stickers_List.Add(s3);

                    DataBase.StickersTable s4 = new DataBase.StickersTable();
                    s4.S_name = "NINJA_Nyankko";
                    s4.S_Visibility = "Visible";
                    s4.S_image = @"\Images\Stickers\Sticker50.png";
                    s4.S_cuont = "34";
                    Stickers_List.Add(s4);

                    DataBase.StickersTable s5 = new DataBase.StickersTable();
                    s5.S_name = "So_Much_Love";
                    s5.S_Visibility = "Visible";
                    s5.S_image = @"\Images\Stickers\Sticker83.png";
                    s5.S_cuont = "36";
                    Stickers_List.Add(s5);

                    DataBase.StickersTable s6 = new DataBase.StickersTable();
                    s6.S_name = "Sukkara_chan";
                    s6.S_Visibility = "Collapsed";
                    s6.S_image = @"\Images\Stickers\Sticker120.png";
                    s6.S_cuont = "36";
                    Stickers_List.Add(s6);

                    DataBase.StickersTable s7 = new DataBase.StickersTable();
                    s7.S_name = "Flower_Hijab";
                    s7.S_Visibility = "Collapsed";
                    s7.S_image = @"\Images\Stickers\Sticker155.png";
                    s7.S_cuont = "40";
                    Stickers_List.Add(s7);

                    DataBase.StickersTable s8 = new DataBase.StickersTable();
                    s8.S_name = "Trendy_boy";
                    s8.S_Visibility = "Collapsed";
                    s8.S_image = @"\Images\Stickers\Sticker195.png";
                    s8.S_cuont = "40";
                    Stickers_List.Add(s8);

                    SQLite_Entity.Connection.InsertAll(Stickers_List);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //Get  data To Stickers Table
        public static ObservableCollection<DataBase.StickersTable> Get_From_StickersTable()
        {
            try
            {
                var Stickers_List = new ObservableCollection<DataBase.StickersTable>();
                var data = SQLite_Entity.Connection.Table<DataBase.StickersTable>().ToList();

                foreach (var item in data)
                {
                    var data_name = Stickers_List.FirstOrDefault(a => a.S_name == item.S_name);

                    if (data_name == null)
                    {
                        DataBase.StickersTable s = new DataBase.StickersTable();
                        s.S_name = item.S_name;
                        s.S_Visibility = item.S_Visibility;
                        s.S_image = item.S_image;
                        s.S_cuont = item.S_cuont;

                        Stickers_List.Add(s);
                    }
                }
                return Stickers_List;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }

        }

        //Update data To Stickers Table
        public static void Update_To_StickersTable(string type_name, string Visibility)
        {
            try
            {
                var data = SQLite_Entity.Connection.Table<DataBase.StickersTable>()
                    .FirstOrDefault(a => a.S_name == type_name);
                if (data != null)
                {
                    data.S_Visibility = Visibility;
                    SQLite_Entity.Connection.Update(data);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //Insert data To Users Contact Profile Table
        public static void Insert_Or_Replace_UsersContactProfileTable(ObservableCollection<UsersContactProfile> credentialsList)
        {
            try
            {
                List<DataBase.UsersContactProfileTable> listOfDatabaseforInsert =
                    new List<DataBase.UsersContactProfileTable>();
                List<DataBase.UsersContactProfileTable> listOfDatabaseForUpdate =
                    new List<DataBase.UsersContactProfileTable>();
                // get data from database
                var result = SQLite_Entity.Connection.Table<DataBase.UsersContactProfileTable>().ToList();
                var Convertedclass = Functions.ConvertUsersContactProfile(result);

                foreach (var credentials in credentialsList)
                {
                    DataBase.UsersContactProfileTable upt = new DataBase.UsersContactProfileTable();

                    upt.UCP_Id = credentials.UCP_Id;
                    upt.UCP_username = credentials.UCP_username;
                    upt.UCP_name = credentials.UCP_name;
                    upt.UCP_cover_picture = credentials.UCP_cover_picture;
                    upt.UCP_profile_picture = credentials.UCP_profile_picture;
                    upt.UCP_verified = credentials.UCP_verified;
                    upt.UCP_lastseen = credentials.UCP_lastseen;
                    upt.UCP_lastseen_time_text = credentials.UCP_lastseen_time_text;
                    upt.UCP_lastseen_unix_time = credentials.UCP_lastseen_unix_time;
                    upt.UCP_url = credentials.UCP_url;
                    upt.UCP_user_platform = credentials.UCP_user_platform;
                    upt.UCP_chat_color = credentials.UCP_chat_color;
                    upt.UCP_Notifications_Message_user = credentials.UCP_Notifications_Message_user;
                    upt.UCP_Notifications_Message_Sound_user = credentials.UCP_Notifications_Message_Sound_user;
                    //user_profile
                    upt.UCP_email = credentials.UCP_email;
                    upt.UCP_first_name = credentials.UCP_first_name;
                    upt.UCP_last_name = credentials.UCP_last_name;
                    upt.UCP_relationship_id = credentials.UCP_relationship_id;
                    upt.UCP_address = credentials.UCP_address;
                    upt.UCP_working = credentials.UCP_working;
                    upt.UCP_working_link = credentials.UCP_working_link;
                    upt.UCP_about = credentials.UCP_about;
                    upt.UCP_school = credentials.UCP_school;
                    upt.UCP_gender = credentials.UCP_gender;
                    upt.UCP_birthday = credentials.UCP_birthday;
                    upt.UCP_website = credentials.UCP_website;
                    upt.UCP_facebook = credentials.UCP_facebook;
                    upt.UCP_google = credentials.UCP_google;
                    upt.UCP_twitter = credentials.UCP_twitter;
                    upt.UCP_linkedin = credentials.UCP_linkedin;
                    upt.UCP_youtube = credentials.UCP_youtube;
                    upt.UCP_vk = credentials.UCP_vk;
                    upt.UCP_instagram = credentials.UCP_instagram;
                    upt.UCP_language = credentials.UCP_language;
                    upt.UCP_ip_address = credentials.UCP_ip_address;
                    upt.UCP_follow_privacy = credentials.UCP_follow_privacy;
                    upt.UCP_post_privacy = credentials.UCP_post_privacy;
                    upt.UCP_message_privacy = credentials.UCP_message_privacy;
                    upt.UCP_confirm_followers = credentials.UCP_confirm_followers;
                    upt.UCP_show_activities_privacy = credentials.UCP_show_activities_privacy;
                    upt.UCP_birth_privacy = credentials.UCP_birth_privacy;
                    upt.UCP_visit_privacy = credentials.UCP_visit_privacy;
                    upt.UCP_showlastseen = credentials.UCP_showlastseen;
                    upt.UCP_status = credentials.UCP_status;
                    upt.UCP_active = credentials.UCP_active;
                    upt.UCP_admin = credentials.UCP_admin;
                    upt.UCP_registered = credentials.UCP_registered;
                    upt.UCP_phone_number = credentials.UCP_phone_number;
                    upt.UCP_is_pro = credentials.UCP_is_pro;
                    upt.UCP_pro_type = credentials.UCP_pro_type;
                    upt.UCP_joined = credentials.UCP_joined;
                    upt.UCP_timezone = credentials.UCP_timezone;
                    upt.UCP_referrer = credentials.UCP_referrer;
                    upt.UCP_balance = credentials.UCP_balance;
                    upt.UCP_paypal_email = credentials.UCP_paypal_email;
                    upt.UCP_notifications_sound = credentials.UCP_notifications_sound;
                    upt.UCP_order_posts_by = credentials.UCP_order_posts_by;
                    upt.UCP_social_login = credentials.UCP_social_login;
                    upt.UCP_device_id = credentials.UCP_device_id;

                    var DataCheck = Convertedclass.FirstOrDefault(a => a.UCP_Id == credentials.UCP_Id);

                    if (DataCheck != null)
                    {
                        var CheckbeforUpdate = result.FirstOrDefault(a => a.UCP_Id == DataCheck.UCP_Id);
                        if (
                            CheckbeforUpdate.UCP_Id != credentials.UCP_Id ||
                            CheckbeforUpdate.UCP_username != credentials.UCP_username ||
                            CheckbeforUpdate.UCP_name != credentials.UCP_name ||
                            CheckbeforUpdate.UCP_cover_picture != credentials.UCP_cover_picture ||
                            CheckbeforUpdate.UCP_profile_picture != credentials.UCP_profile_picture ||
                            CheckbeforUpdate.UCP_verified != credentials.UCP_verified ||
                            CheckbeforUpdate.UCP_lastseen != credentials.UCP_lastseen ||
                            CheckbeforUpdate.UCP_lastseen_time_text != credentials.UCP_lastseen_time_text ||
                            CheckbeforUpdate.UCP_lastseen_unix_time != credentials.UCP_lastseen_unix_time ||
                            CheckbeforUpdate.UCP_url != credentials.UCP_url ||
                            CheckbeforUpdate.UCP_user_platform != credentials.UCP_user_platform ||
                            CheckbeforUpdate.UCP_chat_color != credentials.UCP_chat_color ||
                            CheckbeforUpdate.UCP_Notifications_Message_user != credentials.UCP_Notifications_Message_user ||
                            CheckbeforUpdate.UCP_Notifications_Message_Sound_user != credentials.UCP_Notifications_Message_Sound_user ||
                            CheckbeforUpdate.UCP_email != credentials.UCP_email ||
                            CheckbeforUpdate.UCP_first_name != credentials.UCP_first_name ||
                            CheckbeforUpdate.UCP_last_name != credentials.UCP_last_name ||
                            CheckbeforUpdate.UCP_relationship_id != credentials.UCP_relationship_id ||
                            CheckbeforUpdate.UCP_address != credentials.UCP_address ||
                            CheckbeforUpdate.UCP_working != credentials.UCP_working ||
                            CheckbeforUpdate.UCP_working_link != credentials.UCP_working_link ||
                            CheckbeforUpdate.UCP_about != credentials.UCP_about ||
                            CheckbeforUpdate.UCP_school != credentials.UCP_school ||
                            CheckbeforUpdate.UCP_gender != credentials.UCP_gender ||
                            CheckbeforUpdate.UCP_birthday != credentials.UCP_birthday ||
                            CheckbeforUpdate.UCP_website != credentials.UCP_website ||
                            CheckbeforUpdate.UCP_facebook != credentials.UCP_facebook ||
                            CheckbeforUpdate.UCP_google != credentials.UCP_google ||
                            CheckbeforUpdate.UCP_twitter != credentials.UCP_twitter ||
                            CheckbeforUpdate.UCP_linkedin != credentials.UCP_linkedin ||
                            CheckbeforUpdate.UCP_youtube != credentials.UCP_youtube ||
                            CheckbeforUpdate.UCP_vk != credentials.UCP_vk ||
                            CheckbeforUpdate.UCP_instagram != credentials.UCP_instagram ||
                            CheckbeforUpdate.UCP_language != credentials.UCP_language ||
                            CheckbeforUpdate.UCP_ip_address != credentials.UCP_ip_address ||
                            CheckbeforUpdate.UCP_follow_privacy != credentials.UCP_follow_privacy ||
                            CheckbeforUpdate.UCP_post_privacy != credentials.UCP_post_privacy ||
                            CheckbeforUpdate.UCP_message_privacy != credentials.UCP_message_privacy ||
                            CheckbeforUpdate.UCP_confirm_followers != credentials.UCP_confirm_followers ||
                            CheckbeforUpdate.UCP_show_activities_privacy != credentials.UCP_show_activities_privacy ||
                            CheckbeforUpdate.UCP_birth_privacy != credentials.UCP_birth_privacy ||
                            CheckbeforUpdate.UCP_visit_privacy != credentials.UCP_visit_privacy ||
                            CheckbeforUpdate.UCP_showlastseen != credentials.UCP_showlastseen ||
                            CheckbeforUpdate.UCP_status != credentials.UCP_status ||
                            CheckbeforUpdate.UCP_active != credentials.UCP_active ||
                            CheckbeforUpdate.UCP_admin != credentials.UCP_admin ||
                            CheckbeforUpdate.UCP_registered != credentials.UCP_registered ||
                            CheckbeforUpdate.UCP_phone_number != credentials.UCP_phone_number ||
                            CheckbeforUpdate.UCP_is_pro != credentials.UCP_is_pro ||
                            CheckbeforUpdate.UCP_pro_type != credentials.UCP_pro_type ||
                            CheckbeforUpdate.UCP_joined != credentials.UCP_joined ||
                            CheckbeforUpdate.UCP_timezone != credentials.UCP_timezone ||
                            CheckbeforUpdate.UCP_referrer != credentials.UCP_referrer ||
                            CheckbeforUpdate.UCP_balance != credentials.UCP_balance ||
                            CheckbeforUpdate.UCP_paypal_email != credentials.UCP_paypal_email ||
                            CheckbeforUpdate.UCP_notifications_sound != credentials.UCP_notifications_sound ||
                            CheckbeforUpdate.UCP_order_posts_by != credentials.UCP_order_posts_by ||
                            CheckbeforUpdate.UCP_social_login != credentials.UCP_social_login ||
                            CheckbeforUpdate.UCP_device_id != credentials.UCP_device_id

                        )
                        {
                            CheckbeforUpdate.UCP_Id = credentials.UCP_Id;
                            CheckbeforUpdate.UCP_username = credentials.UCP_username;
                            CheckbeforUpdate.UCP_name = credentials.UCP_name;
                            CheckbeforUpdate.UCP_cover_picture = credentials.UCP_cover_picture;
                            CheckbeforUpdate.UCP_profile_picture = credentials.UCP_profile_picture;
                            CheckbeforUpdate.UCP_verified = credentials.UCP_verified;
                            CheckbeforUpdate.UCP_lastseen = credentials.UCP_lastseen;
                            CheckbeforUpdate.UCP_lastseen_time_text = credentials.UCP_lastseen_time_text;
                            CheckbeforUpdate.UCP_lastseen_unix_time = credentials.UCP_lastseen_unix_time;
                            CheckbeforUpdate.UCP_url = credentials.UCP_url;
                            CheckbeforUpdate.UCP_user_platform = credentials.UCP_user_platform;
                            CheckbeforUpdate.UCP_chat_color = credentials.UCP_chat_color;
                            CheckbeforUpdate.UCP_Notifications_Message_user = credentials.UCP_Notifications_Message_user;
                            CheckbeforUpdate.UCP_Notifications_Message_Sound_user = credentials.UCP_Notifications_Message_Sound_user;
                            CheckbeforUpdate.UCP_email = credentials.UCP_email;
                            CheckbeforUpdate.UCP_first_name = credentials.UCP_first_name;
                            CheckbeforUpdate.UCP_last_name = credentials.UCP_last_name;
                            CheckbeforUpdate.UCP_relationship_id = credentials.UCP_relationship_id;
                            CheckbeforUpdate.UCP_address = credentials.UCP_address;
                            CheckbeforUpdate.UCP_working = credentials.UCP_working;
                            CheckbeforUpdate.UCP_working_link = credentials.UCP_working_link;
                            CheckbeforUpdate.UCP_about = credentials.UCP_about;
                            CheckbeforUpdate.UCP_school = credentials.UCP_school;
                            CheckbeforUpdate.UCP_gender = credentials.UCP_gender;
                            CheckbeforUpdate.UCP_birthday = credentials.UCP_birthday;
                            CheckbeforUpdate.UCP_website = credentials.UCP_website;
                            CheckbeforUpdate.UCP_facebook = credentials.UCP_facebook;
                            CheckbeforUpdate.UCP_google = credentials.UCP_google;
                            CheckbeforUpdate.UCP_twitter = credentials.UCP_twitter;
                            CheckbeforUpdate.UCP_linkedin = credentials.UCP_linkedin;
                            CheckbeforUpdate.UCP_youtube = credentials.UCP_youtube;
                            CheckbeforUpdate.UCP_vk = credentials.UCP_vk;
                            CheckbeforUpdate.UCP_instagram = credentials.UCP_instagram;
                            CheckbeforUpdate.UCP_language = credentials.UCP_language;
                            CheckbeforUpdate.UCP_ip_address = credentials.UCP_ip_address;
                            CheckbeforUpdate.UCP_follow_privacy = credentials.UCP_follow_privacy;
                            CheckbeforUpdate.UCP_post_privacy = credentials.UCP_post_privacy;
                            CheckbeforUpdate.UCP_message_privacy = credentials.UCP_message_privacy;
                            CheckbeforUpdate.UCP_confirm_followers = credentials.UCP_confirm_followers;
                            CheckbeforUpdate.UCP_show_activities_privacy = credentials.UCP_show_activities_privacy;
                            CheckbeforUpdate.UCP_birth_privacy = credentials.UCP_birth_privacy;
                            CheckbeforUpdate.UCP_visit_privacy = credentials.UCP_visit_privacy;
                            CheckbeforUpdate.UCP_showlastseen = credentials.UCP_showlastseen;
                            CheckbeforUpdate.UCP_status = credentials.UCP_status;
                            CheckbeforUpdate.UCP_active = credentials.UCP_active;
                            CheckbeforUpdate.UCP_admin = credentials.UCP_admin;
                            CheckbeforUpdate.UCP_registered = credentials.UCP_registered;
                            CheckbeforUpdate.UCP_phone_number = credentials.UCP_phone_number;
                            CheckbeforUpdate.UCP_is_pro = credentials.UCP_is_pro;
                            CheckbeforUpdate.UCP_pro_type = credentials.UCP_pro_type;
                            CheckbeforUpdate.UCP_joined = credentials.UCP_joined;
                            CheckbeforUpdate.UCP_timezone = credentials.UCP_timezone;
                            CheckbeforUpdate.UCP_referrer = credentials.UCP_referrer;
                            CheckbeforUpdate.UCP_balance = credentials.UCP_balance;
                            CheckbeforUpdate.UCP_paypal_email = credentials.UCP_paypal_email;
                            CheckbeforUpdate.UCP_notifications_sound = credentials.UCP_notifications_sound;
                            CheckbeforUpdate.UCP_order_posts_by = credentials.UCP_order_posts_by;
                            CheckbeforUpdate.UCP_social_login = credentials.UCP_social_login;
                            CheckbeforUpdate.UCP_device_id = credentials.UCP_device_id;

                            listOfDatabaseForUpdate.Add(CheckbeforUpdate);
                        }
                    }
                    else
                    {
                        listOfDatabaseforInsert.Add(upt);
                    }
                }

                if (listOfDatabaseforInsert.Count > 0 || listOfDatabaseForUpdate.Count > 0)
                {
                    SQLite_Entity.Connection.BeginTransaction();
                    if (listOfDatabaseforInsert.Count > 0)
                    {
                        SQLite_Entity.Connection.InsertAll(listOfDatabaseforInsert);
                    }
                    else if (listOfDatabaseForUpdate.Count > 0)
                    {
                        SQLite_Entity.Connection.UpdateAll(listOfDatabaseForUpdate);
                    }
                    SQLite_Entity.Connection.Commit();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        //Remove all data user
        public static void removeUser_All_Table(string User_id, string from_id, string to_id)
        {
            try
            {
                var ChatActivity = SQLite_Entity.Connection.Table<DataBase.ChatActivity>()
                    .FirstOrDefault(a => a.U_Id == User_id);
                if (ChatActivity != null)
                {
                    SQLite_Entity.Connection.Delete(ChatActivity);
                }

                var UsersContactProfile = SQLite_Entity.Connection.Table<DataBase.UsersContactProfileTable>()
                    .FirstOrDefault(a => a.UCP_Id == User_id);
                if (UsersContactProfile != null)
                {
                    SQLite_Entity.Connection.Delete(UsersContactProfile);
                }

                var UsersContact = SQLite_Entity.Connection.Table<DataBase.UsersContactTable>()
                    .FirstOrDefault(a => a.UC_Id == User_id);
                if (UsersContact != null)
                {
                    SQLite_Entity.Connection.Delete(UsersContact);
                }

                SQLite_Entity.Connection.Query<DataBase.MessagesTable>(
                    "Delete FROM MessagesTable WHERE ((Mes_From_Id = " + from_id + " AND Mes_To_Id = " + to_id +
                    ") OR  (Mes_From_Id = " + to_id + " AND Mes_To_Id = " + from_id + "))"
                );
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //Insert data To Call Video Table
        public static void Insert_To_CallVideoTable(Classes.Call_Video credentials)
        {
            try
            {
                DataBase.CallVideoTable CV = new DataBase.CallVideoTable();

                CV.Call_Video_user_id = credentials.Call_Video_user_id;
                CV.Call_Video_User_Name = credentials.Call_Video_User_Name;
                CV.Call_Video_Avatar = credentials.Call_Video_Avatar;
                CV.Call_Video_Call_id = credentials.Call_Video_Call_id;
                CV.Call_Video_Tupe_icon = credentials.Call_Video_Tupe_icon;
                CV.Call_Video_Color_icon = credentials.Call_Video_Color_icon;
                CV.Call_Video_User_DataTime = credentials.Call_Video_User_DataTime;

                SQLite_Entity.Connection.Insert(CV);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Get  data To Call Video Table
        public static ObservableCollection<Classes.Call_Video> get_data_CallVideo()
        {
            try
            {
                var listcCallVideos = new ObservableCollection<Classes.Call_Video>();
                var data = SQLite_Entity.Connection.Table<DataBase.CallVideoTable>().ToList();
 
                foreach (var item in data)
                {
                    Classes.Call_Video CV = new Classes.Call_Video();
                    CV.Call_Video_user_id = item.Call_Video_user_id;
                    CV.Call_Video_User_Name = item.Call_Video_User_Name;
                    CV.Call_Video_Avatar = item.Call_Video_Avatar;
                    CV.Call_Video_Call_id = item.Call_Video_Call_id;
                    CV.Call_Video_Tupe_icon = item.Call_Video_Tupe_icon;
                    CV.Call_Video_Color_icon = item.Call_Video_Color_icon;
                    CV.Call_Video_User_DataTime = item.Call_Video_User_DataTime;

                    if (MainWindow.ModeDarkstlye)
                    {
                        CV.S_Color_Background = "#232323";
                        CV.S_Color_Foreground = "#ffff";
                    }
                    else
                    {
                        CV.S_Color_Background = "#ffff";
                        CV.S_Color_Foreground = "#444";
                    }

                    listcCallVideos.Add(CV);
                }
                return listcCallVideos;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }

        //Remove data Call Video Table
        public static void remove_data_CallVideo(string Call_id)
        {
            try
            {
                var callVideo = SQLite_Entity.Connection.Table<DataBase.CallVideoTable>().FirstOrDefault(a => a.Call_Video_Call_id == Call_id);
                if (callVideo != null)
                {
                    SQLite_Entity.Connection.Delete(callVideo);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

    }
}
