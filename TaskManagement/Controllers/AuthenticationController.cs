using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Entities.Requests;
using TaskManagement.Core.Helpers;
using TaskManagement.Core.Services;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] Register userCred)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { ResponseCode = ResponseCodes.InvalidRequest });

                var user = await _userService.CreateUserAsync(userCred);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ResponseCodes = ResponseCodes.UnexpectedError, ResponseDescription = $"Registration failed: {ex.Message}" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostAsync([FromBody] Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { ResponseCode = ResponseCodes.InvalidRequest });

                if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                    return Unauthorized(new { ResponseCode = ResponseCodes.Unauthorized, Status = "failed", ResponseDescription = "Authentication failed. Check your credentials and try again" });

                var user = await _userService.AuthenticateUserAsync(login);
                return Ok(user);

            }
            catch (Exception ex)
            {
                return Ok(new { ResponseCode = ResponseCodes.Unauthorized, invalid = "Invalid Login Details" });
            }

        }
    }
}
