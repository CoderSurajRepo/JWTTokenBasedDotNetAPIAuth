using JWTTokenBasedAuthDotNetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTTokenBasedAuthDotNetAPI.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthAPIController : ControllerBase
    {
        [HttpGet("AuthUser")]
        public IActionResult AuthUser(string UserName, string Password)
        {
            List<User> users = GetUserDetails();

            if(users.Exists(x => x.UserName == UserName && x.Password == Password))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }

        private static List<User> GetUserDetails()
        {
            return new List<User>
            {
                new User {  UserID = 1, UserName = "surajy", Password = "123" },
                new User { UserID = 2, UserName = "vikasp", Password = "456" },
                new User { UserID = 3, UserName = "nitinp", Password = "789" }
            };
        }
    }
}
