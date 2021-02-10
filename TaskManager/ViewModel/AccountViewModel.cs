using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Models;
using TaskManager.Views.Windows;

namespace TaskManager.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        #region Labels

        private string buttonLabelLogOut = TranslateLanguage.LabelAccBtnLogOut[TranslateLanguage.iLanguage];

        /// <summary>
        /// Label Log Iut
        /// </summary>
        public string ButtonLabelLogOut
        {
            get => buttonLabelLogOut;
            set => Set(ref buttonLabelLogOut, value);
        }


        private string buttonLabelCreate = TranslateLanguage.LabelAccBtnCreate[TranslateLanguage.iLanguage];

        /// <summary>
        /// Label Create a new
        /// </summary>
        public string ButtonLabelCreate
        {
            get => buttonLabelCreate;
            set => Set(ref buttonLabelCreate, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Log Out
        /// </summary>
        public ICommand BtnClickLogOut { get; }
        private bool CanBtnClickLogOutExecute() => true;
        private void OnBtnClickLogOutExecuted()
        {
            AuthWindowModel.PrintKey("Cannot", "authreg_key.txt").GetAwaiter();
            Window authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0].Close();
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        public ICommand BtnClickCreate { get; }
        private bool CanBtnClickCreateExecute() => true;

        private void OnBtnClickCreateExecuted()
        {
            AuthWindowModel.PrintKey("Cannot", "authreg_key.txt").GetAwaiter();
            AuthViewModel.canClickOk = true;
            Window regWindow = new RegistrationWindow();
            regWindow.Show();
            Application.Current.Windows[0].Close();
        }

        #endregion

        public AccountViewModel()
        {
            BtnClickLogOut = new RelayCommand(OnBtnClickLogOutExecuted, CanBtnClickLogOutExecute);
            BtnClickCreate = new RelayCommand(OnBtnClickCreateExecuted, CanBtnClickCreateExecute);
        }
    }
}
