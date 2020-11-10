using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Data.DataBase
{
    public class DataBaseCommands
    {
        public static void LoadDB(MyDbContext db)
        {
            db.Users.Load();
            db.Projects.Load();
            db.ToDos.Load();
        }

        public static void SaveToDb(int i, string s)
        {
            if (s == "ToDo")
            {
                TasksViewModel.toDoTable.Content = TasksViewModel.NotesToDo[i].Content;
                TasksViewModel.toDoTable.LContent = TasksViewModel.NotesToDo[i].Target;
                MainWindowViewModel.db.ToDos.Add(TasksViewModel.toDoTable);
            }
        }

        public static void RemoveFromDb(Note note, string s)
        {
            int j = -1;
            int n = MainWindowViewModel.db.ToDos.Count();
            if (s == "ToDo")
            {
                for (int i = 0; i < n; i++)
                {
                    string ss = MainWindowViewModel.db.ToDos.Local[i].Content;
                    if (note.Target == MainWindowViewModel.db.ToDos.Local[i].LContent && note.Content == MainWindowViewModel.db.ToDos.Local[i].Content)
                    {
                        j = i;
                    }
                }
                if (j >= 0)
                {
                    MainWindowViewModel.db.ToDos.Remove(MainWindowViewModel.db.ToDos.Local[j]);
                    MainWindowViewModel.db.SaveChanges();
                }
            }
        }
        // Содержит ли Бд такую запись
        public static bool IsContain(int ii, string ss)
        {
            List<int> mas = new List<int>(0);
            //int counter = 0;
            int temp = 0;
            int dbToDoCount = MainWindowViewModel.db.ToDos.Count();
            //int temp;
            // Отобрали индексы таблицы записи которая соответсвует нашему проекту
            for (int j = 0; j < dbToDoCount; j++)
            {
                //string s = MainWindowViewModel.db.ToDos.Local[j].Content;
                //int id = MainWindowViewModel.db.ToDos.Local[j].Id;

                //temp = MainWindowViewModel.db.ToDos.Local[j].ProjectId;
                if (TasksViewModel.CurrentProjectId == temp)
                {
                    mas.Add(j);
                }
            }

            // Ищем запись
            int k = 0;
            int n = mas.Count;
            for (int i = 0; i < n; i++)
            {
                if (TasksViewModel.NotesToDo[ii].Content == MainWindowViewModel.db.ToDos.Local[mas[i]].Content)
                {
                    // Нашли запись точь в точь
                    return true;
                }
                else
                {
                    k++;
                }
            }
            if (k == mas.Count)
            {
                // Не нашли запись в Бд
                return false;
            }

            return false;

        }
        
        public static void SaveToDoContentDB(bool sign) 
        {
            int colSize = TasksViewModel.NotesToDo.Count();
            int colDbSize = MainWindowViewModel.db.ToDos.Count();
            for (int i = 0; i < colSize; i++)
            {
                if (IsContain(i, "ToDo") == false)
                {
                    SaveToDb(i, "ToDo");
                    MainWindowViewModel.db.SaveChanges();
                }
            }
        }
    }
}
