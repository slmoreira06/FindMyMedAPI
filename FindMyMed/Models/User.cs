using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public int VAT { get; set; }
        public int PhoneNumber { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
    }
}
