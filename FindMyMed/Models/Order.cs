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
        [Display(Name = "Pendente")]
        Pending,
        [Display(Name = "Completo")]
        Completed,
        [Display(Name = "Cancelado")]
        Cancelled
    }


}
