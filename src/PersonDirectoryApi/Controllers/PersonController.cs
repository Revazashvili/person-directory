using Microsoft.AspNetCore.Mvc;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Services;

namespace PersonDirectoryApi.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    [ValidationActionFilter<PersonCreateDto>]
    public async Task<IActionResult> Create([FromBody] PersonCreateDto personCreateDto, CancellationToken cancellationToken)
    {
        await _personService.CreateAsync(personCreateDto, cancellationToken);

        return Created();
    }
}