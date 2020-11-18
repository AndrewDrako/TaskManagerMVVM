using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.Models;
using TaskManager.ViewModels.Base;
using TaskManager.Views.Windows;

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

        #region Commands

        // Log Out

        public ICommand BtnClickLogOut { get; }
        private bool CanBtnClickLogOutExecute(object p) => true;
        private void OnBtnClickLogOutExecuted(object p)
        {
            Window authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0].Close();
        }

        // Create a new

        public ICommand BtnClickCreate { get; }
        private bool CanBtnClickCreateExecute(object p) => true;
        private void OnBtnClickCreateExecuted(object p)
        {
            Window regWindow = new RegistrationWindow();
            regWindow.Show();
            Application.Current.Windows[0].Close();
        }

        #endregion

        public AccountViewModel()
        {
            #region Commands

            BtnClickLogOut = new LambdaCommand(OnBtnClickLogOutExecuted, CanBtnClickLogOutExecute);
            BtnClickCreate = new LambdaCommand(OnBtnClickCreateExecuted, CanBtnClickCreateExecute);

            #endregion
        }
    }
}
