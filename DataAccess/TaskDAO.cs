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

        public static Task FindTaskById(int id)
        {
            Task task = null;
            try
            {
                using (var context = new GroupStudyContext())
                {
                    task = context.Tasks.SingleOrDefault(x => x.TaskId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return task;
        }

        public static void SaveTask(Task task)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Tasks.Add(task);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static void UpdateTask(Task task)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteTask(Task task)
        {
            try
            {
                using (var context = new GroupStudyContext())
                {
                    var existingTask = context.Tasks.SingleOrDefault(u => u.TaskId == task.TaskId);
                    if (existingTask != null)
                    {
                        context.Tasks.Remove(existingTask);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static int GetNextTaskId()
        {
            int nextTaskId = 0;
            try
            {
                using (var context = new GroupStudyContext())
                {
                    nextTaskId = context.Tasks.Max(u => u.TaskId) + 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return nextTaskId;
        }
    }
}
