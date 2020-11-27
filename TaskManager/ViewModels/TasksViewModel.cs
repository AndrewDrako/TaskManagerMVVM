using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Data.DataBase;
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

        /// <summary>
        /// To Do Column
        /// </summary>
        public static ToDoTable toDoTable;

        /// <summary>
        /// In Progress Column
        /// </summary>
        public static InProgressTable inProgressTable;

        /// <summary>
        /// Done Column
        /// </summary>
        public static DoneTable doneTable;

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

        private Note _ReadSelectedNote;

        public Note ReadSelectedNote
        {
            get => _ReadSelectedNote;
            set
            {
                Set(ref _ReadSelectedNote, value);
            }
        }
        #endregion

        #region коллекция записей/заметок

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

        #region Visibiliity Кнопки save

        private Visibility _Visibility = Visibility.Visible;

        public Visibility ChangeControlVisibility
        {
            get { return _Visibility; }
            set => Set(ref _Visibility, value);
        }

        #endregion

        #region Visibility поля ввода заметок

        private Visibility _VisibilityWriteNote = Visibility.Collapsed;

        public Visibility ChangeControlVisibilityWriteNote
        {
            get { return _VisibilityWriteNote; }
            set => Set(ref _VisibilityWriteNote, value);
        }

        #endregion

        #region Commands

        #region Сохранение заметки

        public ICommand SaveNote { get; }
        private bool CanSaveNoteExecute(object p) => MainWindowModel.IsConnectedToLocalServer;
        private void OnSaveNoteExecuted(object p)
        {
            Model.EditNote(MainWindowViewModel.db, NotesToDo, toDoTable.ProjectId);
            Model.AddNotesToDB(MainWindowViewModel.db, NotesToDo, toDoTable);
            if (this.ChangeControlVisibility == Visibility.Visible)
            {
                this.ChangeControlVisibility = Visibility.Collapsed;
            }

        }

        #endregion

        #region ToDo

        #region Add

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
                      if (this.ChangeControlVisibility == Visibility.Collapsed)
                      {
                          this.ChangeControlVisibility = Visibility.Visible;
                      }

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

                      Note note = obj as Note;
                      if (note != null)
                      {
                          NotesToDo.Remove(note);
                          if (MainWindowModel.IsConnectedToLocalServer == true)
                          {
                              Model.RemoveNoteFromDB(MainWindowViewModel.db, note, toDoTable.ProjectId, "TODO");
                          }

                      }
                  },
                 (obj) => NotesToDo.Count > 0));
            }
        }

        #endregion

        #region Transfer to InProgress

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
                            if (MainWindowModel.IsConnectedToLocalServer == true)
                            {
                                Model.TransferTo(MainWindowViewModel.db, note, inProgressTable);
                                Model.RemoveNoteFromDB(MainWindowViewModel.db, note, toDoTable.ProjectId, "TODO");
                            }

                        }
                    }));
            }
        }

        #endregion

        #endregion

        #region InProgress

        #region Remove

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
                          if (MainWindowModel.IsConnectedToLocalServer == true)
                          {
                              Model.RemoveNoteFromDB(MainWindowViewModel.db, note, inProgressTable.ProjectId, "INPROGRESS");
                          }

                      }
                  },
                 (obj) => NotesInProgress.Count > 0));
            }
        }

        #endregion

        #region Transfer to DONE

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
                            if (MainWindowModel.IsConnectedToLocalServer == true)
                            {
                                Model.TransferTo(MainWindowViewModel.db, note, doneTable);
                                Model.RemoveNoteFromDB(MainWindowViewModel.db, note, inProgressTable.ProjectId, "INPROGRESS");
                            }
                        }
                    }));
            }
        }

        #endregion

        #endregion

        #region Done

        #region Remove

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
                          if (MainWindowModel.IsConnectedToLocalServer == true)
                          {
                              Model.RemoveNoteFromDB(MainWindowViewModel.db, note, doneTable.ProjectId, "DONE");
                          }
                      }
                  },
                 (obj) => NotesDone.Count > 0));
            }
        }

        #endregion

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
            inProgressTable = new InProgressTable();
            doneTable = new DoneTable();
            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                try
                {
                    var projects = MainWindowViewModel.db.Projects.ToList();  // Выгружаем данные из бд в массив
                    foreach (var p in projects)
                    {
                        if (p.UserId == AuthWindowViewModel.authUser.Id)
                        {
                            if (_PName == p.ProjectName && _TName == p.MasterName)
                            {
                                toDoTable.ProjectId = p.Id;
                                inProgressTable.ProjectId = p.Id;
                                doneTable.ProjectId = p.Id;
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка возникла в консрукторе бд для To DO и In Progress");
                }
            }

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

            if (MainWindowModel.IsConnectedToLocalServer == true)
            {

                #region Заполнение коллекций

                #region Заполение To DO

                try
                {
                    var todos = MainWindowViewModel.db.ToDos.ToList();
                    foreach (var t in todos)
                    {
                        if (t.ProjectId == toDoTable.ProjectId)
                        {
                            Note note = new Note
                            {
                                Content = t.Content,
                                Target = t.LContent
                            };
                            NotesToDo.Add(note);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка произошла при заполнении коллеции TODO");
                }

                #endregion

                #region Заполнение In Progress

                try
                {
                    var inprogresses = MainWindowViewModel.db.InProgresses.ToList();
                    foreach (var inp in inprogresses)
                    {
                        if (inp.ProjectId == inProgressTable.ProjectId)
                        {
                            Note note = new Note
                            {
                                Content = inp.Content,
                                Target = inp.LContent
                            };
                            NotesInProgress.Add(note);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка произошла при заполнении коллеции INPROGRESS");
                }

                #endregion

                #region Заполнение Done

                try
                {
                    var dones = MainWindowViewModel.db.Dones.ToList();
                    foreach (var d in dones)
                    {
                        if (d.ProjectId == doneTable.ProjectId)
                        {
                            Note note = new Note
                            {
                                Content = d.Content,
                                Target = d.LContent
                            };
                            NotesDone.Add(note);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка произошла при заполнении коллеции DONE");
                }

                #endregion

                #endregion

            }
        }

        #endregion
    }
}
