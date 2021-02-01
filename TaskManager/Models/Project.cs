using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    public class Project : ViewModel
    { 
        private string projectName;

        public string ProjectName
        {
            get => projectName;
            set
            {   
                Set(ref projectName, value);
            }
        }

        private string personName;

        public string PersonName
        {
            get => personName;
            set => Set(ref personName, value);
        }

        public Project()
        {
           
        } 
    }
}
