using Microsoft.EntityFrameworkCore;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Entities;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Services;

public interface IPersonService
{
    Task CreateAsync(PersonCreateDto createDto, CancellationToken cancellationToken);
    Task UpdateAsync(PersonUpdateDto updateDto, CancellationToken cancellationToken);
    Task DeleteAsync(PersonDeleteDto deleteDto, CancellationToken cancellationToken);
    Task ChangeImageAsync(PersonImageChangeDto imageChangeDto, CancellationToken cancellationToken);
    Task CreateRelationship(RelationshipCreateDto relationshipCreateDto, CancellationToken cancellationToken);
    Task RemoveRelationship(RelationshipRemoveDto relationshipRemoveDto, CancellationToken cancellationToken);
}

internal class PersonService : IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMultimediaService _multimediaService;
    private readonly PersonContext _personContext;

    public PersonService(IUnitOfWork unitOfWork, IMultimediaService multimediaService, PersonContext personContext)
    {
        _unitOfWork = unitOfWork;
        _multimediaService = multimediaService;
        _personContext = personContext;
    }
    
    public async Task CreateAsync(PersonCreateDto createDto, CancellationToken cancellationToken)
    {
        var phoneNumbers = createDto.PhoneNumbers.Select(dto => PhoneNumber.Create(dto.Type, dto.Number)).ToList();
        var relationships = createDto.RelatedPersons?.Select(dto => PersonRelationship.Create(dto.Type, dto.RelatedPersonPersonalNumber)).ToList();

        var person = Person.Create(createDto.FirstName, createDto.LastName,
            createDto.PersonalNumber, createDto.Gender,
            createDto.BirthDate, createDto.CityId,
            createDto.ImageUrl, phoneNumbers, relationships);

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
    
    public async Task DeleteAsync(PersonDeleteDto deleteDto, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetAsync(person => person.PersonalNumber == deleteDto.PersonalNumber, cancellationToken);
        
        _unitOfWork.Persons.Remove(person);
        
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
    
    public async Task ChangeImageAsync(PersonImageChangeDto imageChangeDto, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetAsync(person => person.PersonalNumber == imageChangeDto.PersonalNumber, cancellationToken);

        if (!string.IsNullOrEmpty(imageChangeDto.ImageUrl))
        {
            try
            {
                await _multimediaService.RemoveAsync(person.ImageUrl);
            }
            catch
            {
                // ignore error, we still can remove images that are not referenced using background service
            }
        }
        
        person.UpdateImage(imageChangeDto.ImageUrl);
        
        _unitOfWork.Persons.Update(person);
        
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
    
    public async Task CreateRelationship(RelationshipCreateDto relationshipCreateDto, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetAsync(person => person.PersonalNumber == relationshipCreateDto.PersonalNumber, cancellationToken);

        var relationship = PersonRelationship.Create(relationshipCreateDto.RelatedPerson.Type,
            relationshipCreateDto.RelatedPerson.RelatedPersonPersonalNumber);
        
        person.AddRelationship(relationship);
        
        _unitOfWork.Persons.Update(person);
        
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
    
    public async Task RemoveRelationship(RelationshipRemoveDto relationshipRemoveDto, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Persons.GetAsync(person => person.PersonalNumber == relationshipRemoveDto.PersonalNumber, cancellationToken);
        
        person.RemoveRelationship(relationshipRemoveDto.RelatedPersonPersonalNumber);
        
        _unitOfWork.Persons.Update(person);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}