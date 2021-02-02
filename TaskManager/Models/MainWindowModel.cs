using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager.Models
{
    internal class MainWindowModel
    {
        /// <summary>
        /// bool variable which is responsible for Tasks activity as soon as the project is created
        /// </summary>
        public static bool IsTasksNotEmpty;

        /// <summary>
        /// bool variable that is responsible for connecting to the server
        /// </summary>
        public static bool IsConnectedToLocalServer = true;

        /// <summary>
        /// Basic theme number
        /// </summary>
        public static int iTheme = 0;

        /// <summary>
        /// the method that prints in the txt language of the application
        /// </summary>
        /// <param name="s"></param>
        public static async Task PrintLanguageKey(string s)
        {
            string filename = "language_key.txt";
            string path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + "/Files")) // if there is no folder - create
            {
                await Task.Run(() => Directory.CreateDirectory(path + "/Files"));
            }
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.OpenOrCreate))
            {
                // convert the string to bytes
                byte[] array = System.Text.Encoding.Default.GetBytes(s);
                // writing a byte array to a file
                await Task.Run(() => fstream.Write(array, 0, array.Length));
            }
        }

        /// <summary>
        /// Method reads language from txt
        /// </summary>
        /// <returns></returns>
        public static int ReadLanguageKey()
        {
            string filename = "language_key.txt";
            string path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + "/Files")) // if there is no folder - create
            {
                Directory.CreateDirectory(path + "/Files");
            }
            
            string textFromFile = "English";
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.OpenOrCreate))
            {

                // convert the string to bytes
                byte[] array = new byte[fstream.Length];
                // read data
                fstream.Read(array, 0, array.Length);
                // decode bytes to string
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
                else
                {
                    return 0;
                }
            }
            
        }

        /// <summary>
        /// the method that prints in the txt theme of the application
        /// </summary>
        /// <param name="s"></param>
        public static async Task PrintThemeKey(string s)
        {
            string filename = "theme_key.txt";
            string path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + "/Files")) // if there is no folder - create
            {
                 await Task.Run(() => Directory.CreateDirectory(path + "/Files"));
            }
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.OpenOrCreate))
            {
                // convert the string to bytes
                byte[] array = System.Text.Encoding.Default.GetBytes(s);
                // writing a byte array to a file
                await Task.Run(() => fstream.Write(array, 0, array.Length));
            }
        }

        /// <summary>
        /// Method reads theme from txt
        /// </summary>
        /// <returns></returns>
        public static int ReadThemeKey()
        {
            string filename = "theme_key.txt";
            string path = Directory.GetCurrentDirectory();
            int result = 0;
            if (!Directory.Exists(path + "/Files")) // if there is no folder - create
            {
                Directory.CreateDirectory(path + "/Files");
            }

            string textFromFile;
            using (FileStream fstream = new FileStream(path + "/Files/" + filename, FileMode.OpenOrCreate))
            {

                // convert the string to bytes
                byte[] array = new byte[fstream.Length];
                // read data
                fstream.Read(array, 0, array.Length);
                // decode bytes into a string
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
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;  // get application resource dictionaries

                Application.Current.Resources.Clear();  // clear resources
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);  // add our resource dictionary
            }
            catch
            {
                MessageBox.Show("Не удалось подключить темы приложения");
            }

            return result;
        }
    }
}
