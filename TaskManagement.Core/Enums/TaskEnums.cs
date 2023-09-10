using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Enums
{
    public class TaskEnums
    {
    }
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        Pending,
        InProgress,
        Completed
    }
    public enum NotificationType
    {
        DueDateReminder,
        StatusUpdate
    }
}
