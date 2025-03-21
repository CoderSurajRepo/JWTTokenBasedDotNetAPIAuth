using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTTokenBasedAuthDotNetAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWTTokenBasedAuthDotNetAPI.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthAPIController : ControllerBase
    {
        public readonly IConfiguration Configuration;
        public UserAuthAPIController(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }

        [HttpGet("AuthUser")]
        public IActionResult AuthUser(string UserName, string Password)
        {
            List<User> users = GetUserDetails();

            if (users.Exists(x => x.UserName == UserName && x.Password == Password))
            { 

                string JWTToken = GenerateJWTToken(UserName);

                return Ok(JWTToken);
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

        public string GenerateJWTToken(string UserName)
        {
            var key = Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:Key").Value.ToString());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, UserName)
                }),
                Expires = DateTime.Now.AddSeconds(45),
                Issuer = Configuration["JWT:Issuer"],
                Audience = Configuration["JWT:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken;
        }
    }
}
