using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;
using TaskManager.ViewModel;
using TaskManager.Views.UserControls;

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
        public static User FindUser(MyDbContext myDbContext, int id)
        {
            var users = myDbContext.Users.ToList();
            foreach (var ur in users)
            {
                if (ur.Id == id)
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

        public static async Task RemoveProjectFromDB(MyDbContext myDbContext, Project project)
        {
            try
            {
                var projects = myDbContext.Projects.ToList();
                foreach (var p in projects)
                {
                    if (p.ProjectName == project.ProjectName)
                    {
                        await Task.Run(() => myDbContext.Projects.Attach(p));
                        await Task.Run(() => myDbContext.Projects.Remove(p));
                        await Task.Run(() => myDbContext.SaveChanges());
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
        public static async Task AddProjectToDB(MyDbContext myDbContext, Project project, ProjectTable projectTable, object obj)
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
                    await Task.Run(() => myDbContext.Projects.Attach(projectTable));
                    await Task.Run(() => myDbContext.Projects.Add(projectTable));
                    await Task.Run(() => myDbContext.SaveChanges());
                    projects = myDbContext.Projects.ToList();
                }
                MainViewModel.tasks = new Tasks(); // Открытие tasks
                MainViewModel.SecondButtonClick.Execute(obj);
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
        public static async Task RemoveNoteFromDB(MyDbContext myDbContext, Note note, int projectId, string s)
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
                                await Task.Run(() => myDbContext.ToDos.Attach(t));
                                await Task.Run(() => myDbContext.ToDos.Remove(t));
                                await Task.Run(() => myDbContext.SaveChanges());
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
                                await Task.Run(() => myDbContext.InProgresses.Attach(inp));
                                await Task.Run(() => myDbContext.InProgresses.Remove(inp));
                                await Task.Run(() => myDbContext.SaveChanges());
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
                                await Task.Run(() => myDbContext.Dones.Attach(d));
                                await Task.Run(() => myDbContext.Dones.Remove(d));
                                await Task.Run(() => myDbContext.SaveChanges());
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
        public static async Task TransferTo(MyDbContext myDbContext, Note note , InProgressTable inProgressTable)
        {
            try
            {
                bool checker = false;
                var inprogresses = myDbContext.InProgresses.ToList();
                foreach (var inp in inprogresses)
                {
                    if (inProgressTable.ProjectId == inp.ProjectId)
                    {
                        if (inp.Content == note.Content && inp.LContent == note.Target)
                        {
                            checker = true;
                            break;
                        }
                    }
                }
                if (checker == false)
                {
                    inProgressTable.Content = note.Content;
                    inProgressTable.LContent = note.Target;
                    await Task.Run(() => myDbContext.InProgresses.Attach(inProgressTable));
                    await Task.Run(() => myDbContext.InProgresses.Add(inProgressTable));
                    await Task.Run(() => myDbContext.SaveChanges());
                }
            }
            catch
            {
                MessageBox.Show("Ошибка возникла при переносе заметки в In Progress");
            }
            await Task.Run(() => RemoveNoteFromDB(myDbContext, note, inProgressTable.ProjectId, "TODO"));
        }

        /// <summary>
        /// Transfer note to the Done
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="note"></param>
        /// <param name="doneTable"></param>
        public static async Task TransferTo(MyDbContext myDbContext, Note note, DoneTable doneTable)
        {
            try
            {
                bool checker = false;
                var dones = myDbContext.Dones.ToList();
                foreach (var d in dones)
                {
                    if (doneTable.ProjectId == d.ProjectId)
                    {
                        if (d.Content == note.Content && d.LContent == note.Target)
                        {
                            checker = true;
                            break;
                        }
                    }
                }
                if (checker == false)
                {
                    doneTable.Content = note.Content;
                    doneTable.LContent = note.Target;
                    await Task.Run(() => myDbContext.Dones.Attach(doneTable));
                    await Task.Run(() => myDbContext.Dones.Add(doneTable));
                    await Task.Run(() => myDbContext.SaveChanges());
                }
            }
            catch
            {
                MessageBox.Show("Проблема возникла при переносе(добавлении) заметки из In Progress");
            }
            await Task.Run(() => RemoveNoteFromDB(myDbContext, note, doneTable.ProjectId, "INPROGRESS"));
        }

        /// <summary>
        /// Change note 
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="NotesToDo"></param>
        /// <param name="projectId"></param>
        public static async Task EditNote(MyDbContext myDbContext, ObservableCollection<Note> NotesToDo, ToDoTable toDoTable)
        {
            try
            {
                int projectId = toDoTable.ProjectId;
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
                            await Task.Run(() => myDbContext.ToDos.Attach(t));
                            await Task.Run(() => myDbContext.ToDos.Remove(t));
                            await Task.Run(() => myDbContext.SaveChanges());
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
            await Task.Run(() => AddNotesToDB(myDbContext, NotesToDo, toDoTable));
        }

        /// <summary>
        /// Add note to data base
        /// </summary>
        /// <param name="myDbContext"></param>
        /// <param name="NotesToDo"></param>
        /// <param name="toDoTable"></param>
        public static async Task AddNotesToDB(MyDbContext myDbContext, ObservableCollection<Note> NotesToDo, ToDoTable toDoTable)
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
                        await Task.Run(() => myDbContext.ToDos.Attach(toDoTable));
                        await Task.Run(() => myDbContext.ToDos.Add(toDoTable));
                        await Task.Run(() => myDbContext.SaveChanges());
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
