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

        #region Home labels

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

        // tooltip add

        public static string[] AddTT = new string[2]
        {
            "Create a project",
            "Создать проект"
        };

        // tooltip remove

        public static string[] RemoveTT = new string[2]
        {
            "Delete a project",
            "Удалить проект"
        };

        // tooltip Select

        public static string[] SelectTT = new string[2]
        {
            "Run this project",
            "Запустить выбранный проект"
        };

        #endregion

        #region Tasks Labels

        // Structure of notes and tasks with title...

        public static string[] LabelTasks1 = new string[2]
        {
            "Structure of notes and tasks with title",
            "Структуру заметок и задач с названием"
        };

        // prepared by the team...

        public static string[] LabelTasks2 = new string[2]
        {
            "prepared by the team",
            "подготовила команда"
        };

        // TO DO label

        public static string[] LabelTasksToDo = new string[2]
        {
            "TO DO",
            "СДЕЛАТЬ"
        };

        // IN Progress label

        public static string[] LabelTasksInProgress = new string[2]
        {
            "IN PROGRESS",
            "В ПРОЦЕССЕ"
        };

        // DONE label

        public static string[] LabelTasksDone = new string[2]
        {
            "DONE",
            "ГОТОВО"
        };

        // write a note label

        public static string[] LabelTasks3 = new string[2]
        {
            "Write a note label",
            "Напишите заметку"
        };

        // Why do you need to do it

        public static string[] LabelTasks4 = new string[2]
        {
            "Why do you need to do it",
            "Для чего это нужно сделать"
        };

        // Save note tooltip

        public static string[] SaveNoteTT = new string[2]
        {
            "Save all notes",
            "Сохранить заметки"
        };

        // Remove note tooltip

        public static string[] DelNoteTT = new string[2]
        {
            "Delete a note",
            "Удалить заметку"
        };

        // Add note tooltip

        public static string[] AddNoteTT = new string[2]
        {
            "Create a note",
            "Создать заметку"
        };

        // Transfer note tooltip

        public static string[] TransferNoteTT = new string[2]
        {
            "Transfer to next state",
            "Переместить в следущее положение"
        };

        // Clear note tooltip

        public static string[] ClearNoteTT = new string[2]
        {
            "Clear done note",
            "Очистить выполненную задачу"
        };

        #endregion

        #region Settings Labels

        // Language label

        public static string[] LabelSettings1 = new string[2]
        {
            "Language",
            "Язык"
        };

        // Add colors label

        public static string[] LabelSettings2 = new string[2]
        {
            "Change theme",
            "Изменить тему"
        };

        // Button apply label

        public static string[] LabelSettingsButton = new string[2]
        {
            "Apply",
            "Применить"
        };

        #endregion

        #region Account Labels

        // button log out label

        public static string[] LabelAccBtn1 = new string[2]
        {
            "Log out",
            "Выйти"
        };

        // button create a new label

        public static string[] LabelAccBtn2 = new string[2]
        {
            "Create a new",
            "Создать новый"
        };

        #endregion

        #region Help Labels

        // How to contact me

        public static string[] LabelHelp1 = new string[2]
        {
            "How to contact me?",
            "Как связаться со мной?"
        };

        // What is this app about

        public static string[] LabelHelp2 = new string[2]
        {
            "What is this app about?",
            "О чем это приложение?"
        };

        #endregion

        #region RegWindow Labels

        // Enter email label

        public static string[] RegLabelEnterEmail = new string[2]
        {
            "Enter your email",
            "Введите свой электронный адрес"
        };

        // Enter nickname

        public static string[] RegLabelEnterNickname = new string[2]
        {
            "What is your nickname",
            "Придумайте имя пользователя"
        };

        // Enter password

        public static string[] RegLabelEnterPassword = new string[2]
        {
            "Your password",
            "Придумайте пароль"
        };

        // Enter password x1

        public static string[] RegLabelEnterPassword1 = new string[2]
        {
            "Enter password",
            "Введите пароль"
        };

        // Enter password x2

        public static string[] RegLabelEnterPassword2 = new string[2]
        {
            "Repeat password",
            "Повторите пароль"
        };

        // Enter Key

        public static string[] RegLabelKey = new string[2]
        {
            "Check your mail and enter the key from the letter",
            "Проверьте почту и введите ключ из письма"
        };



        // Button content

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

        // Enter username

        public static string[] AuthLabelEnterUsername = new string[2]
        {
            "Enter your nickname",
            "Введите имя пользователя"
        };

        // Enter password
        // same as RegLabelEnterPassword1

        // Button content register

        public static string[] AuthLabelRegister = new string[2]
        {
            "Register",
            "Зарегистрироваться"
        };

        #endregion

        public static int iLanguage;
    }
}
