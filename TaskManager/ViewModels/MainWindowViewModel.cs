using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.ViewModels.Base;
using TaskManager.Views.Pages;

namespace TaskManager.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Страницы приложения

        private Page _Home;
        private Page _Manager;

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

        private string _ButtonContent2 = "Dashboard";

        public string ButtonContent2
        {
            get => _ButtonContent2;

            set => Set(ref _ButtonContent2, value);
        }

        #endregion

        #region Основная (текущая страница)

        private Page _CurrentPage;

        public Page CurrentPage
        {
            get => _CurrentPage;
            set => Set(ref _CurrentPage, value);
        }

        #endregion

        #region Клик кнопки номер 1

        public ICommand Button1Click { get; }

        private bool CanButton1ClickExecute(object p) => true;

        private void OnButton1ClickExecuted(object p)
        {
            CurrentPage = _Home;
        }

        #endregion

        #region Конструктор класса MainWindowViewModel

        public MainWindowViewModel()
        {
            _Home = new Views.Pages.Home();
            _Manager = new Views.Pages.Manager();

            CurrentPage = _Manager;

            #region Команды

            Button1Click = new LambdaCommand(OnButton1ClickExecuted, CanButton1ClickExecute);

            #endregion

        }

        #endregion
    }
}
