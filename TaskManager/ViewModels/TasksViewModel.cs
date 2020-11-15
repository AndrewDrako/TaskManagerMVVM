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
            set => Set(ref _ReadSelectedNote, value);
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

        #region Visibiliity Поля для ввода

        //private Visibility _VisibilityRead = Visibility.Visible;

        //public Visibility ChangeControlVisibilityRead
        //{
        //    get { return _VisibilityRead; }
        //    set
        //    {
        //        Set(ref _VisibilityRead, value);
        //    }
        //}

        #endregion

        #region Commands

        #region Сохранение заметки

        public ICommand SaveNote { get; }
        private bool CanSaveNoteExecute(object p) => true;
        private void OnSaveNoteExecuted(object p)
        {
            try
            {
                var todos = MainWindowViewModel.db.ToDos.ToList();
                bool isContains = false; // Чекер для проверки измененных заметок To Do
                foreach (var t in todos)
                {
                    for (int i = 0; i < NotesToDo.Count(); i++)  // Ищем элемент который есть в БД , но отсутсвует в коллекции заметок
                    {
                        if (t.Content == NotesToDo[i].Content && t.LContent == NotesToDo[i].Target)  // Если очередной элемент из Бд присуствует в текущей коллекции, то чекер становиться true, и прекращаем искать
                        {
                            isContains = true;
                            break;
                        }
                    }
                    if (isContains == false)  // Усли очередной эл-нт из бд отсуствует в текущей коллекции, то мы его удаляем из бд
                    {
                        MainWindowViewModel.db.ToDos.Attach(t);
                        MainWindowViewModel.db.ToDos.Remove(t);
                        MainWindowViewModel.db.SaveChanges();
                        //todos = MainWindowViewModel.db.ToDos.ToList();
                        
                    }
                    isContains = false;  // Ставим чекер false, чтобы продолжить цикл
                }
                todos = MainWindowViewModel.db.ToDos.ToList();
                bool checker = false;
                for (int i = 0; i < NotesToDo.Count(); i++)
                {
                    foreach (var t in todos)
                    {
                        if (NotesToDo[i].Content == t.Content && NotesToDo[i].Target == t.LContent)
                        {
                            checker = true;
                            break;
                        }
                    }
                    if (NotesToDo[i].Content != "" && checker == false)
                    {
                        toDoTable.Content = NotesToDo[i].Content;
                        toDoTable.LContent = NotesToDo[i].Target;
                        MainWindowViewModel.db.ToDos.Attach(toDoTable);
                        MainWindowViewModel.db.ToDos.Add(toDoTable);
                        MainWindowViewModel.db.SaveChanges();
                        todos = MainWindowViewModel.db.ToDos.ToList();
                    }
                    checker = false;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка возникла при сохранении заметок To Do");
            }


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
                      if (this.ChangeControlVisibility == Visibility.Collapsed)
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
                          try
                          {
                              var todos = MainWindowViewModel.db.ToDos.ToList();
                              foreach (var t in todos)
                              {
                                  if (t.ProjectId == toDoTable.ProjectId)
                                  {
                                      if (note.Content == t.Content && note.Target == t.LContent)
                                      {
                                          MainWindowViewModel.db.ToDos.Attach(t);
                                          MainWindowViewModel.db.ToDos.Remove(t);
                                          MainWindowViewModel.db.SaveChanges();
                                          break;
                                      }
                                  }
                              }
                          }
                          catch
                          {
                              MessageBox.Show("Проблема возникла при удалении заметки из To Do");
                          }

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
                            try
                            {
                                var todos = MainWindowViewModel.db.ToDos.ToList();
                                foreach (var t in todos)
                                {
                                    if (t.ProjectId == toDoTable.ProjectId)
                                    {
                                        if (note.Content == t.Content && note.Target == t.LContent)
                                        {
                                            MainWindowViewModel.db.ToDos.Attach(t);
                                            MainWindowViewModel.db.ToDos.Remove(t);
                                            MainWindowViewModel.db.SaveChanges();
                                            break;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Проблема возникла при переносе заметки из To Do");
                            }

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

            #region For ToDo
            toDoTable = new ToDoTable();
            try
            {
                var projects = MainWindowViewModel.db.Projects.ToList();  // Выгружаем данные из бд в массив
                foreach (var p in projects)
                {
                    if (_PName == p.ProjectName && _TName == p.MasterName)
                    {
                        toDoTable.ProjectId = p.Id;
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка возникла в консрукторе бд для To DO");
            }

            #endregion

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

            #region Заполение To DO
            try
            {
                var todos = MainWindowViewModel.db.ToDos.ToList();
                foreach (var t in todos)
                {
                    if (t.ProjectId == toDoTable.ProjectId)
                    {
                        Note note = new Note();
                        note.Content = t.Content;
                        note.Target = t.LContent;
                        NotesToDo.Add(note);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка произошла при заполнении коллеции TODO");
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
