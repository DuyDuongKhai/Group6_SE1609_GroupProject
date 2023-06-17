using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DataAccess
{
    public class GroupMemberDAO
    {
        public static List<GroupMember> GetGroupMembers()
        {
            var listGroupMembers = new List<GroupMember>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listGroupMembers = context.GroupMembers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listGroupMembers;
        }
        public static GroupMember FindGroupMemberById(int Id)
        {
            GroupMember c = new GroupMember();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.GroupMembers.SingleOrDefault(x => x.GroupMemberId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public static List<User> GetMemberFromGroup(int Id)
        {
            List<User> list=new List<User>();
            var group = FindGroupMemberById(Id);
            try
            {
                using (var context = new GroupStudyContext())
                {
                    list = context.GroupMembers
                .Where(gm => gm.GroupId == group.GroupId && gm.User != null)
                .Select(gm => gm.User)
                .ToList();

                }
                if(list.Count==0)
                {
                    throw new Exception($"No user is in the {group.Group.GroupName}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        

        public static void SaveGroupMember(GroupMember c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    if (context.GroupMembers.SingleOrDefault(x => x.UserId == c.UserId) == null)
                    {
                        context.GroupMembers.Add(c);
                        context.SaveChanges();
                    }else
                    {
                        throw new Exception("Already had that member");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void CreateGroupMemberAdmin(GroupMember c)
        {
            try
            {

                using (var context = new GroupStudyContext())
                {
                    var user = context.Users.SingleOrDefault(x=>x.UserId== c.UserId);
                    if (user.Role.Equals("Group Admin"))
                    {
                        context.GroupMembers.Add(c);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Only Group Admin can create group");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateGroupMember(GroupMember c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<GroupMember>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteGroupMember(GroupMember c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.GroupMembers.SingleOrDefault(u => u.GroupId == c.GroupId&& u.UserId == c.UserId);
                    context.GroupMembers.Remove(c1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("User does not exist in the group");
            }
        }
    }
}
