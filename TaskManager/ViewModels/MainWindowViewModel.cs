using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Data.DataBase.Base;
using TaskManager.Infrastructure.Commands;
using TaskManager.Models;
using TaskManager.ViewModels.Base;
using TaskManager.Views.UserControls;
using System.Threading;

namespace TaskManager.ViewModels
{
    public class MainWindowViewModel : ViewModel
    { 
        /// <summary>
        /// Data base context who takes info from AuthViewModel
        /// </summary>
        public static MyDbContext db = AuthWindowViewModel.dbContext;

        private UserControl home;
        public static UserControl tasks;
        private UserControl settings;
        private UserControl account;
        private UserControl help;

        #region Labels

        private string title = "Task Manager";

        public string Title
        {
            get => title;

            set => Set(ref title, value);
        }

        private string buttonContentHome;

        public string ButtonContentHome
        {
            get => TranslateLanguage.LabelHome[TranslateLanguage.iLanguage];

            set => Set(ref buttonContentHome, value);
        }

        private string buttonContentTasks;

        public string ButtonContentTasks
        {
            get => TranslateLanguage.LabelTask[TranslateLanguage.iLanguage];

            set => Set(ref buttonContentTasks, value);
        }

        private string buttonContentSettings;

        public string ButtonContentSettings
        {
            get => TranslateLanguage.LabelSettings[TranslateLanguage.iLanguage];

            set => Set(ref buttonContentSettings, value);
        }

        private string buttonContentAccount;

        public string ButtonContentAccount
        {
            get => TranslateLanguage.LabelAcc[TranslateLanguage.iLanguage];

            set => Set(ref buttonContentAccount, value);
        }

        private string buttonContentHelp;

        public string ButtonContentHelp
        {
            get => TranslateLanguage.LabelHelp[TranslateLanguage.iLanguage];

            set => Set(ref buttonContentHelp, value);
        }

        #endregion

        private double userControlOpacity;
        public double UserControlOpacity
        {
            get => userControlOpacity;
            set => Set(ref userControlOpacity, value);
        }

        #region Major/Current page/usercontrol

        private UserControl currentPage;

        public UserControl CurrentPage
        {
            get => currentPage;
            set
            {
                Set(ref currentPage, value);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Home button click
        /// </summary>
        public ICommand FirstButtonClick { get; }

        private bool CanFirstButtonClickExecute(object p) => true;

        private void OnFirstButtonClickExecuted(object p)
        {
            SlowOpacity(home);
            if (tasks != null)
            {
                TasksViewModel.SaveNote.Execute(p);
            }
        }

        /// <summary>
        /// Tasks button click
        /// </summary>
        public static ICommand SecondButtonClick { get; set; }

        private bool CanSecondButtonClickExecute(object p) => MainWindowModel.IsTasksNotEmpty;

        private void OnSecondButtonClickExecuted(object p)
        {
            SlowOpacity(tasks);
        }

        /// <summary>
        /// Settings button click
        /// </summary>
        public ICommand ThirdButtonClick { get; }

        private bool CanThirdButtonClickExecute(object p) => true;

        private void OnThirdButtonClickExecuted(object p)
        {
            SlowOpacity(settings);
            if (tasks != null)
            {
                TasksViewModel.SaveNote.Execute(p);
            }
        }

        /// <summary>
        /// Account button click
        /// </summary>
        public ICommand FourthButtonClick { get; }

        private bool CanFourthButtonClickExecute(object p) => MainWindowModel.IsConnectedToLocalServer;

        private void OnFourthButtonClickExecuted(object p)
        {
            SlowOpacity(account);
            if (tasks != null)
            {
                TasksViewModel.SaveNote.Execute(p);
            }
        }

        /// <summary>
        /// Help button click
        /// </summary>
        public ICommand FifthButtonClick { get; }

        private bool CanFifthButtonClickExecute(object p) => true;

        private void OnFifthButtonClickExecuted(object p)
        {
            SlowOpacity(help);
        }

        /// <summary>
        /// Close Application button
        /// </summary>
        public ICommand CloseApplication { get; }
        private bool CanCloseApplicationExecute(object p) => true;
        private void OnCloseApplicationExecuted(object p)
        {
            if (tasks != null)
            {
                TasksViewModel.SaveNote.Execute(p);
            }
        }

        #endregion

        /// <summary>
        /// The function is responsible for smooth switching between UserControls
        /// </summary>
        /// <param name="page"></param>
        private async void SlowOpacity(UserControl page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.1; i -= 0.1)
                {
                    UserControlOpacity = i;
                    Thread.Sleep(25);
                }
                CurrentPage = page;
                for (double i = 0.2; i < 1.1; i += 0.1)
                {
                    UserControlOpacity = i;
                    Thread.Sleep(25);
                }

            });
        } 

        public MainWindowViewModel()
        { 
            settings = new Settings();

            home = new Home();
            MainWindowModel.IsTasksNotEmpty = false;
            account = new Account();
            help = new Help();
            UserControlOpacity = 1;
            CurrentPage = home;

            CloseApplication = new LambdaCommand(OnCloseApplicationExecuted, CanCloseApplicationExecute);
            FirstButtonClick = new LambdaCommand(OnFirstButtonClickExecuted, CanFirstButtonClickExecute);
            SecondButtonClick = new LambdaCommand(OnSecondButtonClickExecuted, CanSecondButtonClickExecute);
            ThirdButtonClick = new LambdaCommand(OnThirdButtonClickExecuted, CanThirdButtonClickExecute);
            FourthButtonClick = new LambdaCommand(OnFourthButtonClickExecuted, CanFourthButtonClickExecute);
            FifthButtonClick = new LambdaCommand(OnFifthButtonClickExecuted, CanFifthButtonClickExecute);
        }
    }
}
