using AutoMapper;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Lara.Service.Service;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    private readonly IMapper _mapper;
    
    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }
    
    public async Task<ReadUserDto> Add(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<ApplicationUser>(createUserDto);
        
        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.ToDictionary(error => error.Code, error => error.Description);

            throw new UserCreationException(errors);
        };
        
        var roleResult = await _userManager.AddToRoleAsync(user, "CUSTOMER");

        if (roleResult.Succeeded) return _mapper.Map<ReadUserDto>(user);
        {
            var errors = result.Errors.ToDictionary(error => error.Code, error => error.Description);

            throw new UserCreationException(errors);
        }

    }
}