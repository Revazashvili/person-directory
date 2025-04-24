using System.Text.RegularExpressions;
using FluentValidation;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Validators;

public class PersonCreateDtoValidator : AbstractValidator<PersonCreateDto>
{
    public PersonCreateDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Length(2, 50)
            .Must(BeValidName);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Length(2, 50)
            .Must(BeValidName);

        RuleFor(x => x.Gender)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);

        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Length(11)
            .Matches("^[0-9]{11}$");

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Must(BeAtLeast18YearsOld);

        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);

        RuleForEach(x => x.PhoneNumbers)
            .SetValidator(new PhoneNumberDtoValidator());

        RuleForEach(x => x.RelatedPersons)
            .SetValidator(new RelatedPersonDtoValidator());
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