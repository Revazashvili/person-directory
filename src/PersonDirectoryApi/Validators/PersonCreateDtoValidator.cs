using System.Text.RegularExpressions;
using FluentValidation;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Validators;

public class PersonCreateDtoValidator : AbstractValidator<PersonCreateDto>
{
    public PersonCreateDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Length(2, 50)
            .WithMessage(localizer[LocalizedStringKeys.NameLenghtBetween2And50])
            .Must(BeValidName)
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat]);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Length(2, 50)
            .WithMessage(localizer[LocalizedStringKeys.NameLenghtBetween2And50])
            .Must(BeValidName)
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat]);

        RuleFor(x => x.Gender)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);

        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Matches("^[0-9]{11}$")
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat])
            .MustAsync(async (dto, val, cancellationToken) =>
            {
                var exists = await unitOfWork.Persons.ExistsAsync(person => person.PersonalNumber == dto.PersonalNumber, cancellationToken);
                return !exists;
            })
            .WithMessage(localizer[LocalizedStringKeys.PersonalNumberAlreadyExists]);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Must(BeAtLeast18YearsOld)
            .WithMessage(localizer[LocalizedStringKeys.AtLeast18YearsOldRestriction]);;

        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .MustAsync((dto, val, cancellationToken) =>
            {
                return unitOfWork.Cities.ExistsAsync(city => city.Id == dto.CityId, cancellationToken);
            })
            .WithMessage(localizer[LocalizedStringKeys.CityDoesNotExists]);

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

        RuleForEach(x => x.RelatedPersons)
            .SetValidator(new RelatedPersonDtoValidator(localizer));
    }
    
    private bool BeAtLeast18YearsOld(DateTime birthDate)
    {
        return birthDate <= DateTime.Today.AddYears(-18);
    }

    private bool BeValidName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;
        
        var georgian = new Regex("^[ა-ჰ]+$");
        var latin = new Regex("^[a-zA-Z]+$");
        return georgian.IsMatch(name) || latin.IsMatch(name);
    }
}