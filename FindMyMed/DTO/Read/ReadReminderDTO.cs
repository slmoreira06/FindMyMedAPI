using FindMyMed.Models;

namespace FindMyMed.DTO.Read
{
    public class ReadReminderDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Repetition Repeat { get; set; }
        public Status Status { get; set; }
        public string MessageSid { get; set; }
    }
}
