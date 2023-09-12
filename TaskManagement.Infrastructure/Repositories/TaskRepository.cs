using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _context;
        public TaskRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> AddTask(Todo todo)
        {
            var result = await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteTask(Todo todo)
        {
                _context.Todos.Remove(todo);
                return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Todo> GetTask(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Todo> GetTasks()
        {
            return _context.Todos;
        }

        public async Task<Todo> UpdateTask(Todo todo)
        {
            var result = _context.Update(todo);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
