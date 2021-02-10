using GalaSoft.MvvmLight;

namespace TaskManager.Models
{
    public class AppLanguage : ViewModelBase
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
