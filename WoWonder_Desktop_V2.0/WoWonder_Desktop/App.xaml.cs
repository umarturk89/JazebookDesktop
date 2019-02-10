using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Windows;
using CefSharp;
using WoWonderClient;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.Forms;
using WoWonder_Desktop.language;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                SQLite_Entity.Connect();

                ////language codes operating system windows
                CultureInfo ci = CultureInfo.CurrentCulture;

                try
                {
                    var data_lang = SQLite_Entity.Connection.Table<DataBase.SettingsTable>().FirstOrDefault();
                    if (data_lang != null)
                    {
                        if (data_lang.Lang_Resources != "")
                        {
                            if (data_lang.Lang_Resources == "ar" || data_lang.Lang_Resources == "Arabic" ||
                                data_lang.Lang_Resources == "ar-AR")
                            {
                                Settings.FlowDirection_RightToLeft = true;
                            }

                            LocalResources.Culture = new System.Globalization.CultureInfo(data_lang.Lang_Resources);

                            System.Threading.Thread.CurrentThread.CurrentUICulture =
                                new CultureInfo(data_lang.Lang_Resources);
                            System.Threading.Thread.CurrentThread.CurrentCulture =
                                new CultureInfo(data_lang.Lang_Resources);
                        }
                    }
                    else
                    {
                        //language codes operating system windows
                        var Lang_OS_windows2 = Settings.Lang_Resources;
                        var Name_LocalResources = @"pack://application:,,,/" +
                                                  Assembly.GetExecutingAssembly().GetName().Name + ";component/" +
                                                  "/language/LocalResources." + Lang_OS_windows2 + ".resx";

                        LocalResources.Culture = new System.Globalization.CultureInfo(Lang_OS_windows2);

                        if (Lang_OS_windows2 == "ar" || Lang_OS_windows2 == "Arabic" ||
                            Lang_OS_windows2 == "ar-AR")
                        {
                            Settings.FlowDirection_RightToLeft = true;
                        }

                        ////////      
                        System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(Lang_OS_windows2);
                        System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(Lang_OS_windows2);

                        data_lang.Lang_Resources = Lang_OS_windows2;
                        SQLiteCommandSender.Update_SettingsTable(data_lang);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }


                var user = SQLite_Entity.Get_SessionInfo();

                var cefSettings = new CefSettings();
                cefSettings.CefCommandLineArgs.Add("enable-media-stream", "enable-media-stream");
                cefSettings.CefCommandLineArgs.Add("emulation-mode=iphone5", "emulation-mode=iphone5");

                cefSettings.LocalesDirPath = Application.Current.StartupUri + @"\Locales";
                Cef.Initialize(cefSettings);


                if (user != null)
                {
                    if (user.Status == "Active")
                    {
                        WoWonderClient.Current.AccessToken = user.Session;
                        UserDetails.User_id = user.UserId;

                        // INSERT DATA PROFILE USER TO LIST
                        SQLiteCommandSender.Select_From_ProfileTable_By_ID(user.UserId);
                        SQLiteCommandSender.GetUsersSettings();

                        var client = new Client(Settings.TripleDesAppServiceProvider);

                        //var first_rofile = MemoryVariables.UsersProfileList.First();
                        MainWindow wn = new MainWindow();
                        wn.Show();
                    }
                    else if (user.Status == "Pending")
                    {
                        WoWonderClient.Current.AccessToken = user.Session;
                        UserDetails.User_id = user.UserId;
                        MainWindow wn = new MainWindow();
                        wn.Show();
                    }
                }
                else
                {
                    Login_Window fff = new Login_Window();
                    fff.Show();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Console.WriteLine(exception);
            }
        }
    }
}
