using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class AuthWindowModel
    {
        public static int Key; 
        public static void PrintKey(string s, string filename)
        {
            //string filename = "authreg_key.txt";
            string path = Directory.GetCurrentDirectory();
            //или ;
            if (!Directory.Exists(path + "/Files")) //если папки нет - создаем
            {
                Directory.CreateDirectory(path + "/Files");
            }
            //FileStream fs = File.Create(path + "/Files/" + filename);
            //StreamWriter writer = new StreamWriter(fs);
            //writer.Write("text");
            //writer.Close();
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(s);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

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

        public static string ReadLastUserName()
        {
            string filename = "last_user_name.txt";
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
            return textFromFile;
        }


    }
}
