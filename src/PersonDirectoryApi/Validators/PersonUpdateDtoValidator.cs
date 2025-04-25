using FluentValidation;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Validators;

public class PersonUpdateDtoValidator : AbstractValidator<PersonUpdateDto>
{
    public PersonUpdateDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Length(2, 50)
            .WithMessage(localizer[LocalizedStringKeys.NameLenghtBetween2And50])
            .Must(PersonCommonValidator.NameValidator)
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat]);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Length(2, 50)
            .WithMessage(localizer[LocalizedStringKeys.NameLenghtBetween2And50])
            .Must(PersonCommonValidator.NameValidator)
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat]);

        RuleFor(x => x.Gender)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);

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
        
        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Must(PersonCommonValidator.AgeValidator)
            .WithMessage(localizer[LocalizedStringKeys.AtLeast18YearsOldRestriction]);;

        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .MustAsync((dto, val, cancellationToken) =>
            {
                return unitOfWork.Cities.ExistsAsync(city => city.Id == dto.CityId, cancellationToken);
            })
            .WithMessage(localizer[LocalizedStringKeys.CityDoesNotExists]);

        RuleFor(x => x.PhoneNumbers)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);
        
        RuleForEach(x => x.PhoneNumbers)
            .SetValidator(new PhoneNumberDtoValidator(localizer))
            .MustAsync(async (dto, val, cancellationToken) =>
            {
                foreach (var phoneNumber in dto.PhoneNumbers)
                {
                    var exists = await unitOfWork.Persons
                        .ExistsAsync(person => person.PersonalNumber != dto.PersonalNumber
                                               && person.PhoneNumbers.Any(phone => phone.Number == phoneNumber.Number),
                            cancellationToken);
                }
                
                return true;
            })
            .WithMessage(localizer[LocalizedStringKeys.PhoneNumberAlreadyExists]);
    }
}