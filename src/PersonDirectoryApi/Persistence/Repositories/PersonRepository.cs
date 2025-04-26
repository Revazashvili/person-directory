using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.Repositories;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person?> GetByPersonalNumberAsync(string personalNumber, CancellationToken cancellationToken);
    Task<List<Person>> GetAllAsync(PersonSearchDto personSearchDto, CancellationToken cancellationToken);
    Task<bool> ExistsWithPersonalNumberAsync(string personalNumber, CancellationToken cancellationToken);
}

public class PersonRepository : Repository<Person>, IPersonRepository
{
    private readonly PersonContext _context;

    public PersonRepository(PersonContext context) : base(context)
    {
        _context = context;
    }

    public Task<Person?> GetByPersonalNumberAsync(string personalNumber, CancellationToken cancellationToken)
    {
        return _context.Persons
            .Include(person => person.City)
            .Include(person => person.PhoneNumbers)
            .Include(person => person.Relationships)
            .ThenInclude(x => x.RelatedPerson)
            .FirstOrDefaultAsync(person => person.PersonalNumber == personalNumber, cancellationToken);
    }

    public Task<List<Person>> GetAllAsync(PersonSearchDto personSearchDto, CancellationToken cancellationToken)
    {
        var query = _context.Persons
            .Include(person => person.City)
            .Include(person => person.PhoneNumbers)
            .Include(person => person.Relationships)
            .ThenInclude(x => x.RelatedPerson)
            .AsQueryable();
        
        if(!string.IsNullOrEmpty(personSearchDto.PersonalNumber))
            query = query.Where(person => person.PersonalNumber.Contains(personSearchDto.PersonalNumber));
        
        if(!string.IsNullOrEmpty(personSearchDto.FirstName))
            query = query.Where(person => person.FirstName.Contains(personSearchDto.FirstName));
        
        if(!string.IsNullOrEmpty(personSearchDto.LastName))
            query = query.Where(person => person.LastName.Contains(personSearchDto.LastName));

        if(personSearchDto.BirthDate is not null)
            query = query.Where(person => person.BirthDate == personSearchDto.BirthDate);
        
        if(personSearchDto.CityId is not null)
            query = query.Where(person => person.CityId == personSearchDto.CityId);
        
        if(!string.IsNullOrEmpty(personSearchDto.PhoneNumber))
            query = query.Where(person => person.PhoneNumbers.Any(number => number.Number == personSearchDto.PhoneNumber));
        
        return query
            .Take(personSearchDto.PageSize)
            .Skip((personSearchDto.PageNumber - 1) * personSearchDto.PageSize)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsWithPersonalNumberAsync(string personalNumber, CancellationToken cancellationToken) => 
        _context.Persons.AnyAsync(person => person.PersonalNumber == personalNumber, cancellationToken);
}