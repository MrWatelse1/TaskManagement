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
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskManagementDbContext _context;
        public ProjectRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Project> AddProject(Project Project)
        {
            var result = await _context.Projects.AddAsync(Project);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteProject(Project Project)
        {
            _context.Projects.Remove(Project);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Project> GetProject(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Project> GetProjectByProjectName(string name)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Name == name);
        }

        public IQueryable<Project> GetProjects()
        {
            return _context.Projects;
        }

        public async Task<Project> UpdateProject(Project Project)
        {
            var result = _context.Update(Project);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
