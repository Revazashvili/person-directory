using FluentValidation;
using PersonDirectoryApi.Dtos;

namespace PersonDirectoryApi.Validators;

public class RelatedPersonDtoValidator : AbstractValidator<RelatedPersonDto>
{
    public RelatedPersonDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotNull();

        RuleFor(x => x.RelatedPersonId)
            .GreaterThan(0);
    }
}