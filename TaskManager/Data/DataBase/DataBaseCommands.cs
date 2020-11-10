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
        
        public static void SaveToDoContentDB(MyDbContext db, ToDoTable todo, bool sign) 
        {
            int colSize = TasksViewModel.NotesToDo.Count();
            int colDbSize = MainWindowViewModel.db.ToDos.Count();
            int needSize = 0;
            int[] mas = new int[100];
            bool checker = false;
            int iCounter = 0;
            // Удаляем из БД данные которые у нас есть по этому проекту в этом столбце
            for (int j = 0; j < colDbSize; j++)
            {
                if (TasksViewModel.CurrentProjectId == MainWindowViewModel.db.ToDos.Local[j].Id)
                {
                    MainWindowViewModel.db.ToDos.Remove(MainWindowViewModel.db.ToDos.Local[j]);
                }
            }
            // Добавляем данные в БД которые у нас в колонке 
            for (int i = 0; i < colSize; i++)
            {
                to
            }
            
        }
    }
}
