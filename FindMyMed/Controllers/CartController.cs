using FindMyMed.DAL;
using FindMyMed.DTO;
using FindMyMed.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICartsRepository repository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IOrderItemsRepository orderItemsRepository;
        private readonly IProductsRepository productsRepository;

        public CartController(ICartsRepository repository, IOrdersRepository ordersRepository, 
            IOrderItemsRepository orderItemsRepository, IProductsRepository productsRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.ordersRepository = ordersRepository;
            this.orderItemsRepository = orderItemsRepository;
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<ReadCartDTO> SaveCart(UpdateCartDTO cart)
        {
            int product = 0;
            Product prod = new Product();
            double sum = 0;
            if (cart is null)
                return NotFound();
            else
            {
                cart.Order = ordersRepository.GetOrderById(cart.OrderId);
                cart.Order.Items = orderItemsRepository.GetOrderItemsByOrder(cart.OrderId);
                foreach (OrderItem item in cart.Order.Items)
                {
                    product = item.ProductId;
                    prod = productsRepository.GetProductById(product);
                    sum += prod.Price * item.Quantity;
                }
                cart.TotalPrice = sum;
                repository.SaveCart(cart);
            }
            var cartRead = mapper.Map<UpdateCartDTO>(cart);

            return Ok(cartRead);
        }
    }
}
