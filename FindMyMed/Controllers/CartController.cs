﻿using AutoMapper;
using FindMyMed.DAL;
using FindMyMed.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {        
        private readonly ICartsRepository repository;

        public CartController(ICartsRepository repository)
        {
            this.repository = repository;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<ReadCartDTO> SaveCart(UpdateCartDTO cart)
        {
            if (cart is null)
                return NotFound();

            repository.SaveCart(cart);

            return NoContent();
        }
    }
}