using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi.Entities;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Persistence;

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    public DbSet<PersonRelationship> Relationships { get; set; }
    public DbSet<LocalizedString> Localizations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}