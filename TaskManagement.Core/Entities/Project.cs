using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
