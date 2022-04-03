using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public StatusEnum Status { get; set; }
        [EnumDataType(typeof(Types))]
        [JsonConverter(typeof(StringEnumConverter))]
        public Types Type { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public Account()
        {
            this.Status = StatusEnum.Activo;
        }
    }

    public enum StatusEnum
    {
        Activo,
        Inactivo,
    }
    public enum Types
    {
        [Display(Name = "Utilizador")]
        User,
        [Display(Name = "Farmácia")]
        Pharm,
    }
}
