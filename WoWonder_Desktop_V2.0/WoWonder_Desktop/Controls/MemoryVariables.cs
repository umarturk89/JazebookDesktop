using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Controls
{
  public  class MemoryVariables
    {
        public  static List<ProfileVariables>UsersProfileList = new List<ProfileVariables>();

        public class ProfileVariables
        {
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

        // INSERT DATA MY PROFILE USER TO LIST
        public static void Add_To_user_profile_List(DataBase.ProfilesTable profile)
        {
            if (profile != null)
            {
                ProfileVariables pv = new ProfileVariables();

                string AvatarSplit = profile.pm_Avatar.Split('/').Last();

                pv.pm_UserId = profile.pm_UserId;
                pv.pm_Username = profile.pm_Username;
                pv.pm_Email = profile.pm_Email;
                pv.pm_First_name = profile.pm_First_name;
                pv.pm_Last_name = profile.pm_Last_name;
                pv.pm_Avatar = Functions.Get_image(UserDetails.User_id, AvatarSplit, profile.pm_Avatar);
                pv.pm_Cover = profile.pm_Cover;
                pv.pm_Relationship_id = profile.pm_Relationship_id;
                pv.pm_Address = profile.pm_Address;
                pv.pm_Working = profile.pm_Working;
                pv.pm_Working_link = profile.pm_Working_link;
                pv.pm_About = profile.pm_About;
                pv.pm_School = profile.pm_School;
                pv.pm_Gender = profile.pm_Gender;
                pv.pm_Birthday = profile.pm_Birthday;
                pv.pm_Website = profile.pm_Website;
                pv.pm_Facebook = profile.pm_Facebook;
                pv.pm_Google = profile.pm_Google;
                pv.pm_Twitter = profile.pm_Twitter;
                pv.pm_Linkedin = profile.pm_Linkedin;
                pv.pm_Youtube = profile.pm_Youtube;
                pv.pm_Instagram = profile.pm_Instagram;
                pv.pm_Vk = profile.pm_Vk;
                pv.pm_Language = profile.pm_Language;
                pv.pm_Ip_address = profile.pm_Ip_address;
                pv.pm_Verified = profile.pm_Verified;
                pv.pm_Lastseen = profile.pm_Lastseen;
                pv.pm_Showlastseen = profile.pm_Showlastseen;
                pv.pm_Status = profile.pm_Status;
                pv.pm_Active = profile.pm_Active;
                pv.pm_Admin = profile.pm_Admin;
                pv.pm_Registered = profile.pm_Registered;
                pv.pm_Phone_number = profile.pm_Phone_number;
                pv.pm_Is_pro = profile.pm_Is_pro;
                pv.pm_Pro_type = profile.pm_Pro_type;
                pv.pm_Joined = profile.pm_Joined;
                pv.pm_Timezone = profile.pm_Timezone;
                pv.pm_Referrer = profile.pm_Referrer;
                pv.pm_Balance = profile.pm_Balance;
                pv.pm_Paypal_email = profile.pm_Paypal_email;
                pv.pm_Notifications_sound = profile.pm_Notifications_sound;
                pv.pm_Order_posts_by = profile.pm_Order_posts_by;
                pv.pm_Social_login = profile.pm_Social_login;
                pv.pm_Device_id = profile.pm_Device_id;
                pv.pm_Url = profile.pm_Url;
                pv.pm_Name = profile.pm_Name;

                UsersProfileList.Add(pv);
            }
        }
    }
}
