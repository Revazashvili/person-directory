using FluentValidation;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Dtos;

public record RelationshipRemoveDto(string PersonalNumber, string RelatedPersonPersonalNumber);

public class RelationshipRemoveDtoValidator : AbstractValidator<RelationshipRemoveDto>
{
    public RelationshipRemoveDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Matches("^[0-9]{11}$")
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat])
            .MustAsync((dto, val, cancellationToken) => unitOfWork.Persons.ExistsWithPersonalNumberAsync(dto.PersonalNumber, cancellationToken))
            .WithMessage(localizer[LocalizedStringKeys.PersonDoesNotExists]);
        
        RuleFor(x => x.RelatedPersonPersonalNumber)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Matches("^[0-9]{11}$")
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat])
            .MustAsync((dto, val, cancellationToken) => unitOfWork.Persons.ExistsWithPersonalNumberAsync(dto.PersonalNumber, cancellationToken))
            .WithMessage(localizer[LocalizedStringKeys.PersonDoesNotExists]);
    }
}