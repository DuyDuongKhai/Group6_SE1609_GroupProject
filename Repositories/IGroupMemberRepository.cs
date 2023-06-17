using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Repositories
{
    public interface IGroupMemberRepository
    {
        void SaveGroupMember(GroupMember c);
        GroupMember GetGroupMemberById(int id);
        void DeleteGroupMember(GroupMember c);
        void UpdateGroupMember(GroupMember c);

        List<User> GetUsers();
        List<Group> GetGroups();
        List<GroupMember> GetGroupMembers();
        List<User> GetMemberFromGroup(int groupId);
        void CreateGroupMemberAdmin(GroupMember c);

    }
}
