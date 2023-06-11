using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        void SaveUser(User c);
        User GetUserById(int id);
        void DeleteUser(User c);
        void UpdateUser(User c);

        List<User> GetUsers();
      
    }
}
