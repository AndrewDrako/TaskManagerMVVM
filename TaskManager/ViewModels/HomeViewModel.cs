using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Data.DataBase;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Infrastructure.Commands;
using TaskManager.Infrastructure.Commands.Base;
using TaskManager.Models;
using TaskManager.ViewModels.Base;
using TaskManager.Views.UserControls;

namespace TaskManager.ViewModels
{
    internal class HomeViewModel : ViewModel
    {
        #region  data base: records in the table ProjectTable

        public static ProjectTable projectTable;

        #endregion

        #region  selected project

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
                OnPropertyChanged("SelectedProject");
            }
        }

        #endregion

        #region Collection of projects in the application

        public ObservableCollection<Project> Projects { get; set; }

        #endregion

        #region Labels

        private string _Welcome;
        private string _Email = AuthWindowViewModel.authUser.UserName;  
        private string _Label2;
        private string _Label3;
        private string _Label4;

        public string Welcome
        {
            get => TranslateLanguage.LabelWelcome[TranslateLanguage.iLanguage];
            set => Set(ref _Welcome, value);
        }

        public string Email
        {
            get => _Email;
            set => Set(ref _Email, value);
        }

        /// <summary>
        /// Selected project label
        /// </summary>
        public string Label2
        {
            get => TranslateLanguage.LabelSP[TranslateLanguage.iLanguage];
            set => Set(ref _Label2, value);
        }

        /// <summary>
        /// Project name label
        /// </summary>
        public string Label3
        {
            get => TranslateLanguage.LabelPN[TranslateLanguage.iLanguage];
            set => Set(ref _Label3, value);
        }

        /// <summary>
        /// Сreator name label
        /// </summary>
        public string Label4
        {
            get => TranslateLanguage.LabelON[TranslateLanguage.iLanguage];
            set => Set(ref _Label4, value);
        }

        #endregion

        #region Tooltips

        #region add tooltip

        private string _AddTT;
        public string AddTT
        {
            get => TranslateLanguage.AddTT[TranslateLanguage.iLanguage];
            set => Set(ref _AddTT, value);
        }

        #endregion

        #region remove tooltip

        private string _RemoveTT;
        public string RemoveTT
        {
            get => TranslateLanguage.RemoveTT[TranslateLanguage.iLanguage];
            set => Set(ref _RemoveTT, value);
        }

        #endregion

        #region select tooltip

        private string _SelectTT;
        public string SelectTT
        {
            get => TranslateLanguage.SelectTT[TranslateLanguage.iLanguage];
            set => Set(ref _SelectTT, value);
        }

        #endregion

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

        #region Add

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      this.ChangeControlVisibility = Visibility.Visible;
                      Project project = new Project();
                      Projects.Insert(0, project);
                      SelectedProject = project;

                  }));
            }
        }

        #endregion

        #region Remove

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Project project = obj as Project;
                      if (true)
                      {
                          this.ChangeControlVisibility = Visibility.Collapsed;
                          Projects.Remove(project);
                          MainWindowModel.IsTasksNotEmpty = false;
                          if (MainWindowModel.IsConnectedToLocalServer == true)
                          {
                              Model.RemoveProjectFromDB(MainWindowViewModel.db, project);
                          }
                      }
                  },
                 (obj) => Projects.Count > 0));
            }
        }

        #endregion

        #region Run

        private RelayCommand selectProject;
        public RelayCommand SelectProject
        {
            get
            {
                return selectProject ??
                    (selectProject = new RelayCommand(obj =>
                    {
                        Project project = obj as Project;
                        if (project.PersonName != null && project.ProjectName != null)
                        {
                            this.ChangeControlVisibility = Visibility.Collapsed;
                            MainWindowModel.IsTasksNotEmpty = true;  // Разблокировка кнопки tasks
                            TasksViewModel._PName = project.ProjectName;
                            TasksViewModel._TName = project.PersonName;
                            if (MainWindowModel.IsConnectedToLocalServer == true)
                            {
                                Model.EditProjectName(MainWindowViewModel.db, Projects, projectTable.UserId);
                                Model.AddProjectToDB(MainWindowViewModel.db, project, projectTable);
                            }
                            MainWindowViewModel._Tasks = new Tasks(); // Открытие tasks
                            MainWindowViewModel.SecondButtonClick.Execute(obj);

                        }
                    }));
            }
        }

        #endregion

        #endregion

        #region Class designer 

        public HomeViewModel()
        {
            #region Object collection

            Projects = new ObservableCollection<Project>
            {

            };

            #endregion

            #region database designer
            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                projectTable = new ProjectTable();
                projectTable.UserId = AuthWindowViewModel.authUser.Id;
            }
            #endregion

            #region filling the collection
            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                try
                {
                    var projects = MainWindowViewModel.db.Projects.ToList();
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
            #endregion

        }

        #endregion


    }
}
