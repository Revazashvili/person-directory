using FluentValidation;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Dtos;

public record CitySearchDto(int PageNumber, int PageSize);

public class CitySearchDtoValidator : AbstractValidator<CitySearchDto>
{
    public CitySearchDtoValidator(IStringLocalizer localizer)
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