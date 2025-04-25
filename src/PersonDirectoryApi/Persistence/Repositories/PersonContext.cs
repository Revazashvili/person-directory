using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.Repositories;

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    public DbSet<PersonRelationship> PersonRelation { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}