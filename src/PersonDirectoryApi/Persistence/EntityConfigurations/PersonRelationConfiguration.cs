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
                
        builder.Property(r => r.PersonId)
            .IsRequired();
                
        builder.Property(r => r.RelatedPersonId)
            .IsRequired();
                
        builder.HasOne(r => r.Person)
            .WithMany(p => p.Relations)
            .HasForeignKey(r => r.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasOne(r => r.RelatedPerson)
            .WithMany()
            .HasForeignKey(r => r.RelatedPersonId)
            .OnDelete(DeleteBehavior.Restrict);
                
        builder.HasIndex(r => new { r.PersonId, r.RelatedPersonId, r.Type })
            .IsUnique();
                
        builder.ToTable("person_relations");
    }
}