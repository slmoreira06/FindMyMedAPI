using System.ComponentModel.DataAnnotations;
using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class CreateAccountDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public Types Type { get; set; }
    }
}
