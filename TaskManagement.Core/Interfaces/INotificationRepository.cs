using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces
{
    public interface INotificationRepository
    {
        IQueryable<Notification> GetNotifications();
        Task<Notification> GetNotification(int id);
        Task AddNotification(Notification notification);
        Task<Notification> UpdateNotification(Notification notification);
        Task<bool> DeleteNotification(Notification notification);
    }
}
