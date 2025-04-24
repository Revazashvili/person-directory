using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Entities;

public class PhoneNumber
{
    public int Id { get; set; }
    public PhoneNumberType Type { get; set; }
    public string Number { get; set; }
        
    public int PersonId { get; set; }
    public Person Person { get; set; }
}