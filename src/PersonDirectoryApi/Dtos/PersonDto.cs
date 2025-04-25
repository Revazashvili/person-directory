using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record PersonDto(string FirstName, 
    string LastName,
    Gender Gender, 
    string PersonalNumber,
    string ImageUrl,
    DateTime BirthDate, 
    CityDto City,
    List<PhoneNumberDto> PhoneNumbers,
    List<RelatedPersonDto>? RelatedPersons);