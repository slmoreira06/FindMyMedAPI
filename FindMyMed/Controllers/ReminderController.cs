using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Twilio;
using Twilio.Types;
using Twilio.TwiML;
using System.Security.Claims;
using FindMyMed.DAL;
using FindMyMed.Models;
using FindMyMed.DTO.Create;
using Twilio.Rest.Api.V2010.Account;
using Twilio.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FindMyMed.DTO.Read;
using FindMyMed.DTO.Update;
using FindMyMed.DAL.Repositories;
using AutoMapper;

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

        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<IEnumerable<ReadReminderDTO>> GetReminders()
        {
            var reminders = repository.GetReminders();
            return Ok(mapper.Map<IEnumerable<ReadReminderDTO>>(reminders));
        }

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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadReminderDTO> CreateReminder(CreateReminderDTO reminderDTO)
        {
            Reminder reminder = mapper.Map<Reminder>(reminderDTO);
            var accountSid = this.configuration.GetSection("Twilio")["AccountSid"];
            var authToken = this.configuration.GetSection("Twilio")["AuthToken"];
            TwilioClient.Init(accountSid, authToken);
            var email = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault().Value;
            User user = new User();
            user = usersRepository.GetUserByEmail(email);
            var time = DateTime.UtcNow;

            var from = new PhoneNumber(this.configuration.GetSection("Twilio")["PhoneNumber"]);
            var to = new PhoneNumber("+351" + user.Phone.ToString());
            var body = "Dear " + user.FirstName + " " + user.LastName + ", please note the requested reminder!: \n"+ "\""+ reminder.Text + "\"";
            
            if (reminder.Repeat is Repetition.Once)
                time = DateTime.UtcNow.AddMinutes(5);
            
            else if (reminder.Repeat is Repetition.Daily)
                time = DateTime.UtcNow.AddHours(24);
            
            else if(reminder.Repeat is Repetition.Weekly)
                time = DateTime.UtcNow.AddDays(7);
            
            else if(reminder.Repeat is Repetition.Monthly)
                time = DateTime.UtcNow.AddMonths(1);
            
            var message = MessageResource.Create(
                to: to,
                from: from,
                body: body,
                sendAt: time,
                scheduleType: MessageResource.ScheduleTypeEnum.Fixed
                );


            repository.CreateReminder(reminder);
            var remRead = mapper.Map<ReadReminderDTO>(reminder);
            return CreatedAtAction(nameof(GetReminders), new { id = remRead.Id }, remRead);
        }

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
