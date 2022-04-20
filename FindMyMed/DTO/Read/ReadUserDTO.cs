namespace FindMyMed.DTO
{
    public class ReadUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime Birthday { get; set; }
        public int VAT { get; set; }
        public int Phone { get; set; }
        public int UserPoints { get; set; }
    }
}
