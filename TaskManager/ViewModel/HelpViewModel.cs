using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.Windows;
using TaskManager.Models;

namespace TaskManager.ViewModel
{
    public class HelpViewModel : ViewModelBase
    {
        #region Labels

        private string labelFirstTitle;
        private string labelSecondTitle;

        /// <summary>
        /// How to contact me
        /// </summary>
        public string LabelFirstTitle
        {
            get => TranslateLanguage.LabelHelpFirstTitle[TranslateLanguage.iLanguage];
            set => Set(ref labelFirstTitle, value);
        }

        /// <summary>
        /// What is this app about
        /// </summary>
        public string LabelSecondTitle
        {
            get => TranslateLanguage.LabelHelpSecondTitle[TranslateLanguage.iLanguage];
            set => Set(ref labelSecondTitle, value);
        }

        #endregion

        #region Commands

        // Vk
        public RelayCommand ButtonClickMyContactVK { get; }

        private bool CanButtonClickMyContactVKExecute() => true;

        private void OnButtonClickMyContactVKExecuted()
        {
            try
            {
                Process.Start("https://vk.com/andrew_drako");
            }
            catch
            {
                MessageBox.Show("Ошибка открытия браузера, попробуйте снова");
            }
        }

        // LinkedIn

        public RelayCommand ButtonClickMyContactLI { get; }

        private bool CanButtonClickMyContactLIExecute() => true;

        private void OnButtonClickMyContactLIExecuted()
        {
            try
            {
                Process.Start("https://www.linkedin.com/in/andrew-drako-30b8ab193/");
            }
            catch
            {
                MessageBox.Show("Ошибка открытия браузера, попробуйте снова");
            }
        }

        // GitHub

        public RelayCommand ButtonClickMyContactGH { get; }

        private bool CanButtonClickMyContactGHExecute() => true;

        private void OnButtonClickMyContactGHExecuted()
        {
            try
            {
                Process.Start("https://github.com/AndrewDrako");
            }
            catch
            {
                MessageBox.Show("Ошибка открытия браузера, попробуйте снова");
            }
        }

        // Instagram

        public RelayCommand ButtonClickMyContactI { get; }

        private bool CanButtonClickMyContactIExecute() => true;

        private void OnButtonClickMyContactIExecuted()
        {
            try
            {
                Process.Start("https://www.instagram.com/andrew_drako/");
            }
            catch
            {
                MessageBox.Show("Ошибка открытия браузера, попробуйте снова");
            }
        }

        #endregion

        public HelpViewModel()
        {
            ButtonClickMyContactVK = new RelayCommand(OnButtonClickMyContactVKExecuted, CanButtonClickMyContactVKExecute);
            ButtonClickMyContactLI = new RelayCommand(OnButtonClickMyContactLIExecuted, CanButtonClickMyContactLIExecute);
            ButtonClickMyContactGH = new RelayCommand(OnButtonClickMyContactGHExecuted, CanButtonClickMyContactGHExecute);
            ButtonClickMyContactI = new RelayCommand(OnButtonClickMyContactIExecuted, CanButtonClickMyContactIExecute);
        }
    }
}
