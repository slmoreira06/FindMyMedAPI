using AutoMapper;
using FindMyMed.DAL;
using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoriesRepository repository;
        private readonly IMapper mapper;

        public InventoryController(IInventoriesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<IEnumerable<ReadInventoryDTO>> GetInventories()
        {
            var inventories = repository.GetInventories();
            return Ok(mapper.Map<IEnumerable<ReadInventoryDTO>>(inventories));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<ReadInventoryDTO> GetInventoryByProduct(int id)
        {
            var inv = repository.GetInventoryByProduct(id);

            if (inv is null)
                return NotFound();

            return Ok(mapper.Map<ReadInventoryDTO>(inv));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<ReadInventoryDTO> CreateInventory(CreateInventoryDTO invDTO)
        {
            Inventory inv = mapper.Map<Inventory>(invDTO);
            bool result = repository.CreateInventory(inv);
            if (result == false)
            {
                return BadRequest("Non existing product or pharmacy");
            }
            else
            {
                var invRead = mapper.Map<ReadInventoryDTO>(inv);
                return CreatedAtAction(nameof(GetInventories), new { id = invRead.Id }, invRead);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<ReadInventoryDTO> UpdateInventory(int id, UpdateInventoryDTO inv)
        {
            if (inv is null)
                return NotFound();

            repository.UpdateInventory(id, inv);

            return NoContent();
        }
    }
}
