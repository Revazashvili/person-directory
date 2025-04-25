using FluentValidation;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Validators;

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