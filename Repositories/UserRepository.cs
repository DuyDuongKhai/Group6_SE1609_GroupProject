using BusinessObject.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository


    {
        public void DeleteUser(User c) => UserDAO.DeleteUser(c);
        

        public void SaveUser(User c) => UserDAO.SaveUser(c);

        public void UpdateUser(User c) => UserDAO.UpdateUser(c);

        public User GetUserById(int id) => UserDAO.FindUserById(id);
        public User GetUserByEmail(string email) => UserDAO.FindUserByEmail(email);

      
        public List<User> GetUsers() => UserDAO.GetUsers();
        public List<User> GetAllGroupAdmin() => UserDAO.GetAllGroupAdmin();
        public int GetNextUserId()
        {
            int nextUserId = UserDAO.GetNextUserId();
            return nextUserId;
        }
        public List<Group> SearchGroups(string keyword)
        {
            return UserDAO.SearchGroups(keyword);
        }

    }
}
