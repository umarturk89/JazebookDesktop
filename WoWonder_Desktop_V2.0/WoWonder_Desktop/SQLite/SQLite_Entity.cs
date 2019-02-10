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
    public class SQLite_Entity
    {
        public static SQLiteConnection Connection;

        // Open Connection in Database
        public static void Connect()
        {
            try
            {
                var Database_Destination = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Settings.Application_Name + "\\Database\\";
                if (Directory.Exists(Database_Destination) == false)
                {
                    Directory.CreateDirectory(Database_Destination);
                }
                Connection = new SQLiteConnection(Path.Combine(Database_Destination, "MainDatabase.db3"));

                //Create Table in Database
                Connection.CreateTable<DataBase.LoginTable>();
                Connection.CreateTable<DataBase.SettingsTable>();
                Connection.CreateTable<DataBase.ProfilesTable>();
                Connection.CreateTable<DataBase.UsersContactProfileTable>();
                Connection.CreateTable<DataBase.ChatActivity>();
                Connection.CreateTable<DataBase.UsersContactTable>();
                Connection.CreateTable<DataBase.MessagesTable>();
                Connection.CreateTable<DataBase.GifsTable>();
                Connection.CreateTable<DataBase.StickersTable>(); 
                Connection.CreateTable<DataBase.CallVideoTable>();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        // Close Connection in Database
        public static void Close()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception)
            {

            }
        }
        // delete table 
        public static void DropAll()
        {
            try
            {
                Connection.DropTable<DataBase.LoginTable>();
                Connection.DropTable<DataBase.SettingsTable>();
                Connection.DropTable<DataBase.ProfilesTable>();
                Connection.DropTable<DataBase.UsersContactProfileTable>();
                Connection.DropTable<DataBase.ChatActivity>();
                Connection.DropTable<DataBase.UsersContactTable>();
                Connection.DropTable<DataBase.MessagesTable>();
                Connection.DropTable<DataBase.GifsTable>();
                Connection.DropTable<DataBase.StickersTable>();
                Connection.DropTable<DataBase.CallVideoTable>();
            }
            catch (Exception)
            {

            }
        }

        public static DataBase.LoginTable Get_SessionInfo()
        {
            var S = SQLite_Entity.Connection.Table<DataBase.LoginTable>().FirstOrDefault();
            if (S == null)
            {
                return null;
            }
            else
            {
                return S;
            }
        }
    }
}
