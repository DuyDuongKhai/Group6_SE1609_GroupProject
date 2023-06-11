using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        public static List<User> GetUsers()
        {
            var listUsers = new List<User>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listUsers = context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listUsers;
        }
        public static User FindUserById(int Id)
        {
            User c = new User();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.Users.SingleOrDefault(x => x.UserId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public static void SaveUser(User c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Users.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateUser(User c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<User>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteUser(User c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.Users.SingleOrDefault(u => u.UserId == c.UserId);
                    context.Users.Remove(c1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
