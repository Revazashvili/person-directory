using FluentValidation;
using PersonDirectoryApi.Entities;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Dtos;

public record PersonSearchDto(
    string? FirstName,
    string? LastName,
    string? PersonalNumber,
    DateTime? BirthDate,
    int? CityId,
    string? PhoneNumber,
    int PageNumber,
    int PageSize)
{
    public static PersonSearchDto ForPaging(int pageNumber, int pageSize)
    {
        return new PersonSearchDto(null, null, null, null, null, null, pageNumber, pageSize);
    }
}

    
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