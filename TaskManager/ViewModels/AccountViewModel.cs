using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class AccountViewModel : ViewModel
    {
        #region Labels

        private string _ButtonLabel1 = "Log out";

        public string ButtonLabel1 
        { 
            get => _ButtonLabel1; 
            set => Set(ref _ButtonLabel1, value); 
        }

        private string _ButtonLabel2 = "Create a new";

        public string ButtonLabel2
        {
            get => _ButtonLabel2;
            set => Set(ref _ButtonLabel2, value);
        }

        #endregion
    }
}
