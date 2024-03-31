using AutoMapper;
using Lara.Data.Repository;
using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Domain.Exceptions;
using Lara.Service.Validators;

namespace Lara.Service.Service;

public class BorrowedBookService : BaseService<BorrowedBook>
{
    private IBaseService<Book> _bookService;
    
    public BorrowedBookService(BorrowedBookRepository repository, IBaseService<Book> bookService, IMapper mapper): base(repository, mapper)
    {
        _bookService = bookService;
    }

    public override BorrowedBook Add<TValidator, TEntityDto>(TEntityDto obj)
    {
        var bookDto = obj as CreateBorrowedDto;

        var book = _bookService.GetById(bookDto.BookId);

        if (book.Quantity == 0)
        {
            throw new ExceededQuantityException($"Não é possível pegar o livro {book.Title} emprestado, pois não há nenhum disponível.");
        }

        var repo = (BorrowedBookRepository)_repository;

        if (repo.CheckIfAlreadyBorrowed(bookDto.UserId, bookDto.BookId))
        {
            throw new AlreadyBorrowed($"Usuário solicitante já está em posse do livro {book.Title}");
        }

        var borrowedBook = base.Add<TValidator, TEntityDto>(obj);

        // Atualiza a quantidade de livros disponíveis.
        book.Quantity -= borrowedBook.Quantity;
         _bookService.Update(book);

        return borrowedBook;
    }
}