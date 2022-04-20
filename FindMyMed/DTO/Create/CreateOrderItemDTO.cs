using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class CreateOrderItemDTO
    {
        public int Quantity { get; set; }
        public string Reference { get; set; }
        public int ProductId { get; set; }
    }
}
