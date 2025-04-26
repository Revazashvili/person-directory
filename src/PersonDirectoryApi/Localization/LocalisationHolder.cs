namespace PersonDirectoryApi.Localization;

public static class LocalisationHolder
{
    public static HashSet<LocalizedString> Strings { get; } =
    [
        new() { Key = LocalizedStringKeys.FieldRequired, Value = "ველი სავალდებულოა", Culture = "ka" },
        new() { Key = LocalizedStringKeys.FieldRequired, Value = "field is required", Culture = "en" },
        new() { Key = LocalizedStringKeys.FieldGreaterThan0, Value = "ველი უნდა იყოს 0-ზე მეტი", Culture = "ka" },
        new() { Key = LocalizedStringKeys.FieldGreaterThan0, Value = "field must be greater than 0", Culture = "en" },
        new() { Key = LocalizedStringKeys.AtLeast18YearsOldRestriction, Value = "Person should be at least 18 years old", Culture = "en" },
        new() { Key = LocalizedStringKeys.AtLeast18YearsOldRestriction, Value = "მომხმარებელი უნდა იყოს მინიმუმ 18 წლის", Culture = "ka" },
        new() { Key = LocalizedStringKeys.CityDoesNotExists, Value = "City does not exists", Culture = "en" },
        new() { Key = LocalizedStringKeys.CityDoesNotExists, Value = "ქალაქი არ მოიძებნა", Culture = "ka" },
        new() { Key = LocalizedStringKeys.PersonDoesNotExists, Value = "Person does not exists", Culture = "en" },
        new() { Key = LocalizedStringKeys.PersonDoesNotExists, Value = "მომხმარებელი არ მოიძებნა", Culture = "ka" },
        new() { Key = LocalizedStringKeys.PersonalNumberAlreadyExists, Value = "Person with this personal number already exists", Culture = "en" },
        new() { Key = LocalizedStringKeys.PersonalNumberAlreadyExists, Value = "პირადი ნომრით ჩანაწერი უკვე არსებობს", Culture = "ka" },
        new() { Key = LocalizedStringKeys.PhoneNumberAlreadyExists, Value = "Person with this phone number already exists", Culture = "en" },
        new() { Key = LocalizedStringKeys.PhoneNumberAlreadyExists, Value = "მობილურის ნომრით ჩანაწერი უკვე არსებობს", Culture = "ka" },
        new() { Key = LocalizedStringKeys.NameLenghtBetween2And50, Value = "Length should be between 2 and 50", Culture = "en" },
        new() { Key = LocalizedStringKeys.NameLenghtBetween2And50, Value = "სიმბოლოების რამდენობა უნდა იყოს 2-სა ანდ 50-ს შორის", Culture = "ka" },
        new() { Key = LocalizedStringKeys.InvalidFormat, Value = "Format is invalid", Culture = "en" },
        new() { Key = LocalizedStringKeys.InvalidFormat, Value = "ფორმატი არავალიდურია", Culture = "ka" },
        new() { Key = LocalizedStringKeys.RelationshipAlreadyExists, Value = "Relationship already exists", Culture = "en" },
        new() { Key = LocalizedStringKeys.RelationshipAlreadyExists, Value = "კავშირი უკვე არსებობს", Culture = "ka" }
    ];
}