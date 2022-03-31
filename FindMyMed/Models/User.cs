﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public int VAT { get; set; }
        public int Phone { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public User() { }
    }
}
