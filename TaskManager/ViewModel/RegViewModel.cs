using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Models;
using TaskManager.Views.Windows;

namespace TaskManager.ViewModel
{
    public class RegViewModel : ViewModelBase
    {
        #region Labels

        private string labelEmail;

        /// <summary>
        /// Enter Email
        /// </summary>
        public string LabelEmail
        {
            get => TranslateLanguage.RegLabelEnterEmail[TranslateLanguage.iLanguage];
            set => Set(ref labelEmail, value);
        }

        private string labelNickname;

        /// <summary>
        /// Enter Nickname
        /// </summary>
        public string LabelNickname
        {
            get => TranslateLanguage.RegLabelEnterNickname[TranslateLanguage.iLanguage];
            set => Set(ref labelNickname, value);
        }

        private string labelPassword;

        /// <summary>
        /// Enter password
        /// </summary>
        public string LabelPassword
        {
            get => TranslateLanguage.RegLabelEnterPassword[TranslateLanguage.iLanguage];
            set => Set(ref labelPassword, value);
        }

        private string labelKey;

        /// <summary>
        /// Enter key
        /// </summary>
        public string LabelKey
        {
            get => TranslateLanguage.RegLabelKey[TranslateLanguage.iLanguage];
            set => Set(ref labelKey, value);
        }

        private string labelCheckEmail;

        /// <summary>
        /// Check email
        /// </summary>
        public string LabelCheckEmail
        {
            get => TranslateLanguage.RegLabelKey[TranslateLanguage.iLanguage];
            set => Set(ref labelCheckEmail, value);
        }

        private string btnContentReg;

        /// <summary>
        /// Registration button content
        /// </summary>
        public string BtnContentReg
        {
            get => TranslateLanguage.RegBtnOk[TranslateLanguage.iLanguage];
            set => Set(ref btnContentReg, value);
        }

        private string btnContentAccept;

        /// <summary>
        /// Accept key from Email button content
        /// </summary>
        public string BtnContentAccept
        {
            get => TranslateLanguage.RegBtnAccept[TranslateLanguage.iLanguage];
            set => Set(ref btnContentAccept, value);
        }

        private string btnContentAuth;

        /// <summary>
        /// Log In button content
        /// </summary>
        public string BtnContentAuth
        {
            get => TranslateLanguage.RegBtnLogIn[TranslateLanguage.iLanguage];
            set => Set(ref btnContentAuth, value);
        }

        private string btnContentBack;

        /// <summary>
        /// Back button content
        /// </summary>
        public string BtnContentBack
        {
            get => TranslateLanguage.RegBtnBack[TranslateLanguage.iLanguage];
            set => Set(ref btnContentBack, value);
        }

        #endregion

        #region OutPuts

        public static string userEmail;

        /// <summary>
        /// Email
        /// </summary>
        public string UserEmail
        {
            get => userEmail;
            set => Set(ref userEmail, value);
        }

        public static string userName;

        /// <summary>
        /// Username
        /// </summary>
        public string UserName
        {
            get => userName;
            set => Set(ref userName, value);
        }

        private string keyInput;

        /// <summary>
        /// KeyInput
        /// </summary>
        public string KeyInput
        {
            get => keyInput;
            set => Set(ref keyInput, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Click after enter email, password and username
        /// </summary>
        public RelayCommand BtnClick { get; }
        private bool CanBtnClickExecute() => true;

        private void OnBtnClickExecuted()
        {
            if (UserEmail == null || UserEmail == null)
            {
                MessageBox.Show("Заполните поля");
                return;
            }

            User user = Model.FindUser(AuthViewModel.dbContext, UserName);
            if (user != null)
            {
                MessageBox.Show("Введенное имя занято попробуйте другое");
                return;
            }
            this.ChangeControlVisibilityFirst = Visibility.Collapsed;
            this.ChangeControlVisibilitySecond = Visibility.Visible;
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

        /// <summary>
        /// Click after enter key from email
        /// </summary>
        public RelayCommand<object> BtnClickAccept { get; }
        private bool CanBtnClickAcceptExecute() => AuthViewModel.canClickOk;
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

                User user = Model.FindUser(AuthViewModel.dbContext, password, UserName);
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
                    AuthViewModel.dbContext.Users.Attach(user);
                    AuthViewModel.dbContext.Users.Add(user);
                    AuthViewModel.dbContext.SaveChanges();
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

        /// <summary>
        /// Log In click
        /// </summary>
        public RelayCommand BtnClickLogIn { get; }
        private bool CanBtnClickLogInExecute() => true;
        private void OnBtnClickLogInExecuted()
        {
            Window authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0].Close();
        }

        /// <summary>
        /// Back click
        /// </summary>
        public RelayCommand BtnClickBack { get; }
        private bool CanBtnClickBackExecute() => true;
        private void OnBtnClickBackExecuted()
        {
            this.ChangeControlVisibilityFirst = Visibility.Visible;
            this.ChangeControlVisibilitySecond = Visibility.Collapsed;
        }

        #endregion

        private Visibility visibilityFirst = Visibility.Visible;

        /// <summary>
        /// First section visibility
        /// </summary>
        public Visibility ChangeControlVisibilityFirst
        {
            get { return visibilityFirst; }
            set => Set(ref visibilityFirst, value);
        }

        private Visibility visibilitySecond = Visibility.Collapsed;

        /// <summary>
        /// Second section visibility
        /// </summary>
        public Visibility ChangeControlVisibilitySecond
        {
            get { return visibilitySecond; }
            set => Set(ref visibilitySecond, value);
        }

        /// <summary>
        /// Random Key from email
        /// </summary>
        public static int KeyFromEmail;

        public RegViewModel()
        {
            UserEmail = null;
            UserName = null;

            BtnClick = new RelayCommand(OnBtnClickExecuted, CanBtnClickExecute);
            BtnClickAccept = new RelayCommand<object>((obj) => OnBtnClickAcceptExecuted(obj), CanBtnClickAcceptExecute());
            BtnClickLogIn = new RelayCommand(OnBtnClickLogInExecuted, CanBtnClickLogInExecute);
            BtnClickBack = new RelayCommand(OnBtnClickBackExecuted, CanBtnClickBackExecute);
        }
    }
}
