namespace FindMyMed.DTO
{
    public class ReadOrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Reference { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
