using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.ViewModels.Base;
using TaskManager.Infrastructure.Commands;
using System.Windows;

namespace TaskManager.ViewModels
{
    public class RegistrationWindowViewModel : ViewModel
    {
        #region Команды

        public ICommand BtnClick { get; }
        private bool CanBtnClickExecute(object p) => true;
        private void OnBtnClickExecuted(object p)
        {
            this.ChangeControlVisibility1 = Visibility.Collapsed;
        }

        #endregion

        #region Visibility 

        private Visibility _VisibilityFirst = Visibility.Visible;

        public Visibility ChangeControlVisibility1
        {
            get { return _VisibilityFirst; }
            set => Set(ref _VisibilityFirst, value);
        }

        private Visibility _VisibilitySecond = Visibility.Collapsed;

        public Visibility ChangeControlVisibility2
        {
            get { return _VisibilitySecond; }
            set => Set(ref _VisibilitySecond, value);
        }

        #endregion

        #region Конструктор

        public RegistrationWindowViewModel()
        {
            BtnClick = new LambdaCommand(OnBtnClickExecuted, CanBtnClickExecute);
        }

        #endregion
    }
}
