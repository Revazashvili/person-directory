using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Validators;

namespace PersonDirectoryApi.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    [HttpGet]
    // [ValidationActionFilter]
    public IActionResult Get()
    {
        return Ok("hello");
    }
    
    [HttpPost]
    [ValidationActionFilter<PersonCreateDto>]
    public IActionResult Create(PersonCreateDto personCreateDto)
    {
        return Ok("hello");
    }
}