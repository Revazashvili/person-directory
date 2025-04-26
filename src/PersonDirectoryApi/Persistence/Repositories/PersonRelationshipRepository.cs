using Microsoft.EntityFrameworkCore;

namespace PersonDirectoryApi.Persistence.Repositories;

public interface IPersonRelationshipRepository
{
    Task<bool> ExistsAsync(string personalNumber, string relatedPersonPersonalNumber, CancellationToken cancellationToken);
}

public class PersonRelationshipRepository : IPersonRelationshipRepository
{
    private readonly PersonContext _personContext;

    public PersonRelationshipRepository(PersonContext personContext)
    {
        _personContext = personContext;
    }

    public Task<bool> ExistsAsync(string personalNumber, string relatedPersonPersonalNumber,
        CancellationToken cancellationToken) =>
        _personContext.Relationships.AnyAsync(relationship => 
                relationship.PersonPersonalNumber == personalNumber &&
                relationship.RelatedPersonPersonalNumber == relatedPersonPersonalNumber,
            cancellationToken);
}