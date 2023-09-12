using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface ITaskRepository
    {
        IQueryable<Todo> GetTasks();
        Task<Todo> GetTask(int id);
        Task<Todo> AddTask(Todo todo);
        Task<Todo> UpdateTask(Todo todo);
        Task <bool> DeleteTask(Todo todo);
    }
}
