using FluentValidation;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Validators;

public class RelatedPersonDtoValidator : AbstractValidator<RelatedPersonDto>
{
    public RelatedPersonDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Type)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);

        RuleFor(x => x.RelatedPersonId)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldGreaterThan0]);
    }
}