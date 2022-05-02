using FindMyMed.DTO.Update;
using FindMyMed.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.DAL.Repositories
{
    public interface ICalendarEventsRepository
    {
        bool CreateCalendarEvent(CalendarEvent calendarEvent);
        IEnumerable<CalendarEvent> GetEvents();
        CalendarEvent GetCalendarEventById(int id);
        UpdateCalendarEventDTO UpdateCalendarEvent(int id, UpdateCalendarEventDTO eventDTO);
        bool DeleteCalendarEvent(CalendarEvent calendarEvent);

    }
}
