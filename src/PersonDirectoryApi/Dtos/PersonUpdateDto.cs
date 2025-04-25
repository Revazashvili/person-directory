using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record PersonUpdateDto(string FirstName, 
    string LastName,
    Gender Gender, 
    string PersonalNumber,
    DateTime BirthDate, 
    int CityId,
    List<PhoneNumberDto> PhoneNumbers);