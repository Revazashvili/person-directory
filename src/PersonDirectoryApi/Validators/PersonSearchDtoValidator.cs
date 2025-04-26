using FluentValidation;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Validators;

public class PersonSearchDtoValidator : AbstractValidator<PersonSearchDto>
{
    public PersonSearchDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.PageSize)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldGreaterThan0]);
        
        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldGreaterThan0]);
    }
}