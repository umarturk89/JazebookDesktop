using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WoWonder_Desktop.Controls
{
    public class MessageDataTemplate : DataTemplateSelector
    {
        public  DataTemplate Coming_Text_DataTemplate { get; set; }
        public  DataTemplate Going_Text_DataTemplate { get; set; }
        public DataTemplate Going_Image_DataTemplate { get; set; }
        public DataTemplate Comming_Image_DataTemplate { get; set; }
        public DataTemplate Comming_Sound_DataTemplate { get; set; }
        public DataTemplate Going_Sound_DataTemplate { get; set; }
        public DataTemplate Comming_video_DataTemplate { get; set; }
        public DataTemplate Going_video_DataTemplate { get; set; }
        public DataTemplate Going_Contact_DataTemplate { get; set; }
        public DataTemplate Comming_Contact_DataTemplate { get; set; }
        public DataTemplate Going_Sticker_DataTemplate { get; set; }
        public DataTemplate Comming_Sticker_DataTemplate { get; set; }
        public DataTemplate Going_File_DataTemplate { get; set; }
        public DataTemplate Comming_File_DataTemplate { get; set; }
        public DataTemplate Comming_Gifs_DataTemplate { get; set; }
        public DataTemplate Going_Gifs_DataTemplate  { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var msg = item as Classes.Messages;
            if (msg.Mes_Type == "left_text")
            {
                return Coming_Text_DataTemplate;
            }
            else if (msg.Mes_Type == "right_text")
            {
                return Going_Text_DataTemplate;
            }
            else if (msg.Mes_Type == "right_image")
            {
                return Going_Image_DataTemplate;
            }
            else if (msg.Mes_Type == "left_image")
            {
                return Comming_Image_DataTemplate;
            }
            else if (msg.Mes_Type == "left_file")
            {
                return Comming_File_DataTemplate;
            }
            else if (msg.Mes_Type == "right_file")
            {
                return Going_File_DataTemplate;
            }
            else if (msg.Mes_Type == "right_video")
            {
                return Going_video_DataTemplate;
            }
            else if (msg.Mes_Type == "left_video")
            {
                return Comming_video_DataTemplate;
            }
            else if (msg.Mes_Type == "right_audio")
            {
                return Going_Sound_DataTemplate;
            }
            else if (msg.Mes_Type == "left_audio")
            {
                return Comming_Sound_DataTemplate;
            }
            else if (msg.Mes_Type == "right_contact")
            {
                return Going_Contact_DataTemplate;
            }
            else if (msg.Mes_Type == "left_contact")
            {
                return Comming_Contact_DataTemplate;
            }
            else if (msg.Mes_Type == "right_sticker")
            {
                return Going_Sticker_DataTemplate;
            }
            else if (msg.Mes_Type == "left_sticker")
            {
                return Comming_Sticker_DataTemplate;
            }
            else if (msg.Mes_Type == "right_gif")
            {
                return Going_Gifs_DataTemplate;
            }
            else if (msg.Mes_Type == "left_gif")
            {
                return Comming_Gifs_DataTemplate;
            }
            else
            {
                return Comming_Gifs_DataTemplate;
            }
        }
    }
}
