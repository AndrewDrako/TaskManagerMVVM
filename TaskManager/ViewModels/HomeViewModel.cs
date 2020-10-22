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

        /// <summary>
        /// Бывшая CreateProjectWindowViewModel.cs
        /// </summary>

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

        public ICommand ButtonClick { get; }

        private bool CanButtonClickExecute(object p) => true;

        private void OnButtonClickExecuted(object p)
        {
            #region Закрытие

            Window window = p as Window;

            if (window is null)
            {
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);
            }

            if (window is null)
            {
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);
            }

            window?.Close();

            #endregion

            #region ПЕредаем данные

            CreateProjectModel.SetProjectName(_ProjectName);
            CreateProjectModel.SetTeamName(_TeamName);
            CreateProjectModel.PrintToTxt();

            MainWindowModel.IsTasksNotEmpty = true;


            


            #endregion
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
            ButtonClick = new LambdaCommand(OnButtonClickExecuted, CanButtonClickExecute);

            #endregion
        }

        #endregion

    }
}
