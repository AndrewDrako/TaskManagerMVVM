using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;
using TaskManager.Models;
using TaskManager.ViewModels;

namespace TaskManager.Data.DataBase
{
    public class DataBaseCommands
    {
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
                MessageBox.Show("Не удается найти локальный сервер на вашем ПК\n");
                MainWindowModel.IsConnectedToLocalServer = false;
                Window mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();
            }
        }
    }
}
