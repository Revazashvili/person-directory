using System.Text.RegularExpressions;
using FluentValidation;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi;

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

public class ImageUrlValidator : AbstractValidator<string>
{
    public ImageUrlValidator(IStringLocalizer localizer)
    {
        RuleFor(url => url)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Must(BeAValidUrl)
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat])
            .Must(EndWithImageExtension)
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat]);
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private static bool EndWithImageExtension(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        return url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
               || url.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
               || url.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
               || url.EndsWith(".gif", StringComparison.OrdinalIgnoreCase)
               || url.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)
               || url.EndsWith(".webp", StringComparison.OrdinalIgnoreCase);
    }
}
