using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> AllNotifications();
        Task<Notification> GetNotification(int id);
        Task AddNotification(int userId, NotificationDTO notification);
        Task<Notification> UpdateNotification(int id, Notification notification);
        Task<bool> DeleteNotification(int id);
        Task<bool> MarkNotificationAsReadOrUnreadAsync(int notificationId, bool readStatus);
    }
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }
        public async Task AddNotification(int userId, NotificationDTO notification)
        {
            var mappedNotification = new Notification();
            mappedNotification.Message = notification.Message;
            mappedNotification.Type = notification.Type;
            mappedNotification.Timestamp = notification.Timestamp;
            mappedNotification.readStatus = notification.readStatus;
            mappedNotification.TodoId = notification.TodoId;
            mappedNotification.CreatedDate = DateTime.Now;

            await _notificationRepository.AddNotification(mappedNotification);
        }

        public async Task<List<Notification>> AllNotifications()
        {
            var response = await _notificationRepository.GetNotifications().ToListAsync();

            return response;
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var notificationExists = await _notificationRepository.GetNotification(id);
            if (notificationExists == null) throw new Exception("Notification does not Exist");

            var response = await _notificationRepository.DeleteNotification(notificationExists);
            return response;
        }

        public async Task<Notification> GetNotification(int id)
        {
            return await _notificationRepository.GetNotification(id);
        }

        public async Task<Notification> UpdateNotification(int id, Notification notification)
        {
            var notificationExists = await _notificationRepository.GetNotification(id);
            if (notificationExists == null) throw new Exception("Notification does not Exist");

            notificationExists.ModifiedDate = DateTime.Now;
            notificationExists.Message = notification.Message;
            notificationExists.Type = notification.Type;
            notificationExists.Timestamp = notification.Timestamp;
            notificationExists.readStatus = notification.readStatus;
            notificationExists.TodoId = notification.TodoId;


            var result = await _notificationRepository.UpdateNotification(notificationExists);
            return result;
        }

        public async Task<bool> MarkNotificationAsReadOrUnreadAsync(int notificationId, bool readStatus)
        {
            // Implement logic to mark the notification as read or unread in the database.
            var notification = await _notificationRepository.GetNotification(notificationId);
            if (notification == null)
            {
                return false; // Notification not found.
            }

            notification.readStatus = readStatus; // Update the IsRead property.
            await _notificationRepository.UpdateNotification(notification);

            return true; // Successfully marked as read or unread.
        }
    }
}
