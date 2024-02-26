using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Service.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Lara.Application.API.Controllers;

public class BookController : LaraControllerBase
{
    private IBaseService<Book> _bookService;

    public BookController(IBaseService<Book> bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_bookService.Get());
    }

[HttpPost]
    public IActionResult Create([FromBody] BookDto bookDto)
    {
        var book = _bookService.Add<BookValidator, BookDto>(bookDto);

        return Created(string.Empty, book);
    }
}