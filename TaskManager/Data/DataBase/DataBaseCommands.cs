using System.Data.Entity;
using System.Windows;
using TaskManager.Data.DataBase.Base;
using TaskManager.Models;


namespace TaskManager.Data.DataBase
{
    public class DataBaseCommands
    {
        /// <summary>
        /// Load tables from sql server
        /// </summary>
        /// <param name="db"></param>
        public static void LoadDB(MyDbContext db)
        {
            try
            {
                db.Users.Load();
                db.Projects.Load();
                db.ToDos.Load();
                db.InProgresses.Load();
                db.Dones.Load();
            }
            catch
            {
                MessageBox.Show("Не удается найти загрузить данные с сервера\n");
                MainWindowModel.IsConnectedToLocalServer = false;
                Application.Current.Shutdown();
            }
        }
    }
}
