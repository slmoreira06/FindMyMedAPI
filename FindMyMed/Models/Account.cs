﻿namespace FindMyMed.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public StatusEnum Status { get; set; }
        public Types Type { get; set; }

        public Account()
        {
        }
    }

    public enum StatusEnum
    {
        Activo,
        Inactivo,
    }
    public enum Types
    {
        User,
        Pharm,
        Admin
    }
}
