namespace FindMyMed.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Reference { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
