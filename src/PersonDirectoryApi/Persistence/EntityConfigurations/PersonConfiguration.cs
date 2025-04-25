using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.EntityConfigurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.PersonalNumber);
        
        builder.Property(p => p.PersonalNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Gender)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(p => p.CityId)
            .IsRequired();

        builder.Property(p => p.ImageUrl)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(p => p.City)
            .WithMany(c => c.Residents)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => new { p.FirstName, p.LastName });
        builder.HasIndex(p => p.BirthDate);

        builder.ToTable("people");
    }
}