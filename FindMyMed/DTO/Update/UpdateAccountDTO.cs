using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class UpdateAccountDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public StatusEnum Status { get; set; }
    }
}
