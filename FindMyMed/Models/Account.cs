using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public StatusEnum Status { get; set; }
        public Types Type { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }

    public enum StatusEnum
    {
        Activo,
        Inactivo,
    }
    public enum Types
    {
        [Display(Name = "Utilizador")]
        User,
        [Display(Name = "Farmácia")]
        Pharm,
    }
}
