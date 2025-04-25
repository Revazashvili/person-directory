using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Entities;

public class Person
{
    private Person() { }
    

    public int Id { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Gender Gender { get; private set; }
    public string PersonalNumber { get; private set; }
    public DateTime BirthDate { get; private set; }
    public int CityId { get; private set; }
    public string ImageUrl { get; private set; }

    public City City { get; private set; }
    public ICollection<PhoneNumber> PhoneNumbers { get; private set; } = new List<PhoneNumber>();
    public ICollection<PersonRelation> Relations { get; private set; } = new List<PersonRelation>();

    public static Person Create(string firstName, string lastName, string personalNumber, Gender gender, int cityId, string imageUrl, 
        List<PhoneNumber> phoneNumbers, List<PersonRelation> relations)
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
            Relations = relations
        };
    }
}