using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class UpdateOrderDTO
    {
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
