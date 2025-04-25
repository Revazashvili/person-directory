using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Entities;

public class PersonRelation
{
    public int Id { get; private set; }
    public RelationType Type { get; private set; }
    public int PersonId { get; private set; }
    public Person Person { get; private set; }
    public int RelatedPersonId { get; private set; }
    public Person RelatedPerson { get; private set; }

    public static PersonRelation Create(RelationType type, int relatedPersonId)
    {
        return new PersonRelation
        {
            Type = type,
            RelatedPersonId = relatedPersonId,
        };
    }
}