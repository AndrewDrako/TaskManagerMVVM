using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class HomeViewModel : ViewModel
    {
        #region Добро пожаловать .com

        private string _Welcome = "Welcome";
        private string _Email = "3954014@gmail.com";

        public string Welcome
        {
            get => _Welcome;
            set => Set(ref _Welcome, value);
        }

        public string Email
        {
            get => _Email;
            set => Set(ref _Email, value);
        }

        #endregion
    }
}
