﻿using System;
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
        public static User user;

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

        #region Button content

        private string _BtnContent1;

        public string BtnContent1
        {
            get => TranslateLanguage.RegBtnOk[TranslateLanguage.iLanguage];
            set => Set(ref _BtnContent1, value);
        }

        private string _BtnContent2;

        public string BtnContent2
        {
            get => TranslateLanguage.RegBtnAccept[TranslateLanguage.iLanguage];
            set => Set(ref _BtnContent2, value);
        }

        private string _BtnContent3;

        public string BtnContent3
        {
            get => TranslateLanguage.RegBtnLogIn[TranslateLanguage.iLanguage];
            set => Set(ref _BtnContent3, value);
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


            var users = AuthWindowViewModel.dbContext.Users.ToList();
            foreach(var ur in users)
            {
                if (UserName == ur.UserName)
                {
                    MessageBox.Show("Введенное имя занято попробуйте другое");
                    return;
                }
            }
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
            if (true)
            {
                var passwordBox = p as PasswordBox;
                var password = passwordBox.Password;
                MessageBox.Show("Це шикарно");
                user.Email = UserEmail;
                user.Password = password;
                user.UserName = UserName;
                var users = AuthWindowViewModel.dbContext.Users.ToList();
                var checker = false;
                foreach(var ur in users)
                {
                    if (user.UserName == ur.UserName && user.Password == ur.Password)
                    {
                        checker = true;
                        MessageBox.Show("У вас уже есть аккаунт");
                        break;
                    }
                }
                if (checker == false)
                {
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
            // Создание окна AuthWindow
            Window authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0].Close();
        }

        #endregion

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
            user = new User();

            #region Commands
            
            BtnClick = new LambdaCommand(OnBtnClickExecuted, CanBtnClickExecute);
            BtnClickAccept = new LambdaCommand(OnBtnClickAcceptExecuted, CanBtnClickAcceptExecute);
            BtnClickLogIn = new LambdaCommand(OnBtnClickLogInExecuted, CanBtnClickLogInExecute);
            
            #endregion
        }

        #endregion
    }
}
