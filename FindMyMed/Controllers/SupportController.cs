using AutoMapper;
using FindMyMed.DAL;
using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : Controller
    {
        private readonly ISupportsRepository repository;
        private readonly IMapper mapper;

        public SupportController(ISupportsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<IEnumerable<ReadSupportDTO>> GetSupports()
        {
            var sup = repository.GetSupports();
            return Ok(mapper.Map<IEnumerable<ReadSupportDTO>>(sup));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<ReadSupportDTO> GetSupportById(int id)
        {
            var sup = repository.GetSupportById(id);

            if (sup is null)
                return NotFound(); 

            return Ok(mapper.Map<ReadSupportDTO>(sup));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<ReadSupportDTO> CreateSupport(CreateSupportDTO supDTO)
        {
            Support sup = mapper.Map<Support>(supDTO);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(sup.Name,sup.Email));
            email.To.Add(MailboxAddress.Parse("a8544@alunos.ipca.pt"));
            email.Subject = "Email de suporte";
            email.Body = new TextPart(TextFormat.Plain) { Text = "\nEmail de suporte\n\n" +
                "\tNome: " + sup.Name + "\n\tEmail: " + sup.Email + "\n\tContacto: " + sup.Phone +
                "\n\tAssunto: " + sup.Subject + "\n\tMensagem: " + sup.Message};

            using var smtp = new SmtpClient();
            smtp.Connect("smtp-relay.sendinblue.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("slmoreira06.94@gmail.com", "ELPxsAUjmZYnwJFv");
            smtp.Send(email);
            smtp.Disconnect(true);

            repository.CreateSupport(sup);

            var supRead = mapper.Map<ReadSupportDTO>(sup);

            return CreatedAtAction(nameof(GetSupports), new { id = supRead.Id }, supRead);
        }
    }
}
