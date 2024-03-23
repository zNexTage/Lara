using AutoMapper;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lara.Service.Service;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
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
        }

        ;

        var roleResult = await _userManager.AddToRoleAsync(user, "CUSTOMER");

        if (roleResult.Succeeded) return _mapper.Map<ReadUserDto>(user);
        {
            var errors = result.Errors.ToDictionary(error => error.Code, error => error.Description);

            throw new UserCreationException(errors);
        }
    }

    public async Task<ReadUserDto> Get(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            throw new NotFoundException($"Não foi localizado um usuário com Id {id}");
        }

        return _mapper.Map<ReadUserDto>(user);
    }

    public async Task<ReadUserDto> Login(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (!result.Succeeded)
        {
            throw new Exception("Não foi possível realizar o login! Verifique as credencias informadas");
        }

        var user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(user =>
            user.Email == email)!;
        
        return _mapper.Map<ReadUserDto>(user);
    }
}