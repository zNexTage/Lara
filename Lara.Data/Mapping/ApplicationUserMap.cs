using Lara.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lara.Data.Mapping;

public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(prop => prop.FirstName)
            .IsRequired()
            .HasColumnType("VARCHAR(80)");
        
        builder.Property(prop => prop.LastName)
            .IsRequired()
            .HasColumnType("VARCHAR(80)");
    }
}