/*
 * Projeto De Desenvolvimento de Software
/*! \brief Projeto De Desenvolvimento de Software
 *         Engenharia de Sistemas informáticos
 *         
 * !@file accounts.cs
 * <Author 1>A8544 Sara Moreira</Author 1>
 * <Author 2>A18854 Isabela Magalhaes</Author 2>
 * <Author 3>A18864 Patricia Santos</Author 3>
 * <Author 4>A19334 José Matos</Author 4>
 * <Author 5>A19255 Alexandre Carvalho/Author 5>
 *
 */
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
    public class ProductController : Controller
    {

        private readonly IProductsRepository repository;
        private readonly IMapper mapper;

        public ProductController(IProductsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        /// <summary>
        /// List all products 
        /// </summary>
        /// <returns>StatusCode</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<ReadProductDTO>> GetProducts()
        {
            var prod = repository.GetProducts();
            return Ok(mapper.Map<IEnumerable<ReadProductDTO>>(prod));
        }
        /// <summary>
        /// List a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<ReadProductDTO> GetProductById(int id)
        {
            var prod = repository.GetProductById(id);

            if (prod is null)
                return NotFound();

            return Ok(mapper.Map<ReadProductDTO>(prod));
        }
        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="prodDTO"></param>
        /// <returns>StatusCode</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<ReadProductDTO> CreateProduct(CreateProductDTO prodDTO)
        {
            Product prod = mapper.Map<Product>(prodDTO);
            repository.CreateProduct(prod);

            var prodRead = mapper.Map<ReadProductDTO>(prod);

            return CreatedAtAction(nameof(GetProducts), new { id = prodRead.Id }, prodRead);
        }
        /// <summary>
        /// Update a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prod"></param>
        /// <returns>StatusCode</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Pharm, Admin")]
        public ActionResult<ReadProductDTO> UpdateProduct(int id, UpdateProductDTO prod)
        {
            if (prod is null)
                return NotFound();

            repository.UpdateProduct(id, prod);

            return NoContent();
        }
    }
}
