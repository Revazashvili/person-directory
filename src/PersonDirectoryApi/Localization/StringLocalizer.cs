using System.Globalization;

namespace PersonDirectoryApi.Localization;

public class StringLocalizer : IStringLocalizer
{
    public string this[string name] => LocalisationHolder.Strings
                                           .FirstOrDefault(x => x.Key == name && x.Culture == CultureInfo.CurrentCulture
                                                   .TwoLetterISOLanguageName)?.Value
                                       ?? string.Empty;
}