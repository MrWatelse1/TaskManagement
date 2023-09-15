using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Core.DTOs
{
    public class ProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class GetProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<Todo>? Todos { get; set; }
    }

}
