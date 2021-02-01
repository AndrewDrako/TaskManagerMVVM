using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.Models;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class HelpViewModel : ViewModel
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
        public ICommand ButtonClickMyContactVK { get; }

        private bool CanButtonClickMyContactVKExecute(object p) => true;

        private void OnButtonClickMyContactVKExecuted(object p)
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

        public ICommand ButtonClickMyContactLI { get; }

        private bool CanButtonClickMyContactLIExecute(object p) => true;

        private void OnButtonClickMyContactLIExecuted(object p)
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

        public ICommand ButtonClickMyContactGH { get; }

        private bool CanButtonClickMyContactGHExecute(object p) => true;

        private void OnButtonClickMyContactGHExecuted(object p)
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

        public ICommand ButtonClickMyContactI { get; }

        private bool CanButtonClickMyContactIExecute(object p) => true;

        private void OnButtonClickMyContactIExecuted(object p)
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
            ButtonClickMyContactVK = new LambdaCommand(OnButtonClickMyContactVKExecuted, CanButtonClickMyContactVKExecute);
            ButtonClickMyContactLI = new LambdaCommand(OnButtonClickMyContactLIExecuted, CanButtonClickMyContactLIExecute);
            ButtonClickMyContactGH = new LambdaCommand(OnButtonClickMyContactGHExecuted, CanButtonClickMyContactGHExecute);
            ButtonClickMyContactI = new LambdaCommand(OnButtonClickMyContactIExecuted, CanButtonClickMyContactIExecute);
        }
    }
}
