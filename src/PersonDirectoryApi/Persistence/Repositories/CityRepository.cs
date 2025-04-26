using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.Repositories;

public interface ICityRepository
{
    Task<List<City>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}

public class CityRepository : ICityRepository
{
    private readonly PersonContext _context;

    public CityRepository(PersonContext context)
    {
        _context = context;
    }

    public Task<List<City>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken) => 
        _context.Cities.Take(pageSize).Skip((pageNumber - 1) * pageSize).ToListAsync(cancellationToken);

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) =>
        _context.Cities.AnyAsync(city => city.Id == id, cancellationToken);
}