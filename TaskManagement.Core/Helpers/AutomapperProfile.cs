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
            #endregion

            #region Project Mappings
            CreateMap<ProjectDTO, Project>();
            #endregion
        }

    }
}
