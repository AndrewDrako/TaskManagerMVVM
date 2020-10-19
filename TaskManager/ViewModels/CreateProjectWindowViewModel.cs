using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.ViewModels.Base;
using TaskManager.Views.Windows;

namespace TaskManager.ViewModels
{
    class CreateProjectWindowViewModel : ViewModel
    {
        #region Имя проекта и имя команды Label

        private string _ProjectName;

        public string ProjectName
        {
            get => _ProjectName;
            set => Set(ref _ProjectName, value);
        }

        private string _TeamName;

        public string TeamName
        {
            get => _TeamName;
            set => Set(ref _TeamName, value);
        }

        #endregion

        #region Название кнопки OK

        private string _ButtonOKContent = "OK";

        public string ButtonOKContent
        {
            get => _ButtonOKContent;
            set => Set(ref _ButtonOKContent, value);
        }


        #endregion

        #region Enter project name

        private string _EnterProjectNameLabel = "Enter project name";

        public string EnterProjectNameLabel
        {
            get => _EnterProjectNameLabel;
            set => Set(ref _EnterProjectNameLabel, value);
        }

        #endregion

        #region Enter team name

        private string _EnterTeamNameLabel = "Enter owner name or team name";

        public string EnterTeamNameLabel
        {
            get => _EnterTeamNameLabel;
            set => Set(ref _EnterTeamNameLabel, value);
        }

        #endregion

        #region Button OK click and save info

        //public ICommand ButtonClick { get; }

        //private bool CanButtonClickExecute(object p) => true;

        //private void OnButtonClickExecuted(object p)
        //{
        //    HomeViewModel._CreateProjectWindow.Close();
        //}

        #endregion

        #region Консруктор

        public CreateProjectWindowViewModel()
        {
            //ButtonClick = new LambdaCommand(OnButtonClickExecuted, CanButtonClickExecute);
        }

        #endregion
    }
}
