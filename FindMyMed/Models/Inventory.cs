namespace FindMyMed.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int PharmacyId { get; set; }
    }
}
