using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    public class Project : ViewModel
    {
        #region Project name

        private string _ProjectName;

        public string ProjectName
        {
            get => _ProjectName;
            set
            {   
                Set(ref _ProjectName, value);
            }
        }

        #endregion

        #region Project creator name

        private string _PersonName;

        public string PersonName
        {
            get => _PersonName;
            set => Set(ref _PersonName, value);
        }

        #endregion

        public Project()
        {
           
        }

        
        
    }
}
