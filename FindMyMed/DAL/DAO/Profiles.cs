using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public class Profiles : AutoMapper.Profile
    {
        public Profiles()
        {
            CreateMap<Account, ReadAccountDTO>();
            CreateMap<CreateAccountDTO, Account>();
            CreateMap<Account, UpdateAccountDTO>();
            CreateMap<UpdateAccountDTO, Account>();

            CreateMap<User, ReadUserDTO>();
            CreateMap<User, UpdateUserDTO>();
            CreateMap<UpdateUserDTO, User>();

            CreateMap<Pharmacy, ReadPharmDTO>();
            CreateMap<Pharmacy, UpdatePharmDTO>();
            CreateMap<UpdatePharmDTO, Pharmacy>();

            CreateMap<Order, ReadOrderDTO>();
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<Order, UpdateOrderDTO>();
            CreateMap<UpdateOrderDTO, Order>();

            CreateMap<OrderItem, ReadOrderItemDTO>();
            CreateMap<OrderItem, UpdateOrderItemDTO>();
            CreateMap<UpdateOrderItemDTO, OrderItem>();

            CreateMap<Product, ReadProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<Product, UpdateProductDTO>();
            CreateMap<UpdateProductDTO, Product>();

            CreateMap<Inventory, ReadInventoryDTO>();
            CreateMap<CreateInventoryDTO, Inventory>();
            CreateMap<Inventory, UpdateInventoryDTO>();
            CreateMap<UpdateInventoryDTO, Inventory>();

            CreateMap<Support, ReadSupportDTO>();
            CreateMap<CreateSupportDTO, Support>();

            CreateMap<Cart, ReadCartDTO>();
            CreateMap<Cart, UpdateCartDTO>();
            CreateMap<UpdateCartDTO, Cart>();
        }
    }
}
