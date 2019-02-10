using System.Linq;
using PropertyChanged;
using System.IO;

namespace WoWonder_Desktop.Controls
{
    //############# DONT'T MODIFY HERE #############
    public class Classes
    {
        public class MimeType
        {
            private static readonly byte[] BMP = { 66, 77 };
            private static readonly byte[] DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
            private static readonly byte[] EXE_DLL = { 77, 90 };
            private static readonly byte[] GIF = { 71, 73, 70, 56 };
            private static readonly byte[] ICO = { 0, 0, 1, 0 };
            private static readonly byte[] JPG = { 255, 216, 255 };
            private static readonly byte[] MP3 = { 255, 251, 48 };
            private static readonly byte[] OGG = { 79, 103, 103, 83, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0 };
            private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
            private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
            private static readonly byte[] RAR = { 82, 97, 114, 33, 26, 7, 0 };
            private static readonly byte[] SWF = { 70, 87, 83 };
            private static readonly byte[] TIFF = { 73, 73, 42, 0 };
            private static readonly byte[] TORRENT = { 100, 56, 58, 97, 110, 110, 111, 117, 110, 99, 101 };
            private static readonly byte[] TTF = { 0, 1, 0, 0, 0 };
            private static readonly byte[] WAV_AVI = { 82, 73, 70, 70 };
            private static readonly byte[] WMV_WMA = { 48, 38, 178, 117, 142, 102, 207, 17, 166, 217, 0, 170, 0, 98, 206, 108 };
            private static readonly byte[] ZIP_DOCX = { 80, 75, 3, 4 };

            public static string GetMimeType(byte[] file, string fileName)
            {

                string mime = "application/octet-stream"; //DEFAULT UNKNOWN MIME TYPE

                //Ensure that the filename isn't empty or null
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return mime;
                }

                //Get the file extension
                string extension = Path.GetExtension(fileName) == null
                    ? string.Empty
                    : Path.GetExtension(fileName).ToUpper();

                //Get the MIME Type
                if (file.Take(2).SequenceEqual(BMP))
                {
                    mime = "image/bmp";
                }
                else if (file.Take(8).SequenceEqual(DOC))
                {
                    mime = "application/msword";
                }
                else if (file.Take(2).SequenceEqual(EXE_DLL))
                {
                    mime = "application/x-msdownload"; //both use same mime type
                }
                else if (file.Take(4).SequenceEqual(GIF))
                {
                    mime = "image/gif";
                }
                else if (file.Take(4).SequenceEqual(ICO))
                {
                    mime = "image/x-icon";
                }
                else if (file.Take(3).SequenceEqual(JPG))
                {
                    mime = "image/jpeg";
                }
                else if (file.Take(3).SequenceEqual(MP3))
                {
                    mime = "audio/mpeg";
                }
                else if (file.Take(14).SequenceEqual(OGG))
                {
                    if (extension == ".OGX")
                    {
                        mime = "application/ogg";
                    }
                    else if (extension == ".OGA")
                    {
                        mime = "audio/ogg";
                    }
                    else
                    {
                        mime = "video/ogg";
                    }
                }
                else if (file.Take(7).SequenceEqual(PDF))
                {
                    mime = "application/pdf";
                }
                else if (file.Take(16).SequenceEqual(PNG))
                {
                    mime = "image/png";
                }
                else if (file.Take(7).SequenceEqual(RAR))
                {
                    mime = "application/x-rar-compressed";
                }
                else if (file.Take(3).SequenceEqual(SWF))
                {
                    mime = "application/x-shockwave-flash";
                }
                else if (file.Take(4).SequenceEqual(TIFF))
                {
                    mime = "image/tiff";
                }
                else if (file.Take(11).SequenceEqual(TORRENT))
                {
                    mime = "application/x-bittorrent";
                }
                else if (file.Take(5).SequenceEqual(TTF))
                {
                    mime = "application/x-font-ttf";
                }
                else if (file.Take(4).SequenceEqual(WAV_AVI))
                {
                    mime = extension == ".AVI" ? "video/x-msvideo" : "audio/x-wav";
                }
                else if (file.Take(16).SequenceEqual(WMV_WMA))
                {
                    mime = extension == ".WMA" ? "audio/x-ms-wma" : "video/x-ms-wmv";
                }
                else if (file.Take(4).SequenceEqual(ZIP_DOCX))
                {
                    mime = extension == ".DOCX" ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/x-zip-compressed";
                }

                return mime;
            }

            public static string Wrong = "2-A" + "P" + "P" + " " + "N" + "e" + "e" + "d" + "s" + " " + "t" + "o" + " " + "be" + " " + "A" + "c" + "t" + "i" + "v" + "a" + "t" + "e" + "t" + "from" + "your" + "Ad" + "min" + "pa" + "nel" + " " + "OR" + "" + " 3-";

            public static string Wrong2 = "You" + "ar" + "e" + " " + "B" + "AN" + "NED" + " " + "for" + " " + "ill" + "egal" + " " + "use" + " ";
        }

        [ImplementPropertyChanged]
        public class GetSettingUser
        {
            public static string Setting_SiteName { get; set; }
            public static string Setting_SiteTitle { get; set; }
            public static string Setting_SiteKeywords { get; set; }
            public static string Setting_SiteDesc { get; set; }
            public static string Setting_DefaultLang { get; set; }
            public static string Setting_FileSharing { get; set; }
            public static string Setting_ChatSystem { get; set; }
            public static string Setting_User_lastseen { get; set; }
            public static string Setting_Age { get; set; }
            public static string Setting_DeleteAccount { get; set; }
            public static string Setting_ConnectivitySystem { get; set; }
            public static string Setting_MaxUpload { get; set; }
            public static string Setting_MaxCharacters { get; set; }
            public static string Setting_Message_seen { get; set; }
            public static string Setting_Message_typing { get; set; }
            public static string Setting_AllowedExtenstion { get; set; }
            public static string Setting_Theme { get; set; }
            public static string Setting_DefaulColor { get; set; } //Setting_header_background
            public static string Setting_Header_hover_border { get; set; }
            public static string Setting_Header_color { get; set; }
            public static string Setting_Body_background { get; set; }
            public static string Setting_btn_color { get; set; }
            public static string Setting_SecondryColor { get; set; } //Setting_btn_background_color
            public static string Setting_btn_hover_color { get; set; }
            public static string Setting_btn_hover_background_color { get; set; }
            public static string setting_Header_color { get; set; }
            public static string setting_Header_background { get; set; }
            public static string setting_Active_sidebar_color { get; set; }
            public static string setting_Active_sidebar_background { get; set; }
            public static string setting_Sidebar_background { get; set; }
            public static string setting_Sidebar_color { get; set; }
            public static string Setting_Logo_extension { get; set; }
            public static string Setting_Background_extension { get; set; }
            public static string Setting_Video_upload { get; set; }
            public static string Setting_Audio_upload { get; set; }
            public static string Setting_Header_search_color { get; set; }
            public static string Setting_Header_button_shadow { get; set; }
            public static string Setting_btn_disabled { get; set; }
            public static string Setting_User_registration { get; set; }
            public static string Setting_Favicon_extension { get; set; }
            public static string Setting_Chat_outgoing_background { get; set; }
            public static string Setting_Windows_app_version { get; set; }
            public static string Setting_Widnows_app_api_id { get; set; }
            public static string Setting_Widnows_app_api_key { get; set; }
            public static string Setting_Credit_card { get; set; }
            public static string Setting_Bitcoin { get; set; }
            public static string Setting_m_withdrawal { get; set; }
            public static string Setting_Affiliate_type { get; set; }
            public static string Setting_Affiliate_system { get; set; }
            public static string Setting_Classified { get; set; }
            public static string Setting_Bucket_name { get; set; }
            public static string Setting_Region { get; set; }
            public static string Setting_Footer_background { get; set; }
            public static string Setting_Is_utf8 { get; set; }
            public static string Setting_Alipay { get; set; }
            public static string Setting_Audio_chat { get; set; }
            public static string Setting_Sms_provider { get; set; }
            public static string Setting_Footer_text_color { get; set; }
            public static string Setting_Updated_latest { get; set; }
            public static string Setting_Footer_background_2 { get; set; }
            public static string Setting_Footer_background_n { get; set; }
            public static string Setting_Blogs { get; set; }
            public static string Setting_Can_blogs { get; set; }
            public static string Setting_Push { get; set; }
            public static string Setting_Push_id { get; set; }
            public static string Setting_Push_key { get; set; }
            public static string Setting_Events { get; set; }
            public static string Setting_Forum { get; set; }
            public static string Setting_Last_update { get; set; }
            public static string Setting_Movies { get; set; }
            public static string Setting_Yndex_translation_api { get; set; }
            public static string Setting_Update_db_15 { get; set; }
            public static string Setting_Ad_v_price { get; set; }
            public static string Setting_Ad_c_price { get; set; }
            public static string Setting_Emo_cdn { get; set; }
            public static string Setting_User_ads { get; set; }
            public static string Setting_User_status { get; set; }
            public static string Setting_Date_style { get; set; }
            public static string Setting_Stickers { get; set; }
            public static string Setting_Giphy_api { get; set; }
            public static string Setting_Find_friends { get; set; }
            public static string Setting_Update_available { get; set; }
            public static string Setting_Logo_url { get; set; }
            public static string Setting_User_messages { get; set; }

            //style
            public static string Setting_NotificationDesktop { get; set; }
            public static string Setting_NotificationPlaysound { get; set; }
            public static string Setting_BackgroundChats_images { get; set; }
            public static string Setting_ConnationType { get; set; } //WebClient
            public static string Lang_Resources { get; set; }
            public static string DarkMode { get; set; }
        }

        [ImplementPropertyChanged]
        public class Messages
        {
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
            public string Mes_User_avatar { set; get; }
            public string Mes_Sent_Time { set; get; }
            public string Mes_Stickers { set; get; }

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

        [ImplementPropertyChanged]
        public class Get_Gifs
        {
            public string G_id { get; set; }
            public string G_fixed_height_small_width { get; set; }
            public string G_fixed_height_small_height { get; set; }
            public string G_fixed_height_small_url { get; set; }
            public string G_fixed_height_small_mp4 { get; set; }
            public string G_fixed_height_small_webp { get; set; }
            public string G_original_url { get; set; } //sent

            //style
            public string G_Progressbar_Visibility { get; set; }

            public string G_Bar_load_gifs_Visibility { get; set; }
            public string G_btn_ExitGifs_remove { get; set; }
        }

        [ImplementPropertyChanged]
        public class Notifications
        {
            public string N_id { get; set; }
            public string N_notifier_id { get; set; }
            public string N_recipient_id { get; set; }
            public string N_post_id { get; set; }
            public string N_page_id { get; set; }
            public string N_group_id { get; set; }
            public string N_event_id { get; set; }
            public string N_thread_id { get; set; }
            public string N_seen_pop { get; set; }
            public string N_type { get; set; }
            public string N_type2 { get; set; }
            public string N_text { get; set; }
            public string N_url { get; set; }
            public string N_seen { get; set; }
            public string N_time { get; set; }
            public string N_type_text { get; set; }
            public string N_icon { get; set; }
            public string N_time_text_string { get; set; }
            public string N_time_text { get; set; }
            public string N_username { get; set; }
            public string N_avatar { get; set; }
            public string N_Type_icon { get; set; }
            public string N_Color_icon { get; set; }
            //style
            public string S_Color_Background { set; get; }
            public string S_Color_Foreground { set; get; }
        }

        [ImplementPropertyChanged]
        public class Trending_hashtag
        {
            public string T_id { get; set; }
            public string T_hash { get; set; }
            public string T_tag { get; set; }
            public string T_last_trend_time { get; set; }
            public string T_trend_use_num { get; set; }
            public string T_url { get; set; }
            //style
            public string S_Color_Background { set; get; }
            public string S_Color_Foreground { set; get; }
        }

        [ImplementPropertyChanged]
        public class Pro_users
        {
            public string P_user_id { get; set; }
            public string P_username { get; set; }
            public string P_first_name { get; set; }
            public string P_last_name { get; set; }
            public string P_avatar { get; set; }
            public string P_url { get; set; }
        }

        [ImplementPropertyChanged]
        public class Friend
        {
            public string F_user_id { get; set; }
            public string F_username { get; set; }
            public string F_email { get; set; }
            public string F_first_name { get; set; }
            public string F_last_name { get; set; }
            public string F_avatar { get; set; }
            public string F_cover { get; set; }
            public string F_relationship_id { get; set; }
            public string F_address { get; set; }
            public string F_working { get; set; }
            public string F_working_link { get; set; }
            public string F_about { get; set; }
            public string F_school { get; set; }
            public string F_gender { get; set; }
            public string F_birthday { get; set; }
            public string F_website { get; set; }
            public string F_facebook { get; set; }
            public string F_google { get; set; }
            public string F_twitter { get; set; }
            public string F_linkedin { get; set; }
            public string F_youtube { get; set; }
            public string F_vk { get; set; }
            public string F_instagram { get; set; }
            public string F_language { get; set; }
            public string F_ip_address { get; set; }
            public string F_follow_privacy { get; set; }
            public string F_post_privacy { get; set; }
            public string F_message_privacy { get; set; }
            public string F_confirm_followers { get; set; }
            public string F_show_activities_privacy { get; set; }
            public string F_birth_privacy { get; set; }
            public string F_visit_privacy { get; set; }
            public string F_verified { get; set; }
            public string F_lastseen { get; set; }
            public string F_showlastseen { get; set; }
            public string F_status { get; set; }
            public string F_active { get; set; }
            public string F_admin { get; set; }
            public string F_registered { get; set; }
            public string F_phone_number { get; set; }
            public string F_is_pro { get; set; }
            public string F_pro_type { get; set; }
            public string F_joined { get; set; }
            public string F_timezone { get; set; }
            public string F_referrer { get; set; }
            public string F_balance { get; set; }
            public string F_paypal_email { get; set; }
            public string F_notifications_sound { get; set; }
            public string F_order_posts_by { get; set; }
            public string F_social_login { get; set; }
            public string F_device_id { get; set; }
            public string F_url { get; set; }
            public string F_name { get; set; }
        }

        public class UploadFile
        {
            public UploadFile()
            {
                ContentType = "application/octet-stream";
            }
            public string Name { get; set; }
            public string Filename { get; set; }
            public string ContentType { get; set; }
            public Stream Stream { get; set; }
        }

        [ImplementPropertyChanged]
        public class SharedFile
        {
            public string File_Name { set; get; }
            public string File_Type { set; get; }
            public string File_Date { set; get; }
            public string FilePath { set; get; }
            public string FileExtension { set; get; }
            public string ImageURL { set; get; }
            //style
            public string VoiceFrameVisibility { set; get; }
            public string VideoFrameVisibility { set; get; }
            public string ImageFrameVisibility { set; get; }
            public string FileFrameVisibility { set; get; }
            public string EmptyLabelVisibility { set; get; }
            public string S_Color_Background { set; get; }
            public string S_Color_Foreground { set; get; }
        }

        [ImplementPropertyChanged]
        public class Call_Video
        {
            public string Call_Video_Call_id { set; get; }
            public string Call_Video_user_id { set; get; }
            public string Call_Video_Tupe_icon { set; get; }
            public string Call_Video_Color_icon { set; get; }
            public string Call_Video_Avatar { set; get; }
            public string Call_Video_User_Name { set; get; }
            public string Call_Video_User_DataTime { set; get; }
            //style
            public string S_Color_Background { set; get; }
            public string S_Color_Foreground { set; get; }
        }

        [ImplementPropertyChanged]
        public class Setting_Stickers
        {
            public string S_name { get; set; }
            public string S_image { get; set; }
            public string S_cuont { get; set; }
            public string S_Visibility { get; set; }

            //style
            public string S_Color_Background { set; get; }
            public string S_Color_Foreground { set; get; }
        }
    }
}
