using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IGroupRepository
    {
        void SaveGroup(Group c);
        Group GetGroupById(int id);
        void DeleteGroup(Group c);
        void UpdateGroup(Group c);
        int GetNextId();

        List<User> GetUsers();
        List<Group> GetGroups();
       
    }
}
