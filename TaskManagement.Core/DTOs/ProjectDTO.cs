using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Enums;

namespace TaskManagement.Core.DTOs
{
    public class ProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<TodoDto> Todos { get; set; }
    }
}
