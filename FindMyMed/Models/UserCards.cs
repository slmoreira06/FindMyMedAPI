using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class UserCards
    {
        public int CardNumber { get; set; }
        public int Points { get; set; }
       
    }
}
