using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
