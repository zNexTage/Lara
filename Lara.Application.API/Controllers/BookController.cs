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

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _bookService.Delete(id);

        return NoContent();
    }

    [HttpPost]
    public IActionResult Create([FromBody] BookDto bookDto)
    {
        var book = _bookService.Add<BookValidator, BookDto>(bookDto);

        return Created(string.Empty, book);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] BookDto bookDto)
    {
        var book = _bookService.Update<BookValidator, BookDto>(id, bookDto);

        return Created("", book);
    }
}