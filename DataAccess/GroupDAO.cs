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
                       .ToList();
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
                    c = context.Groups.SingleOrDefault(x => x.GroupId == Id);

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
                    context.Groups.Add(c);
                    context.SaveChanges();
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
                    context.Groups.Remove(c1);
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
