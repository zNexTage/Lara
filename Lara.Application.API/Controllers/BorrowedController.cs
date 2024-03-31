using System.Security.Claims;
using AutoMapper;
using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Domain.Exceptions;
using Lara.Service.Service;
using Lara.Service.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lara.Application.API.Controllers;

public class BorrowedController : LaraControllerBase
{
    private readonly BorrowedBookService _borrowedService;
    private readonly IMapper _mapper;

    public BorrowedController(BorrowedBookService borrowedService, IMapper mapper)
    {
        _borrowedService = borrowedService;
        _mapper = mapper;
    }
    
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost]
    public IActionResult BorrowBook(CreateBorrowedDto createBorrowedDto)
    {
        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        createBorrowedDto.UserId = userId;

        try
        {
            var createdBorrowed = _borrowedService.Add<BorrowedValidator, CreateBorrowedDto>(createBorrowedDto);
            return Created("", _mapper.Map<ReadBorrowedDto>(createdBorrowed));
        }
        catch (Exception err)
        {
            return err switch
            {
                NotFoundException =>
                    //Caso o livro não seja encontrado
                    NotFound(err.Message),
                ExceededQuantityException or AlreadyBorrowed => BadRequest(err.Message),
                _ => StatusCode(500,
                    "Ops! Ocorreu um erro interno. Estamos ciente do problema e estamos trabalhando para conserta-lo.")
            };
        }
    }
}