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

        public ICommand BtnClickLogOut { get; }
        private bool CanBtnClickLogOutExecute(object p) => true;

        /// <summary>
        /// Log Out
        /// </summary>
        /// <param name="p"></param>
        private void OnBtnClickLogOutExecuted(object p)
        {
            AuthWindowModel.PrintKey("Cannot", "authreg_key.txt").GetAwaiter();
            Window authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0].Close();
        }

        public ICommand BtnClickCreate { get; }
        private bool CanBtnClickCreateExecute(object p) => true;

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="p"></param>
        private void OnBtnClickCreateExecuted(object p)
        {
            AuthWindowModel.PrintKey("Cannot", "authreg_key.txt").GetAwaiter();
            AuthWindowViewModel.canClickOk = true;
            Window regWindow = new RegistrationWindow();
            regWindow.Show();
            Application.Current.Windows[0].Close();
        }

        #endregion

        public AccountViewModel()
        {
            BtnClickLogOut = new LambdaCommand(OnBtnClickLogOutExecuted, CanBtnClickLogOutExecute);
            BtnClickCreate = new LambdaCommand(OnBtnClickCreateExecuted, CanBtnClickCreateExecute);
        }
    }
}
