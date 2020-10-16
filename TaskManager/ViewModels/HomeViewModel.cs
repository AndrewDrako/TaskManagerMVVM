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

        #region Open project or create new -- label

        private string _Label2 = "Open project or create new...";

        public string Label2
        {
            get => _Label2;
            set => Set(ref _Label2, value);
        }

        #endregion

        #region Change a note or create -- label

        private string _Label3 = "Change a note or create...";

        public string Label3
        {
            get => _Label3;
            set => Set(ref _Label3, value);
        }

        #endregion
    }
}
