﻿using AutoMapper;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public InventoryController(IInventoriesRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Find every inventory, deploy an error 200(OK), only admin and pharm can view the inventory. 
        /// </summary>
        /// <returns>StatusCode</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<IEnumerable<ReadInventoryDTO>> GetInventories()
        {
            var inventories = repository.GetInventories();
            return Ok(mapper.Map<IEnumerable<ReadInventoryDTO>>(inventories));
        }

        /// <summary>
        /// Find an inventory by id, deploy a code 200(OK) meaning the inventory was found or 404(NOT FOUND) meaning no inventory was found. Only admin can view the inventory.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
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

        /// <summary>
        /// Create an inventory, can deploy a code 201(CREATED) meaning an inventory was created, or an error 400  couldn't create a inventory.
        /// </summary>
        /// <param name="invDTO"></param>
        /// <returns>StatusCode</returns>
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

        /// <summary>
        /// Edit an inventory, can deploy a code 201(CREATED) meaning a inventory was created, or an error 400  couldn't create a inventory.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inv"></param>
        /// <returns>StatusCode</returns>
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
