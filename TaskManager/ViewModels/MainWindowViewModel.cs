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

        #region Конструктор класса MainWindowViewModel

        public MainWindowViewModel()
        {
            #region Создание окон

            _Home = new Views.UserControls.Home();
            _Tasks = new Views.UserControls.Tasks();
            _Settings = new Views.UserControls.Settings();

            CurrentPage = _Home;

            #endregion

            #region Команды

            FirstButtonClick = new LambdaCommand(OnFirstButtonClickExecuted, CanFirstButtonClickExecute);
            SecondButtonClick = new LambdaCommand(OnSecondButtonClickExecuted, CanSecondButtonClickExecute);
            ThirdButtonClick = new LambdaCommand(OnThirdButtonClickExecuted, CanThirdButtonClickExecute);

            #endregion

        }

        #endregion
    }
}
