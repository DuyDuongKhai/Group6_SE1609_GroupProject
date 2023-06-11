using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
