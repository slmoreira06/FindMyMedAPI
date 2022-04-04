﻿using FindMyMed.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMyMed.DTO
{
    public class CreateAccountDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public Types Type { get; set; }
    }
}