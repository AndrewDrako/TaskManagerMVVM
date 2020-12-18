using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Data.DataBase.Base;
using TaskManager.Infrastructure.Commands;
using TaskManager.Models;
using TaskManager.ViewModels.Base;
using TaskManager.Views.UserControls;
using TaskManager.Data.DataBase;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Views.Windows;
using System.Threading;

namespace TaskManager.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region DBContext

        /// <summary>
        /// Data base context who takes info from AuthViewModel
        /// </summary>
        public static MyDbContext db = AuthWindowViewModel.dbContext;

        #endregion

        #region UserControls

        private UserControl _Home;
        public static UserControl _Tasks;
        private UserControl _Settings;
        private UserControl _Account;
        private UserControl _Help;

        #endregion

        #region Labels

        #region Title

        private string _Title = "Task Manager";

        public string Title
        {
            get => _Title;

            set => Set(ref _Title, value);
        }

        #endregion

        #region Button content Home

        private string _ButtonContent1;

        public string ButtonContent1
        {
            get => TranslateLanguage.LabelHome[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent1, value);
        }

        #endregion

        #region Button content Tasks

        private string _ButtonContent2;

        public string ButtonContent2
        {
            get => TranslateLanguage.LabelTask[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent2, value);
        }

        #endregion

        #region Button content Settings

        private string _ButtonContent3;

        public string ButtonContent3
        {
            get => TranslateLanguage.LabelSettings[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent3, value);
        }

        #endregion

        #region Button content Account

        private string _ButtonContent4;

        public string ButtonContent4
        {
            get => TranslateLanguage.LabelAcc[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent4, value);
        }

        #endregion

        #region Button content Help

        private string _ButtonContent5;

        public string ButtonContent5
        {
            get => TranslateLanguage.LabelHelp[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent5, value);
        }

        #endregion

        #endregion

        #region Opacity

        private double _UserControlOpacity;
        public double UserControlOpacity
        {
            get => _UserControlOpacity;
            set => Set(ref _UserControlOpacity, value);
        }

        #endregion

        #region Major/Current page/usercontrol

        private UserControl _CurrentPage;

        public UserControl CurrentPage
        {
            get => _CurrentPage;
            set
            {
                Set(ref _CurrentPage, value);
            }
        }

        #endregion

        #region Commands

        #region Home button click

        public ICommand FirstButtonClick { get; }

        private bool CanFirstButtonClickExecute(object p) => true;

        private void OnFirstButtonClickExecuted(object p)
        {
            SlowOpacity(_Home);
        }

        #endregion

        #region Tasks button click

        public static ICommand SecondButtonClick { get; set; }

        private bool CanSecondButtonClickExecute(object p) => MainWindowModel.IsTasksNotEmpty;

        private void OnSecondButtonClickExecuted(object p)
        {
            SlowOpacity(_Tasks);
            
        }

        #endregion

        #region Settings button click

        public ICommand ThirdButtonClick { get; }

        private bool CanThirdButtonClickExecute(object p) => true;

        private void OnThirdButtonClickExecuted(object p)
        {
            CurrentPage = _Settings;
        }

        #endregion

        #region Account button click

        public ICommand FourthButtonClick { get; }

        private bool CanFourthButtonClickExecute(object p) => MainWindowModel.IsConnectedToLocalServer;

        private void OnFourthButtonClickExecuted(object p)
        {
            CurrentPage = _Account;
        }

        #endregion

        #region Help button click

        public ICommand FifthButtonClick { get; }

        private bool CanFifthButtonClickExecute(object p) => true;

        private void OnFifthButtonClickExecuted(object p)
        {
            CurrentPage = _Help;
        }

        #endregion

        #region Close Application button

        public ICommand CloseApplication { get; }
        private bool CanCloseApplicationExecute(object p) => true;
        private void OnCloseApplicationExecuted(object p)
        {
            
        }

        #endregion

        #endregion

        #region Functions

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

        #endregion

        #region Class Designer
        public MainWindowViewModel()
        { 
            _Settings = new Settings();

            #region Create usercontrols

            _Home = new Home();
            MainWindowModel.IsTasksNotEmpty = false;
            _Account = new Account();
            _Help = new Help();
            UserControlOpacity = 1;
            CurrentPage = _Home;

            #endregion

            #region Commands

            CloseApplication = new LambdaCommand(OnCloseApplicationExecuted, CanCloseApplicationExecute);
            FirstButtonClick = new LambdaCommand(OnFirstButtonClickExecuted, CanFirstButtonClickExecute);
            SecondButtonClick = new LambdaCommand(OnSecondButtonClickExecuted, CanSecondButtonClickExecute);
            ThirdButtonClick = new LambdaCommand(OnThirdButtonClickExecuted, CanThirdButtonClickExecute);
            FourthButtonClick = new LambdaCommand(OnFourthButtonClickExecuted, CanFourthButtonClickExecute);
            FifthButtonClick = new LambdaCommand(OnFifthButtonClickExecuted, CanFifthButtonClickExecute);

            #endregion

        }

        #endregion
    }
}
