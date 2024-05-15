using AutoMapper;
using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lara.Service.Service;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IBaseTokenService _jwtService;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IBaseTokenService jwtService,
        IMapper mapper)
    {
        _userManager = userManager;
        _jwtService = jwtService;
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

    public async Task<TokenDto> Login(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (!result.Succeeded)
        {
            throw new Exception("Não foi possível realizar o login! Verifique as credencias informadas");
        }

        var user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(user =>
            user.Email == email)!;

        var token = await _jwtService.GenerateToken(user);

        return token;
    }
}