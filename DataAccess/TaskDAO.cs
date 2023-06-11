using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = BusinessObject.Models.Task;

namespace DataAccess
{
    public class TaskDAO
    {
        public static List<Task> GetTasks()
        {
            var listTasks = new List<Task>();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    listTasks = context.Tasks.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listTasks;
        }
        public static Task FindTaskById(int Id)
        {
            Task c = new Task();
            try
            {
                using (var context = new GroupStudyContext())
                {
                    c = context.Tasks.SingleOrDefault(x => x.TaskId == Id);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return c;
        }

        public static void SaveTask(Task c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Tasks.Add(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateTask(Task c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry<Task>(c).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteTask(Task c)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var c1 = context.Tasks.SingleOrDefault(u => u.TaskId == c.TaskId);
                    context.Tasks.Remove(c1);
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
