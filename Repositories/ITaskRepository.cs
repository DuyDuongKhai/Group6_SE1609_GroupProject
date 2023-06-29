using System;
using System.Linq;
using System.Text;
using BusinessObject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Task = BusinessObject.Models.Task;

namespace Repositories
{
    public interface ITaskRepository
    {
        void SaveTask(Task c);
        Task GetTaskById(int id);
        void DeleteTask(Task c);
        void UpdateTask(Task c);
        int GetNextTaskId();
        List<User> GetUsers();
        List<Group> GetGroups();
        List<Task> GetTasks();
        List<Task> GetTasksByGroupId(int groupId);
        List<Task> GetListTaskByGroupAndUserId(int groupId, int memberId);
    }
}
