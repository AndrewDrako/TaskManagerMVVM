using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Data.DataBase.Base;
using TaskManager.Data.DataBase.Tables;

namespace TaskManager.Models
{
    public class Model
    {
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

        public static void EditProjectName(MyDbContext myDbContext, ObservableCollection<Project> Projects, int userId)
        {
            bool isContains = false; // Чекер для проверки измененных проектов
            bool isCurrentProject = false;
            try
            {
                var projects = myDbContext.Projects.ToList();  // Выгружаем данные из бд в массив
                foreach (var p in projects)  // Идем по массиву
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
                    isContains = false;  // Ставим чекер false, чтобы продолжить цикл
                }

            }
            catch
            {
                MessageBox.Show("Ошибка возникла при редактировании проектов");
            }
        }

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
    }
}
