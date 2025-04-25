using System.Text.RegularExpressions;

namespace PersonDirectoryApi.Validators;

internal static class PersonCommonValidator
{
    internal static bool AgeValidator(DateTime birthDate) => birthDate <= DateTime.Today.AddYears(-18);
    
    internal static bool NameValidator(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;
        
        var georgian = new Regex("^[ა-ჰ]+$");
        var latin = new Regex("^[a-zA-Z]+$");
        return georgian.IsMatch(name) || latin.IsMatch(name);
    }
}