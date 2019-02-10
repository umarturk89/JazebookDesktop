using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWonder_Desktop.Controls
{
    public static class UserDetails
    {
        public static string access_token = "";
        public static string User_id = "";
        public static string Username = "";
        public static string Full_name = "";
        public static string Password = "";
        public static string Email = "";
        public static string Cookie = "";
        public static string Status;
        public static string avatar = "";
        public static string cover = "";
        public static string Device_ID = "";
        public static string Lang = "";
        public static string Lat = "";
        public static string Lng = "";
        public static bool NotificationPopup { get; set; } = true;

        public static Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        public static string Time = unixTimestamp.ToString();

    }
}
