using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.EntityConfigurations;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
                
        builder.Property(p => p.Type)
            .IsRequired()
            .HasConversion<string>();
                
        builder.Property(p => p.Number)
            .IsRequired()
            .HasMaxLength(50);
                
        builder.Property(p => p.PersonPersonalNumber)
            .IsRequired();
                
        builder.HasOne(p => p.Person)
            .WithMany(p => p.PhoneNumbers)
            .HasForeignKey(p => p.PersonPersonalNumber)
            .OnDelete(DeleteBehavior.Cascade);
                
        builder.ToTable("phone_numbers");
    }
}