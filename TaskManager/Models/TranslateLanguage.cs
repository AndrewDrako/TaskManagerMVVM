using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    internal class TranslateLanguage 
    {
        #region MainWindow Labels

        // Button home

        public static string[] LabelHome = new string[2]
        {
            "Home",
            "Главная"
        };

        // Button tasks

        public static string[] LabelTask = new string[2]
        {
            "Tasks",
            "Задачи"
        };

        // button settings

        public static string[] LabelSettings = new string[2]
        {
            "Settings",
            "Настройки"
        };

        // button account

        public static string[] LabelAcc = new string[2]
        {
            "Account",
            "Аккаунт"
        };

        // button help

        public static string[] LabelHelp = new string[2]
        {
            "Help",
            "Помощь"
        };

        #endregion

        #region Home

        // Welcome Label

        public static string[] LabelWelcome = new string[2]
        {
            "Welcome",
            "Добро пожаловать"
        };

        // Selceted project label

        public static string[] LabelSP = new string[2]
        {
            "Selected project",
            "Выбранный проект"
        };

        // Project name label

        public static string[] LabelPN = new string[2]
        {
            "Project name", 
            "Имя проекта"
        };

        // Owner name label

        public static string[] LabelON = new string[2]
        {
            "Owner",
            "Владелец"
        };



        #endregion

        public static int iLanguage;
    }
}
