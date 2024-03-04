using Lara.Domain.DataTransferObjects;
using Lara.Domain.Exceptions;
using Lara.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace Lara.Application.API.Controllers
{
    public class UserController : LaraControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
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
    }
}
