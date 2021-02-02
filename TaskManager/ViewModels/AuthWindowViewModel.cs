using System.Configuration;
using System.Data.SqlClient;
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
        /// <summary>
        /// The major data base context
        /// </summary>
        public static MyDbContext dbContext;
        
        /// <summary>
        /// Local table which contains users
        /// </summary>
        public static User authUser;

        #region Labels

        private string labelUsername;

        /// <summary>
        /// Enter username
        /// </summary>
        public string LabelUsername
        {
            get => TranslateLanguage.AuthLabelEnterUsername[TranslateLanguage.iLanguage];
            set => Set(ref labelUsername, value);
        }

        private string labelPassword;

        /// <summary>
        /// Enter password
        /// </summary>
        public string LabelPassword
        {
            get => TranslateLanguage.RegLabelEnterPassword1[TranslateLanguage.iLanguage];
            set => Set(ref labelPassword, value);
        }

        private string btnContentOk;

        /// <summary>
        /// The name of the button that continues authorization
        /// </summary>
        public string BtnContentOk
        {
            get => TranslateLanguage.RegBtnOk[TranslateLanguage.iLanguage];
            set => Set(ref btnContentOk, value);
        }

        private string btnContentRegister;

        /// <summary>
        /// The name of the button that opens the registration window
        /// </summary>
        public string BtnContentRegister
        {
            get => TranslateLanguage.AuthLabelRegister[TranslateLanguage.iLanguage];
            set => Set(ref btnContentRegister, value);
        }

        #endregion

        /// <summary>
        /// The field that hosts the main theme of the application
        /// </summary>
        public static int selectedTheme;

        #region Outputs

        private string userName;

        /// <summary>
        /// Username/Nickname
        /// </summary>
        public string Username
        {
            get => userName;
            set => Set(ref userName, value);
        }

        #endregion

        public static bool canClickOk = false;

        /// <summary>
        /// Boolean variable that is responsible for the activity of the continue button
        /// </summary>
        public bool CanClickOk
        {
            get => canClickOk;
            set => Set(ref canClickOk, value);
        }

        #region Commands
 
        public ICommand BtnClickOk { get; }
        private bool CanBtnClickOkExecute(object p) => CanClickOk;

        /// <summary>
        /// Continue button click
        /// </summary>
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
                authUser.UserName = user.UserName;
                Window mainWindow = new MainWindow();
                mainWindow.Show();
                AuthWindowModel.PrintKey("Can", "authreg_key.txt").GetAwaiter();
                AuthWindowModel.PrintKey(user.UserName, "last_user_name.txt").GetAwaiter();
                Application.Current.Windows[0].Close();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль");
            }
        }

        public ICommand BtnClickReg { get; }
        private bool CanBtnClickRegExecute(object p) => true;

        /// <summary>
        /// Registration button click
        /// </summary>
        private void OnBtnClickRegExecuted(object p)
        {
            Window regWindow = new RegistrationWindow();
            regWindow.Show();
            Application.Current.Windows[0].Close();
        }

        #endregion

        public AuthWindowViewModel()
        {
            AuthWindowModel.Key = AuthWindowModel.ReadKey();  // With auth and registration or not

            TranslateLanguage.iLanguage = MainWindowModel.ReadLanguageKey();  // Translation dictionary

            selectedTheme = MainWindowModel.ReadThemeKey();  // App Theme

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("Невозможно найти сервер!\nПриложение продолжит работу но не сможет хранить данные.");
                MainWindowModel.IsConnectedToLocalServer = false;
            }
            dbContext = new MyDbContext();
            authUser = new User();
            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                if (AuthWindowModel.Key == 0)
                {
                    AsyncCommands.ConnectToDB(dbContext);
                }
                else
                {
                    try
                    {
                        if (AuthWindowModel.Key == 1)
                        {
                            DataBaseCommands.LoadDB(dbContext);
                            User user = Model.FindUser(dbContext, AuthWindowModel.ReadLastUserName());  // from txt insert username
                            if (user != null)
                            {
                                authUser.Id = user.Id;
                                authUser.Password = user.Password;
                                authUser.Email = user.Email;
                                authUser.UserName = user.UserName;
                                Window mainWindow = new MainWindow();
                                mainWindow.Show();
                                Application.Current.Windows[0].Close();
                            }
                            else
                            {
                                AuthWindowModel.PrintKey("Cannot", "authreg_key.txt").GetAwaiter();
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
            else
            {
                authUser.UserName = "Guest";
                Window mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();
            }

            BtnClickOk = new LambdaCommand(OnBtnClickOkExecuted, CanBtnClickOkExecute);
            BtnClickReg = new LambdaCommand(OnBtnClickRegExecuted, CanBtnClickRegExecute);
        }
    }
}
