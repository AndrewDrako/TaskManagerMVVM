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
            "#EEEEEE",
            "#000000"
        };


        public static string[] BtnColor = new string[2]
        {
            "#38c2a4",
            "#0088FF"
        };

        public static string[] BackgroundColor = new string[2]
        {
            "",
            ""
        };

        public static int iTheme;
    }
}
