using FindMyMed.Models;

namespace FindMyMed.DTO.Create
{
    public class CreateReminderDTO
    {
        public string Text { get; set; }
        public Repetition Repeat { get; set; }
        public int Hours { get; set; }
        public Status Status { get; set; }
        public string MessageSid { get; set; }
    }

}
