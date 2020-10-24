using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    internal class Project : ViewModel
    {
        #region Имя проекта

        private string _ProjectName;

        public string ProjectName
        {
            get => _ProjectName;
            set => Set(ref _ProjectName, value);
        }

        #endregion

        #region Имя создателея проекта

        private string _PersonName;

        public string PersonName
        {
            get => _PersonName;
            set => Set(ref _PersonName, value);
        }

        #endregion
    }
}
