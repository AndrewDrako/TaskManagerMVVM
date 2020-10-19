using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class TasksViewModel : ViewModel
    {
        #region Имя проекта и имя команды на доске

        public static string _PName = CreateProjectWindowViewModel._ProjectName;

        public string PName
        {
            get => _PName;
            set => Set(ref _PName, value);
        }

        public static string _TName = CreateProjectWindowViewModel._TeamName;

        public string TName
        {
            get => _TName;
            set => Set(ref _TName, value);
        }

        #endregion


        #region Конструктор

        public TasksViewModel()
        {
            //_PName = CreateProjectWindowViewModel._ProjectName;
            //_TName = CreateProjectWindowViewModel._TeamName;
        }

        #endregion
    }
}
