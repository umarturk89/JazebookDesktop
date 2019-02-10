using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WoWonder_Desktop.Controls;
using WoWonder_Desktop.language;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for Managelocalstrong_Window.xaml
    /// </summary>
    public partial class Managelocalstrong_Window : Window
    {
        private double size = 0;

        public Managelocalstrong_Window()
        {
            InitializeComponent();
            this.Title = "Manage local storage (" + Settings.Application_Name + ")";

            count_all_file();

            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            ModeDark_Window();
        }

        //Functions Get count all file
        public void count_all_file()
        {
            try
            {
                //count all directry
                //var directoryInfo = new DirectoryInfo(Functions.Files_Destination);
                //int directoryCount = directoryInfo.GetDirectories().Length;

                // Will Retrieve count of all type #Images in directry and sub directries
                int images_Count = Directory.GetFiles(Functions.Files_Destination, "*.jpg",
                                           SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.jpeg", SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.gif", SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.png", SearchOption.AllDirectories)
                                       .Length;

                // Will Retrieve count of all type  #Video in directry and sub directries
                int video_Count = Directory.GetFiles(Functions.Files_Destination, "*.mp4",
                                          SearchOption.AllDirectories)
                                      .Length + Directory
                                      .GetFiles(Functions.Files_Destination, "*.mpeg", SearchOption.AllDirectories)
                                      .Length + Directory
                                      .GetFiles(Functions.Files_Destination, "*.avi", SearchOption.AllDirectories)
                                      .Length + Directory
                                      .GetFiles(Functions.Files_Destination, "*.flv", SearchOption.AllDirectories)
                                      .Length + Directory
                                      .GetFiles(Functions.Files_Destination, "*.wmv", SearchOption.AllDirectories)
                                      .Length + Directory
                                      .GetFiles(Functions.Files_Destination, "*.mpg-4", SearchOption.AllDirectories)
                                      .Length + Directory
                                      .GetFiles(Functions.Files_Destination, "*.mpg", SearchOption.AllDirectories)
                                      .Length;

                // Will Retrieve count of all type #Audio in directry and sub directries
                int sounds_Count = Directory.GetFiles(Functions.Files_Destination, "*.mp3",
                                           SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.aac", SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.aiff", SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.amr", SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.arf", SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.m4a", SearchOption.AllDirectories)
                                       .Length + Directory
                                       .GetFiles(Functions.Files_Destination, "*.wav", SearchOption.AllDirectories)
                                       .Length;

                // Will Retrieve count of all type #Files in directry and sub directries
                int file_Count = Directory.GetFiles(Functions.Files_Destination, "*.txt",
                                         SearchOption.AllDirectories)
                                     .Length + Directory
                                     .GetFiles(Functions.Files_Destination, "*.pdf", SearchOption.AllDirectories)
                                     .Length +
                                 Directory.GetFiles(Functions.Files_Destination, "*.rar",
                                         SearchOption.AllDirectories)
                                     .Length + Directory
                                     .GetFiles(Functions.Files_Destination, "*.zip", SearchOption.AllDirectories)
                                     .Length + Directory
                                     .GetFiles(Functions.Files_Destination, "*.docx", SearchOption.AllDirectories)
                                     .Length + Directory
                                     .GetFiles(Functions.Files_Destination, "*.doc", SearchOption.AllDirectories)
                                     .Length;

                // Calculate total size of all pngs.
                double x = GetDirectorySize(Functions.Files_Destination);
                double total_size = x / 1024.0F / 1024.0F;

                Lbl_cuont_images.Content = LocalResources.label_cuont_images + " = " + images_Count;
                Lbl_cuont_video.Content = LocalResources.label_cuont_video + " = " + video_Count;
                Lbl_cuont_sounds.Content = LocalResources.label_cuont_sounds + " = " + sounds_Count;
                Lbl_cuont_file.Content = LocalResources.label_cuont_file + " = " + file_Count;
                Lbl_total_size.Content = LocalResources.label_total_size + " = " + total_size.ToString("0.### MB");
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //Functions Get Directory Size
        private double GetDirectorySize(string directory)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(directory))
                {
                    GetDirectorySize(dir);
                }

                foreach (FileInfo file in new DirectoryInfo(directory).GetFiles())
                {
                    size += file.Length;
                }
                return size;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        //Click Link Clear all data file
        private void Lnk_Clear_all_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Functions.clearFolder();
                Lbl_cuont_images.Content = LocalResources.label_cuont_images + " = " + 0;
                Lbl_cuont_video.Content = LocalResources.label_cuont_video + " = " + 0;
                Lbl_cuont_sounds.Content = LocalResources.label_cuont_sounds + " = " + 0;
                Lbl_cuont_file.Content = LocalResources.label_cuont_file + " = " + 0;
                Lbl_total_size.Content = LocalResources.label_total_size + " = " + 0.ToString("0.00 MB");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Click Button Cancel 
        private void Btn_cancel_OnClick(object sender, RoutedEventArgs e)
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

                BorderMain.Background = new SolidColorBrush(DarkBackgroundColor);
                txt_Local_strong.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_cuont_images.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_cuont_video.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_cuont_sounds.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_cuont_file.Foreground = new SolidColorBrush(WhiteBackgroundColor);
                Lbl_total_size.Foreground = new SolidColorBrush(WhiteBackgroundColor);
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
