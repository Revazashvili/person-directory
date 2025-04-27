using System.Globalization;
using System.Text.Json;
using PersonDirectoryApi.Persistence;

namespace PersonDirectoryApi.Localization;

public class DbStringLocalizer : IStringLocalizer
{
    private static Dictionary<string, Dictionary<string, string>> _localizedStrings;
    public DbStringLocalizer(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PersonContext>();
        LoadAllStrings(context);
    }

    public string this[string name]
    {
        get
        {
            if (_localizedStrings.TryGetValue(name, out var results)
                && results.TryGetValue(CultureInfo.CurrentCulture
                    .TwoLetterISOLanguageName, out var value))
            {
                return value;
            }
            
            return "default error";
        }
    }

    private void LoadAllStrings(PersonContext context)
    {
        _localizedStrings = context.Localizations
            .ToList()
            .GroupBy(x => x.Key)
            .ToDictionary(x => x.Key,
                x => x.ToDictionary(y => y.Culture, y => y.Value)
            );

        Console.WriteLine(JsonSerializer.Serialize(_localizedStrings));
        
        if (_localizedStrings.TryGetValue(LocalizedStringKeys.FieldRequired, out var results)
            && results.TryGetValue("ka", out var value))
        {
            Console.WriteLine(value);
        }
    }
}