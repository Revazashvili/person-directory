using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.EntityConfigurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
                
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
                
        builder.HasIndex(c => c.Name)
            .IsUnique();
                
        builder.ToTable("cities");
    }
}