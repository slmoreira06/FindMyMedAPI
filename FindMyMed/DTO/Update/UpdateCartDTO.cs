using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class UpdateCartDTO
    {
        public string PaymentMethod { get; set; }
        public int UsedPoints { get; set; }
        public float TotalPrice { get; set; }
        public CartStatus Status { get; set; }
    }
}
