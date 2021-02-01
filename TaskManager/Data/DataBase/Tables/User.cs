using System.Collections.Generic;


namespace TaskManager.Data.DataBase.Tables
{
    public class User
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Connection with ProjectTable
        /// </summary>
        public virtual ICollection<ProjectTable> ProjectTables { get; set; }
    }
}
