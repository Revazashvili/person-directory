using PersonDirectoryApi.Entities;
using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record PersonDto(
    string FirstName,
    string LastName,
    Gender Gender,
    string PersonalNumber,
    string ImageUrl,
    DateTime BirthDate,
    CityDto City,
    List<PhoneNumberDto> PhoneNumbers,
    List<RelatedPersonDto>? RelatedPersons)
{
    public static implicit operator PersonDto(Person person)
    {
        var cityDto = new CityDto(person.City.Id, person.City.Name);
        
        var phoneNumbers = person.PhoneNumbers
            .Select(dto => new PhoneNumberDto(dto.Type, dto.Number))
            .ToList();
        var relationships = person.Relationships
            .Select(dto => new RelatedPersonDto(dto.Type, dto.RelatedPersonPersonalNumber))
            .ToList();

        return new PersonDto(person.FirstName, person.LastName, person.Gender, person.PersonalNumber, person.ImageUrl,
            person.BirthDate, cityDto, phoneNumbers, relationships);
    }
}