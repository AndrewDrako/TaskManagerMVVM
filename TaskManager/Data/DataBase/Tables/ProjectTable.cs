using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data.DataBase.Tables
{
    public class ProjectTable
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User reference
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Project name
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Creator name
        /// </summary>
        public string MasterName { get; set; }

        /// <summary>
        /// Connection with User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Connection with TodoTable
        /// </summary>
        public virtual ICollection<ToDoTable> ToDoTables { get; set; }

        /// <summary>
        /// Connection with InProgressTable
        /// </summary>
        public virtual ICollection<InProgressTable> InProgressTables { get; set; }

        /// <summary>
        /// Connection with DoneTable
        /// </summary>
        public virtual ICollection<DoneTable> DoneTables { get; set; }
    }
}
