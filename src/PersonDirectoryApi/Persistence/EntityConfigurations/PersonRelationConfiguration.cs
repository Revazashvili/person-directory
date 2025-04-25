using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.EntityConfigurations;

public class PersonRelationConfiguration : IEntityTypeConfiguration<PersonRelationship>
{
    public void Configure(EntityTypeBuilder<PersonRelationship> builder)
    {
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
                
        builder.Property(r => r.Type)
            .IsRequired()
            .HasConversion<string>();
                
        builder.Property(r => r.PersonPersonalNumber)
            .IsRequired();
                
        builder.Property(r => r.RelatedPersonPersonalNumber)
            .IsRequired();
                
        builder.HasOne(r => r.Person)
            .WithMany(p => p.Relationships)
            .HasForeignKey(r => r.PersonPersonalNumber)
            .OnDelete(DeleteBehavior.Cascade);
                
        builder.HasOne(r => r.RelatedPerson)
            .WithMany()
            .HasForeignKey(r => r.RelatedPersonPersonalNumber)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => new { r.PersonPersonalNumber, r.RelatedPersonPersonalNumber, r.Type })
            .IsUnique();
                
        builder.ToTable("person_relationships");
    }
}