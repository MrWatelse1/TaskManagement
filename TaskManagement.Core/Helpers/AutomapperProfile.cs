using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Entities.Requests;

namespace TaskManagement.Core.Helpers
{
    public class AutomapperProfile : Profile 
    {
        public AutomapperProfile()
        {
            #region User Mappings
                CreateMap<User, UserDTO>().ReverseMap();
                CreateMap<Register, User>();
            #endregion

            #region Task Mappings
            CreateMap<TaskDTO, Todo>();
            CreateMap<Todo, GetTaskDTO>()
                .ForMember(dest => dest.User, option => option
                .MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Project, option => option
                .MapFrom(src => src.Project.Name));
            #endregion

            #region Project Mappings
            CreateMap<ProjectDTO, Project>();
            CreateMap<Project, GetProjectDTO>()
                .ForMember(dest => dest.Todos, option => option
                .MapFrom(src => src.Todos.Select(c => new Todo()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    CreatedDate = c.CreatedDate,
                    DueDate = c.DueDate,
                    Priority = c.Priority,
                    Status = c.Status,
                })));
            #endregion
        }

    }
}
