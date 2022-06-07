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
    public class OrderController : Controller
    {
        private readonly IOrdersRepository repository;
        private readonly IOrderItemsRepository itemsRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        /// <param name="itemsRepository"></param>
        public OrderController(IOrdersRepository repository, IMapper mapper, IOrderItemsRepository itemsRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.itemsRepository = itemsRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<IEnumerable<ReadOrderDTO>> GetOrders()
        {
            var order = repository.GetOrders();

            foreach (Order orderLine in order)
            {
                orderLine.Items = itemsRepository.GetOrderItemsByOrder(orderLine.Id);
            }
            return Ok(mapper.Map<IEnumerable<ReadOrderDTO>>(order));
        }

        /// <summary>
        /// List an order and return a code 200(OK) menaning users and admin can view order list or 404(NOT FOUND) meaning order not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadOrderDTO> GetOrderById(int id)
        {
            var order = repository.GetOrderById(id);

            if (order is null)
                return NotFound();

            return Ok(mapper.Map<ReadOrderDTO>(order));
        }

        /// <summary>
        /// User can create an order response is a code 201(CREATED) or an error 400(BAD REQUEST) order was not created
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadOrderDTO> CreateOrder(CreateOrderDTO orderDTO)
        {
            Order order = mapper.Map<Order>(orderDTO);
            repository.CreateOrder(order);
            order.Items = itemsRepository.GetOrderItemsByOrder(order.Id);
            var orderRead = mapper.Map<ReadOrderDTO>(order);

            return CreatedAtAction(nameof(GetOrders), new { id = orderRead.Id }, orderRead);
        }
        
        /// <summary>
        /// Cancel the result of an order with a code 204(NO CONTENT) meaning order was sucessufully canceled or 404(NOT FOUND) order was not canceled
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadOrderDTO> CancelOrder(int id)
        {
            bool result = false;

            result = repository.CancelOrder(id);
            if (result is false)
                return NotFound();
            return Ok(result);
        }
    }
}
