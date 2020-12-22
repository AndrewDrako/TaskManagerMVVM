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
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Project reference
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Note content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Note little content
        /// </summary>
        public string LContent { get; set; }

        /// <summary>
        /// Connection with ProjectTable
        /// </summary>
        public virtual ProjectTable ProjectTable { get; set; }
    }
}
