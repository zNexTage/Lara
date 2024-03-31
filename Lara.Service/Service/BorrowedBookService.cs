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
    
    public BorrowedBookService(BorrowedBookRepository repository, IBaseService<Book> bookService, IMapper mapper): base(repository, mapper)
    {
        _bookService = bookService;
    }

    public override BorrowedBook Add<TValidator, TEntityDto>(TEntityDto obj)
    {
        var bookDto = obj as CreateBorrowedDto;

        _bookService.GetById(bookDto.BookId);
        
        return base.Add<TValidator, TEntityDto>(obj);
    }
}