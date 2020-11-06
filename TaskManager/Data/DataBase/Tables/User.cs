using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data.DataBase.Tables
{
    public class User
    {
        /// <summary>
        /// Первичный ключ 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Название проекта
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Имя Создателя проекта
        /// </summary>
        public string MasterName { get; set; }

        /// <summary>
        /// Содержимое To Do
        /// </summary>
        public string ToDoContext { get; set; }

        /// <summary>
        /// Предметная область To Do
        /// </summary>
        public string ToDoLilContext { get; set; }
        /// <summary>
        /// Содержимое In Progress
        /// </summary>
        public string InProgressContext { get; set; }

        /// <summary>
        /// Предметная область In Progress
        /// </summary>
        public string InProgressLilContext { get; set; }
        /// <summary>
        /// Содержимое Done
        /// </summary>
        public string DoneContext { get; set; }

        /// <summary>
        /// Предметная область Done
        /// </summary>
        public string DoneLilContext { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
