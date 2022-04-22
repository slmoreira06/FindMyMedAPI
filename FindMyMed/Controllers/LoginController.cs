using FindMyMed.DAL;
using FindMyMed.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FindMyMed.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAccountsRepository repository;

        public LoginController(IAccountsRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost, Route("login")]
        public IActionResult Login(LoginAccount loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.Email) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                if (repository.GetAccount(loginDTO) != null)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Email, loginDTO.Email)
                    };
                    var token = new JwtSecurityToken
                   (
                       issuer: "http://localhost:5000",
                       audience: "http://localhost:4200",
                       claims: claims,
                       expires: DateTime.UtcNow.AddDays(60),
                       notBefore: DateTime.UtcNow,
                       signingCredentials: new SigningCredentials(
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4")),
                           SecurityAlgorithms.HmacSha256)
                   );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(tokenString);
                }
            }
            catch { return BadRequest("Username and/or Password not specified"); }
            return Unauthorized();
        }
    }

}
