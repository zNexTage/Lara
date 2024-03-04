using Lara.Data.Mapping;
using Lara.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lara.Data.Repository;

public class PgSqlContext : IdentityDbContext<ApplicationUser>
{
    public PgSqlContext(DbContextOptions<PgSqlContext> options) : base(options)
    {
        
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>(new BookMap().Configure);
        modelBuilder.Entity<ApplicationUser>(new ApplicationUserMap().Configure);
    }
}