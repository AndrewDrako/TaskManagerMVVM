using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    public class Project : ViewModel
    {


        #region Имя проекта

        private string _ProjectName;

        public string ProjectName
        {
            get => _ProjectName;
            set
            {   
                Set(ref _ProjectName, value);
            }
        }

        #endregion

        #region Имя создателея проекта

        private string _PersonName;

        public string PersonName
        {
            get => _PersonName;
            set => Set(ref _PersonName, value);
        }

        #endregion

        #region Design

        private string _FontColor;
        private string _PageColor;
        private string _PageColor2;
        private string _BtnColor;

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

        #endregion


        public Project()
        {
           
        }

        
        
    }
}
