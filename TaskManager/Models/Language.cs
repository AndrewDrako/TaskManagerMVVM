using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    internal class AppLanguage : ViewModel
    {
        private string language;

        public string Language
        {
            get => language;
            set => Set(ref language, value);
        }

        public AppLanguage()
        {
            
        }
    }
}
