﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;

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
            await LoadDataFromDB(db);
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

        //public async Task GetDataFromDB(MyDbContext db, User user, )


    }
}
