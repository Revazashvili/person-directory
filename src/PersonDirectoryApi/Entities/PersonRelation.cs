using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Entities;

public class PersonRelation
{
    public int Id { get; set; }
    public RelationType Type { get; private set; }
    public string PersonPersonalNumber { get; private set; }
    public Person Person { get; private set; }
    public string RelatedPersonPersonalNumber { get; private set; }
    public Person RelatedPerson { get; private set; }

    public static PersonRelation Create(RelationType type, string relatedPersonPersonalNumber)
    {
        return new PersonRelation
        {
            Type = type,
            RelatedPersonPersonalNumber = relatedPersonPersonalNumber,
        };
    }
}