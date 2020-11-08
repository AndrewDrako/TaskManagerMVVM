using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        #region  data base Записи в таблице ProjectTable

        public static ProjectTable projectTable;

        #endregion

        #region Projects
        //
        private Project _SelectedProject;

        public Project SelectedProject
        {
            get { return _SelectedProject; }
            set
            {
                if (this.ChangeControlVisibility == Visibility.Collapsed)
                {
                    this.ChangeControlVisibility = Visibility.Visible;
                }
                _SelectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }

        #endregion

        #region Коллекция проектов

        public ObservableCollection<Project> Projects { get; set; }

        #endregion

        #region Labels

        private string _Welcome;
        private string _Email = "3954014@gmail.com";  
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

        public string Label2
        {
            get => TranslateLanguage.LabelSP[TranslateLanguage.iLanguage];
            set => Set(ref _Label2, value);
        }

        public string Label3
        {
            get => TranslateLanguage.LabelPN[TranslateLanguage.iLanguage];
            set => Set(ref _Label3, value);
        }

        public string Label4
        {
            get => TranslateLanguage.LabelON[TranslateLanguage.iLanguage];
            set => Set(ref _Label4, value);
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

        #region Команды

        // Команда добавления проекта

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      if (this.ChangeControlVisibility == Visibility.Collapsed)
                      {
                          this.ChangeControlVisibility = Visibility.Visible;
                      }
                      Project project = new Project();
                      Projects.Insert(0, project);
                      SelectedProject = project;
                  }));
            }
        }

        // Команда удаления проекта

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Project project = obj as Project;
                      if (project != null)
                      {
                          Projects.Remove(project);
                          if (this.ChangeControlVisibility == Visibility.Visible)
                          {
                              this.ChangeControlVisibility = Visibility.Collapsed;
                          }
                          //int j = 0;
                          
                          //for (int i = 0; i < Projects.Count(); i++)
                          //{ 
                          //      if (project.ProjectName == MainWindowViewModel.db.Users.Local[i].ProjectName)
                          //      {
                          //          j = i;
                          //          //MainWindowViewModel.user = MainWindowViewModel.db.Users.Local[i];
                          //          break;
                          //      }
                          //}
                          //MainWindowViewModel.user = MainWindowViewModel.db.Users.Local[j];
                          //MainWindowViewModel.db.Users.Remove((MainWindowViewModel.user));
                          //MainWindowViewModel.db.SaveChanges();
                          //if (Projects.Count == 0)
                          //{
                          //    MainWindowModel.IsTasksNotEmpty = false;
                          //}
                      }
                  },
                 (obj) => Projects.Count > 0));
            }
        }

        // Команда выбора проекта

        private RelayCommand selectProject;
        public RelayCommand SelectProject
        {
            get
            {
                return selectProject ??
                    (selectProject = new RelayCommand(obj =>
                    {
                        Project project = obj as Project;
                        if (project != null && project.PersonName != "" && project.ProjectName != "")
                        {
                            MainWindowModel.IsTasksNotEmpty = true;
                            TasksViewModel._PName = project.ProjectName;
                            TasksViewModel._TName = project.PersonName;
                            MainWindowViewModel._Tasks = new Tasks();
                            bool checker = false; // Проверка на не повторяющииеся проекты
                            int n_size = MainWindowViewModel.db.Projects.Count();
                            for (int i = 0; i < n_size; i++)
                            {
                                if (project.ProjectName == MainWindowViewModel.db.Projects.Local[i].ProjectName)
                                {
                                    checker = true;
                                    break;
                                }
                            }
                            if (checker == false)
                            {
                                projectTable.ProjectName = project.ProjectName;
                                projectTable.MasterName = project.PersonName;
                                MainWindowViewModel.db.Projects.Add(projectTable);
                                MainWindowViewModel.db.SaveChanges();
                            }
                            //AsyncCommands.AddToDB(MainWindowViewModel.db, MainWindowViewModel.user);
                        }
                    }));
            }
        }

        #endregion

        #region Колво записей в таблице

        public static int _GetCount = -1;

        #endregion

        //public static async Task GetCount(MyDbContext db)
        //{
        //    await Task.Run(() => _GetCount = db.Users.Count());
        //}

        #region Конструктор 

        public HomeViewModel()
        {
            #region Коллекция объектов

            Projects = new ObservableCollection<Project>
            {

            };

            #endregion

            #region Конструтор БД

            projectTable = new ProjectTable();
            projectTable.UserId = MainWindowViewModel.user.Id;

            #endregion

            #region БД: заносим данные в коллекцию объектов

            // Колво записей втаблице Бд
            //int amt;
            // Заносит в _Count колво записей в таблице
            //DataBaseCommands.GetCount(MainWindowViewModel.db);

            //amt = _GetCount;
            //User user;
            //Project project;
            //for (int i = 0; i < amt; i++)
            //{
            //    project = new Project();
            //    user = MainWindowViewModel.db.Users.Local[i];
            //    project.ProjectName = user.ProjectName;
            //    project.PersonName = user.MasterName;
            //    Projects.Add(project);
            //}

            #endregion

            #region Commands

            #endregion
        }

        #endregion


    }
}
