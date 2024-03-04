using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Domain.Exceptions;
using Lara.Service.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lara.Application.API.Controllers;

public class BookController : LaraControllerBase
{
    private IBaseService<Book> _bookService;

    public BookController(IBaseService<Book> bookService)
    {
        _bookService = bookService;
    }
    
    /// <summary>
    /// Retorna uma lista de livros
    /// </summary>
    /// <returns>Lista de livros</returns>
    /// <response code="200">Retorna a lista de livros cadastrados</response>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_bookService.Get());
    }
    
    /// <summary>
    /// Retorna um livro pelo id
    /// </summary>
    /// <param name="id">id do livro. Número inteiro</param>
    /// <response code="200">Retorna o livro pesquisado</response>
    /// <response code="404">Livro não localizado</response>
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var book = _bookService.GetById(id);
            return Ok(book);
        }
        catch (NotFoundException err)
        {
            return NotFound(err.Message);
        }

    }
    
    /// <summary>
    /// Remove um livro pelo seu id
    /// </summary>
    /// <param name="id">Id do livro que deverá ser removido</param>
    /// <response code="204">Livro foi removido com sucesso</response>
    /// <response code="404">Livro não localizado. Verifique o id</response>
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        try
        {
            _bookService.Delete(id);

            return NoContent();
        }
        catch (NotFoundException err)
        {
            return NotFound($"Não foi localizado livro com o ID {id}");
        }

    }
    
    /// <summary>
    /// Registra um livro na base de dados
    /// </summary>
    /// <param name="bookDto"></param>
    /// <response code="201">Livro registrado com sucesso</response>
    /// <response code="400">Os dados informados estão inválidos</response>
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public IActionResult Create([FromBody] BookDto bookDto)
    {
        try
        {
            var book = _bookService.Add<BookValidator, BookDto>(bookDto);

            return Created(string.Empty, book);
        }
        catch (FluentValidation.ValidationException err)
        {
            return BadRequest(err.Data);
        }
    }
    
    /// <summary>
    /// Atualiza um livro
    /// </summary>
    /// <param name="id">Id do livro para ser atualizado</param>
    /// <param name="bookDto">Dados para atualizar o livro</param>
    /// <response code="201">Livro atualizado com sucesso.</response>
    /// <response code="404">Livro não localizado. Verifique o id</response>
    /// <response code="400">Os dados informados estão inválidos</response>
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] BookDto bookDto)
    {
        try
        {
            var book = _bookService.Update<BookValidator, BookDto>(id, bookDto);

            return Created("", book);
        }
        catch (FluentValidation.ValidationException err)
        {
            return BadRequest(err.Data);
        }
        catch (NotFoundException err)
        {
            return NotFound($"Não foi localizado livro com o ID {id}");
        }
    }
}