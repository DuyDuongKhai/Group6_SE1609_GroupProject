using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void SaveGroupMember(GroupMember c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.GroupMembers.Add(c);
                    context.SaveChanges();
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
                    var c1 = context.GroupMembers.SingleOrDefault(u => u.GroupMemberId == c.GroupMemberId);
                    context.GroupMembers.Remove(c1);
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
