using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.ViewModels.Base;
using TaskManager.Views.UserControls;

namespace TaskManager.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Страницы приложения

        private UserControl _Home;
        private UserControl _Tasks;
        private UserControl _Settings;
        private UserControl _Account;
        private UserControl _Help;

        #endregion

        #region Заголовок окна

        private string _Title = "Task Manager";

        public string Title
        {
            get => _Title;

            set => Set(ref _Title, value);
        }

        #endregion

        #region Название кнопки номер 1

        private string _ButtonContent1 = "Home";

        public string ButtonContent1
        {
            get => _ButtonContent1;

            set => Set(ref _ButtonContent1, value);
        }

        #endregion

        #region Название кнопки номер 2

        private string _ButtonContent2 = "Tasks";

        public string ButtonContent2
        {
            get => _ButtonContent2;

            set => Set(ref _ButtonContent2, value);
        }

        #endregion

        #region Название кнопки номер 3

        private string _ButtonContent3 = "Settings";

        public string ButtonContent3
        {
            get => _ButtonContent3;

            set => Set(ref _ButtonContent3, value);
        }

        #endregion

        #region Название кнопки номер 4

        private string _ButtonContent4 = "Account";

        public string ButtonContent4
        {
            get => _ButtonContent4;

            set => Set(ref _ButtonContent4, value);
        }

        #endregion

        #region Название кнопки номер 5

        private string _ButtonContent5 = "Help";

        public string ButtonContent5
        {
            get => _ButtonContent5;

            set => Set(ref _ButtonContent5, value);
        }

        #endregion

        #region Основная (текущая страница)

        private UserControl _CurrentPage;

        public UserControl CurrentPage
        {
            get => _CurrentPage;
            set => Set(ref _CurrentPage, value);
        }

        #endregion

        #region Клик кнопки номер 1

        public ICommand FirstButtonClick { get; }

        private bool CanFirstButtonClickExecute(object p) => true;

        private void OnFirstButtonClickExecuted(object p)
        {
            CurrentPage = _Home;
        }

        #endregion

        #region Клик кнопки номер 2

        public ICommand SecondButtonClick { get; }

        private bool CanSecondButtonClickExecute(object p) => true;

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

        private bool CanFourthButtonClickExecute(object p) => true;

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

        #region Конструктор класса MainWindowViewModel

        public MainWindowViewModel()
        {
            #region Создание окон

            _Home = new Views.UserControls.Home();
            _Tasks = new Views.UserControls.Tasks();
            _Settings = new Views.UserControls.Settings();
            _Account = new Account();
            _Help = new Help();

            CurrentPage = _Home;

            #endregion

            #region Команды

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
