namespace PersonDirectoryApi.Dtos;

public record PersonSearchDto(string? FirstName, 
    string? LastName,
    string? PersonalNumber,
    DateTime? BirthDate, 
    int? CityId,
    string? PhoneNumber,
    int PageNumber,
    int PageSize);