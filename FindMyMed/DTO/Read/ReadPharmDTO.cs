﻿namespace FindMyMed.DTO
{
    public class ReadPharmDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int Phone { get; set; }
        public int VAT { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Distance { get; set; }
    }
}
