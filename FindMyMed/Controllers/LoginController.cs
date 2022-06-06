using FindMyMed.DAL;
using FindMyMed.DTO;
using FindMyMed.Models;
using AutoMapper;
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
        private readonly IMapper mapper;

        public LoginController(IAccountsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{email}")]
        public ActionResult<ReadAccountDTO> GetAccountByEmail(string email)
        {
            var account = repository.GetAccountByEmail(email);

            if (account is null)
                return NotFound();

            return Ok(mapper.Map<ReadAccountDTO>(account));
        }

        [HttpPost]
        public IActionResult Login(LoginAccount loginDTO)
        {

            var issuer = "http://localhost:5000";
            var audience = "http://localhost:4200";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Now its ime to define the jwt token which will be responsible of creating our tokens
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // We get our secret from the appsettings
            var key = Encoding.ASCII.GetBytes("DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4");
            if (string.IsNullOrEmpty(loginDTO.Email) ||
            string.IsNullOrEmpty(loginDTO.Password))
                return BadRequest("Username and/or Password not specified");
            var account = repository.GetAccount(loginDTO);
            if (account != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                {
                            new Claim(JwtRegisteredClaimNames.Email, account.Email),
                            new Claim(JwtRegisteredClaimNames.Sub, account.Email),
                            new Claim(ClaimTypes.Role, account.Type.ToString()),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        }),

                    Expires = DateTime.UtcNow.AddHours(6),
                    Audience = audience,
                    Issuer = issuer,
                    // here we are adding the encryption alogorithim information which will be used to decrypt our token
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var tokenString = jwtTokenHandler.WriteToken(token);

                return Ok(tokenString);
            }
            else
                return Unauthorized();
        }
    }

}
