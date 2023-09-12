using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Services;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ApiBaseController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _taskService.AllTasks();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var task = await _taskService.GetTask(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Todo todo)
        {
            try
            {
                var createdTask = await _taskService.AddTask(todo);
                return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Todo todo)
        {
            try
            {
                var updatedTask = await _taskService.UpdateTask(id, todo);
                if (updatedTask == null)
                {
                    return NotFound();
                }
                return Ok(updatedTask);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var result = await _taskService.DeleteTask(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
