using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Entities;

public class PersonRelation
{
    public int Id { get; set; }
    public RelationType Type { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public int RelatedPersonId { get; set; }
    public Person RelatedPerson { get; set; }
}