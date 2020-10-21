using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.Models;
using TaskManager.ViewModels.Base;
using TaskManager.Views.UserControls;

namespace TaskManager.ViewModels
{
    internal class HomeViewModel : ViewModel
    {
        /// <summary>
        /// Labels
        /// </summary>

        #region Windows (окна)

        public static Window _CreateProjectWindow;

        #endregion

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

        #region Имя проекта

        //private string _PName = CreateProjectModel.ProjectName[0];

        public string PName
        {
            get => CreateProjectModel.ProjectName[0];
            set => Set(ref CreateProjectModel.ProjectName[0], value);
        }

        #endregion

        /// <summary>
        /// Commands
        /// </summary>

        #region Button create new project click

        public ICommand CreateProjectClick { get; }

        private bool CanCreateProjectClickExecute(object p) => true;

        private void OnCreateProjectClickExecuted(object p)
        {
            _CreateProjectWindow = new Views.Windows.CreateProjectWindowxaml();
            _CreateProjectWindow.Show();
            if (this.ChangeControlVisibility == Visibility.Collapsed)
            {
                this.ChangeControlVisibility = Visibility.Visible;
            }
            

        }

        #endregion

        #region Buttons visibility

        

        private Visibility _Visibility = Visibility.Collapsed;

        public Visibility ChangeControlVisibility
        {
            get => _Visibility;
            set => Set(ref _Visibility, value);
            
        }
        

        #endregion

        #region Конструктор 

        public HomeViewModel()
        {
            #region Окна создания
            //_CreateProjectWindow = new Views.Windows.CreateProjectWindowxaml();
            

            #endregion

            #region Комманды

            CreateProjectClick = new LambdaCommand(OnCreateProjectClickExecuted, CanCreateProjectClickExecute);

            #endregion
        }

        #endregion

    }
}
