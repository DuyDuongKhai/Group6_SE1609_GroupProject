using System;
using DataAccess;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories
{
    public class GroupMemberRepository : IGroupMemberRepository
    {
        public void DeleteGroupMember(GroupMember c) => GroupMemberDAO.DeleteGroupMember(c);

        public void SaveGroupMember(GroupMember c) => GroupMemberDAO.SaveGroupMember(c);

        public void UpdateGroupMember(GroupMember c) => GroupMemberDAO.UpdateGroupMember(c);

        public List<User> GetUsers() => UserDAO.GetUsers();

        public List<Group> GetGroups() => GroupDAO.GetGroups();
        public GroupMember GetGroupMemberById(int id) => GroupMemberDAO.FindGroupMemberById(id);

        public List<GroupMember> GetGroupMembers() => GroupMemberDAO.GetGroupMembers();
    
        public List<User> GetMemberFromGroup(int groupId)=> GroupMemberDAO.GetMemberFromGroup(groupId);
        public void CreateGroupMemberAdmin(GroupMember c) => GroupMemberDAO.CreateGroupMemberAdmin(c);

    }
}
