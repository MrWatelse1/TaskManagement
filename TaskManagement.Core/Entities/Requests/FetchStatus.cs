using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Entities.Requests
{
    public class FetchStatus
    {
        public int Status { get; set; }
        public int Priority { get; set; }
    }
}
