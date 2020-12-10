using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    public class Note : ViewModel
    {
        #region Первичный ключ

        public int Id { get; set; }

        #endregion

        #region Цвет заметки

        private string _Color;
        private string _FontColor;
        private string _BtnColor;

        public string Color
        {
            get => ChangeAppTheme.NoteColor[ChangeAppTheme.iTheme];
            set => Set(ref _Color, value);
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

        #region Контент заметки

        private string _Content = "";
        public string Content
        {
            get
            {
                
                
                return _Content ;
            }
            set => Set(ref _Content, value);
        }

        #endregion

        #region Цель записи. Заголовок

        private string _Target;
        public string Target
        {
            get => _Target;
            set => Set(ref _Target, value);
        }

        #endregion
    }
}
