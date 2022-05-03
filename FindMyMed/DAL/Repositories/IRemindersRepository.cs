using FindMyMed.DTO.Create;
using FindMyMed.DTO.Update;
using FindMyMed.Models;

namespace FindMyMed.DAL.Repositories
{
    public interface IRemindersRepository
    {
        bool CreateReminder(Reminder reminder);
        IEnumerable<Reminder> GetReminders();
        Reminder GetReminderById(int id);
        UpdateReminderDTO CancelReminder(int id, UpdateReminderDTO reminderDTO);
    }
}
