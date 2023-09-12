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
    public class ProjectController : ApiBaseController
    {
            private readonly IProjectService _projectService;

            public ProjectController(IProjectService projectService)
            {
                _projectService = projectService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllProjects()
            {
                try
                {
                    var projects = await _projectService.AllProjects();
                    return Ok(projects);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetProjectById(int id)
            {
                try
                {
                    var project = await _projectService.GetProject(id);
                    if (project == null)
                    {
                        return NotFound();
                    }
                    return Ok(project);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }

            [HttpPost]
            public async Task<IActionResult> CreateProject([FromBody] Project project)
            {
                try
                {
                    var createdProject = await _projectService.AddProject(project);
                    return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
            {
                try
                {
                    var updatedProject = await _projectService.UpdateProject(id, project);
                    if (updatedProject == null)
                    {
                        return NotFound();
                    }
                    return Ok(updatedProject);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProject(int id)
            {
                try
                {
                    var result = await _projectService.DeleteProject(id);
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
}

