namespace FindMyMed.DTO.Create
{
    public class CreateCalendarEventDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
