using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.Infrastructure.Commands.Base;
using TaskManager.Models;
using TaskManager.ViewModels.Base;
using TaskManager.Views.UserControls;

namespace TaskManager.ViewModels
{
    internal class HomeViewModel : ViewModel
    {
        #region Projects

        private Project _SelectedProject;

        

        #endregion

        #region Коллекция проектов

        public ObservableCollection<Project> Projects { get; set; }

        #endregion

        #region Labels

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
                          if (Projects.Count == 0)
                          {
                              MainWindowModel.IsTasksNotEmpty = false;
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
                        if (project != null)
                        {
                            Project.PrintToTxt();
                            MainWindowModel.IsTasksNotEmpty = true;
                            TasksViewModel._PName = project.ProjectName;
                            TasksViewModel._TName = project.PersonName;
                            MainWindowViewModel._Tasks = new Tasks();
                        }
                    }));
            }
        }
        #endregion

        public Project SelectedProject
        {
            get { return _SelectedProject; }
            set
            {
                _SelectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }

        #region Конструктор 

        public HomeViewModel()
        {
            Projects = new ObservableCollection<Project>
            {

            };
        }

        #endregion

        
    }
}
