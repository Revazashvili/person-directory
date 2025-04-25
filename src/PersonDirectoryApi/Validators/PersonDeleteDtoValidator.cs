using FluentValidation;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Validators;

public class PersonDeleteDtoValidator : AbstractValidator<PersonDeleteDto>
{
    public PersonDeleteDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
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
    }
}