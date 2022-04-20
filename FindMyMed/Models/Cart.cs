﻿namespace FindMyMed.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public int UsedPoints { get; set; }
        public float TotalPrice { get; set; }
        public CartStatus Status { get; set; }
    }

    public enum CartStatus
    {
        Payd,
        Cancelled,
        Pending
    }
}
