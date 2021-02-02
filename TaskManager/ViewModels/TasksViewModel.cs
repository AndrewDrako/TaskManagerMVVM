using System.Collections.ObjectModel;
using System.Linq;
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

        public string[] Colors;
        public string[] ColorsRepeat;

        public int Counter = 0;

        #region Labels

        private string labelTitleFirst;

        /// <summary>
        /// Проект под именем.../ Project with name...
        /// </summary>
        public string LabelTitleFirst
        {
            get => TranslateLanguage.LabelTasksFirstTitle[TranslateLanguage.iLanguage];
            set => Set(ref labelTitleFirst, value);
        }
        
        private string labelSecTitle;

        /// <summary>
        /// Подготвила команда.../Prepared by the team
        /// </summary>
        public string LabelSecTitle
        {
            get => TranslateLanguage.LabelTasksSecTitle[TranslateLanguage.iLanguage];
            set => Set(ref labelSecTitle, value);
        }

        private string labelToDo = TranslateLanguage.LabelTasksToDo[TranslateLanguage.iLanguage];

        /// <summary>
        /// ToDo
        /// </summary>
        public string LabelToDo
        {
            get => labelToDo;
            set => Set(ref labelToDo, value);
        }

        private string labelInPr = TranslateLanguage.LabelTasksInProgress[TranslateLanguage.iLanguage];

        /// <summary>
        /// InProgress
        /// </summary>
        public string LabelInPr
        {
            get => labelInPr;
            set => Set(ref labelInPr, value);
        }

        private string labelDone = TranslateLanguage.LabelTasksDone[TranslateLanguage.iLanguage];

        /// <summary>
        /// Done
        /// </summary>
        public string LabelDone
        {
            get => labelDone;
            set => Set(ref labelDone, value);
        }

        private string labelTitleThree = TranslateLanguage.LabelTasksTitleThree[TranslateLanguage.iLanguage];

        /// <summary>
        /// Write a note
        /// </summary>
        public string LabelTitleThree
        {
            get => labelTitleThree;
            set => Set(ref labelTitleThree, value);
        }

        private string labelTitleFour = TranslateLanguage.LabelTasksTitleFour[TranslateLanguage.iLanguage];

        /// <summary>
        /// Для чего это нужно сделать/For what
        /// </summary>
        public string LabelTitleFour
        {
            get => labelTitleFour;
            set => Set(ref labelTitleFour, value);
        }

        #endregion

        #region ToolTips 

        private string saveNoteTT;

        /// <summary>
        /// Save note
        /// </summary>
        public string SaveNoteTT
        {
            get => TranslateLanguage.SaveNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref saveNoteTT, value);
        }

        private string addNoteTT;

        /// <summary>
        /// Add note
        /// </summary>
        public string AddNoteTT
        {
            get => TranslateLanguage.AddNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref addNoteTT, value);
        }

        private string delNoteTT;

        /// <summary>
        /// Remove note 
        /// </summary>
        public string DelNoteTT
        {
            get => TranslateLanguage.DelNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref delNoteTT, value);
        } 

        private string transferNoteTT;

        /// <summary>
        /// Transfer note
        /// </summary>
        public string TransferNoteTT
        {
            get => TranslateLanguage.TransferNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref transferNoteTT, value);
        }

        private string clearNoteTT;

        /// <summary>
        /// Clear note 
        /// </summary>
        public string ClearNoteTT
        {
            get => TranslateLanguage.ClearNoteTT[TranslateLanguage.iLanguage];
            set => Set(ref clearNoteTT, value);
        }

        #endregion

        private Note selectedNote;

        public Note SelectedNote
        {
            get
            {
                return selectedNote;
            }
            set
            {
                this.ChangeControlVisibility = Visibility.Visible;
                Set(ref selectedNote, value);
            }
        }

        private Note readSelectedNote;

        public Note ReadSelectedNote
        {
            get => readSelectedNote;
            set
            {
                Set(ref readSelectedNote, value);
            }
        }

        public ObservableCollection<Note> NotesToDo { get; set; }

        public ObservableCollection<Note> NotesInProgress { get; set; }
        public ObservableCollection<Note> NotesDone { get; set; }

        public static string pName;

        public string PName
        {
            get => pName;
            set => Set(ref pName, value);
        }

        public static string tName;

        public string TName
        {
            get => tName;
            set => Set(ref tName, value);
        }

        private Visibility _Visibility = Visibility.Collapsed;

        /// <summary>
        /// Visibiliity save button
        /// </summary>
        public Visibility ChangeControlVisibility
        {
            get { return _Visibility; }
            set => Set(ref _Visibility, value);
        }

        private Visibility _VisibilityWriteNote = Visibility.Collapsed;

        /// <summary>
        /// Visibility input
        /// </summary>
        public Visibility ChangeControlVisibilityWriteNote
        {
            get { return _VisibilityWriteNote; }
            set => Set(ref _VisibilityWriteNote, value);
        }

        #region Commands

        #region Save

        public static ICommand SaveNote { get; set; }
        private bool CanSaveNoteExecute(object p) => MainWindowModel.IsConnectedToLocalServer;
        private void OnSaveNoteExecuted(object p)
        {
            Model.EditNote(MainWindowViewModel.db, NotesToDo, toDoTable).GetAwaiter();
            //Model.AddNotesToDB(MainWindowViewModel.db, NotesToDo, toDoTable).GetAwaiter();
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
                              Model.RemoveNoteFromDB(MainWindowViewModel.db, note, toDoTable.ProjectId, "TODO").GetAwaiter();
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
                                Model.TransferTo(MainWindowViewModel.db, note, inProgressTable, doneTable, toDoTable).GetAwaiter();
                                //Model.RemoveNoteFromDB(MainWindowViewModel.db, note, toDoTable.ProjectId, "TODO").GetAwaiter();
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
                              Model.RemoveNoteFromDB(MainWindowViewModel.db, note, inProgressTable.ProjectId, "INPROGRESS").GetAwaiter();
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
                                Model.TransferTo(MainWindowViewModel.db, note, doneTable, inProgressTable, toDoTable).GetAwaiter();
                                //Model.RemoveNoteFromDB(MainWindowViewModel.db, note, inProgressTable.ProjectId, "INPROGRESS").GetAwaiter();
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
                              Model.RemoveNoteFromDB(MainWindowViewModel.db, note, doneTable.ProjectId, "DONE").GetAwaiter();
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

        public TasksViewModel()
        {   
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
                            if (pName == p.ProjectName && tName == p.MasterName)
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

            SaveNote = new LambdaCommand(OnSaveNoteExecuted, CanSaveNoteExecute);

            NotesToDo = new ObservableCollection<Note>
            {

            };
            NotesInProgress = new ObservableCollection<Note>
            {

            };
            NotesDone = new ObservableCollection<Note>
            {

            };


            //todo: Color Notes

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

            if (MainWindowModel.IsConnectedToLocalServer == true)  // checking the connection to the server
            {

                // Filling collections

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

            }
        }
    }
}
