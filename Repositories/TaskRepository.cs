using BusinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = BusinessObject.Models.Task;

namespace Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public void DeleteTask(Task c) => TaskDAO.DeleteTask(c);

        public void SaveTask(Task c) => TaskDAO.SaveTask(c);

        public void UpdateTask(Task c) => TaskDAO.UpdateTask(c);

        public Task GetTaskById(int id) => TaskDAO.FindTaskById(id);

        public List<Group> GetGroups() => GroupDAO.GetGroups();
        public List<User> GetUsers() => UserDAO.GetUsers();
        public List<Task> GetTasks() => TaskDAO.GetTasks();
    }
}
