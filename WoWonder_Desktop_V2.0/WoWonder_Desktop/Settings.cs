using System.Reflection;

namespace WoWonder_Desktop
{
    //WOWONDER DESKTOP MESSENGER V2.0
    //*********************************************************
    //1- Add Your name application From Solution Explorer go to WoWonder_Desktop > MainWindow.xaml > Rename Title = "Wowonder"  in lines(16 - 67)
    //2- Add Your logo From Solution Explorer go to WoWonder_Desktop > Images
    //3- Right click on Resources folder > Add > Existing Item > pick your own logo.png
    //Same thing for all images and icons

    public class Settings
    {
        //Main Settings >>>>>
        //*********************************************************
        public static string TripleDesAppServiceProvider = "NFYnA2qriwLLUe74dlNM90fBmeMKm8QuEm2TgU8DdI0iiV8/7rUmdfeSr8kyg3IhGohvfbsEUEgYhdr3Q1gMhx3LuTWxBn1nVq48JF5WS5DpRlXe4uGoYHz6P8lfauvt7CP2cdmA5Mcs9DDeFj3KOYjsdgbkAv8lUyk59n0dN3Ol4jHA+34KC4ZdVqsWr6omeBcpou/XQlH2qoZmVu7P9yPpmAHMMeOgL8/nwwG2T5yJOn26E9bkNliUZ86uIMSXda++Vwg1fMMyg2Cd6Yd/Dw==";

       
        public static string Application_Name = "Wowonder";

        public static bool MobileURL_Visibility = true;
        public static string MobileURL = "https://play.google.com/store/apps/details?id=com.facebook.katana";

        //Main Colors 
        //*********************************************************
        public static string Main_Color = "#a84849";
        public static string Secondery_Color = "#444";
        public static bool Change_ChatPanelColor = true;

        //Dark Colors >> 
        public static string DarkBackground_Color = "#232323";
        public static string LigthBackground_Color = "#444";
        public static string WhiteBackground_Color = "#efefef";

        //Ligth Colors >> 
        public static string DarkBackgroundColor = "#444";
        public static string LigthBackgroundColor = "#f8f8f8";
        public static string WhiteBackgroundColor = "#ffff";
        
        //Bypass Web Erros (OnLogin crach or error set it to true)
        //*********************************************************
        public static bool WebException_Security = true; // >>>>  Security Protocol Type ssl

        //Language Settings >>>>>
        //*********************************************************
        public static bool FlowDirection_RightToLeft = false;
        public static string Lang_Resources = ""; // >>>>  default language

        //Messages Control 
        ///*********************************************************
        public static int RefreshChatActitvityPer = 8; // >>>> 8 seconds
        public static int Update_Message_Receiver_INT = 5; // >>>>  5 seconds

        // login using social media
        //*********************************************************
        public static bool Facebook_Icon = true;
        public static bool Twitter_Icon = true;
        public static bool Vk_Icon = true;
        public static bool Google_Icon = true;
        public static bool Instagram_Icon = true;

        //Login Icons 
        //*********************************************************
        public static string logo_Img = "/Images/icon.png";
        public static string Login_Img = "signup_bg.png";
        public static string Register_Img = "44.jpg";
        public static string avatar_Img = @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" + "/Images/d-avatar.jpg";
        public static string BackgroundChats_images = @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" + "/Images/Backgrounds/emoji-background.png";

        //=============== Message Popup Window Style ==========================
        public static string PopUpBackroundColor = "White";
        public static string PopUpTextFromcolor = "#444";
        public static string PopUpMsgTextcolor = "#444";

        //=============== Call ==========================
        public static bool VideoCall = false;

        //=============== Notification ==========================
        public static string NotificationDesktop = "true";
        public static string NotificationPlaysound = "true";
        public static string Notifications_Message_user = "true";
        public static string Notifications_Message_Sound_user = "true";


        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    }
}
