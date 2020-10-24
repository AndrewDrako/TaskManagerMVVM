﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Project SelectedProject
        {
            get => _SelectedProject;
            set => Set(ref _SelectedProject, value);
        }

        #endregion

        #region Коллекция проектов

        public ObservableCollection<Project> Projects { get; set; }

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
                      }
                  },
                 (obj) => Projects.Count > 0));
            }
        }

        #endregion

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
