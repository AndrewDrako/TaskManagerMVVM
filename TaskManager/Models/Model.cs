using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;

namespace TaskManager.Models
{
    public class Model
    { 
        /// <summary>
        /// Method finds users from dbcontext
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="password"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User FindUser(MyDbContext myDbContext, string password, string userName)
        {
            var users = myDbContext.Users.ToList();
            foreach (var ur in users)
            {
                if (ur.Password == password && ur.UserName == userName)
                {
                    return ur;
                }

            }
            return null;
        }
        
        /// <summary>
        /// Method finds users from dbcontext only with username
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User FindUser(MyDbContext myDbContext, string userName)
        {
            var users = myDbContext.Users.ToList();
            foreach (var ur in users)
            {
                if (ur.UserName == userName)
                {
                    return ur;
                }

            }
            return null;
        }

        /// <summary>
        /// Remove project from data base
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="project"></param>
        public static void RemoveProjectFromDB(MyDbContext myDbContext, Project project)
        {
            try
            {
                var projects = myDbContext.Projects.ToList();
                foreach (var p in projects)
                {
                    if (p.ProjectName == project.ProjectName)
                    {
                        myDbContext.Projects.Attach(p);
                        myDbContext.Projects.Remove(p);
                        myDbContext.SaveChanges();
                    }
                }
            }
            catch
            {
                MessageBox.Show("проблема возникла при удалении проекта");
            }
        }

        /// <summary>
        /// Change project name and creator
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="Projects"></param>
        /// <param name="userId"></param>
        public static void EditProjectName(MyDbContext myDbContext, ObservableCollection<Project> Projects, int userId)
        {
            bool isContains = false; // Чекер для проверки измененных проектов
            bool isCurrentProject = false;
            try
            {
                var projects = myDbContext.Projects.ToList();  // Выгружаем данные из бд в массив
                foreach (var p in projects)  // Идем по массиву
                {
                    if (p.UserId == userId)
                    {
                        for (int i = 0; i < Projects.Count(); i++)  // Ищем элемент который есть в БД , но отсутсвует в коллекции проектов
                        {
                            if (p.UserId == userId)
                            {
                                if (p.ProjectName == Projects[i].ProjectName && p.MasterName == Projects[i].PersonName)  // Если очередной элемент из Бд присуствует в текущей коллекции, то чекер становиться true, и прекращаем искать
                                {
                                    isContains = true;
                                    break;
                                }
                                isCurrentProject = true;
                            }
                        }
                        if (isContains == false && isCurrentProject == true)  // Усли очередной эл-нт из бд отсуствует в текущей коллекции, то мы его удаляем из бд
                        {
                            myDbContext.Projects.Attach(p);
                            myDbContext.Projects.Remove(p);
                            myDbContext.SaveChanges();
                            break;
                        }
                        isContains = false;
                    }// Ставим чекер false, чтобы продолжить цикл
                }

            }
            catch
            {
                MessageBox.Show("Ошибка возникла при редактировании проектов");
            }
        }

        /// <summary>
        /// Add project
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="project"></param>
        /// <param name="projectTable"></param>
        public static void AddProjectToDB(MyDbContext myDbContext, Project project, ProjectTable projectTable)
        {
            try
            {
                bool checker = false; // Проверка на не повторяющииеся проекты
                var projects = myDbContext.Projects.ToList();
                foreach (var p in projects)
                {
                    if (projectTable.UserId == p.UserId && project.ProjectName == p.ProjectName && project.PersonName == p.MasterName)
                    {
                        checker = true;
                        break;
                    }
                }
                if (checker == false)
                {
                    projectTable.ProjectName = project.ProjectName;
                    projectTable.MasterName = project.PersonName;
                    myDbContext.Projects.Attach(projectTable);
                    myDbContext.Projects.Add(projectTable);
                    myDbContext.SaveChanges();
                    projects = myDbContext.Projects.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Исключение возникло при добавлении проекта(выборе)");
            }
        }

        /// <summary>
        /// Remove note from data base
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="note"></param>
        /// <param name="projectId"></param>
        /// <param name="s"></param>
        public static void RemoveNoteFromDB(MyDbContext myDbContext, Note note, int projectId, string s)
        {
            if (s == "TODO")
            {
                try
                {
                    var todos = myDbContext.ToDos.ToList();
                    foreach (var t in todos)
                    {
                        if (t.ProjectId == projectId)
                        {
                            if (note.Content == t.Content && note.Target == t.LContent)
                            {
                                myDbContext.ToDos.Attach(t);
                                myDbContext.ToDos.Remove(t);
                                myDbContext.SaveChanges();
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Проблема возникла при удалении заметки из To Do");
                }
            }
            if (s == "INPROGRESS")
            {
                try
                {
                    var inprogresses = myDbContext.InProgresses.ToList();
                    foreach (var inp in inprogresses)
                    {
                        if (inp.ProjectId == projectId)
                        {
                            if (note.Content == inp.Content && note.Target == inp.LContent)
                            {
                                myDbContext.InProgresses.Attach(inp);
                                myDbContext.InProgresses.Remove(inp);
                                myDbContext.SaveChanges();
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Проблема возникла при удалении заметки из In Progress");
                }
            }
            if(s == "DONE")
            {
                try
                {
                    var dones = myDbContext.Dones.ToList();
                    foreach (var d in dones)
                    {
                        if (d.ProjectId == projectId)
                        {
                            if (note.Content == d.Content && note.Target == d.LContent)
                            {
                                myDbContext.Dones.Attach(d);
                                myDbContext.Dones.Remove(d);
                                myDbContext.SaveChanges();
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Проблема возникла при удалении заметки из Done");
                }
            }
        }
        
        /// <summary>
        /// Transfer note to the In progress
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="note"></param>
        /// <param name="inProgressTable"></param>
        public static void TransferTo(MyDbContext myDbContext, Note note , InProgressTable inProgressTable)
        {
            try
            {
                bool checker = false;
                var inprogresses = myDbContext.InProgresses.ToList();
                foreach (var inp in inprogresses)
                {
                    if (inp.Content == note.Content && inp.LContent == note.Target)
                    {
                        checker = true;
                        break;
                    }
                }
                if (checker == false)
                {
                    inProgressTable.Content = note.Content;
                    inProgressTable.LContent = note.Target;
                    myDbContext.InProgresses.Attach(inProgressTable);
                    myDbContext.InProgresses.Add(inProgressTable);
                    myDbContext.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка возникла при переносе заметки в In Progress");
            }
        }

        /// <summary>
        /// Transfer note to the Done
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="note"></param>
        /// <param name="doneTable"></param>
        public static void TransferTo(MyDbContext myDbContext, Note note, DoneTable doneTable)
        {
            try
            {
                bool checker = false;
                var dones = myDbContext.Dones.ToList();
                foreach (var d in dones)
                {
                    if (d.Content == note.Content && d.LContent == note.Target)
                    {
                        checker = true;
                        break;
                    }
                }
                if (checker == false)
                {
                    doneTable.Content = note.Content;
                    doneTable.LContent = note.Target;
                    myDbContext.Dones.Attach(doneTable);
                    myDbContext.Dones.Add(doneTable);
                    myDbContext.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Проблема возникла при переносе(добавлении) заметки из In Progress");
            }
        }

        /// <summary>
        /// Change note 
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="NotesToDo"></param>
        /// <param name="projectId"></param>
        public static void EditNote(MyDbContext myDbContext, ObservableCollection<Note> NotesToDo, int projectId)
        {
            try
            {
                var todos = myDbContext.ToDos.ToList();
                bool isContains = false; // Чекер для проверки измененных заметок To Do
                foreach (var t in todos)
                {
                    if (t.ProjectId == projectId)
                    {
                        for (int i = 0; i < NotesToDo.Count(); i++)  // Ищем элемент который есть в БД , но отсутсвует в коллекции заметок
                        {
                            if (t.Content == NotesToDo[i].Content && t.LContent == NotesToDo[i].Target)  // Если очередной элемент из Бд присуствует в текущей коллекции,
                            {                                                                            // то чекер становиться true, и прекращаем искать
                                isContains = true;
                                break;
                            }
                        }
                        if (isContains == false)  // Усли очередной эл-нт из бд отсуствует в текущей коллекции, то мы его удаляем из бд
                        {
                            myDbContext.ToDos.Attach(t);
                            myDbContext.ToDos.Remove(t);
                            myDbContext.SaveChanges();
                            //todos = MainWindowViewModel.db.ToDos.ToList();

                        }
                    }
                    isContains = false;  // Ставим чекер false, чтобы продолжить цикл
                }
            }
            catch
            {
                MessageBox.Show("Исключение возникло при сохранении измененного проекта");
            }
        }

        /// <summary>
        /// Add note to data base
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="NotesToDo"></param>
        /// <param name="toDoTable"></param>
        public static void AddNotesToDB(MyDbContext myDbContext, ObservableCollection<Note> NotesToDo, ToDoTable toDoTable)
        {
            try
            {
                var todos = myDbContext.ToDos.ToList();
                bool checker = false;
                for (int i = 0; i < NotesToDo.Count(); i++)
                {
                    foreach (var t in todos)
                    {
                        if (toDoTable.ProjectId == t.ProjectId && NotesToDo[i].Content == t.Content && NotesToDo[i].Target == t.LContent)
                        {
                            checker = true;
                            break;
                        }
                    }
                    if (NotesToDo[i].Content != "" && checker == false)
                    {
                        toDoTable.Content = NotesToDo[i].Content;
                        toDoTable.LContent = NotesToDo[i].Target;
                        myDbContext.ToDos.Attach(toDoTable);
                        myDbContext.ToDos.Add(toDoTable);
                        myDbContext.SaveChanges();
                        todos = myDbContext.ToDos.ToList();
                    }
                    checker = false;
                }
            }
            catch
            {
                MessageBox.Show("Исключение возникло при добавлении заметки в БД");
            }
        }
    }
}
