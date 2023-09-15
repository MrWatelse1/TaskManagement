using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Helpers;
using TaskManagement.Core.Services;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ApiBaseController
    {
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;

        public NotificationController(INotificationService notificationService, IUserService userService)
        {
            _notificationService = notificationService;
            _userService = userService;
        }


        [HttpGet("GetUserAsync")]
        public async Task<UserDTO> GetLoggedInUserAsync()
        {
            var useremail = GetAuthUserEmail();
            var user = await _userService.GetUserByEmail(useremail);
            return user;
        }


        [HttpGet]
        [Route("GetAllNotifications")]
        [Authorize]
        public async Task<IActionResult> GetAllNotifications()
        {
            try
            {
                var notifications = await _notificationService.AllNotifications();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetNotificationById")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            try
            {
                var notification = await _notificationService.GetNotification(id);
                if (notification == null)
                {
                    return NotFound();
                }
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("CreateNotification")]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDTO notification)
        {
            UserDTO user;
            try
            {
                user = await GetLoggedInUserAsync();
                await _notificationService.AddNotification(user.Id, notification);
                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Status = ResponseCodes.UnexpectedError, ResponseDescription = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateNotification")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] Notification notification)
        {
            try
            {
                var updatedNotification = await _notificationService.UpdateNotification(id, notification);
                if (updatedNotification == null)
                {
                    return NotFound();
                }
                return Ok(updatedNotification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("DeleteNotification")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                var result = await _notificationService.DeleteNotification(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        // PUT /api/notifications/markasreadorunread/{notificationId}
        [HttpPut]
        [Route("MarkNotificationAsReadOrUnread")]
        public async Task<IActionResult> MarkNotificationAsReadOrUnread(int notificationId, [FromBody] bool readStatus)
        {
            try
            {
                var success = await _notificationService.MarkNotificationAsReadOrUnreadAsync(notificationId, readStatus);
                if (success)
                {
                    string status = readStatus ? "read" : "unread";
                    return Ok($"Notification marked as {status}.");
                }
                else
                {
                    return NotFound("Notification not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
