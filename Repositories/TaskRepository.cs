using System;
using DataAccess;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Task = BusinessObject.Models.Task;

namespace Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public void DeleteTask(Task c) => TaskDAO.DeleteTask(c);

        public void SaveTask(Task c) => TaskDAO.SaveTask(c);

        public void UpdateTask(Task c) => TaskDAO.UpdateTask(c);

        public Task GetTaskById(int id) => TaskDAO.FindTaskById(id);
        public int GetNextTaskId()
        {
            int nextTaskId = TaskDAO.GetNextTaskId();
            return nextTaskId;
        }
        public List<Group> GetGroups() => GroupDAO.GetGroups();
        public List<User> GetUsers() => UserDAO.GetUsers();
        public List<Task> GetTasks() => TaskDAO.GetTasks();

        public List<Task> GetTasksByGroupId(int groupId)=> TaskDAO.GetListTaskByGroupId(groupId);
        public List<Task> GetListTaskByGroupAndUserId(int groupId, int memberId)=>TaskDAO.GetListTaskByGroupAndUserId(groupId, memberId);
    }
}
