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
using TaskManager.Data.DataBase.Tables;

namespace TaskManager.ViewModels
{
    public class RegistrationWindowViewModel : ViewModel
    {
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

        #region Enter key
        
        private string _Label6;

        public string Label6
        {
            get => TranslateLanguage.RegLabelKey[TranslateLanguage.iLanguage];
            set => Set(ref _Label6, value);
        }

        #endregion

        #region Check email

        private string _Label8;
        public string Label8
        {
            get => TranslateLanguage.RegLabelKey[TranslateLanguage.iLanguage];
            set => Set(ref _Label8, value);
        }

        #endregion

        #region Buttons content

        private string _BtnContent1;

        /// <summary>
        /// Registration button content
        /// </summary>
        public string BtnContent1
        {
            get => TranslateLanguage.RegBtnOk[TranslateLanguage.iLanguage];
            set => Set(ref _BtnContent1, value);
        }

        private string _BtnContent2;

        /// <summary>
        /// Accept key from Email button content
        /// </summary>
        public string BtnContent2
        {
            get => TranslateLanguage.RegBtnAccept[TranslateLanguage.iLanguage];
            set => Set(ref _BtnContent2, value);
        }

        private string _BtnContent3;

        /// <summary>
        /// Log In button content
        /// </summary>
        public string BtnContent3
        {
            get => TranslateLanguage.RegBtnLogIn[TranslateLanguage.iLanguage];
            set => Set(ref _BtnContent3, value);
        }

        #endregion

        #endregion

        #region OutPuts

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

        #region KeyInput

        private string _KeyInput;
        public string KeyInput
        {
            get => _KeyInput;
            set => Set(ref _KeyInput, value);
        }

        #endregion

        #endregion

        #region Commands

        #region Click after enter email, password and username

        public ICommand BtnClick { get; }
        private bool CanBtnClickExecute(object p) => true;
        private void OnBtnClickExecuted(object p)
        {
            if (UserEmail == null || UserEmail == null)
            {
                MessageBox.Show("Заполните поля");
                return;
            }

            User user = Model.FindUser(AuthWindowViewModel.dbContext, UserName);
            if (user != null)
            {
                MessageBox.Show("Введенное имя занято попробуйте другое");
                return;
            }
            this.ChangeControlVisibility1 = Visibility.Collapsed;
            this.ChangeControlVisibility2 = Visibility.Visible;
            Random rnd = new Random();
            KeyFromEmail = rnd.Next(100000, 999999);
            try
            {
                RegModel.SendEmailAsync(UserEmail, KeyFromEmail).GetAwaiter();
            }
            catch
            {
                MessageBox.Show("Невозможно отправить на почту");
            }
        }

        #endregion

        #region Click after enter key from email

        public ICommand BtnClickAccept { get; }
        private bool CanBtnClickAcceptExecute(object p) => AuthWindowViewModel._CanClickOk;
        private void OnBtnClickAcceptExecuted(object p)
        {
            if (KeyInput == null)
            {
                MessageBox.Show("Поле не должно быть пусто!");
                return;
            }
            if (KeyInput == KeyFromEmail.ToString())
            {
                var passwordBox = p as PasswordBox;
                var password = passwordBox.Password;

                User user = Model.FindUser(AuthWindowViewModel.dbContext, password, UserName);
                if (user != null)
                {
                    MessageBox.Show("У вас уже есть аккаунт");
                }
                else
                {
                    user = new User
                    {
                        Email = UserEmail,
                        Password = password,
                        UserName = UserName
                    };
                    AuthWindowViewModel.dbContext.Users.Attach(user);
                    AuthWindowViewModel.dbContext.Users.Add(user);
                    AuthWindowViewModel.dbContext.SaveChanges();
                    MessageBox.Show("Отлично, у вас есть аккаунт!\nТеперь выполните вход");
                    Window authWindow = new AuthWindow();
                    authWindow.Show();
                    Application.Current.Windows[0].Close();
                }
            }
            else
            {
                MessageBox.Show("Неверный код");
            }
        }

        #endregion

        #region Log In click

        public ICommand BtnClickLogIn { get; }
        private bool CanBtnClickLogInExecute(object p) => true;
        private void OnBtnClickLogInExecuted(object p)
        {
            Window authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0].Close();
        }

        #endregion

        #endregion

        #region Visibility 

        private Visibility _VisibilityFirst = Visibility.Visible;

        /// <summary>
        /// First section visibility
        /// </summary>
        public Visibility ChangeControlVisibility1
        {
            get { return _VisibilityFirst; }
            set => Set(ref _VisibilityFirst, value);
        }

        private Visibility _VisibilitySecond = Visibility.Collapsed;

        /// <summary>
        /// Second section visibility
        /// </summary>
        public Visibility ChangeControlVisibility2
        {
            get { return _VisibilitySecond; }
            set => Set(ref _VisibilitySecond, value);
        }

        #endregion

        #region Random Key from email

        public static int KeyFromEmail;

        #endregion

        #region Class Designer

        public RegistrationWindowViewModel()
        {
            #region Commands
            
            BtnClick = new LambdaCommand(OnBtnClickExecuted, CanBtnClickExecute);
            BtnClickAccept = new LambdaCommand(OnBtnClickAcceptExecuted, CanBtnClickAcceptExecute);
            BtnClickLogIn = new LambdaCommand(OnBtnClickLogInExecuted, CanBtnClickLogInExecute);
            
            #endregion
        }

        #endregion
    }
}
