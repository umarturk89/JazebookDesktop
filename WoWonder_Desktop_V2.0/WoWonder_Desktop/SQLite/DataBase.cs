using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWonder_Desktop.Controls;

namespace WoWonder_Desktop.SQLite
{
    public class DataBase
    {
        public class LoginTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string Username { get; set; }
            public string Password { get; set; }
            public string Session { get; set; }
            public string UserId { get; set; }
            public string Status { get; set; }
        }

        public class SettingsTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
          
            public string S_SiteName { get; set; }
            public string S_SiteTitle { get; set; }
            public string S_SiteKeywords { get; set; }
            public string S_SiteDesc { get; set; }
            public string S_DefaultLang { get; set; }
            public string S_FileSharing { get; set; }
            public string S_ChatSystem { get; set; }
            public string S_User_lastseen { get; set; }
            public string S_Age { get; set; }
            public string S_DeleteAccount { get; set; }
            public string S_ConnectivitySystem { get; set; }
            public string S_MaxUpload { get; set; }
            public string S_MaxCharacters { get; set; }
            public string S_Message_seen { get; set; }
            public string S_Message_typing { get; set; }
            public string S_AllowedExtenstion { get; set; }
            public string S_Theme { get; set; }
            public string S_DefaulColor { get; set; } //Setting_header_background
            public string S_Header_hover_border { get; set; }
            public string S_Header_color { get; set; }
            public string S_Body_background { get; set; }
            public string S_btn_color { get; set; }
            public string S_SecondryColor { get; set; } //Setting_btn_background_color
            public string S_btn_hover_color { get; set; }
            public string S_btn_hover_background_color { get; set; }
            public string Setting_Header_color { get; set; }
            public string Setting_Header_background { get; set; }
            public string Setting_Active_sidebar_color { get; set; }
            public string Setting_Active_sidebar_background { get; set; }
            public string Setting_Sidebar_background { get; set; }
            public string Setting_Sidebar_color { get; set; }
            public string S_Logo_extension { get; set; }
            public string S_Background_extension { get; set; }
            public string S_Video_upload { get; set; }
            public string S_Audio_upload { get; set; }
            public string S_Header_search_color { get; set; }
            public string S_Header_button_shadow { get; set; }
            public string S_btn_disabled { get; set; }
            public string S_User_registration { get; set; }
            public string S_Favicon_extension { get; set; }
            public string S_Chat_outgoing_background { get; set; }
            public string S_Windows_app_version { get; set; }
            public string S_Widnows_app_api_id { get; set; }
            public string S_Widnows_app_api_key { get; set; }
            public string S_Credit_card { get; set; }
            public string S_Bitcoin { get; set; }
            public string S_m_withdrawal { get; set; }
            public string S_Affiliate_type { get; set; }
            public string S_Affiliate_system { get; set; }
            public string S_Classified { get; set; }
            public string S_Bucket_name { get; set; }
            public string S_Region { get; set; }
            public string S_Footer_background { get; set; }
            public string S_Is_utf8 { get; set; }
            public string S_Alipay { get; set; }
            public string S_Audio_chat { get; set; }
            public string S_Sms_provider { get; set; }
            public string S_Footer_text_color { get; set; }
            public string S_Updated_latest { get; set; }
            public string S_Footer_background_2 { get; set; }
            public string S_Footer_background_n { get; set; }
            public string S_Blogs { get; set; }
            public string S_Can_blogs { get; set; }
            public string S_Push { get; set; }
            public string S_Push_id { get; set; }
            public string S_Push_key { get; set; }
            public string S_Events { get; set; }
            public string S_Forum { get; set; }
            public string S_Last_update { get; set; }
            public string S_Movies { get; set; }
            public string S_Yndex_translation_api { get; set; }
            public string S_Update_db_15 { get; set; }
            public string S_Ad_v_price { get; set; }
            public string S_Ad_c_price { get; set; }
            public string S_Emo_cdn { get; set; }
            public string S_User_ads { get; set; }
            public string S_User_status { get; set; }
            public string S_Date_style { get; set; }
            public string S_Stickers { get; set; }
            public string S_Giphy_api { get; set; }
            public string S_Find_friends { get; set; }
            public string S_Update_available { get; set; }
            public string S_Logo_url { get; set; }
            public string S_User_messages { get; set; }

            //style
            public string S_NotificationDesktop { get; set; }
            public string S_NotificationPlaysound { get; set; }
            public string S_BackgroundChats_images { get; set; }
            public string WebClient { get; set; } //Setting_ConnationType
            public string Lang_Resources { get; set; }
            public string DarkMode { get; set; }

        }

        public class ProfilesTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string pm_UserId { get; set; }
            public string pm_Username { get; set; }
            public string pm_Email { get; set; }
            public string pm_First_name { get; set; }
            public string pm_Last_name { get; set; }
            public string pm_Avatar { get; set; }
            public string pm_Cover { get; set; }
            public string pm_Relationship_id { get; set; }
            public string pm_Address { get; set; }
            public string pm_Working { get; set; }
            public string pm_Working_link { get; set; }
            public string pm_About { get; set; }
            public string pm_School { get; set; }
            public string pm_Gender { get; set; }
            public string pm_Birthday { get; set; }
            public string pm_Website { get; set; }
            public string pm_Facebook { get; set; }
            public string pm_Google { get; set; }
            public string pm_Twitter { get; set; }
            public string pm_Linkedin { get; set; }
            public string pm_Youtube { get; set; }
            public string pm_Vk { get; set; }
            public string pm_Instagram { get; set; }
            public string pm_Language { get; set; }
            public string pm_Ip_address { get; set; }
            public string pm_Verified { get; set; }
            public string pm_Lastseen { get; set; }
            public string pm_Showlastseen { get; set; }
            public string pm_Status { get; set; }
            public string pm_Active { get; set; }
            public string pm_Admin { get; set; }
            public string pm_Registered { get; set; }
            public string pm_Phone_number { get; set; }
            public string pm_Is_pro { get; set; }
            public string pm_Pro_type { get; set; }
            public string pm_Joined { get; set; }
            public string pm_Timezone { get; set; }
            public string pm_Referrer { get; set; }
            public string pm_Balance { get; set; }
            public string pm_Paypal_email { get; set; }
            public string pm_Notifications_sound { get; set; }
            public string pm_Order_posts_by { get; set; }
            public string pm_Social_login { get; set; }
            public string pm_Device_id { get; set; }
            public string pm_Url { get; set; }
            public string pm_Name { get; set; }
        }

        public class UsersContactProfileTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            //users
            public string UCP_Id { set; get; }
            public string UCP_username { set; get; }
            public string UCP_name { set; get; }
            public string UCP_profile_picture { set; get; }
            public string UCP_cover_picture { set; get; }
            public string UCP_verified { set; get; }
            public string UCP_lastseen { set; get; }
            public string UCP_lastseen_time_text { set; get; }
            public string UCP_lastseen_unix_time { set; get; }
            public string UCP_url { set; get; }
            public string UCP_user_platform { set; get; }
            public string UCP_chat_color { set; get; }
            public string UCP_Notifications_Message_user { get; set; }
            public string UCP_Notifications_Message_Sound_user { get; set; }


            //user_profile
            public string UCP_email { get; set; }
            public string UCP_first_name { get; set; }
            public string UCP_last_name { get; set; }
            public string UCP_relationship_id { get; set; }
            public string UCP_address { get; set; }
            public string UCP_working { get; set; }
            public string UCP_working_link { get; set; }
            public string UCP_about { get; set; }
            public string UCP_school { get; set; }
            public string UCP_gender { get; set; }
            public string UCP_birthday { get; set; }
            public string UCP_website { get; set; }
            public string UCP_facebook { get; set; }
            public string UCP_google { get; set; }
            public string UCP_twitter { get; set; }
            public string UCP_linkedin { get; set; }
            public string UCP_youtube { get; set; }
            public string UCP_vk { get; set; }
            public string UCP_instagram { get; set; }
            public string UCP_language { get; set; }
            public string UCP_ip_address { get; set; }
            public string UCP_follow_privacy { get; set; }
            public string UCP_post_privacy { get; set; }
            public string UCP_message_privacy { get; set; }
            public string UCP_confirm_followers { get; set; }
            public string UCP_show_activities_privacy { get; set; }
            public string UCP_birth_privacy { get; set; }
            public string UCP_visit_privacy { get; set; }
            public string UCP_showlastseen { get; set; }
            public string UCP_status { get; set; }
            public string UCP_active { get; set; }
            public string UCP_admin { get; set; }
            public string UCP_registered { get; set; }
            public string UCP_phone_number { get; set; }
            public string UCP_is_pro { get; set; }
            public string UCP_pro_type { get; set; }
            public string UCP_joined { get; set; }
            public string UCP_timezone { get; set; }
            public string UCP_referrer { get; set; }
            public string UCP_balance { get; set; }
            public string UCP_paypal_email { get; set; }
            public string UCP_notifications_sound { get; set; }
            public string UCP_order_posts_by { get; set; }
            public string UCP_social_login { get; set; }
            public string UCP_device_id { get; set; }
        }

        public class ChatActivity
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string U_Id { set; get; }
            public string U_username { set; get; }
            public string U_name { set; get; }
            public string U_profile_picture { set; get; }
            public string U_cover_picture { set; get; }
            public string U_verified { set; get; }
            public string U_lastseen { set; get; }
            public string U_lastseenWithoutCut { set; get; }
            public string U_lastseen_time_text { set; get; }
            public string u_url { set; get; }
            public string U_lastseen_unix_time { set; get; }
            public string U_chat_color { set; get; }
            public string M_Id { set; get; }
            public string From_Id { set; get; }
            public string To_Id { set; get; }
            public string M_text { set; get; }
            public string M_media { set; get; }
            public string M_mediaFileName { set; get; }
            public string M_mediaFileNamese { set; get; }
            public string M_time { set; get; }
            public string M_seen { set; get; }
            public string M_date_time { set; get; }
            public string M_stickers { set; get; }
            public string S_Color_onof { set; get; }
            public string S_ImageProfile { set; get; }
            public string S_noProfile_color { set; get; }
            public string S_Message_FontWeight { set; get; }
            public string S_Message_color { set; get; }
            public string IsSeeniconcheck { set; get; }
            public string ChatColorcirclevisibilty { set; get; }
            public string MediaIconvisibilty { set; get; }
            public string MediaIconImage { set; get; }
            public string App_Main_Later { set; get; }
            public string UsernameTwoLetters { set; get; }
        }

        public class UsersContactTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string UC_Id { set; get; }
            public string UC_username { set; get; }
            public string UC_name { set; get; }
            public string UC_profile_picture { set; get; }
            public string UC_cover_picture { set; get; }
            public string UC_verified { set; get; }
            public string UC_lastseen { set; get; }
            public string UC_lastseen_time_text { set; get; }
            public string UC_lastseen_unix_time { set; get; }
            public string UC_url { set; get; }
            public string UC_user_platform { set; get; }
            //style
            public string UC_App_Main_Later { set; get; }
            public string UC_Color_onof { set; get; }
            public string UC_chat_color { set; get; }

            //user_profile
            public string UC_email { get; set; }
            public string UC_first_name { get; set; }
            public string UC_last_name { get; set; }
            public string UC_relationship_id { get; set; }
            public string UC_address { get; set; }
            public string UC_working { get; set; }
            public string UC_working_link { get; set; }
            public string UC_about { get; set; }
            public string UC_school { get; set; }
            public string UC_gender { get; set; }
            public string UC_birthday { get; set; }
            public string UC_website { get; set; }
            public string UC_facebook { get; set; }
            public string UC_google { get; set; }
            public string UC_twitter { get; set; }
            public string UC_linkedin { get; set; }
            public string UC_youtube { get; set; }
            public string UC_vk { get; set; }
            public string UC_instagram { get; set; }
            public string UC_language { get; set; }
            public string UC_ip_address { get; set; }
            public string UC_follow_privacy { get; set; }
            public string UC_post_privacy { get; set; }
            public string UC_message_privacy { get; set; }
            public string UC_confirm_followers { get; set; }
            public string UC_show_activities_privacy { get; set; }
            public string UC_birth_privacy { get; set; }
            public string UC_visit_privacy { get; set; }
            public string UC_showlastseen { get; set; }
            public string UC_status { get; set; }
            public string UC_active { get; set; }
            public string UC_admin { get; set; }
            public string UC_registered { get; set; }
            public string UC_phone_number { get; set; }
            public string UC_is_pro { get; set; }
            public string UC_pro_type { get; set; }
            public string UC_joined { get; set; }
            public string UC_timezone { get; set; }
            public string UC_referrer { get; set; }
            public string UC_balance { get; set; }
            public string UC_paypal_email { get; set; }
            public string UC_notifications_sound { get; set; }
            public string UC_order_posts_by { get; set; }
            public string UC_social_login { get; set; }
            public string UC_device_id { get; set; }
        }

        public class MessagesTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string Mes_Id { set; get; }
            public string Mes_From_Id { set; get; }
            public string Mes_To_Id { set; get; }
            public string Mes_Text { set; get; }
            public string Mes_Media { set; get; }
            public string Mes_MediaFileName { set; get; }
            public string Mes_MediaFileNames { set; get; }
            public string Mes_Time { set; get; }
            public string Mes_Seen { set; get; }
            public string Mes_Deleted_one { set; get; }
            public string Mes_Deleted_two { set; get; }
            public string Mes_Sent_push { set; get; }
            public string Mes_Notification_id { set; get; }
            public string Mes_Type_two { set; get; }
            public string Mes_Time_text { set; get; }
            public string Mes_Position { set; get; }
            public string Mes_Type { set; get; }
            public string Mes_File_size { set; get; }
            public string Mes_Sent_Time { set; get; }
            public string Mes_Stickers { set; get; }
            public string Mes_User_avatar { set; get; }

            //style
            public int Progress_Value { set; get; }
            public string sound_time { set; get; }
            public int sound_slider_value { set; get; }
            public string Pause_Visibility { set; get; }
            public string Play_Visibility { set; get; }
            public string Download_Visibility { set; get; }
            public string Progress_Visibility { set; get; }
            public string Icon_File_Visibility { set; get; }
            public string Hlink_Download_Visibility { set; get; }
            public string Hlink_Open_Visibility { set; get; }
            public string Img_user_message { set; get; }
            public string Type_Icon_File { set; get; }
            public string Color_box_message { set; get; }
        }

        public class GifsTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string G_id { get; set; }
            public string G_fixed_height_small_width { get; set; }
            public string G_fixed_height_small_height { get; set; }
            public string G_fixed_height_small_url { get; set; }
            public string G_fixed_height_small_mp4 { get; set; }
            public string G_fixed_height_small_webp { get; set; }
            public string G_original_url { get; set; }
            public string G_btn_ExitGifs_remove { get; set; }
            public string G_Progressbar_Visibility { get; set; }
            public string G_Bar_load_gifs_Visibility { get; set; }
        }

        public class StickersTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string S_name { get; set; }
            public string S_image { get; set; }
            public string S_cuont { get; set; }
            public string S_Visibility { get; set; }
        }

        public class CallVideoTable
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string Call_Video_Call_id { set; get; }
            public string Call_Video_user_id { set; get; }
            public string Call_Video_Tupe_icon { set; get; }
            public string Call_Video_Color_icon { set; get; }
            public string Call_Video_Avatar { set; get; }
            public string Call_Video_User_Name { set; get; }
            public string Call_Video_User_DataTime { set; get; }
        }
    }
}
