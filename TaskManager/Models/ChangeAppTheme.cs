using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class ChangeAppTheme
    {
        public static string[] BackgroundImg = new string[2]
        {
            "/Design/Images/ThemeGreen.jpg",
            "{x:null}"
        };


        public static string[] FontColor = new string[2]
        {
            "#FFFFFF",
            "#000000"
        };


        public static string[] BtnColor = new string[2]
        {
            "#38c2a4",
            "#0088FF"
        };

        public static string[] BackgroundColor = new string[2]
        {
            "#323232",
            "#DADADA"
        };

        public static string[] PageBackground = new string[2]
        {
            "{x:null}",
            "#83A8E4"
        };

        public static string[] PageColor = new string[2]
        {
            "#3F3F3F",
            "#FFFFFF"
        };

        public static string[] PageColor2 = new string[2]
        {
            "#4E4E4E",
            "#DEDEDE"
        };

        public static string[] NoteColor = new string[2]
        {
            "#4E4E4E",
            "#BCD1E9"
        };

        public static string[] AppTheme = new string[2]
        {
            "/Design/Themes/Custom.xaml",
            "/Design/Themes/Light.xaml"
        };

        public static int iTheme;
    }
}
