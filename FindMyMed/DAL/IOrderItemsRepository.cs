using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IOrderItemsRepository
    {
        bool CreateOrderItem(OrderItem orderItem);
        OrderItem GetOrderItemById(int id);
        IEnumerable<OrderItem> GetOrderItems();
        UpdateOrderItemDTO UpdateOrderItem(int id, UpdateOrderItemDTO orderItemDTO);
    }
}
