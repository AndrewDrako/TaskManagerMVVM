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
        #region Data base

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

        #region Проект под именем.../ Project with name...
        private string _Label1;
        public string Label1
        {
            get => TranslateLanguage.LabelTasks1[TranslateLanguage.iLanguage];
            set => Set(ref _Label1, value);
        }
        #endregion

        #region Подготвила команда.../Prepared by the team
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

        #region Write a note
        private string _Label6 = TranslateLanguage.LabelTasks3[TranslateLanguage.iLanguage];
        public string Label6
        {
            get => _Label6;
            set => Set(ref _Label6, value);
        }
        #endregion

        #region Для чего это нужно сделать/For what
        private string _Label7 = TranslateLanguage.LabelTasks4[TranslateLanguage.iLanguage];
        public string Label7
        {
            get => _Label7;
            set => Set(ref _Label7, value);
        }
        #endregion
        #endregion

        #region ToolTips

        #region Save note 

        private string _SaveNoteTT;
        public string SaveNoteTT
        {
            get => TranslateLanguage.SaveNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref _SaveNoteTT, value);
        }

        #endregion

        #region Add note 

        private string _AddNoteTT;
        public string AddNoteTT
        {
            get => TranslateLanguage.AddNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref _AddNoteTT, value);
        }

        #endregion

        #region Remove note 

        private string _DelNoteTT;
        public string delNoteTT
        {
            get => TranslateLanguage.DelNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref _DelNoteTT, value);
        }

        #endregion

        #region Transfer note 

        private string _TransferNoteTT;
        public string TransferNoteTT
        {
            get => TranslateLanguage.TransferNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref _TransferNoteTT, value);
        }

        #endregion

        #region Clear note 

        private string _ClearNoteTT;
        public string ClearNoteTT
        {
            get => TranslateLanguage.ClearNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref _ClearNoteTT, value);
        }

        #endregion

        #endregion

        #region Note Selected

        private Note _SelectedNote;

        public Note SelectedNote
        {
            get
            {
                return _SelectedNote;
            }
            set
            {
                this.ChangeControlVisibility = Visibility.Visible;
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

        #region Note collection

        public ObservableCollection<Note> NotesToDo { get; set; }

        public ObservableCollection<Note> NotesInProgress { get; set; }
        public ObservableCollection<Note> NotesDone { get; set; }

        #endregion

        #region Project and team name on the kanban

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

        #region Visibiliity save button

        private Visibility _Visibility = Visibility.Collapsed;

        public Visibility ChangeControlVisibility
        {
            get { return _Visibility; }
            set => Set(ref _Visibility, value);
        }

        #endregion

        #region Visibility input

        private Visibility _VisibilityWriteNote = Visibility.Collapsed;

        public Visibility ChangeControlVisibilityWriteNote
        {
            get { return _VisibilityWriteNote; }
            set => Set(ref _VisibilityWriteNote, value);
        }

        #endregion

        #region Commands

        #region Save

        public static ICommand SaveNote { get; set; }
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
                      //note.Color = Colors[Counter++];
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
        
        /// <summary>
        /// Current project id
        /// </summary>
        public static int CurrentProjectId { get; set; }

        #region Class designer

        public TasksViewModel()
        {
            #region Data base designer
            
            toDoTable = new ToDoTable();
            inProgressTable = new InProgressTable();
            doneTable = new DoneTable();
            if (MainWindowModel.IsConnectedToLocalServer == true)
            {
                try
                {
                    var projects = MainWindowViewModel.db.Projects.ToList();  // Unloading data from the database into an array
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

            #region Collections

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

            if (MainWindowModel.IsConnectedToLocalServer == true)  // checking the connection to the server
            {

                #region Filling collections

                #region To DO

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

                #region In Progress

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

                #region Done

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
