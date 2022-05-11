namespace FindMyMed.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public int UsedPoints { get; set; }
        public double TotalPrice { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public CartStatus Status { get; set; }
        public Checkout Checkout { get; set; }
    }

    public enum CartStatus
    {
        Paid,
        Cancelled,
        Pending
    }

    public enum Checkout
    {
        Yes,
        No,
    }
}
