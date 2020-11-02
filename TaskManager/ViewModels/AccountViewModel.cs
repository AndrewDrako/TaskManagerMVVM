using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class AccountViewModel : ViewModel
    {
        #region Labels

        private string _ButtonLabel1 = TranslateLanguage.LabelAccBtn1[TranslateLanguage.iLanguage];

        public string ButtonLabel1 
        { 
            get => _ButtonLabel1; 
            set => Set(ref _ButtonLabel1, value); 
        }

        private string _ButtonLabel2 = TranslateLanguage.LabelAccBtn2[TranslateLanguage.iLanguage];

        public string ButtonLabel2
        {
            get => _ButtonLabel2;
            set => Set(ref _ButtonLabel2, value);
        }

        #endregion
    }
}
