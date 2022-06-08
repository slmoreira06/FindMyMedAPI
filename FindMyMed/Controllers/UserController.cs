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
        /// <summary>
        /// Find all users
        /// </summary>
        /// <returns>StatusCode</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<IEnumerable<ReadUserDTO>> GetUsers()
        {
            var users = repository.GetUsers();
            return Ok(mapper.Map<IEnumerable<ReadUserDTO>>(users));
        }
        /// <summary>
        /// Find an specific user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadUserDTO> GetUserById(int id)
        {
            var user = repository.GetUserById(id);

            if (user is null)
                return NotFound();

            return Ok(mapper.Map<ReadUserDTO>(user));
        }
        /// <summary>
        /// Update an specific user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadUserDTO> UpdateUserProfile(int id, UpdateUserDTO user)
        {
            if (user is null)
                return NotFound();

            repository.UpdateUserProfile(id, user);

            return NoContent();
        }
    }
}
