using Microsoft.AspNetCore.Mvc;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Entities;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Localization;
using PersonDirectoryApi.Persistence.Repositories;
using PersonDirectoryApi.Validators;

namespace PersonDirectoryApi.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    private readonly IStringLocalizer _localizer;
    private readonly IUnitOfWork _unitOfWork;

    public PersonController(IStringLocalizer localizer, IUnitOfWork unitOfWork)
    {
        _localizer = localizer;
        _unitOfWork = unitOfWork;
    }
    
    [HttpPost]
    [ValidationActionFilter<PersonCreateDto>]
    public IActionResult Create(PersonCreateDto personCreateDto)
    {
        var person = new PersonCreateDto("", "", Gender.Female, "59001120839", DateTime.Now.AddYears(-23), 1,
            new List<PhoneNumberDto>(), new List<RelatedPersonDto>());

        var personCreateDtovalidator = new PersonCreateDtoValidator(_localizer, _unitOfWork);

        var validationResult = personCreateDtovalidator.Validate(person);
        return Ok(validationResult);
    }
}