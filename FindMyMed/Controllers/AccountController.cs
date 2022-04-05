/*
 * Projeto De Desenvolvimento de Software
/*! \brief Projeto De Desenvolvimento de Software
 *         Engenharia de Sistemas informáticos
 *         
 * !@file accounts.cs
 * <Author 1>A8544 Sara Moreira</Author 1>
 * <Author 2>A18854 Isabela Magalhaes</Author 2>
 * <Author 3>A18864 Patricia Santos</Author 3>
 * <Author 4>A19334 José Matos</Author 4>
 * <Author 5>A19255 Alexandre Carvalho/Author 5>
 *
 */
using AutoMapper;
using FindMyMed.DAL;
using FindMyMed.DTO;
using FindMyMed.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindMyMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
       
        private readonly IAccountsRepository repository;
        private readonly IMapper mapper;

        public AccountController(IAccountsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<ReadAccountDTO>> GetAccounts()
        {
            var accounts = repository.GetAccounts();
            return Ok(mapper.Map<IEnumerable<ReadAccountDTO>>(accounts));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<ReadAccountDTO> GetAccountById(int id)
        {
            var account = repository.GetAccountById(id);

            if (account is null)
                return NotFound();

            return Ok(mapper.Map<ReadAccountDTO>(account));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<ReadAccountDTO> CreateAccount(CreateAccountDTO accDTO)
        {
            Account acc = mapper.Map<Account>(accDTO);
            repository.CreateAccount(acc);

            var accRead = mapper.Map<ReadAccountDTO>(acc);

            return CreatedAtAction(nameof(GetAccounts), new { id = accRead.Id }, accRead);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<ReadAccountDTO> DeactivateAccount(int  id, UpdateAccountDTO acc)
        {
            if (acc is null)
                return NotFound();

            repository.DeactivateAccount(id, acc);

            return NoContent();
        }
    }
}
