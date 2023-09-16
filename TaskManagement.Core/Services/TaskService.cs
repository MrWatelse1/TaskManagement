using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Entities.Requests;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public interface ITaskService
    {
        Task<List<Todo>> AllTasks();
        Task<Todo> GetTask(int id);
        Task AddTask(int userId, TaskDTO todo);
        Task<Todo> UpdateTask(int id, TaskDTO todo);
        Task<bool> DeleteTask(int id);
        Task<List<Todo>> GetTasksByStatusAndPriority(FetchStatus fetchStatus);
        Task<List<Todo>> GetTasksDueForWeek(DateTime startOfWeek, DateTime endOfWeek);
        Task<bool> AssignTaskToProject(int taskId, int? projectId);
    }
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task AddTask(int userId, TaskDTO todo)
        {
            var mappedTask = new Todo();
            mappedTask.Title = todo.Title;
            mappedTask.Description = todo.Description;
            mappedTask.Priority = todo.Priority;
            mappedTask.Status = todo.Status;
            mappedTask.UserId = userId;
            mappedTask.ProjectId = todo.ProjectId;
            mappedTask.CreatedDate = DateTime.Now;

            await _taskRepository.AddTask(mappedTask);
        }

        public async Task<List<Todo>> AllTasks()
        {
            var response = await _taskRepository.GetTasks().ToListAsync();

            //var taskDto = _mapper.Map<GetTaskDTO>(response);
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

        public async Task<Todo> UpdateTask(int id, TaskDTO todo)
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

        public async Task<List<Todo>> GetTasksByStatusAndPriority(FetchStatus fetchStatus)
        {
            // Add error handling and validation as needed.

            var tasks = await _taskRepository.GetTasks().Where( t => ((int)t.Status == fetchStatus.Status) && ((int)t.Priority == fetchStatus.Priority))
                .ToListAsync();

            return tasks;
        }

        public async Task<List<Todo>> GetTasksDueForWeek(DateTime startOfWeek, DateTime endOfWeek)
        {
            try
            {
                if (startOfWeek > endOfWeek)
                {
                    throw new ArgumentException("Start date must be before end date.");
                }

                var tasks = await _taskRepository.GetTasks()
                    .Where(t => t.DueDate >= startOfWeek && t.DueDate <= endOfWeek)
                    .ToListAsync();

                return tasks;
            }
            catch (ArgumentException ex)
            {
                // Handle validation error (e.g., invalid date range).
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., database errors).
                throw new ApplicationException("An error occurred while fetching tasks due for the week.", ex);
            }
        }

        public async Task<bool> AssignTaskToProject(int taskId, int? projectId)
        {
            // Implement logic to assign the task to the project or remove it from the project in the database.
            var task = await _taskRepository.GetTask(taskId);
            if (task == null)
            {
                return false; // Task not found.
            }

            if (projectId.HasValue)
            {
                // Assign the task to the project.
                var project = await _projectRepository.GetProject(projectId.Value);
                if (project == null)
                {
                    return false; // Project not found.
                }

                task.ProjectId = projectId.Value; // Set the task's project ID.
            }
            else
            {
                // Remove the task from the project.
                task.ProjectId = 0; // Clear the task's project ID.
            }

            await _taskRepository.UpdateTask(task);

            return true; // Successfully assigned or removed from the project.
        }


    }
}
