using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager.Models
{
    internal class MainWindowModel
    {

        /// <summary>
        /// bool переменная которая отвечает за активность TAsks как только создан проект
        /// </summary>
        public static bool IsTasksNotEmpty;

        public static bool IsConnectedToLocalServer = true;

        public static void PrintLanguageKey(string s)
        {
            string filename = "language_key.txt";
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
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(s);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

        public static int ReadLanguageKey()
        {
            string filename = "language_key.txt";
            string path = Directory.GetCurrentDirectory();
            //или ;
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
            if (textFromFile == "English")
            {
                return 0;
            }
            else
            {
                if (textFromFile == "Russian")
                {
                    return 1;
                }
            }
            return 0;

        }
    }
}
