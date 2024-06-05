using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthContoller : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthContoller(IConfiguration config)
        {
            configuration = config;
        }

        [HttpGet]
        public IActionResult Auth()
        {
            var signigKey = Encoding.UTF8.GetBytes(configuration.GetSection("SecretKey").ToString() ?? "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDes = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signigKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDes);

            return Ok(tokenHandler.WriteToken(token));
        }
    }
}