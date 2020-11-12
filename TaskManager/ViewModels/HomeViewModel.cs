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
        #region  data base Записи в таблице ProjectTable

        public static ProjectTable projectTable;

        #endregion

        #region Projects
        public static Project _SelectedProject;
        
        //public static bool check = true;

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

        private Visibility _Visibility = Visibility.Visible;

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
                      //if (this.ChangeControlVisibility == Visibility.Collapsed)
                      //{
                      //    this.ChangeControlVisibility = Visibility.Visible;
                      //}
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
                      if (true)
                      {
                          Projects.Remove(project);
                          MainWindowModel.IsTasksNotEmpty = false;
                          //if (this.ChangeControlVisibility == Visibility.Visible)
                          //{
                          //    this.ChangeControlVisibility = Visibility.Collapsed;
                          //}
                          try
                          {
                              var projects = MainWindowViewModel.db.Projects.ToList();
                              foreach(var p in projects)
                              {
                                  if (p.ProjectName == project.ProjectName)
                                  {
                                      MainWindowViewModel.db.Projects.Attach(p);
                                      MainWindowViewModel.db.Projects.Remove(p);
                                      MainWindowViewModel.db.SaveChanges();
                                  }
                              }
                          }
                          catch
                          {
                              MessageBox.Show("проблема возникла при удалении проекта");
                          }
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
                        if (project.PersonName != null && project.ProjectName != null)
                        {
                            MainWindowModel.IsTasksNotEmpty = true;  // Разблокировка кнопки tasks
                            TasksViewModel._PName = project.ProjectName;
                            TasksViewModel._TName = project.PersonName;
                            bool isContains = false; // Чекер для проверки измененных проектов
                            bool checker = false; // Проверка на не повторяющииеся проекты
                            try
                            {
                                var projects = MainWindowViewModel.db.Projects.ToList();  // Выгружаем данные из бд в массив
                                foreach (var p in projects)  // Идем по массиву
                                {
                                    for (int i = 0; i < Projects.Count(); i++)  // Ищем элемент который есть в БД , но отсутсвует в коллекции проектов
                                    {
                                        if (p.ProjectName == Projects[i].ProjectName && p.MasterName == Projects[i].PersonName)  // Если очередной элемент из Бд присуствует в текущей коллекции, то чекер становиться true, и прекращаем искать
                                        {
                                            isContains = true;
                                            break;
                                        }
                                    }
                                    if (isContains == false)  // Усли очередной эл-нт из бд отсуствует в текущей коллекции, то мы его удаляем из бд
                                    {
                                        MainWindowViewModel.db.Projects.Attach(p);
                                        MainWindowViewModel.db.Projects.Remove(p);
                                        MainWindowViewModel.db.SaveChanges();
                                        break;
                                    }
                                    isContains = false;  // Ставим чекер false, чтобы продолжить цикл
                                }
                                foreach (var p in projects)
                                {
                                    if(project.ProjectName == p.ProjectName && project.PersonName == p.MasterName)
                                    {

                                        checker = true;
                                        break;
                                    }
                                }
                                if (checker == false)
                                {
                                    projectTable.ProjectName = project.ProjectName;
                                    projectTable.MasterName = project.PersonName;
                                    MainWindowViewModel.db.Projects.Attach(projectTable);
                                    MainWindowViewModel.db.Projects.Add(projectTable);
                                    MainWindowViewModel.db.SaveChanges();
                                }
                                MainWindowViewModel._Tasks = new Tasks(); // Открытие tasks
                            }
                            catch
                            {
                                MessageBox.Show("Проблема возникла при выборе проекта");
                            }
                            
                        }
                    }));
            }
        }

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

            projectTable = new ProjectTable();
            projectTable.UserId = MainWindowViewModel.db.Users.Local[0].Id;

            #endregion

            #region БД: заносим данные в коллекцию объектов

            try
            {
                var projects = MainWindowViewModel.db.Projects.ToList();
                foreach (var p in projects)
                {
                    if (p.UserId == projectTable.UserId)
                    {
                        Project pr = new Project();
                        pr.ProjectName = p.ProjectName;
                        pr.PersonName = p.MasterName;
                        Projects.Add(pr);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка произошла при заполнении коллекции Проекты");
            }
            
            #endregion

            #region Commands

            #endregion
        }

        #endregion


    }
}
