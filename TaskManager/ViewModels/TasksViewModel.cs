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

        public ObservableCollection<Note> Notes { get; set; }

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
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {

                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      Note note = new Note();
                      Notes.Insert(0, note);
                      SelectedNote = note;
                  }));
            }
        }
        #endregion

        #endregion

        #region Конструктор

        public TasksViewModel()
        {
            Notes = new ObservableCollection<Note>
            {

            };
        }

        #endregion
    }
}
