using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class ReadOrderDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
