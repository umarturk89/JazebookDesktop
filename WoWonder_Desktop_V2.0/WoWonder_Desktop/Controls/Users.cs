using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using PropertyChanged;

namespace WoWonder_Desktop.Controls
{
    [ImplementPropertyChanged]
    public class Users
    {
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

        //style
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
        public string S_Color_Background { set; get; }
        public string S_Color_Foreground { set; get; }
    }

    [ImplementPropertyChanged]
    public class UsersContact
    {
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
        public string UC_null_UserPlatform { set; get; }

        //style
        public string UC_App_Main_Later { set; get; }
        public string UC_Color_onof { set; get; }
        public string UC_chat_color { set; get; }
        public string S_Color_Background { set; get; }
        public string S_Color_Foreground { set; get; }

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

    [ImplementPropertyChanged]
    public class UsersContactProfile
    {
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
        //style
        public string UCP_App_Main_Later { set; get; }
        public string UCP_Color_onof { set; get; }
        public string UCP_null_UserPlatform { set; get; }
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

    [ImplementPropertyChanged]
    public class UsersFriends
    {
        public string U_Id { set; get; }
        public string U_username { set; get; }
        public string U_name { set; get; }
        public string Avatar { set; get; }
        public string U_cover_picture { set; get; }
        public string U_verified { set; get; }
        public string U_lastseen { set; get; }
        public string U_lastseenWithoutCut { set; get; }
        public string U_lastseen_time_text { set; get; }
        public string u_url { set; get; }
        public string U_lastseen_unix_time { set; get; }
        public string U_chat_color { set; get; }
        //Messeges
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

        //style
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

    [ImplementPropertyChanged]
    public class Search
    {
        public string user_Id { set; get; }
        public string Search_Username { set; get; }
        public string Search_Name { set; get; }
        public string Search_Profile_picture { set; get; }
        public string Search_Cover_picture { set; get; }
        public string Search_Verified { set; get; }
        public string Search_Lastseen { set; get; }
        public string Search_Gender { set; get; }
        public string Search_color_Gender { set; get; }
        public string Search_Lastseen_time_text { set; get; }
        public string Search_Url { set; get; }
        public string Search_Is_following { set; get; }
        public string Search_color_follow { set; get; }
        public string Search_text_following { set; get; }
        public string Search_text_color_following { set; get; }
        //style
        public string S_Color_Background { set; get; }
        public string S_Color_Foreground { set; get; }
    }

}