using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Infrastructure.Commands;
using TaskManager.Infrastructure.Commands.Base;
using TaskManager.Models;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class TasksViewModel : ViewModel
    {
        #region БД

        public static ToDoTable toDoTable;

        #endregion

        #region Color Notes

        public string[] Colors;
        public string[] ColorsRepeat;

        public int Counter = 0;

        #endregion

        #region Labels
        #region Проект под именем...
        private string _Label1;
        public string Label1
        {
            get => TranslateLanguage.LabelTasks1[TranslateLanguage.iLanguage];
            set => Set(ref _Label1, value);
        }
        #endregion

        #region Подготвила команда...
        private string _Label2;
        public string Label2
        {
            get => TranslateLanguage.LabelTasks2[TranslateLanguage.iLanguage];
            set => Set(ref _Label2, value);
        }
        #endregion

        #region ToDo
        private string _Label3 = TranslateLanguage.LabelTasksToDo[TranslateLanguage.iLanguage];
        public string Label3
        {
            get => _Label3;
            set => Set(ref _Label3, value);
        }
        #endregion

        #region InProgress
        private string _Label4 = TranslateLanguage.LabelTasksInProgress[TranslateLanguage.iLanguage];
        public string Label4
        {
            get => _Label4;
            set => Set(ref _Label4, value);
        }
        #endregion

        #region Done
        private string _Label5 = TranslateLanguage.LabelTasksDone[TranslateLanguage.iLanguage];
        public string Label5
        {
            get => _Label5;
            set => Set(ref _Label5, value);
        }
        #endregion

        #region Напишите заметку
        private string _Label6 = TranslateLanguage.LabelTasks3[TranslateLanguage.iLanguage];
        public string Label6
        {
            get => _Label6;
            set => Set(ref _Label6, value);
        }
        #endregion

        #region Для чего это нужно сделать
        private string _Label7 = TranslateLanguage.LabelTasks4[TranslateLanguage.iLanguage];
        public string Label7
        {
            get => _Label7;
            set => Set(ref _Label7, value);
        }
        #endregion
        #endregion

        #region Записи/Заметки Selected

        private Note _SelectedNote;
        public Note PreviousNote;

        public Note SelectedNote
        {
            get
            {
                return _SelectedNote;
            }
            set
            {
                Set(ref _SelectedNote, value);
            }
        }

        #endregion

        #region коллекция записей/заметок

        public static ObservableCollection<Note> NotesToDo { get; set; }

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

        #region Visibiliity 

        private Visibility _Visibility = Visibility.Collapsed;

        public Visibility ChangeControlVisibility
        {
            get { return _Visibility; }
            set => Set(ref _Visibility, value);
        }

        #endregion

        #region Commands

        #region Сохранение заметки

        public ICommand SaveNote { get; }
        private bool CanSaveNoteExecute(object p) => true;
        private void OnSaveNoteExecuted(object p)
        {
            //PreviousNote = _SelectedNote;
            PreviousNote = NotesToDo[0];
            //int j = 0;
            //for (int i = 0; i < MainWindowViewModel.db.Users.Count(); i++)
            //{
            //    if (MainWindowViewModel.db.Users.Local[i].ProjectName == PName && MainWindowViewModel.db.Users.Local[i].MasterName == TName)
            //    {
            //        j = i;
            //    }
            //}
            //MainWindowViewModel.db.Users.Local[j].ToDoContext = NotesToDo[0].Content;
            //MainWindowViewModel.db.Users.Local[j].ToDoLilContext = NotesToDo[0].Target;
            //for (int i = 1; i < NotesToDo.Count(); i++)
            //{
            //    MainWindowViewModel.user.ProjectName = PName;
            //    MainWindowViewModel.user.MasterName = TName;
            //    MainWindowViewModel.user.ToDoContext = NotesToDo[i].Content;
            //    MainWindowViewModel.user.ToDoLilContext = NotesToDo[i].Target;
            //    MainWindowViewModel.db.Users.Add(MainWindowViewModel.user);

            //}
            if (this.ChangeControlVisibility == Visibility.Visible)
            {
                this.ChangeControlVisibility = Visibility.Collapsed;
            }
            //MainWindowViewModel.db.SaveChanges();


        }

        #endregion

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
                      note.Color = Colors[Counter++];
                      NotesToDo.Insert(0, note);
                      SelectedNote = note;
                      if (this.ChangeControlVisibility == Visibility.Collapsed && NotesToDo.Count() > 1)
                      {
                          this.ChangeControlVisibility = Visibility.Visible;
                      }

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

        #region К какому проекту относиться запись

        public static int CurrentProjectId { get; set; }

        #endregion

        #region БД


        #endregion

        #region Конструктор

        public TasksViewModel()
        {
            #region Конструктор БД

            toDoTable = new ToDoTable();
            int j = 0;
            int size = MainWindowViewModel.db.Projects.Count();
            for (int i = 0; i < size; i++)
            {
                if (HomeViewModel._SelectedProject.ProjectName == MainWindowViewModel.db.Projects.Local[i].ProjectName)
                {
                    j = i;
                }
            }
            toDoTable.ProjectId = MainWindowViewModel.db.Projects.Local[j].Id;
            CurrentProjectId = MainWindowViewModel.db.Projects.Local[j].Id;

            #endregion

            #region Commands

            SaveNote = new LambdaCommand(OnSaveNoteExecuted, CanSaveNoteExecute);

            #endregion

            #region Коллекции

            NotesToDo = new ObservableCollection<Note>
            {

            };
            NotesInProgress = new ObservableCollection<Note>
            {

            };
            NotesDone = new ObservableCollection<Note>
            {

            };


            // Цвета заметок

            Colors = new string[]
            {
                "#1F85DE",
                "#DEB41F",
                "#62DE1F",
                "#1FDEA1",
                "#8C1FDE",
                "#DE1F9B",
                "#E7435D",
            };
            ColorsRepeat = new string[10];

            #endregion

            #region Заполнение коллекций
            Note note;
            size = MainWindowViewModel.db.ToDos.Count();
            for (int i = 0; i < size; i++)
            {
                if (MainWindowViewModel.db.ToDos.Local[i].ProjectId == toDoTable.ProjectId)
                {
                    note = new Note
                    {
                        //Id = MainWindowViewModel.db.ToDos.Local[i].Id,
                        Content = MainWindowViewModel.db.ToDos.Local[i].Content,
                        Target = MainWindowViewModel.db.ToDos.Local[i].LContent
                    };
                    //note.Color = Colors[0];
                    NotesToDo.Add(note);
                }
            }

            #endregion
        }

        #endregion
    }
}
