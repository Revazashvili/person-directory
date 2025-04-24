namespace PersonDirectoryApi.Localization;

public interface IStringLocalizer
{
    string this[string name] { get; }
}