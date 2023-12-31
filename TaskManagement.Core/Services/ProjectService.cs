﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public interface IProjectService
    {
        Task<Project> GetProject(int id);
        Task<Project> AddProject(ProjectDTO project);
        Task<Project> UpdateProject(int id, ProjectDTO project);
        Task<bool> DeleteProject(int id);
        Task<List<ProjectDto>> AllProjects();
    }
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<Project> AddProject(ProjectDTO project)
        {
            var projectExists = await _projectRepository.GetProjectByProjectName(project.Name.ToLower());
            if (projectExists != null) throw new Exception("Project Already Exist");

            var mappedProject = new Project();
            mappedProject.Name = project.Name;
            mappedProject.Description = project.Description;
            mappedProject.CreatedDate = DateTime.Now;

            var result = await _projectRepository.AddProject(mappedProject);
            return result;
        }

        public async Task<List<ProjectDto>> AllProjects()
        {
            // Use Include to load todos related to projects
            var projects = await _projectRepository.GetProjects()
                .Include(p => p.Todos)
                .ToListAsync();

            // Project the entities into DTOs
            var projectDtos = projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                ModifiedDate = p.ModifiedDate,
                Todos = p.Todos.Select(t => new TodoDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    ModifiedDate = t.ModifiedDate,
                    CreatedDate = t.CreatedDate,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Status = t.Status
                }).ToList()
            }).ToList();

            return projectDtos;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var projectExists = await _projectRepository.GetProject(id);
            if (projectExists == null) throw new Exception("Project Does not Exist");

            var response = await _projectRepository.DeleteProject(projectExists);

            return response;
        }

        public async Task<Project> GetProject(int id)
        {

            return await _projectRepository.GetProject(id);

        }

        public async Task<Project> UpdateProject(int id, ProjectDTO project)
        {
            var projectExists = await _projectRepository.GetProject(id);
            if (projectExists == null) throw new Exception("Project Does not Exist");

            projectExists.ModifiedDate = DateTime.Now;
            projectExists.Name = project.Name;
            projectExists.Description = project.Description;
            //projectExists.Todos = project.Todos;

            var result = await _projectRepository.UpdateProject(projectExists);
            return result;
        }


    }
}
