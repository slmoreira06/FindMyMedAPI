using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class ReadCartDTO
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public int UsedPoints { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public double TotalPrice { get; set; }
        public CartStatus Status { get; set; }
    }
}
