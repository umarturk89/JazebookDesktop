using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.language;
using WoWonder_Desktop.SQLite;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for Setting_Stickers_Window.xaml
    /// </summary>
    public partial class Setting_Stickers_Window : Window
    {
        #region Lists Items Declaration

        public static ObservableCollection<Classes.Setting_Stickers> ListStickers = new ObservableCollection<Classes.Setting_Stickers>();
        public static ObservableCollection<Classes.Setting_Stickers> ListTrending = new ObservableCollection<Classes.Setting_Stickers>();

        #endregion

        #region Variables

        string Stickers_name = "";
        private MainWindow _MainWindow;

        #endregion

        public Setting_Stickers_Window(MainWindow mainWindow)
        {
            InitializeComponent();
            this.Title = "Setting Stickers (" + Settings.Application_Name + ")";

            _MainWindow = mainWindow;
            GetStickers();
            GetTrendingStickers();

            if (Settings.WebException_Security)
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                       | SecurityProtocolType.Tls11
                                                       | SecurityProtocolType.Tls12
                                                       | SecurityProtocolType.Ssl3;
            }


            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }

            ModeDark_Window();
        }

        //########################## My Stickers ##########################

        #region My Stickers

        // Functions Get Stickers */ Visible /*
        public void GetStickers()
        {
            try
            {
                ListStickers.Clear();
                var data_s = SQLiteCommandSender.Get_From_StickersTable();
                if (data_s != null)
                {
                    foreach (var item in data_s)
                    {
                        if (item.S_Visibility == "Visible")
                        {
                            Classes.Setting_Stickers ss = new Classes.Setting_Stickers();
                            ss.S_name = item.S_name;
                            ss.S_Visibility = item.S_Visibility;
                            ss.S_image = item.S_image;
                            ss.S_cuont = item.S_cuont + " " + LocalResources.label_Item_My_Stickers;
                            if (MainWindow.ModeDarkstlye)
                            {
                               ss.S_Color_Background = "#232323";
                               ss.S_Color_Foreground = "#efefef";
                            }
                            else
                            {
                                ss.S_Color_Background = "#ffff";
                                ss.S_Color_Foreground = "#444";
                            }

                            ListStickers.Add(ss);
                        }
                    }
                    StickersListview.ItemsSource = ListStickers;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Selection Changed null is SelectedItem StickersListview
        private void StickersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StickersListview.SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Mouse Move null is SelectedItem StickersListview
        private void StickersListview_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                StickersListview.SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button delete Stickers
        private void Button_delete_Stickers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mi = (Button)sender;
                Stickers_name = mi.CommandParameter.ToString();

                var selectedGroup = ListStickers.FirstOrDefault(a => a.S_name == Stickers_name);

                if (selectedGroup != null)
                {
                    selectedGroup.S_Visibility = "Collapsed";

                    SQLiteCommandSender.Update_To_StickersTable(selectedGroup.S_name, selectedGroup.S_Visibility);

                    var delete_Stickers = ListStickers.FirstOrDefault(a => a.S_name == selectedGroup.S_name);
                    ListStickers.Remove(delete_Stickers);
                    StickersListview.ItemsSource = ListStickers;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        //########################## Trending ##########################

        #region Trending

        // Functions Get Trending Stickers */ Collapsed /*
        public void GetTrendingStickers()
        {
            try
            {
                ListTrending.Clear();
                var data_s = SQLiteCommandSender.Get_From_StickersTable();
                if (data_s != null)
                {
                    foreach (var item in data_s)
                    {
                        if (item.S_Visibility == "Collapsed")
                        {
                            Classes.Setting_Stickers s = new Classes.Setting_Stickers();
                            s.S_name = item.S_name;
                            s.S_Visibility = item.S_Visibility;
                            s.S_image = item.S_image;
                            s.S_cuont = item.S_cuont + " " + LocalResources.label_Item_My_Stickers;
                            if (MainWindow.ModeDarkstlye)
                            {
                                s.S_Color_Background = "#232323";
                                s.S_Color_Foreground = "#efefef";
                            }
                            else
                            {
                                s.S_Color_Background = "#ffff";
                                s.S_Color_Foreground = "#444";
                            }
                            ListTrending.Add(s);
                        }
                    }
                    TrendingListview.ItemsSource = ListTrending;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        // click Button Add Stickers
        private void Button_Add_Stickers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mi = (Button)sender;
                Stickers_name = mi.CommandParameter.ToString();

                var selectedGroup = ListTrending.FirstOrDefault(a => a.S_name == Stickers_name);

                if (selectedGroup != null)
                {
                    selectedGroup.S_Visibility = "Visible";

                    SQLiteCommandSender.Update_To_StickersTable(selectedGroup.S_name, selectedGroup.S_Visibility);

                    var delete_Stickers = ListTrending.FirstOrDefault(a => a.S_name == selectedGroup.S_name);
                    ListTrending.Remove(delete_Stickers);
                    TrendingListview.ItemsSource = ListTrending;

                    ListStickers.Add(selectedGroup);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Selection Changed null is SelectedItem TrendingListview
        private void TrendingListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TrendingListview.SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Event Mouse Move null is SelectedItem TrendingListview
        private void TrendingListview_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                TrendingListview.SelectedItem = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        #endregion

        // click Button dune windows : Setting Stickers
        private void CloseSetting_StickersButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _MainWindow.GetStickers();
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void ModeDark_Window()
        {
            var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
            var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

            if (MainWindow.ModeDarkstlye)
            {
                this.Background = new SolidColorBrush(DarkBackgroundColor);

                Grid.Background = new SolidColorBrush(DarkBackgroundColor);
                StickersTabcontrol.BorderBrush = new SolidColorBrush(DarkBackgroundColor);
                StickersTabcontrol.Background = new SolidColorBrush(DarkBackgroundColor);

                Item_My_Stickers.Background = new SolidColorBrush(DarkBackgroundColor);
                StickersListview.Background = new SolidColorBrush(DarkBackgroundColor);

                Item_Trending.Background = new SolidColorBrush(DarkBackgroundColor);
                TrendingListview.Background = new SolidColorBrush(DarkBackgroundColor);

                // ListBoxItem >> ListStickers
                if (ListStickers.Count > 0)
                {
                    foreach (var Items in ListStickers)
                    {
                        Items.S_Color_Background = "#232323";
                        Items.S_Color_Foreground = "#efefef";
                    }
                }

                // ListBoxItem >> ListTrending
                if (ListTrending.Count > 0)
                {
                    foreach (var Items in ListTrending)
                    {
                        Items.S_Color_Background = "#232323";
                        Items.S_Color_Foreground = "#efefef";
                    }
                }
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var GridWindow = sender as Grid;
                var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
                var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

                if (MainWindow.ModeDarkstlye)
                {
                    GridWindow.Background = new SolidColorBrush(DarkBackgroundColor);
                }
                else
                {
                    GridWindow.Background = new SolidColorBrush(WhiteBackgroundColor);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
