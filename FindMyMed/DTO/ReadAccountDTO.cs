using FindMyMed.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.DTO
{
    public class ReadAccountDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public StatusEnum Status { get; set; }
        public Types Type { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
