using FluentValidation;
using PersonDirectoryApi.Dtos;

namespace PersonDirectoryApi.Validators;

public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotNull();

        RuleFor(x => x.Number)
            .NotEmpty()
            .Length(4, 50);
    }
}