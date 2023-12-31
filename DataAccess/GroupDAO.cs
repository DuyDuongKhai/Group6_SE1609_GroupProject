﻿using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class GroupDAO
    {
        public static List<Group> GetGroups()
        {
            var listGroups = new List<Group>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listGroups = context.Groups
                       .Include(x=>x.GroupAdmin)
                       .Include(x=>x.GroupMembers)
                       .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listGroups;
        }
        public static List<Group> GetGroupsByGroupAdminId(int groupAdminId)
        {
            var listGroups = new List<Group>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listGroups = context.Groups
                       .Include(x => x.GroupAdmin)
                       .Where(x=>x.GroupAdminId==groupAdminId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listGroups;
        }
        public static Group FindGroupById(int Id)
        {
            Group c = new Group();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.Groups
                        .Include(x=>x.GroupAdmin)
                        .Include(x=>x.JoinRequests)
                        .SingleOrDefault(x => x.GroupId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }




        public static void SaveGroup(Group c)
        {
            try
            {

                using (var context = new GroupStudyContext())
                {
                    var user = context.Users.SingleOrDefault(x => x.UserId == c.GroupAdminId);
                    if (user.Role.Equals("GroupAdmin"))
                    {
                        context.Groups.Add(c);
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
        public static void UpdateGroup(Group c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<Group>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteGroup(Group c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.Groups.SingleOrDefault(u => u.GroupId == c.GroupId);
                    var groupMembers = context.GroupMembers.Where(m => m.GroupId == c1.GroupId).ToList();
                    var joinRequests = context.JoinRequests.Where(m => m.GroupId == c1.GroupId).ToList();
                    var posts = context.Posts.Where(m => m.GroupId == c1.GroupId).ToList();
                    var chatMessages = context.ChatMessages.Where(m => m.GroupId == c1.GroupId).ToList();
                    var tasks = context.Tasks.Where(m => m.GroupId == c1.GroupId).ToList();
                    var comments = context.Comments.Where(m => posts.Contains(context.Posts.SingleOrDefault(p => p.PostId == m.PostId))).ToList();


                    context.Comments.RemoveRange(comments);
                    context.GroupMembers.RemoveRange(groupMembers);
                    context.JoinRequests.RemoveRange(c1.JoinRequests);
                    context.Posts.RemoveRange(c1.Posts);
                    context.ChatMessages.RemoveRange(c1.ChatMessages);
                    context.Tasks.RemoveRange(c1.Tasks);
                    context.Groups.Remove(c1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static int GetNextId()
        {
            int nextId = 0;
            try
            {
                using (var context = new GroupStudyContext())
                {
                    nextId = context.Groups.Max(u => u.GroupId) + 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return nextId;
        }
    }
}
