using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Enums;

namespace TaskManagement.Core.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }

        [EnumDataType(typeof(NotificationType))]
        public NotificationType Type { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public int TodoId { get; set; }
        public Todo Todo { get; set; }
    }
}
