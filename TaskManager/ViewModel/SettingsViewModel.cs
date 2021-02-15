using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;
using TaskManager.Models;


namespace TaskManager.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Labels

        private string labelLanguage = TranslateLanguage.LabelSettings1[TranslateLanguage.iLanguage];

        /// <summary>
        /// Label Change language
        /// </summary>
        public string LabelLanguage
        {
            get => labelLanguage;
            set => Set(ref labelLanguage, value);
        }

        private string labelAddColors = TranslateLanguage.LabelSettings2[TranslateLanguage.iLanguage];

        /// <summary>
        /// Label Do you want to add colors
        /// </summary>
        public string LabelAddColors
        {
            get => labelAddColors;
            set => Set(ref labelAddColors, value);
        }

        private string labelButtonApply = TranslateLanguage.LabelSettingsButton[TranslateLanguage.iLanguage];

        /// <summary>
        /// Label content button accept
        /// </summary>
        public string LabelButtonApply
        {
            get => labelButtonApply;
            set => Set(ref labelButtonApply, value);
        }

        #endregion

        public static ObservableCollection<AppLanguage> Languages { get; set; }
        public static ObservableCollection<AppTheme> Themes { get; set; }

        private AppLanguage selectedLanguage;

        public AppLanguage SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                if (selectedLanguage == value)
                    return;
                selectedLanguage = value;
                isEdited = true;
                RaisePropertyChanged("SelectedLanguage");
                ButtonSaveSettingsClick.RaiseCanExecuteChanged();
            }
        }

        private AppTheme selectedTheme;
        public AppTheme SelectedTheme
        {
            get => selectedTheme;
            set
            {
                if (selectedTheme == value)
                    return;
                selectedTheme = value;
                isEdited = true;
                RaisePropertyChanged("SelectedTheme");
                ButtonSaveSettingsClick.RaiseCanExecuteChanged();
            }
        }

        #region Commands

        private bool isEdited = false;
        public bool IsEdited 
        { 
            get => isEdited;
            set
            {
                if (isEdited == value)
                    return;
                isEdited = value;
                RaisePropertyChanged("IsEdited");
            } 
        }
        public RelayCommand ButtonSaveSettingsClick { get; }

        private bool CanButtonSaveSettingsClickExecute() => IsEdited;

        private void OnButtonSaveSettingsClickExecuted()
        {
            //Language
            string s = SelectedLanguage.Language;
            MainWindowModel.PrintLanguageKey(s).GetAwaiter();

            //Theme
            string ss = SelectedTheme.Name;
            MainWindowModel.PrintThemeKey(ss).GetAwaiter();

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        #endregion

        public SettingsViewModel()
        {
            Languages = new ObservableCollection<AppLanguage>
            {
                new AppLanguage {Language = "English"},
                new AppLanguage {Language = "Russian"}
            };
            selectedLanguage = Languages[TranslateLanguage.iLanguage];  // Install Language

            Themes = new ObservableCollection<AppTheme>
            {
                new AppTheme{ Name = "Custom"},
                new AppTheme{ Name = "Light"},
                new AppTheme{ Name = "Dark" }
            };
            selectedTheme = Themes[AuthViewModel.selectedTheme];  // Install Theme

            ButtonSaveSettingsClick = new RelayCommand(OnButtonSaveSettingsClickExecuted, CanButtonSaveSettingsClickExecute);

        }
    }
}
