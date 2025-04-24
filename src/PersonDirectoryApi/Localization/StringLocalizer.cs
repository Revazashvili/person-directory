using System.Globalization;

namespace PersonDirectoryApi.Localization;

public class StringLocalizer : IStringLocalizer
{
    public string this[string name] => LocalisationHolder.Strings
                                           .FirstOrDefault(x => x.Key == name && x.Culture == CultureInfo.CurrentCulture
                                                   .TwoLetterISOLanguageName)?.Value
                                       ?? string.Empty;
}

public class LocalizedString
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string Culture { get; set; }
}

public static class LocalizedStringKeys
{
    public const string FieldRequired = "field_required";
}

public static class LocalisationHolder
{
    public static HashSet<LocalizedString> Strings { get; } =
    [
        new LocalizedString() { Key = LocalizedStringKeys.FieldRequired, Value = "ველი სავალდებულოა", Culture = "ka" },
        new LocalizedString() { Key = LocalizedStringKeys.FieldRequired, Value = "field is required", Culture = "en" }
    ];
}