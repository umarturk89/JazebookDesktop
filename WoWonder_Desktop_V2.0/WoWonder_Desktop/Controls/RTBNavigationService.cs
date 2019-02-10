using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;
using RichTextBox = System.Windows.Controls.RichTextBox;

namespace WoWonder_Desktop.Controls
{
    public static class RTBNavigationService
    {
        //private static string AppName = Settings.Application_Name;
        //private static string folderDestination = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
        //private static string EmogiFolder = folderDestination + "\\" + AppName + @"\Emoji_Icons\";

        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached(
            "Content", typeof(string), typeof(RTBNavigationService), new PropertyMetadata(null, OnContentChanged)
        );

        public static Dictionary<string, string> _dict = new Dictionary<string, string>
        {
            {":(",  @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/sad.png"},
            {":)",  @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/NormalSmile.png"},
            {"(<", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Laughing Emoji.png"},
            {"**)", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Happy Face Emoji.png"},
            {":p", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Crazy Emoji.png"},
            {":_p", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Cheeky Emoji.png"},
            {"B)", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Cool Emoji.png"},
            {";)", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Wink Emoji.png"},
            {":D", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Cringe Emoji.png"},
            {"/_)", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Flirting Emoji.png"},
            {"0)", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Angel Emoji.png"},
            {":_(", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Crying Emoji.png"},
            {":__(", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Bawling Emoji.png"},
            {":*", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Kissing Emoji.png"},
            {"<3", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Heart Emoji.png"},
            {"</3", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Breaking Heart Emoji.png"},
            {"*_*", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Heart Eyes Emoji.png"},
            {"<5", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Star Emoji.png"},
            {":o", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Surprised Emoji.png"},
            {":0", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Scream Emoji.png"},
            {"o(", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Pained Face Emoji.png"},
            {"-_(", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Dissatisfied Emoji.png"},
            {"x(", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Angry Emoji.png"},
            {"X(", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Red Face Emoji.png"},
            {"-_-", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Face With Straight Mouth Emoji.png"},
            {":-/", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Puzzled Emoji.png"},
            {":|", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Straight Faced Emoji.png"},
            {"!_", @"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/" +"/Images/Emoji_Icons/Heavy Exclamation.png"}
        };

        public static string GetEmoticonText(string text)
        {
            string match = string.Empty;
            int lowestPosition = text.Length;

            foreach (KeyValuePair<string, string> pair in _dict)
            {
                if (text.Contains(pair.Key))
                {
                    int newPosition = text.IndexOf(pair.Key);
                    if (newPosition < lowestPosition)
                    {
                        match = pair.Key;
                        lowestPosition = newPosition;
                    }
                }
            }
            return match;
        }
        public static string GetContent(DependencyObject d)
        { return d.GetValue(ContentProperty) as string; }

        public static void SetContent(DependencyObject d, string value)
        {
            d.SetValue(ContentProperty, value);
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                RichTextBox richTextBox = d as RichTextBox;
                if (richTextBox == null)
                    return;
               
                string content = (string)e.NewValue;
                if (string.IsNullOrEmpty(content))
                    return;

                var dd = richTextBox.Margin.Right + SystemInformation.HorizontalResizeBorderThickness +
                         SystemInformation.HorizontalScrollBarThumbWidth + content.Length + 80 ;
                richTextBox.Width = dd;
                StringWriter myWriter = new StringWriter();
                HttpUtility.HtmlDecode(content, myWriter);
                content = myWriter.ToString();

                var Replacer = content.Replace("<br>  <br>", "\r\n");
                content = Replacer;

                richTextBox.Document.Blocks.Clear();
                richTextBox.IsDocumentEnabled = true;
                Paragraph block = new Paragraph();
                var para = new Paragraph { LineHeight = 1 };
                var r = new Run(content);
                string emoticonText = GetEmoticonText(r.Text);
                para.Inlines.Add(r);

                if (!string.IsNullOrEmpty(emoticonText))
                {
                    while (!string.IsNullOrEmpty(emoticonText))
                    {
                        TextPointer tp = r.ContentStart;
                        while (!tp.GetTextInRun(LogicalDirection.Forward).StartsWith(emoticonText))
                            tp = tp.GetNextInsertionPosition(LogicalDirection.Forward);
                        var tr = new TextRange(tp, tp.GetPositionAtOffset(emoticonText.Length)) { Text = string.Empty };
                        string path = _dict[emoticonText];
                        BitmapImage Emogi = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                        Image smiley = new Image();
                        smiley.Source = Emogi;
                        smiley.Width = 19;
                        smiley.Height = 19;

                        new InlineUIContainer(smiley, tp);

                        if (para != null)
                        {
                            var endRun = para.Inlines.LastInline as Run;
                            if (endRun == null)
                            { break; }
                            else
                            { emoticonText = GetEmoticonText(endRun.Text); }
                        }
                    }
                    richTextBox.Document.Blocks.Add(para);
                }
                else
                {
                    string sample = content;
                    var f = sample.Split(new char[] { ' ' });
                    var para2 = new Paragraph();
                    Color Btn_Background_Color = (Color)ColorConverter.ConvertFromString("#3297ac");

                    foreach (var item in f)
                    {
                        if (item.StartsWith("http") || item.StartsWith("www.") || item.StartsWith("ftp:") || item.StartsWith("Smtp:"))
                        {
                            var link = new Hyperlink();
                            link.Inlines.Add(item);
                            link.TargetName = "_blank";
                            link.TextDecorations = null;
                            link.Foreground = new SolidColorBrush(Btn_Background_Color);
                            link.NavigateUri = new Uri(item);
                            para2.Inlines.Add(link);
                            link.Click += OnUrlClick;
                        }
                        else if (item.StartsWith("#"))
                        {
                            var link = new Hyperlink();
                            item.TrimStart(new char[] { '#' });
                            link.Inlines.Add(item);
                            link.TargetName = "_blank";
                            link.Foreground = new SolidColorBrush(Btn_Background_Color);
                            link.TextDecorations = null; ;
                            string Hashtag = item.Replace("#", "");
                            link.NavigateUri = new Uri(WoWonderClient.Client.WebsiteUrl + "/hashtag/" + Hashtag);
                            para2.Inlines.Add(link);
                            link.Click += OnUrlClick;
                        }
                        else if (item.StartsWith("DownloadUrlFrom>>>"))
                        {
                            var link = new Hyperlink();
                            string Url = item.Replace("DownloadUrlFrom>>>", "");
                            link.Inlines.Add("Download");
                            link.TargetName = "_blank";
                            link.Foreground = new SolidColorBrush(Btn_Background_Color);
                            link.TextDecorations = null;
                            link.NavigateUri = new Uri(Url);
                            para2.Inlines.Add(link);
                            link.Click += OnUrlClick;
                        }
                        else
                        {
                            para2.Inlines.Add(item);
                        }
                        para2.Inlines.Add(" ");
                    }
                    richTextBox.Document.Blocks.Add(para2);
                }
            }
            catch { }
        }

        private static void OnUrlClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start((sender as Hyperlink).NavigateUri.AbsoluteUri);
            }
            catch { }
        }
    }
}
