using System.ComponentModel.DataAnnotations;
using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class CreateAccountDTO
    {
       
        public string Email { get; set; }        
        public string UserName { get; set; }
        public string Password { get; set; }
        public Types Type { get; set; }
    }
}
