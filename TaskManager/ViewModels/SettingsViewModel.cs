using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.Models;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class SettingsViewModel : ViewModel
    {
        #region Labels

        private string _LabelLanguage = TranslateLanguage.LabelSettings1[TranslateLanguage.iLanguage];

        public string LabelLanguage
        {
            get => _LabelLanguage;
            set => Set(ref _LabelLanguage, value);
        }

        private string _LabelAddColors = TranslateLanguage.LabelSettings2[TranslateLanguage.iLanguage];

        public string LabelAddColors
        {
            get => _LabelAddColors;
            set => Set(ref _LabelAddColors, value);
        }

        private string _LabelButtonApply = TranslateLanguage.LabelSettingsButton[TranslateLanguage.iLanguage];

        public string LabelButtonApply
        {
            get => _LabelButtonApply;
            set => Set(ref _LabelButtonApply, value);
        }

        #endregion

        #region Коллекция языков

        public static ObservableCollection<AppLanguage> Languages { get; set; }

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

        #region Commands

        public ICommand ButtonSaveSettingsClick { get; }

        private bool CanButtonSaveSettingsClickExecute(object p) => true;

        private void OnButtonSaveSettingsClickExecuted(object p)
        {
            string s = SelectedLanguage.Language;
            MainWindowModel.PrintLanguageKey(s);
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
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
            _SelectedLanguage = Languages[TranslateLanguage.iLanguage];

            // Comands
            ButtonSaveSettingsClick = new LambdaCommand(OnButtonSaveSettingsClickExecuted, CanButtonSaveSettingsClickExecute);
        }

        #endregion
    }
}
