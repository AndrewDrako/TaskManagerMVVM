using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class SettingsViewModel : ViewModel
    {
        #region Коллекция языков

        public ObservableCollection<AppLanguage> Languages { get; set; }

        #endregion

        #region Languages

        private AppLanguage _SelectedLanguage;

        public AppLanguage SelectedLanguage
        {
            get { return _SelectedLanguage; }
            set
            {
                _SelectedLanguage = value;
                OnPropertyChanged("SelectedLanguage");
            }
        }

        #endregion

       

        #region Конструктор

        public SettingsViewModel()
        {
            Languages = new ObservableCollection<AppLanguage>
            {
                new AppLanguage {Language = "English"},
                new AppLanguage {Language = "Russian"},
                new AppLanguage {Language = "Espanol"}
            };
            _SelectedLanguage = Languages[0];
        }

        #endregion
    }
}
