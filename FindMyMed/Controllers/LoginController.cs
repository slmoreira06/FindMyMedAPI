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
                if (repository.GetAccount(loginDTO) == true)
                {

                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("thisisasecretkey@123"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: "ABCXYZ",
                        audience: "http://localhost:51398",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
            }
            catch { return BadRequest("Username and/or Password not specified"); }
            return Unauthorized();
        }
    }
}
