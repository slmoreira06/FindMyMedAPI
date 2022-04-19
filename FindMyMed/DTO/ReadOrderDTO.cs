using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class ReadOrderDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public float TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
