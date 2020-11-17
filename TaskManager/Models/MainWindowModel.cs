using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    internal class MainWindowModel
    {
        // bool переменная которая отвечает за активность TAsks как только создан проект

        public static bool IsTasksNotEmpty; 

        public static void PrintLanguageKey(string s)
        {
            using (FileStream fstream = new FileStream(@"D:\language_key.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(s);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }

        public static int ReadLanguageKey()
        {
            string textFromFile;
            using (FileStream fstream = File.OpenRead(@"D:\language_key.txt"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                textFromFile = System.Text.Encoding.Default.GetString(array);
                
            }
            if (textFromFile == "English\r\n")
            {
                return 0;
            }
            else
            {
                if (textFromFile == "Russian\r\n")
                {
                    return 1;
                }
            }
            return 0;

        }
    }
}
