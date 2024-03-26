using Lara.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lara.Data.Mapping;

public class BorrowedBookMap : IEntityTypeConfiguration<BorrowedBook>
{
    public void Configure(EntityTypeBuilder<BorrowedBook> builder)
    {
        builder.ToTable("BorrowedBooks");

        builder.HasKey(prop => prop.Id);

        builder
            .HasOne(borrowed => borrowed.User)
            .WithMany()
            .HasForeignKey(borrowed => borrowed.UserId);

        builder.HasOne(prop => prop.Book)
            .WithOne()
            .HasForeignKey<BorrowedBook>(borrowed => borrowed.BookId);

        builder.Property(prop => prop.Quantity)
            .IsRequired()
            .HasColumnName("Quantity")
            .HasColumnType("smallint");

        builder.Property(prop => prop.Status)
            .IsRequired()
            .HasColumnName("Status")
            .HasColumnType("smallint");
    }
}