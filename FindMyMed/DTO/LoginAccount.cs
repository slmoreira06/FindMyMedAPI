﻿using FindMyMed.Models;

namespace FindMyMed.DTO
{
    public class LoginAccount
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public StatusEnum Status { get; set; }
    }
}
