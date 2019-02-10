using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WoWonder_Desktop.language;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for Video_MediaPlayer_Window.xaml
    /// </summary>
    public partial class Video_MediaPlayer_Window : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
      
        public Video_MediaPlayer_Window(string path)
        {
            InitializeComponent();
            this.Title = "Video MediaPlayer (" + Settings.Application_Name + ")";

            Vidoe_MediaElement.Source = new Uri(path);
            Vidoe_MediaElement.Play();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            if (Settings.FlowDirection_RightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Vidoe_MediaElement.Source != null)
                {
                    if (Vidoe_MediaElement.NaturalDuration.HasTimeSpan)
                    {
                        if (Convert.ToInt32(Vidoe_MediaElement.NaturalDuration.TimeSpan.TotalSeconds) != SliderVideo.Value)
                        {
                            lblStatus.Content = String.Format("{0} / {1}", Vidoe_MediaElement.Position.ToString(@"mm\:ss"), Vidoe_MediaElement.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                            SliderVideo.Value = Vidoe_MediaElement.Position.TotalSeconds;
                            SliderVideo.Maximum = Convert.ToInt32(Vidoe_MediaElement.NaturalDuration.TimeSpan.TotalSeconds);
                        }
                        else
                        {
                            btnPlay.Visibility = Visibility.Visible;
                            btnPause.Visibility = Visibility.Collapsed;
                            SliderVideo.Value = 0;
                            Vidoe_MediaElement.Stop();
                            timer.Stop();
                        }
                    }
                }
                else
                    lblStatus.Content =LocalResources.Label2_No_file_selected + "...";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void BtnPlay_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                btnPlay.Visibility = Visibility.Collapsed;
                btnPause.Visibility = Visibility.Visible;

                if (SliderVideo.Value > 0)
                {
                    Vidoe_MediaElement.Play();
                    timer.Start();
                }
                else
                {
                    Vidoe_MediaElement.Position = TimeSpan.FromSeconds(0);
                    Vidoe_MediaElement.Play();
                    timer.Start();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void BtnPause_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Vidoe_MediaElement.CanPause)
                {
                    Vidoe_MediaElement.Pause();
                }

                btnPlay.Visibility = Visibility.Visible;
                btnPause.Visibility = Visibility.Collapsed;
                timer.Stop();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void BtnFullScreenExpand_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                btnFullScreenExpand.Visibility = Visibility.Collapsed;
                btnFullScreenCompress.Visibility = Visibility.Visible;

                Vidoe_MediaElement.Width = Double.NaN;
                Vidoe_MediaElement.Height = Double.NaN;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void BtnFullScreenCompress_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                btnFullScreenExpand.Visibility = Visibility.Visible;
                btnFullScreenCompress.Visibility = Visibility.Collapsed;

                Vidoe_MediaElement.Width = 500;
                Vidoe_MediaElement.Height = 450;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.Stop();
                Vidoe_MediaElement.Stop();
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void SliderVideo_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                var mi = (Slider)sender;
                mi.Maximum = Vidoe_MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Thumb_OnDragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            try
            {
                var mi = (Slider)sender;
                TimeSpan ts = new TimeSpan(0, 0, 0, (int)mi.Value);
                Vidoe_MediaElement.Position = ts;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void BtnRpeat_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                timer.Stop();
                Vidoe_MediaElement.Position = TimeSpan.Zero;
                Vidoe_MediaElement.Play();
                timer.Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Vidoe_MediaElement_OnMediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(e.ErrorException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
