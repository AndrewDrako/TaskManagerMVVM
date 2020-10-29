using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    internal class Note : ViewModel
    {
        #region Цвет заметки

        private string _Color;

        public string Color
        {
            get => _Color;
            set => Set(ref _Color, value);
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
