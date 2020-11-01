using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    internal class AppLanguage : ViewModel
    {
        private string _Language;

        public string Language
        {
            get => _Language;
            set => Set(ref _Language, value);
        }

        public AppLanguage()
        {
            
        }
    }
}
