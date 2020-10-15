using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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

        #region Клик кнопки номер 1

        public ICommand Button1Click { get; }

        private bool CanButton1ClickExecute(object p) => true;

        private void OnButton1ClickExecuted(object p)
        {

        }

        #endregion
    }
}
