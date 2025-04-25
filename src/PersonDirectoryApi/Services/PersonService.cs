using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Entities;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Services;

public interface IPersonService
{
    Task CreateAsync(PersonCreateDto createDto, CancellationToken cancellationToken);
}

internal class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;

    public PersonService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task CreateAsync(PersonCreateDto createDto, CancellationToken cancellationToken)
    {
        var phoneNumbers = createDto.PhoneNumbers.Select(dto => PhoneNumber.Create(dto.Type, dto.Number)).ToList();
        var relations = createDto.RelatedPersons?.Select(dto => PersonRelation.Create(dto.Type, dto.RelatedPersonId)).ToList();
        
        var person = Person.Create(createDto.FirstName, createDto.LastName,  createDto.PersonalNumber, createDto.Gender, createDto.CityId, createDto.ImageUrl, phoneNumbers, relations);

        await _unitOfWork.Persons.AddAsync(person, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}