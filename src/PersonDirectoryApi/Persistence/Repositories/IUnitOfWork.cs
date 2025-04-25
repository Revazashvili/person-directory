using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<Person> Persons { get; }
    IRepository<City> Cities { get; }
    IRepository<PhoneNumber> PhoneNumbers { get; }
    IRepository<PersonRelation> PersonRelations { get; }
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly PersonContext _context;

    public UnitOfWork(PersonContext context)
    {
        _context = context;
        Persons = new Repository<Person>(context);
        Cities = new Repository<City>(context);
        PhoneNumbers = new Repository<PhoneNumber>(context);
        PersonRelations = new Repository<PersonRelation>(context);
    }
    
    public IRepository<Person> Persons { get; }
    public IRepository<City> Cities { get; }
    public IRepository<PhoneNumber> PhoneNumbers { get; }
    public IRepository<PersonRelation> PersonRelations { get; }
    public Task<int> CompleteAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
    public void Dispose() => _context.Dispose();
}