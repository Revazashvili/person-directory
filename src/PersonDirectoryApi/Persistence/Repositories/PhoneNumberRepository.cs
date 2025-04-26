using Microsoft.EntityFrameworkCore;

namespace PersonDirectoryApi.Persistence.Repositories;

public interface IPhoneNumberRepository
{
    Task<bool> NotBelongsToPersonAsync(string personalNumber, string phoneNumber, CancellationToken cancellationToken);
}

public class PhoneNumberRepository : IPhoneNumberRepository
{
    private readonly PersonContext _personContext;

    public PhoneNumberRepository(PersonContext personContext)
    {
        _personContext = personContext;
    }

    public Task<bool> NotBelongsToPersonAsync(string personalNumber, string phoneNumber, CancellationToken cancellationToken) =>
        _personContext.PhoneNumbers.AnyAsync(number => number.Number == phoneNumber && number.PersonPersonalNumber != personalNumber, cancellationToken);
}