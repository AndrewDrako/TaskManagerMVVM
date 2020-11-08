using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.DataBase.Tables;

namespace TaskManager.Data.DataBase.Base
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DefaultConnection")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectTable> Projects { get; set; }

        public DbSet<ToDoTable> ToDos { get; set; }
        public DbSet<InProgressTable> InProgresses { get; set; }
        public DbSet<DoneTable> Dones { get; set; }
    }
}
