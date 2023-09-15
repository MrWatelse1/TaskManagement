using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        [HttpGet("User")]
        public string GetAuthUserEmail()
        {
            //return email
            var userEmail = HttpContext.User.FindFirst(c => c.Type == "id")?.Value;
            return userEmail ?? "";

            //return id
            //var userId = HttpContext.User.FindFirst(c => c.Type == "sub")?.Value;
            //return userId ?? "";
        }
    }
}
