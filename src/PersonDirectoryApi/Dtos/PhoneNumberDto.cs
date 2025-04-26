using FluentValidation;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Dtos;

public record PhoneNumberDto(PhoneNumberType Type, string Number);

public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Type)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);

        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Length(4, 50)
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat]);
    }
}