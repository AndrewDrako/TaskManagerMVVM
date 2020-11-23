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
        /// Подключение к БД
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task ConnectToDB(MyDbContext db)
        {
            try
            {
                //await Task.Run(() => db = new MyDbContext());
                await Task.Run(() => db.Users.Load());
                await Task.Run(() => db.Projects.Load());
                await Task.Run(() => db.ToDos.Load());
                await Task.Run(() => db.InProgresses.Load());
                await Task.Run(() => db.Dones.Load());
                AuthWindowViewModel._CanClickOk = true;
            }
            catch
            {
                MessageBox.Show("Не удается найти локальный сервер на вашем ПК\n");
                MainWindowModel.IsConnectedToLocalServer = false;
                Window mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();
            }
        }


        /// <summary>
        /// Загрузка данных из БД
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static async Task LoadDataFromDB(MyDbContext db)
        {
            await Task.Run(() => db.Users.Load());

        }

        /// <summary>
        /// Добавление в БД
        /// </summary>
        /// <param name="db"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task AddToDB(MyDbContext db, User user)
        {
            await Task.Run(() => db.Users.Add(user));
        }

        /// <summary>
        /// Удаление из БД
        /// </summary>
        /// <param name="db"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task RemoveFromDB(MyDbContext db, User user)
        {
            await Task.Run(() => db.Users.Remove(user));
        }
    }
}
