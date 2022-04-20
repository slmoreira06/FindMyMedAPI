using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class ReadInventoryDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
