using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Data.DataBase.Base;
using TaskManager.Models;
using TaskManager.ViewModel;

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
            if (MainWindowModel.IsConnectedToLocalServer != false)
            {
                try
                {

                    await Task.Run(() => db.Users.Load());
                    await Task.Run(() => db.Projects.Load());
                    await Task.Run(() => db.ToDos.Load());
                    await Task.Run(() => db.InProgresses.Load());
                    await Task.Run(() => db.Dones.Load());
                    AuthViewModel.canClickOk = true;
                    AuthViewModel.BtnClickOk.RaiseCanExecuteChanged();
                    RegViewModel.BtnClickAccept.RaiseCanExecuteChanged();
                    
                    

                }
                catch
                {
                    MessageBox.Show("Не удается найти или загрузить данные с сервера\n");
                    MainWindowModel.IsConnectedToLocalServer = false;
                    Application.Current.Shutdown();
                }
            }
        }

        public static async Task ConnectWithDB()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;  // Get the connection string
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                await connection.OpenAsync();
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с БД\nПриложение будет запущено, но не сможет сохранять файлы\n");
                MainWindowModel.IsConnectedToLocalServer = false;
            }
        }
    }
}
