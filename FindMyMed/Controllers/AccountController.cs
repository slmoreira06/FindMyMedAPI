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
        /// This controller operate with any account request from database. 
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
        /// <returns>StatusCode</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<IEnumerable<ReadAccountDTO>> GetAccounts()
        {
            var accounts = repository.GetAccounts();
            return Ok(mapper.Map<IEnumerable<ReadAccountDTO>>(accounts));
        }

        /// <summary>
        /// Show an account, could deploy an error 200(OK) meaning the account was found or deploy an error 404(NOT FOUND) meaning no Account was found. Only admin can check the accounts. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>StatusCode</returns>
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
        /// Create an account, can deploy a code 201(CREATED) meaning an account was created, or an error 400  couldn't create an acccount.
        /// </summary>
        /// <param name="accDTO"></param>
        /// <returns>StatusCode</returns>
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

        /// <summary>
        /// Update an account, can deploy a code 204(NO CONTENT) meaning content was updated , or an error 404(BAD REQUEST) no account corresponding to update.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="acc"></param>
        /// <returns>StatusCode</returns>
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
