using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class CreateSupportDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
