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
        /// Первичный ключ 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Код пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Название проекта
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Имя Создателя проекта
        /// </summary>
        public string MasterName { get; set; }

        /// <summary>
        /// Связь с User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Связь с TodoTable
        /// </summary>
        public virtual ICollection<ToDoTable> ToDoTables { get; set; }

        /// <summary>
        /// связь с InProgressTable
        /// </summary>
        public virtual ICollection<InProgressTable> InProgressTables { get; set; }

        /// <summary>
        /// Связь с DoneTable
        /// </summary>
        public virtual ICollection<DoneTable> DoneTables { get; set; }


    }
}
