using FluentValidation;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Dtos;

public record PersonDeleteDto(string PersonalNumber);

public class PersonDeleteDtoValidator : AbstractValidator<PersonDeleteDto>
{
    public PersonDeleteDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Matches("^[0-9]{11}$")
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat])
            .MustAsync((dto, val, cancellationToken) => unitOfWork.Persons.ExistsWithPersonalNumberAsync(dto.PersonalNumber, cancellationToken))
            .WithMessage(localizer[LocalizedStringKeys.PersonDoesNotExists]);
    }
}