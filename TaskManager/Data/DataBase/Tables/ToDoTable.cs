
namespace TaskManager.Data.DataBase.Tables
{
    public class ToDoTable
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
        /// Note main content
        /// </summary>
        public string LContent { get; set; }

        /// <summary>
        /// Connection with ProjectTable
        /// </summary>
        public virtual ProjectTable ProjectTable { get; set; }
    }
}
