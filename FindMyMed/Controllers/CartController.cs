using FindMyMed.DAL;
using FindMyMed.DTO;
using FindMyMed.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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
        private readonly IInventoriesRepository inventoriesRepository;
        private readonly IUsersRepository usersRepository;
        /// <summary>
        /// This controller will map any Cart request from database.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="ordersRepository"></param>
        /// <param name="orderItemsRepository"></param>
        /// <param name="productsRepository"></param>
        /// <param name="inventoriesRepository"></param>
        /// <param name="usersRepository"></param>
        /// <param name="mapper"></param>
        public CartController(ICartsRepository repository, IOrdersRepository ordersRepository,
            IOrderItemsRepository orderItemsRepository, IProductsRepository productsRepository,
            IInventoriesRepository inventoriesRepository, IUsersRepository usersRepository,
            IMapper mapper)
        {
            this.repository = repository;
            this.ordersRepository = ordersRepository;
            this.orderItemsRepository = orderItemsRepository;
            this.productsRepository = productsRepository;
            this.inventoriesRepository = inventoriesRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Update all items on cart, including customers points, return a code 204(NO CONTENT) or an error 404(NOT FOUND).
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>StatusCode</returns>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadCartDTO> SaveCart(UpdateCartDTO cart)
        {
            int product = 0;
            int wonPoints = 0;
            double sum = 0;
            double newSum = 0;
            List<Product> prods = new List<Product>();
            Product prod = new Product();
            Inventory inv = new Inventory();
            UpdateInventoryDTO invDto = new UpdateInventoryDTO();
            UpdateUserDTO updateUser = new UpdateUserDTO();
            User user = new User();
            var email = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault().Value;
            user = usersRepository.GetUserByEmail(email);

            if (cart is null)
                return NotFound();
            else
            {
                try
                {
                    cart.Order = ordersRepository.GetOrderById(cart.OrderId);
                    cart.Order.Items = orderItemsRepository.GetOrderItemsByOrder(cart.OrderId);
                    foreach (OrderItem item in cart.Order.Items)
                    {
                        product = item.ProductId;
                        prod = productsRepository.GetProductById(product);
                        sum += prod.Price * item.Quantity;
                        inv = inventoriesRepository.GetInventoryByProduct(prod.Id);
                        if (inv.Quantity > item.Quantity)
                        {
                            if (user.UserPoints > cart.UsedPoints)
                            {
                                user.UserPoints -= cart.UsedPoints;
                                cart.TotalPrice = sum;
                                inv.Quantity -= item.Quantity;
                                for (cart.UsedPoints = 10; cart.UsedPoints < 100; cart.UsedPoints += 10)
                                {
                                    cart.TotalPrice--;
                                }
                                newSum = cart.TotalPrice;
                            }
                        }
                        else
                            return NotFound();
                    }
                    for (newSum = 10; newSum < 100; newSum += 10)
                    {
                        wonPoints++;
                    }
                    if (cart.Checkout == Checkout.Yes)
                    {
                        user.UserPoints += wonPoints;
                        usersRepository.UpdateUserProfile(user.Id, updateUser);
                        var newInv = inventoriesRepository.UpdateInventory(inv.Id, invDto);
                        ordersRepository.OrderCheckout(cart.OrderId);
                        repository.SaveCart(cart);
                        var cartRead = mapper.Map<UpdateCartDTO>(cart);
                        return Ok(cartRead);
                    }
                    else
                    {
                        repository.SaveCart(cart);
                        var cartRead = mapper.Map<UpdateCartDTO>(cart);
                        return Ok(cartRead);
                    }

                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
            }
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult CancelCart()
        {
            try
            {
                Cart cart = new Cart();
                cart = repository.GetCart();
                ordersRepository.CancelOrder(cart.OrderId);
                repository.ClearCart();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

