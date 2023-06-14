using System;
using DataAccess;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories
{
    public class GroupRepository : IGroupRepository
    {
        public void DeleteGroup(Group c) => GroupDAO.DeleteGroup(c);

        public void SaveGroup(Group c) => GroupDAO.SaveGroup(c);

        public void UpdateGroup(Group c) => GroupDAO.UpdateGroup(c);

        public List<User> GetUsers() => UserDAO.GetUsers();

      
        public Group GetGroupById(int id) => GroupDAO.FindGroupById(id);

        public List<Group> GetGroups() => GroupDAO.GetGroups();
    }
}
