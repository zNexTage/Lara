using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Exceptions;
using Lara.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace Lara.Application.API.Controllers
{
    public class UserController : LaraControllerBase
    {
        private readonly UserService _userService;
        private readonly IBaseTokenService _jwtService;

        public UserController(UserService userService, IBaseTokenService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        
        /// <summary>
        /// Registra um usuário (cliente) na base de dados
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <response code="201">Retorna os dados do usuário criado.</response>
        /// <response code="400">Os campos que foram preenchidos incorretamente.</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var user = await _userService.Add(createUserDto);
                
                return Created("", user);
            }
            catch (UserCreationException e)
            {
                return BadRequest(e.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var token = await _userService.Login(email, password);

                return Ok(token);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
