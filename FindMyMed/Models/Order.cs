using System.ComponentModel.DataAnnotations;

namespace FindMyMed.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order() { }
    }
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }


}
