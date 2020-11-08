using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            int n = TasksViewModel.NotesToDo.Count();
            int nn = MainWindowViewModel.db.ToDos.Count();
            bool checker = false;
            if (sign == true)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < nn; j++)
                    {
                        if (TasksViewModel.NotesToDo[i].Content == MainWindowViewModel.db.ToDos.Local[j].Content)
                        {
                            if (i == n - 1)
                            {
                                checker = true;
                                break;
                            }
                            i++;
                            j = -1;
                        }
                    }
                    if (checker == false)
                    {
                        todo.Content = TasksViewModel.NotesToDo[i].Content;
                        todo.LContent = TasksViewModel.NotesToDo[i].Target;
                        MainWindowViewModel.db.ToDos.Add(todo);
                        MainWindowViewModel.db.SaveChanges();
                    }
                }
            }
        }
    }
}
