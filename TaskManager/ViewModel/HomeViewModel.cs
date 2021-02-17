using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Models;
using TaskManager.Views.UserControls;

namespace TaskManager.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        /// <summary>
        /// data base: records in the table ProjectTable
        /// </summary>
        public static ProjectTable projectTable;

        public static Project _SelectedProject;

        public Project SelectedProject
        {
            get
            {
                return _SelectedProject;
            }
            set
            {
                _SelectedProject = value;
                if (_SelectedProject != null)
                {
                    this.ChangeControlVisibility = Visibility.Visible;
                }
                if (Projects.Contains(_SelectedProject) && _SelectedProject.ProjectName != null)
                {
                    this.ChangeControlVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged("SelectedProject");
            }
        }

        /// <summary>
        /// Collection of projects in the application
        /// </summary>
        public ObservableCollection<Project> Projects { get; set; }

        #region Labels

        private string welcome;
        private string email = AuthViewModel.authUser.UserName;
        private string labelSP;
        private string labelPN;
        private string labelON;

        public string Welcome
        {
            get => TranslateLanguage.LabelWelcome[TranslateLanguage.iLanguage];
            set => Set(ref welcome, value);
        }

        public string Email
        {
            get => email;
            set => Set(ref email, value);
        }

        /// <summary>
        /// Selected project label
        /// </summary>
        public string LabelSP
        {
            get => TranslateLanguage.LabelSP[TranslateLanguage.iLanguage];
            set => Set(ref labelSP, value);
        }

        /// <summary>
        /// Project name label
        /// </summary>
        public string LabelPN
        {
            get => TranslateLanguage.LabelPN[TranslateLanguage.iLanguage];
            set => Set(ref labelPN, value);
        }

        /// <summary>
        /// Сreator name label
        /// </summary>
        public string LabelON
        {
            get => TranslateLanguage.LabelON[TranslateLanguage.iLanguage];
            set => Set(ref labelON, value);
        }

        #endregion

        #region Tooltips

        private string addTT;

        /// <summary>
        /// add tooltip
        /// </summary>
        public string AddTT
        {
            get => TranslateLanguage.AddTT[TranslateLanguage.iLanguage];
            set => Set(ref addTT, value);
        }

        private string removeTT;

        /// <summary>
        /// remove tooltip
        /// </summary>
        public string RemoveTT
        {
            get => TranslateLanguage.RemoveTT[TranslateLanguage.iLanguage];
            set => Set(ref removeTT, value);
        }

        private string selectTT;

        /// <summary>
        /// select tooltip
        /// </summary>
        public string SelectTT
        {
            get => TranslateLanguage.SelectTT[TranslateLanguage.iLanguage];
            set => Set(ref selectTT, value);
        }

        #endregion

        #region Visibiliity input

        private Visibility _Visibility = Visibility.Collapsed;

        public Visibility ChangeControlVisibility
        {
            get { return _Visibility; }
            set => Set(ref _Visibility, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Add
        /// </summary>
        public RelayCommand AddCommand { get; }
        private bool CanAddCommandExecute() => true;
        private void OnAddCommandExecuted()
        {
            this.ChangeControlVisibility = Visibility.Visible;
            Project project = new Project();
            Projects.Insert(0, project);
            SelectedProject = project;
        }

        /// <summary>
        /// Remove
        /// </summary>
        public RelayCommand<object> RemoveCommand { get; }
        private bool CanRemoveCommandExecute() => true;
        private void OnRemoveCommandExecuted(object obj)
        {
            Project project = obj as Project;
            if (project != null)
            {
                this.ChangeControlVisibility = Visibility.Collapsed;
                Projects.Remove(project);
                MainWindowModel.IsTasksNotEmpty = false;
                if (MainWindowModel.IsConnectedToLocalServer == true)
                {
                    Model.RemoveProjectFromDB(MainViewModel.db, project).GetAwaiter();
                }
            }
        }

        /// <summary>
        /// Run
        /// </summary>
        public RelayCommand<object> SelectProject { get; }
        private bool CanSelectProjectExecute() => true;
        private void OnSelectProjectExecuted(object obj)
        {
            Project project = obj as Project;
            if (project != null)
            {
                if (project.PersonName != null && project.ProjectName != null)
                {
                    this.ChangeControlVisibility = Visibility.Collapsed;
                    MainWindowModel.IsTasksNotEmpty = true;  // Разблокировка кнопки tasks
                    MainViewModel.SecondButtonClick.RaiseCanExecuteChanged();
                    TasksViewModel.pName = project.ProjectName;
                    TasksViewModel.tName = project.PersonName;
                    if (MainWindowModel.IsConnectedToLocalServer == true)
                    {
                        //Model.EditProjectName(MainWindowViewModel.db, Projects, projectTable.UserId);
                        Model.AddProjectToDB(MainViewModel.db, project, projectTable, obj).GetAwaiter();
                    }
                    //MainViewModel.tasks = new Tasks(); // Открытие tasks
                    //MainViewModel.SecondButtonClick.Execute(obj);

                }
            }
        }

        #endregion

        public HomeViewModel()
        {
            AddCommand = new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
            RemoveCommand = new RelayCommand<object>((obj) => OnRemoveCommandExecuted(obj), CanRemoveCommandExecute());
            SelectProject = new RelayCommand<object>((obj) => OnSelectProjectExecuted(obj), CanSelectProjectExecute());

            Projects = new ObservableCollection<Project>  // Object collection
            {

            };

            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                projectTable = new ProjectTable();
                projectTable.UserId = AuthViewModel.authUser.Id;
            }

            // filling collections
            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                try
                {
                    var projects = MainViewModel.db.Projects.ToList();
                    foreach (var p in projects)
                    {
                        if (p.UserId == projectTable.UserId)
                        {
                            Project pr = new Project
                            {
                                ProjectName = p.ProjectName,
                                PersonName = p.MasterName
                            };
                            Projects.Add(pr);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка произошла при заполнении коллекции Проекты");
                }
            }
        }
    }
}
