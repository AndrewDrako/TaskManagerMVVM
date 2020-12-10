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

        private string _Label1 = "How to contact me";

        public string Label1 
        { 
            get => _Label1; 
            set => Set(ref _Label1, value); 
        }

        #endregion

        #region Design

        private string _BackgroundImg;
        private string _FontColor;
        private string _BtnColor;
        private string _PageColor;
        private string _PageColor2;


        /// <summary>
        /// Background image
        /// </summary>
        public string BackgroundImg
        {
            get => ChangeAppTheme.BackgroundImg[ChangeAppTheme.iTheme];
            set => Set(ref _BackgroundImg, value);
        }

        /// <summary>
        /// Page color
        /// </summary>
        public string PageColor
        {
            get => ChangeAppTheme.PageColor[ChangeAppTheme.iTheme];
            set => Set(ref _PageColor, value);
        }



        /// <summary>
        /// Page color second
        /// </summary>
        public string PageColor2
        {
            get => ChangeAppTheme.PageColor2[ChangeAppTheme.iTheme];
            set => Set(ref _PageColor2, value);
        }

        /// <summary>
        /// Font color
        /// </summary>
        public string FontColor
        {
            get => ChangeAppTheme.FontColor[ChangeAppTheme.iTheme];
            set => Set(ref _FontColor, value);
        }

        /// <summary>
        /// Buttons color
        /// </summary>
        public string BtnColor
        {
            get => ChangeAppTheme.BtnColor[ChangeAppTheme.iTheme];
            set => Set(ref _BtnColor, value);
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
