using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class CreateInventoryDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int PharmacyId { get; set; }
    }
}
