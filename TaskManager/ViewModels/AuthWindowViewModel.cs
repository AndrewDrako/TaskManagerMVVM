using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Data.DataBase;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Infrastructure.Commands;
using TaskManager.Models;
using TaskManager.ViewModels.Base;
using TaskManager.Views.Windows;

namespace TaskManager.ViewModels
{
    public class AuthWindowViewModel : ViewModel
    {
        #region DbContext and User

        public static MyDbContext dbContext;
        public static User authUser;

        #endregion

        #region Labels

        #region Enter username

        private string _Label1;
        public string Label1
        {
            get => TranslateLanguage.AuthLabelEnterUsername[TranslateLanguage.iLanguage];
            set => Set(ref _Label1, value);
        }

        #endregion

        #region Enter password

        private string _Label2;
        public string Label2
        {
            get => TranslateLanguage.RegLabelEnterPassword1[TranslateLanguage.iLanguage];
            set => Set(ref _Label2, value);
        }

        #endregion

        #region Button contents

        private string _BtnContentOk;
        public string BtnContentOk
        {
            get => TranslateLanguage.RegBtnOk[TranslateLanguage.iLanguage];
            set => Set(ref _BtnContentOk, value);
        }

        private string _BtnContentRegister;
        public string BtnContentRegister
        {
            get => TranslateLanguage.AuthLabelRegister[TranslateLanguage.iLanguage];
            set => Set(ref _BtnContentRegister, value);
        }

        #endregion

        #endregion

        #region Outputs

        #region Username/Nickname

        private string _Username;

        public string Username
        {
            get => _Username;
            set => Set(ref _Username, value);
        }

        #endregion

        #endregion

        #region Checker unlock button ok

        public static bool _CanClickOk = false;
        public bool CanClickOk
        {
            get => _CanClickOk;
            set => Set(ref _CanClickOk, value);
        }

        #endregion

        #region Commands

        // Команда кнопки ОК

        public  ICommand BtnClickOk { get; }
        private bool CanBtnClickOkExecute(object p) => CanClickOk;
        private void OnBtnClickOkExecuted(object p)
        {
            var passwordBox = p as PasswordBox; 
            var password = passwordBox.Password;

            User user = Model.FindUser(dbContext, password, Username);
            if(user != null)
            {
                authUser = new User();
                authUser.Id = user.Id;
                authUser.Email = user.Email;
                Window mainWindow = new MainWindow();
                mainWindow.Show();
                AuthWindowModel.PrintKey("Can", "authreg_key.txt");
                AuthWindowModel.PrintKey(user.UserName, "last_user_name.txt");
                Application.Current.Windows[0].Close();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль");
            }
        }

        // Команда кнопки регистрация

        public ICommand BtnClickReg { get; }
        private bool CanBtnClickRegExecute(object p) => true;
        private void OnBtnClickRegExecuted(object p)
        {
            Window regWondow = new RegistrationWindow();
            regWondow.Show();
            Application.Current.Windows[0].Close();
        }

        #endregion

        #region Конструктор

        public AuthWindowViewModel()
        {
            #region With auth and registration or not

            AuthWindowModel.Key = AuthWindowModel.ReadKey();

            #endregion

            #region Translation dictionary

            TranslateLanguage.iLanguage = MainWindowModel.ReadLanguageKey();

            #endregion

            #region Связь с БД

            dbContext = new MyDbContext();
            authUser = new User();
            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                if (AuthWindowModel.Key == 0)
                {
                    AsyncCommands.ConnectToDB(dbContext);
                    //DataBaseCommands.LoadDB(dbContext);
                }
                else
                {
                    try
                    {
                        if (AuthWindowModel.Key == 1)
                        {
                            DataBaseCommands.LoadDB(dbContext);
                            User user = Model.FindUser(dbContext, AuthWindowModel.ReadLastUserName());  // из txt вставляем имя пользователя 
                            if (user != null)
                            {
                                authUser.Id = user.Id;
                                authUser.Password = user.Password;
                                authUser.Email = user.Email;
                                Window mainWindow = new MainWindow();
                                mainWindow.Show();
                                Application.Current.Windows[0].Close();
                            }
                            else
                            {
                                AuthWindowModel.PrintKey("Cannot", "authreg_key.txt");
                                MessageBox.Show("Что-то пошло не так, перезайдите в приложение");
                                Application.Current.Shutdown();
                            }

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка возникла со связью с БД\n(AuthWindowViewModel/конструктор/Связь с БД)");
                        Application.Current.Shutdown();
                    }
                }
            }
            //else
            //{
            //    //Window mainWindow = new MainWindow();
            //    //mainWindow.Show();
            //    //Application.Current.Windows[0].Close();

            //}
            #endregion

            #region Commands

            BtnClickOk = new LambdaCommand(OnBtnClickOkExecuted, CanBtnClickOkExecute);
            BtnClickReg = new LambdaCommand(OnBtnClickRegExecuted, CanBtnClickRegExecute);

            #endregion
        }

        #endregion
    }
}
