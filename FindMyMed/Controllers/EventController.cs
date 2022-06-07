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

        /// <summary>
        /// This controller operate with any event request from the database.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public EventController(ICalendarEventsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Show all events return a code 200(OK).
        /// </summary>
        /// <returns>StatusCode</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<IEnumerable<ReadCalendarEventDTO>> GetEvents()
        {
            var events = repository.GetEvents();
            return Ok(mapper.Map<IEnumerable<ReadCalendarEventDTO>>(events));
        }

        /// <summary>
        /// Show the event in calendar of an acount deploy an code 200(OK). Deploy an error 404(NOT FOUND) if Account doesnt correspond to a event.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
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

        /// <summary>
        /// Create a event in the calendar. Deploy a code 201(CREATED), refers to an envent created, or an error 400(BAD REQUEST).
        /// </summary>
        /// <param name="calendarEventDTO"></param>
        /// <returns>Event</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadCalendarEventDTO> CreateCalendarEvent(CreateCalendarEventDTO calendarEventDTO)
        {

            CalendarEvent calendar = mapper.Map<CalendarEvent>(calendarEventDTO);
            repository.CreateCalendarEvent(calendar);

            var calendarRead = mapper.Map<ReadCalendarEventDTO>(calendar);

            return CreatedAtAction(nameof(GetEvents), new { id = calendarRead.Id }, calendarRead);
        }

        /// <summary>
        /// Update an event, can deploy a code 204(NO CONTENT) meaning event was updated , or an error 404(BAD REQUEST) no event corresponding to update.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="calendarEventDTO"></param>
        /// <returns>StatusCode</returns>
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
        /// <summary>
        /// Delete an event, can deploy a code 204(NO CONTENT) meaning event was deleted, or an error 404(BAD REQUEST) no event corresponding to delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult DeleteCalendarEvent(int id)
        {
            var calendarEvent = repository.GetCalendarEventById(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            repository.DeleteCalendarEvent(calendarEvent);

            return NoContent();
        }
    }
}

