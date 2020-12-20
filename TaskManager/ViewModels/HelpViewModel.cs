using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskManager.Infrastructure.Commands;
using TaskManager.Models;
using TaskManager.ViewModels.Base;

namespace TaskManager.ViewModels
{
    internal class HelpViewModel : ViewModel
    {
        #region Labels

        private string _Label1;
        private string _Label2;
        
        /// <summary>
        /// How to contact me
        /// </summary>
        public string Label1 
        { 
            get => TranslateLanguage.LabelHelp1[TranslateLanguage.iLanguage]; 
            set => Set(ref _Label1, value); 
        }

        /// <summary>
        /// What is this app about
        /// </summary>
        public string Label2
        {
            get => TranslateLanguage.LabelHelp2[TranslateLanguage.iLanguage];
            set => Set(ref _Label2, value);
        }

        #endregion

        #region Commands

        // Vk
        public ICommand ButtonClickMyContact1 { get; }

        private bool CanButtonClickMyContact1Execute(object p) => true;

        private void OnButtonClickMyContact1Executed(object p)
        {
            try
            {
                Process.Start("https://vk.com/andrew_drako");
            }
            catch
            {
                MessageBox.Show("Ошибка открытия браузера, попробуйте снова");
            }
        }

        // LinkedIn

        public ICommand ButtonClickMyContact2 { get; }

        private bool CanButtonClickMyContact2Execute(object p) => true;

        private void OnButtonClickMyContact2Executed(object p)
        {
            try
            {
                Process.Start("https://www.linkedin.com/in/andrew-drako-30b8ab193/");
            }
            catch
            {
                MessageBox.Show("Ошибка открытия браузера, попробуйте снова");
            }
        }

        // GitHub

        public ICommand ButtonClickMyContact3 { get; }

        private bool CanButtonClickMyContact3Execute(object p) => true;

        private void OnButtonClickMyContact3Executed(object p)
        {
            try
            {
                Process.Start("https://github.com/AndrewDrako");
            }
            catch
            {
                MessageBox.Show("Ошибка открытия браузера, попробуйте снова");
            }
        }

        // Instagram

        public ICommand ButtonClickMyContact4 { get; }

        private bool CanButtonClickMyContact4Execute(object p) => true;

        private void OnButtonClickMyContact4Executed(object p)
        {
            try
            {
                Process.Start("https://www.instagram.com/andrew_drako/");
            }
            catch
            {
                MessageBox.Show("Ошибка открытия браузера, попробуйте снова");
            }
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
