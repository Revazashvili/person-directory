namespace PersonDirectoryApi.Persistence.Repositories;

public interface IUnitOfWork : IDisposable
{
    IPersonRepository Persons { get; }
    ICityRepository Cities { get; }
    IPhoneNumberRepository PhoneNumbers { get; }
    IPersonRelationshipRepository PersonRelations { get; }
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly PersonContext _context;

    public UnitOfWork(PersonContext context)
    {
        _context = context;
        Persons = new PersonRepository(context);
        Cities = new CityRepository(context);
        PhoneNumbers = new PhoneNumberRepository(context);
        PersonRelations = new PersonRelationshipRepository(context);
    }
    
    public IPersonRepository Persons { get; }
    public ICityRepository Cities { get; }
    public IPhoneNumberRepository PhoneNumbers { get; }
    public IPersonRelationshipRepository PersonRelations { get; }
    public Task<int> CompleteAsync(CancellationToken cancellationToken) => _context.SaveChangesAsync(cancellationToken);
    public void Dispose() => _context.Dispose();
}