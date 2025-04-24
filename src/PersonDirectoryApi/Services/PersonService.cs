using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Entities;

namespace PersonDirectoryApi.Services;

internal interface IPersonService
{
    Task<Person?> CreateAsync(PersonCreateDto dto, CancellationToken cancellationToken);
}

internal class PersonService : IPersonService
{
    public Task<Person?> CreateAsync(PersonCreateDto dto, CancellationToken cancellationToken)
    {
        // იგივე პირადი ნომრით ხომ არ არსებობს ვინმე
        // 
        throw new NotImplementedException();
    }
}