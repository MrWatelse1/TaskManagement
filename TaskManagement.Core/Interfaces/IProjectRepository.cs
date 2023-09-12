using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetProjects();
        Task<Project> GetProject(int id);
        Task<Project> GetProjectByProjectName(string name);
        Task<Project> AddProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task<bool> DeleteProject(Project project);
    }
}
