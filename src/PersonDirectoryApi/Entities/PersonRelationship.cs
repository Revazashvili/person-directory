using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Entities;

public class PersonRelationship
{
    public int Id { get; set; }
    public RelationshipType Type { get; private set; }
    public string PersonPersonalNumber { get; private set; }
    public Person Person { get; private set; }
    public string RelatedPersonPersonalNumber { get; private set; }
    public Person RelatedPerson { get; private set; }

    public static PersonRelationship Create(RelationshipType type, string relatedPersonPersonalNumber)
    {
        return new PersonRelationship
        {
            Type = type,
            RelatedPersonPersonalNumber = relatedPersonPersonalNumber,
        };
    }
}