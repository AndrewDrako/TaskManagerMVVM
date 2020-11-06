using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.DataBase.Base;
using TaskManager.ViewModels;

namespace TaskManager.Data.DataBase
{
    public class DataBaseCommands
    {
        public static void LoadDB(MyDbContext db)
        {
            db.Users.Load();
        }
        public static void GetCount(MyDbContext db)
        {
            HomeViewModel._GetCount = db.Users.Count();
        }
    }
}
