using AutoMapper;
using FindMyMed.DAL;
using FindMyMed.DAL.Repositories;
using FindMyMed.DTO.Create;
using FindMyMed.DTO.Read;
using FindMyMed.DTO.Update;
using FindMyMed.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Twilio;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : TwilioController
    {
        private readonly IRemindersRepository repository;
        private readonly IConfiguration configuration;
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;

        public ReminderController(IRemindersRepository repository, IConfiguration configuration, IUsersRepository usersRepository, IMapper mapper)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Find all reminders
        /// </summary>
        /// <returns>StatusCode</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<IEnumerable<ReadReminderDTO>> GetReminders()
        {
            var reminders = repository.GetReminders();
            return Ok(mapper.Map<IEnumerable<ReadReminderDTO>>(reminders));
        }
        /// <summary>
        /// Find a specific reminder
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadReminderDTO> GetReminderById(int id)
        {
            var reminder = repository.GetReminderById(id);

            if (reminder is null)
                return NotFound();

            return Ok(mapper.Map<ReadReminderDTO>(reminder));
        }
        /// <summary>
        /// Update a reminder
        /// </summary>
        /// <param name="reminderDTO"></param>
        /// <returns>StatusCode</returns>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public async Task<ActionResult<ReadReminderDTO>> CreateReminder(CreateReminderDTO reminderDTO)
        {
            Reminder reminder = mapper.Map<Reminder>(reminderDTO);
            var accountSid = this.configuration.GetSection("Twilio")["AccountSid"];
            var authToken = this.configuration.GetSection("Twilio")["AuthToken"];

            TwilioClient.Init(accountSid, authToken);
            var email = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault().Value;
            User user = new User();
            user = usersRepository.GetUserByEmail(email);
            var time = new TimeSpan();

            var from = new PhoneNumber(this.configuration.GetSection("Twilio")["PhoneNumber"]);
            var to = new PhoneNumber("+351" + user.Phone.ToString());
            var body = "Car@ " + user.FirstName + " " + user.LastName + ", por favor note o lembrete registado!: \n" + "\"" + reminder.Text + "\"";

            if (reminder.Repeat is Repetition.Seconds)
                time = TimeSpan.FromSeconds(180);

            else if (reminder.Repeat is Repetition.Hourly)
                time = TimeSpan.FromHours(8);

            else if (reminder.Repeat is Repetition.Daily)
                time = TimeSpan.FromHours(24);

            else if (reminder.Repeat is Repetition.Weekly)
                time = TimeSpan.FromDays(7);

            else if (reminder.Repeat is Repetition.Monthly)
                time = TimeSpan.FromDays(30);

            repository.CreateReminder(reminder);
            var remRead = mapper.Map<ReadReminderDTO>(reminder);

            var timer = new PeriodicTimer(time);
            while (await timer.WaitForNextTickAsync())
            {
                var message = MessageResource.Create(
                to: to,
                from: from,
                body: body,
                scheduleType: MessageResource.ScheduleTypeEnum.Fixed
                );
            }

            return CreatedAtAction(nameof(GetReminders), new { id = remRead.Id }, remRead);
        }
        /// <summary>
        /// Update a reminder by canceling it.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reminderDTO"></param>
        /// <returns>StatusCode</returns>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<ReadReminderDTO> CancelReminder(int id, UpdateReminderDTO reminderDTO)
        {
            if (reminderDTO is null)
                return NotFound();

            var reminder = repository.GetReminderById(id);

            var messages = MessageResource.Update(pathSid: reminder.MessageSid, status: MessageResource.UpdateStatusEnum.Canceled);
            
            repository.CancelReminder(id, reminderDTO);

            return Ok("Reminder cancelled" + messages);
        }
    }
}
