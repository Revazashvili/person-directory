using PersonDirectoryApi.Enums;

namespace PersonDirectoryApi.Entities;

public class PhoneNumber
{
    public int Id { get; private set; }
    public PhoneNumberType Type { get; private set; }
    public string Number { get; private set; }
        
    public int PersonId { get; private set; }
    public Person Person { get; private set; }

    public static PhoneNumber Create(PhoneNumberType phoneType, string number)
    {
        return new PhoneNumber
        {
            Type = phoneType,
            Number = number
        };
    }
}