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

        public static MyDbContext db = AuthWindowViewModel.dbContext;

        #endregion

        #region Страницы приложения

        private UserControl _Home;
        public static UserControl _Tasks;
        private UserControl _Settings;
        private UserControl _Account;
        private UserControl _Help;

        #endregion

        #region Labels

        #region Заголовок окна

        private string _Title = "Task Manager";

        public string Title
        {
            get => _Title;

            set => Set(ref _Title, value);
        }

        #endregion

        #region Название кнопки номер 1

        private string _ButtonContent1;

        public string ButtonContent1
        {
            get => TranslateLanguage.LabelHome[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent1, value);
        }

        #endregion

        #region Название кнопки номер 2

        private string _ButtonContent2;

        public string ButtonContent2
        {
            get => TranslateLanguage.LabelTask[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent2, value);
        }

        #endregion

        #region Название кнопки номер 3

        private string _ButtonContent3;

        public string ButtonContent3
        {
            get => TranslateLanguage.LabelSettings[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent3, value);
        }

        #endregion

        #region Название кнопки номер 4

        private string _ButtonContent4;

        public string ButtonContent4
        {
            get => TranslateLanguage.LabelAcc[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent4, value);
        }

        #endregion

        #region Название кнопки номер 5

        private string _ButtonContent5;

        public string ButtonContent5
        {
            get => TranslateLanguage.LabelHelp[TranslateLanguage.iLanguage];

            set => Set(ref _ButtonContent5, value);
        }

        #endregion

        #endregion


        #region Основная (текущая страница)

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

        #region Клик кнопки номер 1

        public ICommand FirstButtonClick { get; }

        private bool CanFirstButtonClickExecute(object p) => true;

        private void OnFirstButtonClickExecuted(object p)
        {
            CurrentPage = _Home;
        }

        #endregion

        #region Клик кнопки номер 2

        public static ICommand SecondButtonClick { get; set; }

        private bool CanSecondButtonClickExecute(object p) => MainWindowModel.IsTasksNotEmpty;

        private void OnSecondButtonClickExecuted(object p)
        {
            CurrentPage = _Tasks;
            
        }

        #endregion

        #region Клик кнопки номер 3

        public ICommand ThirdButtonClick { get; }

        private bool CanThirdButtonClickExecute(object p) => true;

        private void OnThirdButtonClickExecuted(object p)
        {
            CurrentPage = _Settings;
        }

        #endregion

        #region Клик кнопки номер 4

        public ICommand FourthButtonClick { get; }

        private bool CanFourthButtonClickExecute(object p) => MainWindowModel.IsConnectedToLocalServer;

        private void OnFourthButtonClickExecuted(object p)
        {
            CurrentPage = _Account;
        }

        #endregion

        #region Клик кнопки номер 5

        public ICommand FifthButtonClick { get; }

        private bool CanFifthButtonClickExecute(object p) => true;

        private void OnFifthButtonClickExecuted(object p)
        {
            CurrentPage = _Help;
        }

        #endregion

        #region Клик кнопки номер 5

        public ICommand SixButtonClick { get; }

        private bool CanSixButtonClickExecute(object p) => true;

        private void OnSixButtonClickExecuted(object p)
        {
            //db.SaveChanges();
        }

        #endregion

        #region Закрытие Окна

        public ICommand CloseApplication { get; }
        private bool CanCloseApplicationExecute(object p) => true;
        private void OnCloseApplicationExecuted(object p)
        {
            
        }

        #endregion

        #endregion

        #region Конструктор 
        public MainWindowViewModel()
        { 
            _Settings = new Settings();

            #region Создание окон

            _Home = new Home();
            MainWindowModel.IsTasksNotEmpty = false;
            _Account = new Account();
            _Help = new Help();

            CurrentPage = _Home;

            #endregion

            #region Команды

            CloseApplication = new LambdaCommand(OnCloseApplicationExecuted, CanCloseApplicationExecute);

            FirstButtonClick = new LambdaCommand(OnFirstButtonClickExecuted, CanFirstButtonClickExecute);
            SecondButtonClick = new LambdaCommand(OnSecondButtonClickExecuted, CanSecondButtonClickExecute);
            ThirdButtonClick = new LambdaCommand(OnThirdButtonClickExecuted, CanThirdButtonClickExecute);
            FourthButtonClick = new LambdaCommand(OnFourthButtonClickExecuted, CanFourthButtonClickExecute);
            FifthButtonClick = new LambdaCommand(OnFifthButtonClickExecuted, CanFifthButtonClickExecute);
            SixButtonClick = new LambdaCommand(OnSixButtonClickExecuted, CanSixButtonClickExecute);


            #endregion

        }

        #endregion
    }
}
