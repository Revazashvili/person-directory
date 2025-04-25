using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Persistence.Repositories;

public class PersonRepository : Repository<Person>, IRepository<Person>
{
    private readonly PersonContext _context;

    public PersonRepository(PersonContext context) : base(context)
    {
        _context = context;
    }

    public new Task<Person?> GetAsync(Expression<Func<Person, bool>> predicate, CancellationToken cancellationToken)
    {
        return _context.Persons
            .Include(person => person.PhoneNumbers)
            .Include(person => person.Relationships)
            .ThenInclude(x => x.RelatedPerson)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly PersonContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(PersonContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) =>
        _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public Task<List<TEntity>> GetAsync(int pageNumber, int pageSize, CancellationToken cancellationToken) => _dbSet
        .Take(pageSize)
        .Skip((pageNumber - 1) * pageSize)
        .ToListAsync(cancellationToken);

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => _dbSet.AnyAsync(predicate, cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) => await _dbSet.AddAsync(entity, cancellationToken);

    public Task AddRangeAsync(IEnumerable<TEntity> entities) => _dbSet.AddRangeAsync(entities);

    public void Update(TEntity entity) => _context.Update(entity);

    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
}