namespace PersonDirectoryApi.Entities;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
        
    public ICollection<Person> Residents { get; set; }
}