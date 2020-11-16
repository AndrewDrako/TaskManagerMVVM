using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;
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
            //await Task.Run(() => db = new MyDbContext());
            await Task.Run(() => db.Users.Load());
            await Task.Run(() => db.Projects.Load());
            await Task.Run(() => db.ToDos.Load());
            await Task.Run(() => db.InProgresses.Load());
            await Task.Run(() => db.Dones.Load());
            MessageBox.Show("Соеденение с Бд завершено");
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
