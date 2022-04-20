using AutoMapper;
using FindMyMed.DAL;
using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : Controller
    {
        private readonly ISupportsRepository repository;
        private readonly IMapper mapper;

        public SupportController(ISupportsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<ReadSupportDTO>> GetSupports()
        {
            var sup = repository.GetSupports();
            return Ok(mapper.Map<IEnumerable<ReadSupportDTO>>(sup));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<ReadSupportDTO> GetSupportById(int id)
        {
            var sup = repository.GetSupportById(id);

            if (sup is null)
                return NotFound();

            return Ok(mapper.Map<ReadSupportDTO>(sup));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<ReadSupportDTO> CreateSupport(CreateSupportDTO supDTO)
        {
            Support sup = mapper.Map<Support>(supDTO);
            repository.CreateSupport(sup);

            var supRead = mapper.Map<ReadSupportDTO>(sup);

            return CreatedAtAction(nameof(GetSupports), new { id = supRead.Id }, supRead);
        }
    }
}
