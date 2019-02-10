using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WoWonder_Desktop.Controls;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for SharedFilesWindow.xaml
    /// </summary>
    public partial class SharedFilesWindow : Window
    {
        public SharedFilesWindow(UsersContactProfile _SelectedUsersContactProfile, UsersContact _SelectedUsersContact,
            string _SelectedType)
        {
            InitializeComponent();
            this.Title = "Shared Files (" + Settings.Application_Name + ")";

            PopluteSharedfilesOn_list();
            if (_SelectedType == "UsersContactProfile")
            {
                HeaderText.Content = _SelectedUsersContactProfile.UCP_first_name + " " +
                                     _SelectedUsersContactProfile.UCP_last_name;
            }
            else
            {
                HeaderText.Content = _SelectedUsersContact.UC_first_name + " " + _SelectedUsersContact.UC_last_name;
            }

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

        //Function Get data My Profile
        public void PopluteSharedfilesOn_list()
        {
            try
            {
                var sharedList = MainWindow.ListSharedFiles.OrderBy(T => T.File_Date);

                var ImagesListItem = new ObservableCollection<Classes.SharedFile>();
                var MediaListItem = new ObservableCollection<Classes.SharedFile>();
                var FileListItem = new ObservableCollection<Classes.SharedFile>();

                if (sharedList.Count() > 1)
                {
                    var imageList = sharedList.Where(a => a.File_Type == "Image");
                    if (imageList.Count() > 0)
                    {
                        foreach (var ImageItem in imageList)
                        {
                            if (MainWindow.ModeDarkstlye)
                            {
                                ImageItem.S_Color_Background = "#232323";
                                ImageItem.S_Color_Foreground = "#efefef";
                            }
                            else
                            {
                                ImageItem.S_Color_Background = "#ffff";
                                ImageItem.S_Color_Foreground = "#444";
                            }
                            ImagesListItem.Add(ImageItem);
                        }
                    }

                    var MediaList = sharedList.Where(a => a.File_Type == "Media");
                    if (MediaList.Count() > 0)
                    {
                        foreach (var ImageItem in MediaList)
                        {
                            if (MainWindow.ModeDarkstlye)
                            {
                                ImageItem.S_Color_Background = "#232323";
                                ImageItem.S_Color_Foreground = "#efefef";
                            }
                            else
                            {
                                ImageItem.S_Color_Background = "#ffff";
                                ImageItem.S_Color_Foreground = "#444";
                            }
                            MediaListItem.Add(ImageItem);
                        }
                    }

                    var FileList = sharedList.Where(a => a.File_Type == "File");
                    if (FileList.Count() > 0)
                    {
                        foreach (var ImageItem in FileList)
                        {
                            if (MainWindow.ModeDarkstlye)
                            {
                                ImageItem.S_Color_Background = "#232323";
                                ImageItem.S_Color_Foreground = "#efefef";
                            }
                            else
                            {
                                ImageItem.S_Color_Background = "#ffff";
                                ImageItem.S_Color_Foreground = "#444";
                            }
                            FileListItem.Add(ImageItem);
                        }
                    }

                    this.ImagesListview.ItemsSource = ImagesListItem;
                    this.MediaListview.ItemsSource = MediaListItem;
                    this.FilesListview.ItemsSource = FileListItem;
                }
                else
                {
                    this.ImagesListview.ItemsSource = sharedList;
                    this.MediaListview.ItemsSource = sharedList;
                    this.FilesListview.ItemsSource = sharedList;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void ImagesListview_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.SharedFile selectedGroup = (Classes.SharedFile)ImagesListview.SelectedItem;

                if (selectedGroup != null)
                {
                    if (selectedGroup.File_Type == "Image")
                    {
                        Process.Start(selectedGroup.FilePath);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void MediaListview_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.SharedFile selectedGroup = (Classes.SharedFile)MediaListview.SelectedItem;

                if (selectedGroup != null)
                {
                    if (selectedGroup.File_Type == "Media")
                    {
                        Process.Start(selectedGroup.FilePath);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void FilesListview_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Classes.SharedFile selectedGroup = (Classes.SharedFile)FilesListview.SelectedItem;

                if (selectedGroup != null)
                {
                    if (selectedGroup.File_Type == "File")
                    {
                        Process.Start(selectedGroup.FilePath);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ModeDark_Window()
        {
            var DarkBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.DarkBackground_Color);
            var WhiteBackgroundColor = (Color)ColorConverter.ConvertFromString(Settings.WhiteBackground_Color);

            if (MainWindow.ModeDarkstlye)
            {
                this.Background = new SolidColorBrush(DarkBackgroundColor);

                Border.Background = new SolidColorBrush(DarkBackgroundColor);
                Lbl_Share_files.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                HeaderText.Foreground = new SolidColorBrush(WhiteBackgroundColor);

                SharedFilesTabcontrol.Background = new SolidColorBrush(DarkBackgroundColor);
                SharedFilesTabcontrol.BorderBrush = new SolidColorBrush(DarkBackgroundColor);

                Item_Images.Background = new SolidColorBrush(DarkBackgroundColor);
                ImagesListview.Background = new SolidColorBrush(DarkBackgroundColor);

                MediaListview.Background = new SolidColorBrush(DarkBackgroundColor);

                Item_Files.Background = new SolidColorBrush(DarkBackgroundColor);
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
