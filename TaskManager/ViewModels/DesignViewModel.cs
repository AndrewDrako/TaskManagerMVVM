using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    public class DesignViewModel : ViewModel
    {
        private string _BackgroundImg;
        private string _FontColor;
        private string _BtnColor;
        private string _PageColor;
        private string _PageColor2;


        /// <summary>
        /// Background image
        /// </summary>
        public string BackgroundImg
        {
            get => ChangeAppTheme.BackgroundImg[ChangeAppTheme.iTheme];
            set => Set(ref _BackgroundImg, value);
        }

        /// <summary>
        /// Page color
        /// </summary>
        public string PageColor
        {
            get => ChangeAppTheme.PageColor[ChangeAppTheme.iTheme];
            set => Set(ref _PageColor, value);
        }



        /// <summary>
        /// Page color second
        /// </summary>
        public string PageColor2
        {
            get => ChangeAppTheme.PageColor2[ChangeAppTheme.iTheme];
            set => Set(ref _PageColor2, value);
        }

        /// <summary>
        /// Font color
        /// </summary>
        public string FontColor
        {
            get => ChangeAppTheme.FontColor[ChangeAppTheme.iTheme];
            set => Set(ref _FontColor, value);
        }

        /// <summary>
        /// Buttons color
        /// </summary>
        public string BtnColor
        {
            get => ChangeAppTheme.BtnColor[ChangeAppTheme.iTheme];
            set => Set(ref _BtnColor, value);
        }
    }
}
