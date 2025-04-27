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

        builder.HasData(new List<City>
        {
            new(1, "Tbilisi"),
            new(2, "Batumi"),
            new(3, "Kutaisi"),
            new(4, "Rustavi"),
            new(5, "Zugdidi"),
            new(6, "Gori"),
            new(7, "Poti"),
            new(8, "Telavi"),
            new(9, "Samtredia"),
            new(10, "Khashuri"),
        });
    }
}