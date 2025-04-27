namespace PersonDirectoryApi.Entities;

public class City
{
    private City() { }

    public City(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public int Id { get; private set; }
    public string Name { get; private set; }
        
    public ICollection<Person> Residents { get; set; }
}