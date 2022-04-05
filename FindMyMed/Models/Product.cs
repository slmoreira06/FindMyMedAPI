using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Ref { get; set; }
        public StatusEnum Status { get; set; }

        public Product()
        {
            this.Status = StatusEnum.Activo;
        }
    }      
    
}
