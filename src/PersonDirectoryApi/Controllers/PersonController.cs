using Microsoft.AspNetCore.Mvc;
using PersonDirectoryApi.Dtos;

namespace PersonDirectoryApi.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    [HttpPost]
    [ValidationActionFilter<PersonCreateDto>]
    public IActionResult Create(PersonCreateDto personCreateDto)
    {
        return Ok("hello");
    }
}