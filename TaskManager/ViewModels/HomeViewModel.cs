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
        #region  data base: Записи в таблице ProjectTable

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
                OnPropertyChanged("SelectedProject");
            }
        }

        #endregion

        #region Коллекция проектов в приложении

        public ObservableCollection<Project> Projects { get; set; }

        #endregion

        #region Labels

        private string _Welcome;
        private string _Email = AuthWindowViewModel.authUser.Email;  
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

        // Выбранный проект

        public string Label2
        {
            get => TranslateLanguage.LabelSP[TranslateLanguage.iLanguage];
            set => Set(ref _Label2, value);
        }

        // Имя проекта

        public string Label3
        {
            get => TranslateLanguage.LabelPN[TranslateLanguage.iLanguage];
            set => Set(ref _Label3, value);
        }

        // Имя команды работающей над проектом

        public string Label4
        {
            get => TranslateLanguage.LabelON[TranslateLanguage.iLanguage];
            set => Set(ref _Label4, value);
        }

        #endregion

        #region Visibiliity input

        private Visibility _Visibility = Visibility.Visible;

        public Visibility ChangeControlVisibility
        {
            get { return _Visibility; }
            set => Set(ref _Visibility, value);
        }

        #endregion

        #region Команды

        #region Добавление проекта

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Project project = new Project();
                      Projects.Insert(0, project);
                      SelectedProject = project;

                  }));
            }
        }

        #endregion

        #region Удаление проекта

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

        #region Выбор проекта

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

        #region Конструктор 

        public HomeViewModel()
        {
            #region Коллекция объектов

            Projects = new ObservableCollection<Project>
            {

            };

            #endregion

            #region Конструтор БД
            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                projectTable = new ProjectTable();
                projectTable.UserId = AuthWindowViewModel.authUser.Id;
            }
            #endregion

            #region БД: заносим данные в коллекцию объектов
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
