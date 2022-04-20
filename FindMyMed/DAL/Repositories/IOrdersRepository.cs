using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IOrdersRepository
    {
        bool CreateOrder(Order order, List<OrderItem> orderItemList);
        Order GetOrderById(int id);
        IEnumerable<Order> GetOrders();
        UpdateOrderDTO UpdateOrder(int id, UpdateOrderDTO orderDTO);
    }
}
