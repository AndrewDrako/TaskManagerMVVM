using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Data.DataBase.Base;
using TaskManager.Models;
using TaskManager.Views.UserControls;

namespace TaskManager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Data base context who takes info from AuthViewModel
        /// </summary>
        public static MyDbContext db = AuthViewModel.dbContext;

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

        private bool CanFirstButtonClickExecute() => true;

        private void OnFirstButtonClickExecuted(object obj)
        {
            CurrentPage = home;
            if (tasks != null)
            {
                TasksViewModel.SaveNote.Execute(obj);
            }
        }

        /// <summary>
        /// Tasks button click
        /// </summary>
        public static ICommand SecondButtonClick { get; set; }

        private bool CanSecondButtonClickExecute() => MainWindowModel.IsTasksNotEmpty;

        private void OnSecondButtonClickExecuted()
        {
            CurrentPage = tasks;
        }

        /// <summary>
        /// Settings button click
        /// </summary>
        public ICommand ThirdButtonClick { get; }

        private bool CanThirdButtonClickExecute() => true;

        private void OnThirdButtonClickExecuted(object obj)
        {
            CurrentPage = settings;
            if (tasks != null)
            {
                TasksViewModel.SaveNote.Execute(obj);
            }
        }

        /// <summary>
        /// Account button click
        /// </summary>
        public ICommand FourthButtonClick { get; }

        private bool CanFourthButtonClickExecute() => MainWindowModel.IsConnectedToLocalServer;

        private void OnFourthButtonClickExecuted(object obj)
        {
            CurrentPage = account;
            if (tasks != null)
            {
                TasksViewModel.SaveNote.Execute(obj);
            }
        }

        /// <summary>
        /// Help button click
        /// </summary>
        public ICommand FifthButtonClick { get; }

        private bool CanFifthButtonClickExecute() => true;

        private void OnFifthButtonClickExecuted()
        {
            CurrentPage = help;
        }

        /// <summary>
        /// Close Application button
        /// </summary>
        public ICommand CloseApplication { get; }
        private bool CanCloseApplicationExecute() => true;
        private void OnCloseApplicationExecuted(object obj)
        {
            if (tasks != null)
            {
                TasksViewModel.SaveNote.Execute(obj);
            }
        }

        #endregion



        
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            settings = new Settings();

            home = new Home();
            MainWindowModel.IsTasksNotEmpty = false;
            account = new Account();
            help = new Help();
            UserControlOpacity = 1;
            CurrentPage = home;

            CloseApplication = new RelayCommand<object>((obj) => OnCloseApplicationExecuted(obj), CanCloseApplicationExecute());
            FirstButtonClick = new RelayCommand<object>((obj) => OnFirstButtonClickExecuted(obj), CanFirstButtonClickExecute());
            SecondButtonClick = new RelayCommand(OnSecondButtonClickExecuted, CanSecondButtonClickExecute);
            ThirdButtonClick = new RelayCommand<object>((obj) => OnThirdButtonClickExecuted(obj), CanThirdButtonClickExecute());
            FourthButtonClick = new RelayCommand<object>((obj) => OnFourthButtonClickExecuted(obj), CanFourthButtonClickExecute());
            FifthButtonClick = new RelayCommand(OnFifthButtonClickExecuted, CanFifthButtonClickExecute);
        }
    }
}