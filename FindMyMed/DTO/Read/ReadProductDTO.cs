using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class ReadProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
    }
}
