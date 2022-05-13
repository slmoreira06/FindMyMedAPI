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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FindMyMed.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly IAccountsRepository repository;
        private readonly IMapper mapper;

        /// <summary>
        /// This controller will map every account on interface to the database. 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public AccountController(IAccountsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        
        /// <summary>
        /// Lists all accounts and deploys the response code 200(OK). Only the admin can see the list.
        /// </summary>
        /// <returns>List{{Accounts}}</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<IEnumerable<ReadAccountDTO>> GetAccounts()
        {
            var accounts = repository.GetAccounts();
            return Ok(mapper.Map<IEnumerable<ReadAccountDTO>>(accounts));
        }

        /// <summary>
        /// Show an account, could deploy an error 200(OK) meaning the Account was found or deploy an error 404(NOT FOUND) meaning no user was found. Only admin can check the user. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Account</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<ReadAccountDTO> GetAccountById(int id)
        {
            var account = repository.GetAccountById(id);

            if (account is null)
                return NotFound();

            return Ok(mapper.Map<ReadAccountDTO>(account));
        }

        /// <summary>
        /// Create an Account, can deploy a error 201(CREATED) meaning an Account was created, or an error 400 meaning could not create an Acccount
        /// </summary>
        /// <param name="accDTO"></param>
        /// <returns></returns>
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        public ActionResult<ReadAccountDTO> DeactivateAccount(int id, UpdateAccountDTO acc)
        {
            if (acc is null)
                return NotFound();

            repository.DeactivateAccount(id, acc);

            return NoContent();
        }
    }
}
