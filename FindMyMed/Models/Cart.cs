namespace FindMyMed.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PharmAddress { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public int VAT { get; set; }
        public int UsedPoints { get; set; }
        public float TotalPrice { get; set; }

    }
}
