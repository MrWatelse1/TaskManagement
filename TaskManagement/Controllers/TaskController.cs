using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Entities.Requests;
using TaskManagement.Core.Helpers;
using TaskManagement.Core.Services;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ApiBaseController
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TaskController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }


        [HttpGet("GetUser")]
        public async Task<UserDTO> GetLoggedInUserAsync()
        {
            var useremail = GetAuthUserEmail();
            var user = await _userService.GetUserByEmail(useremail);
            return user;
        }


        [HttpGet]
        [Authorize]
        [Route("GetAllTasks")]
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

        [HttpGet]
        [Route("GetTaskById")]
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
        [Route("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDTO todo)
        {
            UserDTO user;
            try
            {
                user = await GetLoggedInUserAsync();
                await _taskService.AddTask(user.Id, todo);
                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Status = ResponseCodes.UnexpectedError, ResponseDescription = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateTask")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDTO todo)
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

        [HttpDelete]
        [Route("DeleteTask")]
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

        [HttpGet]
        [Route("GetTasksByStatus")]
        public async Task<IActionResult> GetTasksByStatus([FromBody] FetchStatus fetchStatus)
        {
            try
            {
                var tasks = await _taskService.GetTasksByStatusAndPriority(fetchStatus);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET /api/tasks/duetoday
        [HttpGet]
        [Route("GetTasksDueThisWeek")]
        public async Task<IActionResult> GetTasksDueForCurrentWeek()
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                DateTime startOfWeek = currentDate.StartOfWeek();
                DateTime endOfWeek = startOfWeek.AddDays(6);

                var tasks = await _taskService.GetTasksDueForWeek(startOfWeek, endOfWeek);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT /api/tasks/{taskId}/project
        [HttpPut]
        [Route("AssignTaskToProject")]
        public async Task<IActionResult> AssignTaskToProject(int taskId, [FromBody] int? projectId)
        {
            try
            {
                var success = await _taskService.AssignTaskToProject(taskId, projectId);
                if (success)
                {
                    if (projectId.HasValue)
                    {
                        return Ok("Task assigned to project.");
                    }
                    else
                    {
                        return Ok("Task removed from project.");
                    }
                }
                else
                {
                    return NotFound("Task or project not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
