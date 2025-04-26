using FluentValidation;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Dtos;

public record PersonSearchDto(string? FirstName, 
    string? LastName,
    string? PersonalNumber,
    DateTime? BirthDate, 
    int? CityId,
    string? PhoneNumber,
    int PageNumber,
    int PageSize);
    
    
public class PersonSearchDtoValidator : AbstractValidator<PersonSearchDto>
{
    public PersonSearchDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.PageSize)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldGreaterThan0]);
        
        RuleFor(x => x.PageNumber)
            .NotNull()
            .WithMessage(localizer[LocalizedStringKeys.FieldRequired])
            .GreaterThan(0)
            .WithMessage(localizer[LocalizedStringKeys.FieldGreaterThan0]);
    }
}