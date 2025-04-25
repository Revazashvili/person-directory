using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record PersonCreateDto(string FirstName, 
    string LastName,
    Gender Gender, 
    string PersonalNumber,
    string ImageUrl,
    DateTime BirthDate, 
    int CityId,
    List<PhoneNumberDto> PhoneNumbers,
    List<RelatedPersonDto>? RelatedPersons);