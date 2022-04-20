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
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        private readonly IOrdersRepository repository;
        private readonly IMapper mapper;

        public OrderController(IOrdersRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<ReadOrderDTO>> GetOrders()
        {
            var order = repository.GetOrders();
            return Ok(mapper.Map<IEnumerable<ReadOrderDTO>>(order));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<ReadOrderDTO> GetOrderById(int id)
        {
            var order = repository.GetOrderById(id);

            if (order is null)
                return NotFound();

            return Ok(mapper.Map<ReadOrderDTO>(order));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<ReadOrderDTO> CreateOrder(CreateOrderDTO orderDTO)
        {
            Order order = mapper.Map<Order>(orderDTO);
            List<OrderItem> orderItems = new List<OrderItem>();
            repository.CreateOrder(order, orderItems);

            var orderRead = mapper.Map<ReadOrderDTO>(order);

            return CreatedAtAction(nameof(GetOrders), new { id = orderRead.Id }, orderRead);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<ReadOrderDTO> UpdateOrder(int id, UpdateOrderDTO order)
        {
            if (order is null)
                return NotFound();

            repository.UpdateOrder(id, order);

            return NoContent();
        }
    }
}
