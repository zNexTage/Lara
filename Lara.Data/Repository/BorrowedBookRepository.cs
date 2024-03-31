using Lara.Domain.Entities;
using Lara.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lara.Data.Repository;

public class BorrowedBookRepository : BaseRepository<BorrowedBook>
{
    
    public BorrowedBookRepository(PgSqlContext context) : base(context)
    {
    }

    public override BorrowedBook Select(int id)
    {
        var entity = _context.BorrowedBooks
            .Include(b => b.Book)
            .FirstOrDefault(b => b.Id == id);
            
        if (entity is null)
        {
            throw new NotFoundException($"Não foi encontrado empréstimo com id {id}");
        }

        return entity;
    }

    public override void Insert(BorrowedBook borrowedBook)
    {
        base.Insert(borrowedBook);
        _context.Entry(borrowedBook)
            .Reference(b => b.Book)
            .Load();
        _context.Entry(borrowedBook)
            .Reference(b => b.User)
            .Load();
    }
}