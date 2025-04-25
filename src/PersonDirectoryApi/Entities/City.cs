namespace PersonDirectoryApi.Entities;

public class City
{
    public int Id { get; private set; }
    public string Name { get; private set; }
        
    public ICollection<Person> Residents { get; set; }
}