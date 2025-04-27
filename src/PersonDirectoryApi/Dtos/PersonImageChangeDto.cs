using FluentValidation;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Dtos;

public record PersonImageChangeDto(string PersonalNumber, string ImageUrl);


public class PersonImageUploadDtoValidator : AbstractValidator<PersonImageChangeDto>
{
    public PersonImageUploadDtoValidator(IStringLocalizer localizer, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .Matches("^[0-9]{11}$")
            .WithMessage(localizer[LocalizedStringKeys.InvalidFormat])
            .MustAsync((dto, val, cancellationToken) => unitOfWork.Persons.ExistsWithPersonalNumberAsync(dto.PersonalNumber, cancellationToken))
            .WithMessage(localizer[LocalizedStringKeys.PersonDoesNotExists]);
        
        RuleFor(x => x.ImageUrl)
            .SetValidator(new ImageUrlValidator(localizer));
    }
}