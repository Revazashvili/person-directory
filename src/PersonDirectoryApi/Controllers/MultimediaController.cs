using Microsoft.AspNetCore.Mvc;
using PersonDirectoryApi.Services;

namespace PersonDirectoryApi.Controllers;

[ApiController]
[Route("api/multimedia")]
public class MultimediaController : ControllerBase
{
    private readonly IMultimediaService _multimediaService;

    public MultimediaController(IMultimediaService multimediaService)
    {
        _multimediaService = multimediaService;
    }
    
    [HttpGet("{fileName}")]
    [ProducesResponseType<FileContentResult>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] string fileName, CancellationToken cancellationToken)
    {
        var multimedia = await _multimediaService.GetAsync(fileName, cancellationToken);

        return File(multimedia.Content, multimedia.MimeType);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
    {
        var url = await _multimediaService.UploadAsync(file, cancellationToken);

        return Ok(url);
    }
    
    [HttpDelete("{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Remove([FromRoute] string fileName)
    {
        await _multimediaService.RemoveByNameAsync(fileName);

        return Ok();
    }
}