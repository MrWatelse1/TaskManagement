using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.DTOs;
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
            [Route("GetAllProjects")]
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

            [HttpGet]
            [Route("GetProjectById")]
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
            [Route("CreateProject")]
            public async Task<IActionResult> CreateProject([FromBody] ProjectDTO project)
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

            [HttpPut]
            [Route("UpdateProject")]
            public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDTO project)
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

            [HttpDelete]
            [Route("DeleteProject")]
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

