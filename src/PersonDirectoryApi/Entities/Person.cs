using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Entities;

public class Person
{
    private Person() { }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Gender Gender { get; private set; }
    public string PersonalNumber { get; private set; }
    public DateTime BirthDate { get; private set; }
    public int CityId { get; private set; }
    public string ImageUrl { get; private set; }

    public City City { get; private set; }
    public ICollection<PhoneNumber> PhoneNumbers { get; private set; } = new List<PhoneNumber>();
    public ICollection<PersonRelationship> Relationships { get; private set; } = new List<PersonRelationship>();

    public static Person Create(string firstName, string lastName, string personalNumber, Gender gender, DateTime birthDate, int cityId, string imageUrl, 
        List<PhoneNumber> phoneNumbers, List<PersonRelationship> relationships)
    {
        return new Person
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            PersonalNumber = personalNumber,
            CityId = cityId,
            ImageUrl = imageUrl,
            PhoneNumbers = phoneNumbers,
            Relationships = relationships,
            BirthDate = birthDate
        };
    }

    public void Update(string firstName, string lastname, string personalNumber, Gender gender, DateTime birthDate, int cityId, List<PhoneNumber> phoneNumbers)
    {
        FirstName = firstName;
        LastName = lastname;
        Gender = gender;
        BirthDate = birthDate;
        PersonalNumber = personalNumber;
        CityId = cityId;
        PhoneNumbers = phoneNumbers;
    }
    
    public void UpdateImage(string imageUrl)
    {
        ImageUrl = imageUrl;
    }

    public void AddRelationship(PersonRelationship personRelationship)
    {
        Relationships.Add(personRelationship);
    }
    
    public void RemoveRelationship(string relatedPersonPersonalNumber)
    {
        var relationship = Relationships.First(personRelationship => personRelationship.RelatedPersonPersonalNumber == relatedPersonPersonalNumber);
        
        Relationships.Remove(relationship);
    }
}