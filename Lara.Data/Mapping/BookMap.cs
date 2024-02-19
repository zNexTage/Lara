using Lara.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lara.Data.Mapping;

public class BookMap : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("VARCHAR(100)");

        builder.Property(prop => prop.Image)
            .IsRequired()
            .HasColumnName("Image")
            .HasColumnType("VARCHAR(1000)");
        
        builder.Property(prop => prop.Publisher)
            .IsRequired()
            .HasColumnName("Publisher")
            .HasColumnType("VARCHAR(80)");
        
        builder.Property(prop => prop.Authors)
            .IsRequired()
            .HasColumnName("Authors")
            .HasColumnType("VARCHAR []");
        
        //Ref: https://alexalvess.medium.com/criando-uma-api-em-net-core-baseado-na-arquitetura-ddd-2c6a409c686
    }
}