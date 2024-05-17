using AutoMapper;
using Lara.Data.Repository;
using Lara.Domain.Contracts;
using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;
using Lara.Domain.Exceptions;

namespace Lara.Service.Service;

public class BorrowedBookService : BaseService<BorrowedBook>
{
    private IBaseService<Book> _bookService;
    private UserService _userService;
    private readonly BorrowedMailService _borrowedMailService;

    public BorrowedBookService(BorrowedBookRepository repository,
        IBaseService<Book> bookService,
        IMapper mapper,
        UserService userService,
        BorrowedMailService borrowedMailService
    ) : base(repository, mapper)
    {
        _bookService = bookService;
        _userService = userService;
        _borrowedMailService = borrowedMailService;
    }

    public override BorrowedBook Add<TValidator, TEntityDto>(TEntityDto obj)
    {
        var bookDto = obj as CreateBorrowedDto;

        var book = _bookService.GetById(bookDto.BookId);

        if (book.Quantity == 0)
        {
            throw new ExceededQuantityException(
                $"Não é possível pegar o livro {book.Title} emprestado, pois não há nenhum disponível.");
        }

        var repo = (BorrowedBookRepository)_repository;

        if (repo.CheckIfAlreadyBorrowed(bookDto.UserId, bookDto.BookId))
        {
            throw new AlreadyBorrowed($"Usuário solicitante já está em posse do livro {book.Title}");
        }

        var borrowedBook = base.Add<TValidator, TEntityDto>(obj);

        // Atualiza a quantidade de livros disponíveis.
        book.Quantity -= 1;
        _bookService.Update(book);

        var user = _userService.Get(bookDto.UserId);

        var message = _borrowedMailService.CreateMessage(user.Email, "[LARA] - Empréstimo de livro", borrowedBook);
        
        _borrowedMailService.SendMessage(
            user.Email, 
            "[LARA] - Empréstimo de livro", 
            message);

        return borrowedBook;
    }
}