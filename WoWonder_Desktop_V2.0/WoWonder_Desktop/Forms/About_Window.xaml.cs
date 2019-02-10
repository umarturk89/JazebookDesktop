using System.Windows;

namespace WoWonder_Desktop.Forms
{
    /// <summary>
    /// Interaction logic for About_Window.xaml
    /// </summary>
    public partial class About_Window : Window
    {
        public About_Window()
        {
            InitializeComponent();
            this.Title = "About (" + Settings.Application_Name + ")";
        }

        private void Btn_Close_OnClick(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }
}
