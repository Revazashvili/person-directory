using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Entities;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Services;

public interface IPersonService
{
    Task CreateAsync(PersonCreateDto createDto, CancellationToken cancellationToken);
    Task UpdateAsync(PersonUpdateDto updateDto, CancellationToken cancellationToken);
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
        
        var person = Person.Create(createDto.FirstName, createDto.LastName,  createDto.PersonalNumber, createDto.Gender, createDto.BirthDate, createDto.CityId, createDto.ImageUrl, phoneNumbers, relations);

        await _unitOfWork.Persons.AddAsync(person, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task UpdateAsync(PersonUpdateDto updateDto, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetAsync(person => person.PersonalNumber == updateDto.PersonalNumber, cancellationToken);

        var phoneNumbers = updateDto.PhoneNumbers.Select(dto => PhoneNumber.Create(dto.Type, dto.Number)).ToList();
        
        person.Update(updateDto.FirstName, updateDto.LastName, updateDto.PersonalNumber, updateDto.Gender,
            updateDto.BirthDate, updateDto.CityId, phoneNumbers);
        
        _unitOfWork.Persons.Update(person);
        
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}