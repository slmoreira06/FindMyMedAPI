namespace FindMyMed.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Repetition Repeat { get; set; }
        public int Hours { get; set; }
        public Status Status { get; set; }
        public string MessageSid { get; set; }
    }

    public enum Repetition
    {
        Seconds,
        Hourly,
        Daily,
        Weekly,
        Monthly
    }

    public enum Status
    {
        Active,
        Inactive
    }
}

