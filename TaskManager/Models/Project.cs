using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    internal class Project : ViewModel
    {
        #region Имя проекта

        private string _ProjectName;

        public string ProjectName
        {
            get => _ProjectName;
            set => Set(ref _ProjectName, value);
        }

        #endregion

        #region Имя создателея проекта

        private string _PersonName;

        public string PersonName
        {
            get => _PersonName;
            set => Set(ref _PersonName, value);
        }

        #endregion
        
        public Project()
        {
            ProjectName = "";
            PersonName = "";
        }

        
        public static void PrintToTxt()
        {
            string pathLog = @"D:\text1111.txt";
            // true - добавлять данные в конец файла. false - перезаписать файл заново
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathLog, true))
            {
                //file.WriteLine(_ProjectName);
                //file.WriteLine(_PersonName);
            }
        }
    }
}
