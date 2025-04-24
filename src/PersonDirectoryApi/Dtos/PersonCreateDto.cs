using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Dtos;

public record PersonCreateDto(string FirstName, 
    string LastName,
    Gender Gender, 
    string PersonalNumber,
    DateTime BirthDate, 
    int CityId,
    List<PhoneNumberDto> PhoneNumbers,
    List<RelatedPersonDto> RelatedPersons)
{
    public string FirstName { get; init; } = FirstName;
    public string LastName { get; init; } = LastName;
    public Gender Gender { get; init; } = Gender;
    public string PersonalNumber { get; init; } = PersonalNumber;
    public DateTime BirthDate { get; init; } = BirthDate;
    public int CityId { get; init; } = CityId;
    public List<PhoneNumberDto> PhoneNumbers { get; init; } = PhoneNumbers;
    public List<RelatedPersonDto> RelatedPersons { get; init; } = RelatedPersons;

    public void Deconstruct(out string FirstName, out string LastName, out Gender Gender, out string PersonalNumber, out DateTime BirthDate, out int CityId, out List<PhoneNumberDto> PhoneNumbers, out List<RelatedPersonDto> RelatedPersons)
    {
        FirstName = this.FirstName;
        LastName = this.LastName;
        Gender = this.Gender;
        PersonalNumber = this.PersonalNumber;
        BirthDate = this.BirthDate;
        CityId = this.CityId;
        PhoneNumbers = this.PhoneNumbers;
        RelatedPersons = this.RelatedPersons;
    }
}