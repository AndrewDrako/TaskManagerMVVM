using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data.DataBase.Tables
{
    public class DoneTable
    {
        /// <summary>
        /// Первичный ключ 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Код проекта
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Содержимое заметки
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Для чего нужна заметка
        /// </summary>
        public string LContent { get; set; }

        /// <summary>
        /// связь с ProjectTable
        /// </summary>
        public virtual ProjectTable ProjectTable { get; set; }
    }
}
