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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {

        private readonly IOrderItemsRepository repository;
        private readonly IMapper mapper;

        public OrderItemController(IOrderItemsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<IEnumerable<ReadOrderItemDTO>> GetOrderItemsByOrder(int id)
        {
            var item = repository.GetOrderItemsByOrder(id);
            return Ok(mapper.Map<IEnumerable<ReadOrderItemDTO>>(item));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadOrderItemDTO> GetOrderItemById(int id)
        {
            var item = repository.GetOrderItemById(id);

            if (item is null)
                return NotFound();

            return Ok(mapper.Map<ReadOrderItemDTO>(item));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ("User, Admin"))]
        public ActionResult<ReadOrderItemDTO> UpdateOrderItem(int id, UpdateOrderItemDTO item)
        {
            if (item is null)
                return NotFound();

            repository.UpdateOrderItem(id, item);

            return NoContent();
        }
    }
}
