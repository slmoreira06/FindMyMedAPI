using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IOrderItemsRepository
    {
        OrderItem GetOrderItemById(int id);
        IEnumerable<OrderItem> GetOrderItemsByOrder(int id);
        UpdateOrderItemDTO UpdateOrderItem(int id, UpdateOrderItemDTO orderItemDTO);
    }
}
