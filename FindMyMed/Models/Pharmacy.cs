using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public int VAT { get; set; }
        public string Address { get; set; }
        public int AccountId { get; set; }

        List<Inventory> Inventories { get; set; }

        public Pharmacy() { }
    }
}
