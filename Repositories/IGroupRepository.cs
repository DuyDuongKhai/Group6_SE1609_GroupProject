using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        List<Group> GetGroupsByGroupAdminId(int groupAdminId);
    }
}
