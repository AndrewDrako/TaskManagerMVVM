using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
