using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class HelpViewModel : ViewModel
    {
        #region Labels

        private string _Label1 = "How to contact me";

        public string Label1 
        { 
            get => _Label1; 
            set => Set(ref _Label1, value); 
        }

        #endregion

        #region Commands

        // Vk
        public ICommand ButtonClickMyContact1 { get; }

        private bool CanButtonClickMyContact1Execute(object p) => true;

        private void OnButtonClickMyContact1Executed(object p)
        {
            Process.Start("https://vk.com/andrew_drako");
        }

        // LinkedIn

        public ICommand ButtonClickMyContact2 { get; }

        private bool CanButtonClickMyContact2Execute(object p) => true;

        private void OnButtonClickMyContact2Executed(object p)
        {
            Process.Start("https://www.linkedin.com/in/andrew-drako-30b8ab193/");
        }

        // GitHub

        public ICommand ButtonClickMyContact3 { get; }

        private bool CanButtonClickMyContact3Execute(object p) => true;

        private void OnButtonClickMyContact3Executed(object p)
        {
            Process.Start("https://github.com/AndrewDrako");
        }

        // Instagram

        public ICommand ButtonClickMyContact4 { get; }

        private bool CanButtonClickMyContact4Execute(object p) => true;

        private void OnButtonClickMyContact4Executed(object p)
        {
            Process.Start("https://www.instagram.com/andrew_drako/");
        }

        #endregion

        #region Конструктор

        public HelpViewModel()
        {
            ButtonClickMyContact1 = new LambdaCommand(OnButtonClickMyContact1Executed, CanButtonClickMyContact1Execute);
            ButtonClickMyContact2 = new LambdaCommand(OnButtonClickMyContact2Executed, CanButtonClickMyContact2Execute);
            ButtonClickMyContact3 = new LambdaCommand(OnButtonClickMyContact3Executed, CanButtonClickMyContact3Execute);
            ButtonClickMyContact4 = new LambdaCommand(OnButtonClickMyContact4Executed, CanButtonClickMyContact4Execute);
        }

        #endregion
    }
}
