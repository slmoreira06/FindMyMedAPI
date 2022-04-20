using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class ReadAccountDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public StatusEnum Status { get; set; }
        public Types Type { get; set; }

    }
}
