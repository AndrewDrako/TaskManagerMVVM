using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.ViewModels.Base;
using TaskManager.Infrastructure.Commands;
using System.Windows;
using TaskManager.Models;
using System.Security;
using System.Windows.Controls;
using TaskManager.Data.DataBase.Base;
using TaskManager.Views.Windows;
using TaskManager.Data.DataBase;

namespace TaskManager.ViewModels
{
    public class RegistrationWindowViewModel : ViewModel
    {
        public static MyDbContext myDbContext;

        #region Labels

        #region Enter Email

        private string _Label1;
        public string Label1
        {
            get => TranslateLanguage.RegLabelEnterEmail[TranslateLanguage.iLanguage];
            set => Set(ref _Label1, value);
        }

        #endregion

        #region Enter Nickname

        private string _Label2;
        public string Label2
        {
            get => TranslateLanguage.RegLabelEnterNickname[TranslateLanguage.iLanguage];
            set => Set(ref _Label2, value);
        }

        #endregion

        #region Enter password

        private string _Label3;
        public string Label3
        {
            get => TranslateLanguage.RegLabelEnterPassword[TranslateLanguage.iLanguage];
            set => Set(ref _Label3, value);
        }

        #endregion

        #region Enter password x1

        private string _Label4;
        public string Label4
        {
            get => TranslateLanguage.RegLabelEnterPassword1[TranslateLanguage.iLanguage];
            set => Set(ref _Label4, value);
        }

        #endregion

        #region Repeat password

        private string _Label5;
        public string Label5
        {
            get => TranslateLanguage.RegLabelEnterPassword2[TranslateLanguage.iLanguage];
            set => Set(ref _Label5, value);
        }

        #endregion





        #endregion

        #region TextBoxes

        #region Email

        public static string _UserEmail;
        public string UserEmail 
        {
            get => _UserEmail; 
            set => Set(ref _UserEmail, value); 
        }

        #endregion

        #region Username

        public static string _UserName;
        public string UserName
        {
            get => _UserName;
            set => Set(ref _UserName, value);
        }

        #endregion

        #region Passwords

        private SecureString _Password1;
        public SecureString Password1
        {
            get => _Password1;
            set => Set(ref _Password1, value);
        }

        private SecureString _Password2;
        public SecureString Password2
        {
            get => _Password2;
            set => Set(ref _Password2, value);
        }



        #endregion

        #region KeyInput

        private string _KeyInput;
        public string KeyInput
        {
            get => _KeyInput;
            set => Set(ref _KeyInput, value);
        }

        #endregion

        #endregion

        #region Команды

        public ICommand BtnClick { get; }
        private bool CanBtnClickExecute(object p) => true;
        private void OnBtnClickExecuted(object p)
        {
            var passwordBox = p as PasswordBox;
            var password = passwordBox.Password;
            Random rnd = new Random();
            KeyFromEmail = rnd.Next(100000, 999999);
            try
            {
                //RegModel.SendEmailAsync(UserEmail, KeyFromEmail);
            }
            catch
            {
                MessageBox.Show("Невозможно отправить на почту");
            }
            this.ChangeControlVisibility1 = Visibility.Collapsed;
            this.ChangeControlVisibility2 = Visibility.Visible;
        }


        public ICommand BtnClickAccept { get; }
        private bool CanBtnClickAcceptExecute(object p) => true;
        private void OnBtnClickAcceptExecuted(object p)
        {
            if (KeyInput == null)
            {
                MessageBox.Show("Поле не должно быть пусто!");
                return;
            }
            if (true)
            {
                MessageBox.Show("Це шикарно");
                //Application.Current.MainWindow.Show();
                Window window = new MainWindow();
                window.Show();
            }
            else
            {
                MessageBox.Show("Неверный код");
            }
        }

        #endregion

        #region Visibility 

        private Visibility _VisibilityFirst = Visibility.Visible;

        public Visibility ChangeControlVisibility1
        {
            get { return _VisibilityFirst; }
            set => Set(ref _VisibilityFirst, value);
        }

        private Visibility _VisibilitySecond = Visibility.Collapsed;

        public Visibility ChangeControlVisibility2
        {
            get { return _VisibilitySecond; }
            set => Set(ref _VisibilitySecond, value);
        }

        #endregion

        #region Key from email

        public static int KeyFromEmail;

        #endregion

        #region Конструктор

        public RegistrationWindowViewModel()
        {
            myDbContext = new MyDbContext();
            AsyncCommands.ConnectToDB(myDbContext);


            // Команды
            BtnClick = new LambdaCommand(OnBtnClickExecuted, CanBtnClickExecute);
            BtnClickAccept = new LambdaCommand(OnBtnClickAcceptExecuted, CanBtnClickAcceptExecute);
        }

        #endregion
    }
}
