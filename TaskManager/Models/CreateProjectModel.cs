using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    internal class CreateProjectModel
    {
        public static string[] ProjectName = new string[10];

        public static string[] TeamName = new string[10];

        public static void SetProjectName(string s)
        {
            ProjectName[0] = s;
        }

        public static void SetTeamName(string s)
        {
            TeamName[0] = s;
        }

        public static void PrintToTxt()
        {
            string pathLog = @"D:\text1111.txt";
            // true - добавлять данные в конец файла. false - перезаписать файл заново
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathLog, true))
            {
                file.WriteLine(ProjectName[0] + " / " + TeamName[0]);
            }

        }

    }
}
