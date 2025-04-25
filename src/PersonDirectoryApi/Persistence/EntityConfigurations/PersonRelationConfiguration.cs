using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.EntityConfigurations;

public class PersonRelationConfiguration : IEntityTypeConfiguration<PersonRelation>
{
    public void Configure(EntityTypeBuilder<PersonRelation> builder)
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
            .WithMany(p => p.Relations)
            .HasForeignKey(r => r.PersonPersonalNumber)
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasOne(r => r.RelatedPerson)
            .WithMany()
            .HasForeignKey(r => r.RelatedPersonPersonalNumber)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(r => new { r.PersonPersonalNumber, r.RelatedPersonPersonalNumber, r.Type })
            .IsUnique();
                
        builder.ToTable("person_relations");
    }
}