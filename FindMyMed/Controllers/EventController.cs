using AutoMapper;
using FindMyMed.DAL.Repositories;
using FindMyMed.DTO.Create;
using FindMyMed.DTO.Read;
using FindMyMed.DTO.Update;
using FindMyMed.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly ICalendarEventsRepository repository;
        private readonly IMapper mapper;

        public EventController(ICalendarEventsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
     //   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<IEnumerable<ReadCalendarEventDTO>> GetEvents()
        {
            var events = repository.GetEvents();
            return Ok(mapper.Map<IEnumerable<ReadCalendarEventDTO>>(events));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadCalendarEventDTO> GetCalendarEventById(int id)
        {
            var events = repository.GetCalendarEventById(id);

            if (events is null)
                return NotFound();

            return Ok(mapper.Map<ReadCalendarEventDTO>(events));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
      //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadCalendarEventDTO> CreateCalendarEvent(CreateCalendarEventDTO calendarEventDTO)
        {

            CalendarEvent calendar = mapper.Map<CalendarEvent>(calendarEventDTO);
            repository.CreateCalendarEvent(calendar);

            var calendarRead = mapper.Map<ReadCalendarEventDTO>(calendar);

            return CreatedAtAction(nameof(GetEvents), new { id = calendarRead.Id }, calendarRead);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadCalendarEventDTO> UpdateCalendarEvent(int id, UpdateCalendarEventDTO calendarEventDTO)
        {
            if (calendarEventDTO is null)
                return NotFound();

            repository.UpdateCalendarEvent(id, calendarEventDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult DeleteCalendarEvent(int id)
        {
            repository.DeleteCalendarEvent(id);

            return NoContent();
        }
    }
}

