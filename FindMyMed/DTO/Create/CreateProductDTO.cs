using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
    }
}
