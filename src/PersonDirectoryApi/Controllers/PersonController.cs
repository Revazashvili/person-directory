using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Enums;
using PersonDirectoryApi.Services;
using PersonDirectoryApi.Validators;

namespace PersonDirectoryApi.Controllers;

[ApiController]
[Route("api/person")]
public class PersonController : ControllerBase
{
    private readonly IMultimediaService _multimediaService;

    public PersonController(IMultimediaService multimediaService)
    {
        _multimediaService = multimediaService;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> Get([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        var url = await _multimediaService.UploadAsync(file, cancellationToken);

        var multimedia = await _multimediaService.GetAsync(url, cancellationToken);

        return File(multimedia.Content, multimedia.MimeType);
    }
    
    [HttpPost]
    [ValidationActionFilter<PersonCreateDto>]
    public IActionResult Create(PersonCreateDto personCreateDto)
    {
        return Ok("hello");
    }
}