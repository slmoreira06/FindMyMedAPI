using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Ref { get; set; }

    }
}
