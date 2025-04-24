using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<Person> PhysicalPersons { get; }
    IRepository<City> Cities { get; }
    IRepository<PhoneNumber> PhoneNumbers { get; }
    IRepository<PersonRelation> PersonConnections { get; }
    Task<int> CompleteAsync();
}