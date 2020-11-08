using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;
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
            if (TasksViewModel.NotesToDo.Count() > 0)
            {

            }
        }
    }
}
