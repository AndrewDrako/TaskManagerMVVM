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

        /// <summary>
        /// Button home
        /// </summary>
        public static string[] LabelHome = new string[2]
        {
            "Home",
            "Главная"
        };

        /// <summary>
        /// Button tasks
        /// </summary>
        public static string[] LabelTask = new string[2]
        {
            "Tasks",
            "Задачи"
        };

        /// <summary>
        /// Button settings
        /// </summary>
        public static string[] LabelSettings = new string[2]
        {
            "Settings",
            "Настройки"
        };

        /// <summary>
        /// Button account
        /// </summary>
        public static string[] LabelAcc = new string[2]
        {
            "Account",
            "Аккаунт"
        };

        /// <summary>
        /// Button help
        /// </summary>
        public static string[] LabelHelp = new string[2]
        {
            "Help",
            "Помощь"
        };

        #endregion

        #region Home labels

        /// <summary>
        /// Welcome Label
        /// </summary>
        public static string[] LabelWelcome = new string[2]
        {
            "Welcome",
            "Добро пожаловать"
        };

        /// <summary>
        /// Selected project label
        /// </summary>
        public static string[] LabelSP = new string[2]
        {
            "Selected project",
            "Выбранный проект"
        };

        /// <summary>
        /// Project name label
        /// </summary>
        public static string[] LabelPN = new string[2]
        {
            "Project name", 
            "Имя проекта"
        };

        /// <summary>
        /// Owner name label
        /// </summary>
        public static string[] LabelON = new string[2]
        {
            "Owner",
            "Владелец"
        };

        /// <summary>
        /// tooltip add
        /// </summary>
        public static string[] AddTT = new string[2]
        {
            "Create a project",
            "Создать проект"
        };

        /// <summary>
        /// tooltip remove
        /// </summary>
        public static string[] RemoveTT = new string[2]
        {
            "Delete a project",
            "Удалить проект"
        };

        /// <summary>
        /// tooltip select
        /// </summary>
        public static string[] SelectTT = new string[2]
        {
            "Run this project",
            "Запустить выбранный проект"
        };

        #endregion

        #region Tasks Labels

        /// <summary>
        /// Structure of notes and tasks with title...
        /// </summary>
        public static string[] LabelTasks1 = new string[2]
        {
            "Structure of notes and tasks with title",
            "Структуру заметок и задач с названием"
        };

        /// <summary>
        /// prepared by the team...
        /// </summary>
        public static string[] LabelTasks2 = new string[2]
        {
            "prepared by the team",
            "подготовила команда"
        };

        /// <summary>
        /// TO DO label
        /// </summary>
        public static string[] LabelTasksToDo = new string[2]
        {
            "TO DO",
            "СДЕЛАТЬ"
        };

        /// <summary>
        /// IN Progress label
        /// </summary>
        public static string[] LabelTasksInProgress = new string[2]
        {
            "IN PROGRESS",
            "В ПРОЦЕССЕ"
        };

        /// <summary>
        /// DONE label
        /// </summary>
        public static string[] LabelTasksDone = new string[2]
        {
            "DONE",
            "ГОТОВО"
        };

        /// <summary>
        /// write a note label
        /// </summary>
        public static string[] LabelTasks3 = new string[2]
        {
            "Write a note label",
            "Напишите заметку"
        };

        /// <summary>
        /// Why do you need to do it
        /// </summary>
        public static string[] LabelTasks4 = new string[2]
        {
            "Why do you need to do it",
            "Для чего это нужно сделать"
        };

        /// <summary>
        /// Save note tooltip
        /// </summary>
        public static string[] SaveNoteTT = new string[2]
        {
            "Save all notes",
            "Сохранить заметки"
        };

        /// <summary>
        /// Remove note tooltip
        /// </summary>
        public static string[] DelNoteTT = new string[2]
        {
            "Delete a note",
            "Удалить заметку"
        };

        /// <summary>
        /// Add note tooltip
        /// </summary>
        public static string[] AddNoteTT = new string[2]
        {
            "Create a note",
            "Создать заметку"
        };

        /// <summary>
        /// Transfer note tooltip
        /// </summary>
        public static string[] TransferNoteTT = new string[2]
        {
            "Transfer to next state",
            "Переместить в следущее положение"
        };

        /// <summary>
        /// Clear note tooltip
        /// </summary>
        public static string[] ClearNoteTT = new string[2]
        {
            "Clear done note",
            "Очистить выполненную задачу"
        };

        #endregion

        #region Settings Labels

        /// <summary>
        /// Language label
        /// </summary>
        public static string[] LabelSettings1 = new string[2]
        {
            "Language",
            "Язык"
        };

        /// <summary>
        /// Add colors label
        /// </summary>
        public static string[] LabelSettings2 = new string[2]
        {
            "Change theme",
            "Изменить тему"
        };

        /// <summary>
        /// Button apply label
        /// </summary>
        public static string[] LabelSettingsButton = new string[2]
        {
            "Apply",
            "Применить"
        };

        #endregion

        #region Account Labels

        /// <summary>
        /// button log out label
        /// </summary>
        public static string[] LabelAccBtn1 = new string[2]
        {
            "Log out",
            "Выйти"
        };

        /// <summary>
        /// button create a new label
        /// </summary>
        public static string[] LabelAccBtn2 = new string[2]
        {
            "Create a new",
            "Создать новый"
        };

        #endregion

        #region Help Labels

        /// <summary>
        /// How to contact me
        /// </summary>
        public static string[] LabelHelp1 = new string[2]
        {
            "How to contact me?",
            "Как связаться со мной?"
        };

        /// <summary>
        /// What is this app about
        /// </summary>
        public static string[] LabelHelp2 = new string[2]
        {
            "What is this app about?",
            "О чем это приложение?"
        };

        #endregion

        #region RegWindow Labels

        /// <summary>
        /// Enter email label
        /// </summary>
        public static string[] RegLabelEnterEmail = new string[2]
        {
            "Enter your email",
            "Введите свой электронный адрес"
        };

        /// <summary>
        /// Enter nickname
        /// </summary>
        public static string[] RegLabelEnterNickname = new string[2]
        {
            "What is your nickname",
            "Придумайте имя пользователя"
        };

        /// <summary>
        /// Enter password
        /// </summary>
        public static string[] RegLabelEnterPassword = new string[2]
        {
            "Your password",
            "Придумайте пароль"
        };

        /// <summary>
        /// Enter password x1
        /// </summary>
        public static string[] RegLabelEnterPassword1 = new string[2]
        {
            "Enter password",
            "Введите пароль"
        };

        /// <summary>
        /// Enter password x2
        /// </summary>
        public static string[] RegLabelEnterPassword2 = new string[2]
        {
            "Repeat password",
            "Повторите пароль"
        };

        /// <summary>
        /// Enter Key
        /// </summary>
        public static string[] RegLabelKey = new string[2]
        {
            "Check your mail and enter the key from the letter",
            "Проверьте почту и введите ключ из письма"
        };



        // Button contents

        public static string[] RegBtnOk = new string[2]
        {
            "OK",
            "Продолжить"
        };

        public static string[] RegBtnAccept = new string[2]
        {
            "Accept",
            "Подтвердить"
        };

        public static string[] RegBtnLogIn = new string[2]
        {
            "Log In",
            "Войти"
        };

        #endregion

        #region AuthWindow Labels

        /// <summary>
        /// Enter username
        /// </summary>
        public static string[] AuthLabelEnterUsername = new string[2]
        {
            "Enter your nickname",
            "Введите имя пользователя"
        };

        /// <summary>
        /// Button content register
        /// </summary>
        public static string[] AuthLabelRegister = new string[2]
        {
            "Register",
            "Зарегистрироваться"
        };

        #endregion

        /// <summary>
        /// Number of Languages
        /// </summary>
        public static int iLanguage;
    }
}
