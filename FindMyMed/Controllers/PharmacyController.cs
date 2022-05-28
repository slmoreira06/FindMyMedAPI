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
    public class PharmacyController : Controller
    {
        private readonly IPharmsRepository repository;
        private readonly IMapper mapper;

        public PharmacyController(IPharmsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Find all pharmacies
        /// </summary>
        /// <returns>StatusCode</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<ReadPharmDTO>> GetPharms()
        {
            var pharms = repository.GetPharms();
            return Ok(mapper.Map<IEnumerable<ReadPharmDTO>>(pharms));
        }
        /// <summary>
        /// Find a specific pharmacy
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<ReadPharmDTO> GetPharmById(int id)
        {
            var pharm = repository.GetPharmById(id);

            if (pharm is null)
                return NotFound();

            return Ok(mapper.Map<ReadPharmDTO>(pharm));
        }
        /// <summary>
        /// Update a specific pharmacy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pharm"></param>
        /// <returns>StatusCode</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<ReadPharmDTO> UpdatePharmProfile(int id, UpdatePharmDTO pharm)
        {
            if (pharm is null)
                return NotFound();

            repository.UpdatePharmProfile(id, pharm);

            return NoContent();
        }
    }
}
