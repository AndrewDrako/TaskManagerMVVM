using GalaSoft.MvvmLight;

namespace TaskManager.Models
{
    public class Project : ViewModelBase
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
