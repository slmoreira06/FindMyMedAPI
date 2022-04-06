namespace FindMyMed.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public float TotalPrice { get; set; }
        public int VAT { get; set; }
        public string PaymentMethod { get; set; }
        public int CardNumber { get; set; }
        public int UsedPoints { get; set; }
        public StatusEnum Status { get; set; }


        public Order()
        {
            this.Status = StatusEnum.Activo;
        }

    }

}
