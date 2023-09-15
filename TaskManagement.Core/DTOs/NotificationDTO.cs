using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Enums;

namespace TaskManagement.Core.DTOs
{
    public class NotificationDTO
    {
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool readStatus { get; set; }
        public int TodoId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
