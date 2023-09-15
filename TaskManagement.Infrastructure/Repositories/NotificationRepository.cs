using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TaskManagementDbContext _context;
        public NotificationRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddNotification(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteNotification(Notification notification)
        {
            _context.Notifications.Remove(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Notification> GetNotification(int id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Notification> GetNotifications()
        {
            return _context.Notifications;
        }

        public async Task<Notification> UpdateNotification(Notification notification)
        {
            var result = _context.Update(notification);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}

