using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public interface ITaskService
    {
        Task<List<Todo>> AllTasks();
        Task<Todo> GetTask(int id);
        Task<Todo> AddTask(Todo todo);
        Task<Todo> UpdateTask(int id, Todo todo);
        Task<bool> DeleteTask(int id);
    }
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<Todo> AddTask(Todo todo)
        {
            var mappedTask = _mapper.Map<Todo>(todo);

            mappedTask.CreatedDate = DateTime.Now;
            var result = await _taskRepository.AddTask(mappedTask);
            return result;
        }

        public async Task<List<Todo>> AllTasks()
        {
            var response = await _taskRepository.GetTasks().ToListAsync();

            return response;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var taskExists = await _taskRepository.GetTask(id);
            if (taskExists == null) throw new Exception("Task Does not Exist");

             var response = await _taskRepository.DeleteTask(taskExists);
            return response;
        }

        public async Task<Todo> GetTask(int id)
        {
            return await _taskRepository.GetTask(id);
        }

        public async Task<Todo> UpdateTask(int id, Todo todo)
        {
            var taskExists = await _taskRepository.GetTask(id);
            if (taskExists == null) throw new Exception("Task Does not Exist");

            taskExists.ModifiedDate = DateTime.Now;
            taskExists.Title = todo.Title;
            taskExists.Description = todo.Description;
            taskExists.Priority = todo.Priority;
            taskExists.Status = todo.Status;


            var result = await _taskRepository.UpdateTask(taskExists);
            return result;
        }
    }
}
