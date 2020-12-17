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

        public static int iTheme = 0;
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

        public static void PrintThemeKey(string s)
        {
            string filename = "theme_key.txt";
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

        public static int ReadThemeKey()
        {
            string filename = "theme_key.txt";
            string path = Directory.GetCurrentDirectory();
            int result = 0;
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
            string style;
            if (textFromFile.IndexOf("Light") >= 0)
            {
                style = "Design/Themes/Light";
                result = 1;
            }
            else
            {
                if (textFromFile.IndexOf("Custom") >= 0)
                {
                    style = "Design/Themes/Custom";
                    result = 0;
                }
                else
                {
                    if (textFromFile.IndexOf("Dark") >= 0)
                    {
                        style = "Design/Themes/Dark";
                        result = 2;
                    }
                    else
                    {
                        style = "Design/Themes/Custom";
                        result = 0;
                    }
                }
            }
            try
            {
                var uri = new Uri(style + ".xaml", UriKind.Relative);
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;

                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            catch
            {
                MessageBox.Show("Не удалось подключить темы приложения");
            }

            return result;
        }
    }
}
