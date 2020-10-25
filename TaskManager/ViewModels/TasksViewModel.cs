using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Infrastructure.Commands.Base;
using TaskManager.Models;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class TasksViewModel : ViewModel
    {
        #region Записи

        private Note _SelectedNote;

        public Note SelectedNote
        {
            get => _SelectedNote;
            set => Set(ref _SelectedNote, value);
        }

        #endregion

        #region коллекция записей

        public ObservableCollection<Note> NotesToDo { get; set; }

        public ObservableCollection<Note> NotesInProgress { get; set; }
        public ObservableCollection<Note> NotesDone { get; set; }

        #endregion

        #region Имя проекта и имя команды на доске

        public static string _PName;

        public string PName
        {
            get => _PName;
            set => Set(ref _PName, value);
        }

        public static string _TName;

        public string TName
        {
            get => _TName;
            set => Set(ref _TName, value);
        }



        #endregion

        #region Commands

        #region ToDo

        // Команда добавления задачи

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {

                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Note note = new Note();
                      NotesToDo.Insert(0, note);
                      SelectedNote = note;
                  }));
            }
        }

        // Команда удаления задачи

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Note note = obj as Note;
                      if (note != null)
                      {
                          NotesToDo.Remove(note);
                          
                      }
                  },
                 (obj) => NotesToDo.Count > 0));
            }
        }

        // Команда переноса в IN Progress

        private RelayCommand transferProject;
        public RelayCommand TransferProject
        {
            get
            {
                return transferProject ??
                    (transferProject = new RelayCommand(obj =>
                    {
                        Note note = obj as Note;
                        if (note != null && NotesToDo.Contains(note) == true)
                        {
                            NotesInProgress.Insert(0, note);
                            NotesToDo.Remove(note);
                        }
                    }));
            }
        }
        #endregion

        #region InProgress

        // Команда удаления задачи

        private RelayCommand removeCommandInProgress;
        public RelayCommand RemoveCommandInProgress
        {
            get
            {
                return removeCommandInProgress ??
                  (removeCommandInProgress = new RelayCommand(obj =>
                  {
                      Note note = obj as Note;
                      if (note != null)
                      {
                          NotesInProgress.Remove(note);

                      }
                  },
                 (obj) => NotesInProgress.Count > 0));
            }
        }

        // Команда переноса в Done

        private RelayCommand transferProjectInProgress;
        public RelayCommand TransferProjectInProgress
        {
            get
            {
                return transferProjectInProgress ??
                    (transferProjectInProgress = new RelayCommand(obj =>
                    {
                        Note note = obj as Note;
                        if (note != null && NotesInProgress.Contains(note) == true)
                        {
                            NotesDone.Insert(0, note);
                            NotesInProgress.Remove(note);
                        }
                    }));
            }
        }

        #endregion

        #region Done

        // Команда удаления задачи

        private RelayCommand removeCommandDone;
        public RelayCommand RemoveCommandDone
        {
            get
            {
                return removeCommandDone ??
                  (removeCommandDone = new RelayCommand(obj =>
                  {
                      Note note = obj as Note;
                      if (note != null)
                      {
                          NotesDone.Remove(note);

                      }
                  },
                 (obj) => NotesDone.Count > 0));
            }
        }

        #endregion

        #endregion

        #region Конструктор

        public TasksViewModel()
        {
            NotesToDo = new ObservableCollection<Note>
            {

            };
            NotesInProgress = new ObservableCollection<Note>
            {

            };
            NotesDone = new ObservableCollection<Note>
            {

            };
        }

        #endregion
    }
}
