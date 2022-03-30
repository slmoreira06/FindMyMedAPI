using FindMyMed.Models;
using System.ComponentModel.DataAnnotations;

namespace FindMyMed.DTO
{
    public class UpdateAccountDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public StatusEnum Status { get; set; }
    }
}
