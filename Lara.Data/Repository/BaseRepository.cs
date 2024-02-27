using Lara.Domain.Contracts;
using Lara.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lara.Data.Repository;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly PgSqlContext _context;

    public BaseRepository(PgSqlContext context)
    {
        _context = context;
    }
    
    public void Insert(TEntity obj)
    {
        _context.Set<TEntity>().Add(obj);
        _context.SaveChanges();
    }

    public void Update(TEntity obj)
    {
        _context.Set<TEntity>().Entry(obj).State = EntityState.Modified;
        
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Set<TEntity>().Remove(Select(id));
        _context.SaveChanges();
    }

    public IList<TEntity> Select()
    {
        return _context.Set<TEntity>().ToList();
    }

    public TEntity Select(int id)
    {
        var entity = _context.Set<TEntity>().Find(id);
        if (entity is null)
        {
            throw new Exception($"Não foi encontrado item com id {id}");
        }

        return entity;
    }
}