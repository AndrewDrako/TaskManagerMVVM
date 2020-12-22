using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Data.DataBase
{
    public class AsyncCommands
    {
        /// <summary>
        /// Connect to data base and load tables from sql server
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task ConnectToDB(MyDbContext db)
        {
            try
            {
                await Task.Run(() => db.Users.Load());
                await Task.Run(() => db.Projects.Load());
                await Task.Run(() => db.ToDos.Load());
                await Task.Run(() => db.InProgresses.Load());
                await Task.Run(() => db.Dones.Load());
                AuthWindowViewModel._CanClickOk = true;
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
