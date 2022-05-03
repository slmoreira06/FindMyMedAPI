using FindMyMed.Models;

namespace FindMyMed.DTO.Update
{
    public class UpdateReminderDTO
    {
        public Status Status { get; set; }
        public string MessageSid { get; set; }
    }
}
