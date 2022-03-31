﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.Models
{
    public class Pharmacy
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int Phone { get; set; }
        public int VAT { get; set; }
        public string Address { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public Pharmacy() { }
    }
}
