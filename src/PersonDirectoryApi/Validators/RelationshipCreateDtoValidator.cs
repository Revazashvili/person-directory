using FluentValidation;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Validators;

public class RelationshipCreateDtoValidator : AbstractValidator<RelationshipCreateDto>
{
    public RelationshipCreateDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Matches("^[0-9]{11}$")
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat])
            .MustAsync((dto, val, cancellationToken) =>
            {
                return unitOfWork.Persons.ExistsAsync(person => person.PersonalNumber == dto.PersonalNumber, cancellationToken);
            })
            .WithMessage(localizer[LocalizedStringKeys.PersonDoesNotExists]);
        
        RuleFor(x => x.RelatedPerson)
            .SetValidator(new RelatedPersonDtoValidator(localizer, unitOfWork))
            .MustAsync(async (dto, val, cancellationToken) =>
            {
                var relationshipAlreadyExists = await unitOfWork.PersonRelations.ExistsAsync(
                    person => person.PersonPersonalNumber == dto.PersonalNumber &&
                              person.RelatedPersonPersonalNumber == dto.RelatedPerson.RelatedPersonPersonalNumber,
                    cancellationToken);
                return !relationshipAlreadyExists;
            })
            .WithMessage(localizer[LocalizedStringKeys.RelationshipAlreadyExists]);
    }
}