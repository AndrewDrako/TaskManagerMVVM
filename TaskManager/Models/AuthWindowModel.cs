using System;
using System.IO;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class AuthWindowModel
    {
        public static int Key;
        
        /// <summary>
        /// Method prints to txt file a special key
        /// </summary>
        /// <param name="s"></param>
        /// <param name="filename"></param>
        public static async Task PrintKey(string s, string filename)
        {
            string path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + "/Files")) //если папки нет - создаем
            {
                await Task.Run(() => Directory.CreateDirectory(path + "/Files"));
            }
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(s);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

        public static async Task PrintKey(int id, string filename)
        {
            string path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + "/Files")) //если папки нет - создаем
            {
                await Task.Run(() => Directory.CreateDirectory(path + "/Files"));
            }
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = BitConverter.GetBytes(id);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

        /// <summary>
        /// Read key for registratiob or authorization
        /// </summary>
        /// <returns></returns>
        public static int ReadKey()
        {
            string filename = "authreg_key.txt";
            string path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + "/Files")) //если папки нет - создаем
            {
                Directory.CreateDirectory(path + "/Files");
            }

            string textFromFile;
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.OpenOrCreate))
            {

                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                textFromFile = System.Text.Encoding.Default.GetString(array);

            }
            if (textFromFile == "Cannot")
            {
                return 0;
            }
            else
            {
                if (textFromFile == "Can")
                {
                    return 1;
                }
            }
            return 0;
        }

        public static string LastUsername;

        /// <summary>
        /// Read last user name from txt
        /// </summary>
        /// <returns></returns>
        public static int ReadLastUser()
        {
            string filename = "last_user.txt";
            string path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + "/Files")) //если папки нет - создаем
            {
                Directory.CreateDirectory(path + "/Files");
            }

            int textFromFile;
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.OpenOrCreate))
            {

                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                textFromFile = BitConverter.ToInt32(array, 0);

            }
            return textFromFile;
        }


    }
}
