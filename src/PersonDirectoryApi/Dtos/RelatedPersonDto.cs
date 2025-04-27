using FluentValidation;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Dtos;

public record RelatedPersonDto(RelationshipType Type, string RelatedPersonPersonalNumber);

public class RelatedPersonDtoValidator : AbstractValidator<RelatedPersonDto>
{
    public RelatedPersonDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Type)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);

        RuleFor(x => x.RelatedPersonPersonalNumber)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Matches("^[0-9]{11}$")
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat])
            .MustAsync((dto, _, cancellationToken) => unitOfWork.Persons.ExistsWithPersonalNumberAsync(dto.RelatedPersonPersonalNumber, cancellationToken))
            .WithMessage(localizer[LocalizedStringKeys.PersonDoesNotExists]);
    }
}