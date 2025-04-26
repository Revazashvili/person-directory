using FluentValidation;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;
using PersonDirectoryApi.Validators;

namespace PersonDirectoryApi.Dtos;

public record PersonCreateDto(string FirstName, 
    string LastName,
    Gender Gender, 
    string PersonalNumber,
    string ImageUrl,
    DateTime BirthDate, 
    int CityId,
    List<PhoneNumberDto> PhoneNumbers,
    List<RelatedPersonDto>? RelatedPersons);
    
    
public class PersonCreateDtoValidator : AbstractValidator<PersonCreateDto>
{
    public PersonCreateDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
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
            .MustAsync(async (dto, val, cancellationToken) =>
            {
                var exists = await unitOfWork.Persons.ExistsWithPersonalNumberAsync(dto.PersonalNumber, cancellationToken);
                return !exists;
            })
            .WithMessage(localizer[LocalizedStringKeys.PersonalNumberAlreadyExists]);

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired]);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Must(PersonCommonValidator.AgeValidator)
            .WithMessage(localizer[LocalizedStringKeys.AtLeast18YearsOldRestriction]);
        ;

        RuleFor(x => x.CityId)
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldGreaterThan0])
            .MustAsync((dto, val, cancellationToken) => unitOfWork.Cities.ExistsAsync(dto.CityId, cancellationToken))
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
                    var exists = await unitOfWork.PhoneNumbers.NotBelongsToPersonAsync(dto.PersonalNumber, phoneNumber.Number, cancellationToken);
                    
                    if (exists)
                        return false;
                }

                return true;
            })
            .WithMessage(localizer[LocalizedStringKeys.PhoneNumberAlreadyExists]);

        RuleForEach(x => x.RelatedPersons)
            .SetValidator(new RelatedPersonDtoValidator(localizer, unitOfWork));
    }
}