using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record RelationshipReportDto(string PersonalNumber, 
    string FirstName, 
    string LastName,
    Gender Gender,
    DateTime BirthDate,
    string ImageUrl,
    List<PersonRelationshipsByTypeDto> PersonRelationshipsByType);


public record PersonRelationshipsByTypeDto(RelationshipType Type, int Count);