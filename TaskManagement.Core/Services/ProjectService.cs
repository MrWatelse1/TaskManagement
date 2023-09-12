﻿using AutoMapper;
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
    public interface IProjectService
    {
        Task<List<Project>> AllProjects();
        Task<Project> GetProject(int id);
        Task<Project> AddProject(Project project);
        Task<Project> UpdateProject(int id, Project project);
        Task<bool> DeleteProject(int id);
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

        public async Task<Project> AddProject(Project project)
        {
            var projectExists = await _projectRepository.GetProjectByProjectName(project.Name.ToLower());
            if (projectExists != null) throw new Exception("Project Already Exist");

            var mappedProject = _mapper.Map<Project>(project);

            mappedProject.CreatedDate = DateTime.Now;
            var result = await _projectRepository.AddProject(mappedProject);
            return result;
        }

        public async Task<List<Project>> AllProjects()
        {
            var response = await _projectRepository.GetProjects().ToListAsync();

            return response;
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

        public async Task<Project> UpdateProject(int id, Project project)
        {
            var projectExists = await _projectRepository.GetProject(id);
            if (projectExists == null) throw new Exception("Project Does not Exist");

            projectExists.ModifiedDate = DateTime.Now;
            projectExists.Name = project.Name;
            projectExists.Description = project.Description;
            projectExists.Todos = project.Todos;

            var result = await _projectRepository.UpdateProject(projectExists);
            return result;
        }
    }
}