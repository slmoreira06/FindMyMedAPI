using AutoMapper;
using FindMyMed.DAL;
using FindMyMed.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUsersRepository repository;
        private readonly IMapper mapper;

        public UserController(IUsersRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<ReadUserDTO>> GetUsers()
        {
            var users = repository.GetUsers();
            return Ok(mapper.Map<IEnumerable<ReadUserDTO>>(users));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<ReadUserDTO> GetUserById(int id)
        {
            var user = repository.GetUserById(id);

            if (user is null)
                return NotFound();

            return Ok(mapper.Map<ReadUserDTO>(user));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<ReadUserDTO> UpdateUserProfile(int id, UpdateUserDTO user)
        {
            if (user is null)
                return NotFound();

            repository.UpdateUserProfile(id, user);

            return NoContent();
        }
    }
}
