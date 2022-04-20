using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class CreateOrderDTO
    {
        public DateTime CreationDate { get; set; }
        public float TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
