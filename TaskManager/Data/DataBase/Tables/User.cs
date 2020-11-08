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
        /// Электронный адрес
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Связь с таблицей ProjectTable
        /// </summary>
        public virtual ICollection<ProjectTable> ProjectTables { get; set; }
    }
}
