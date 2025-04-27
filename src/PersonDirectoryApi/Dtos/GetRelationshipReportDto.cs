using FluentValidation;
using PersonDirectoryApi.Localization;

namespace PersonDirectoryApi.Dtos;

public record GetRelationshipReportDto(int PageNumber, int PageSize);

public class GetRelationshipReportDtoValidator : AbstractValidator<GetRelationshipReportDto>
{
    public GetRelationshipReportDtoValidator(IStringLocalizer localizer)
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