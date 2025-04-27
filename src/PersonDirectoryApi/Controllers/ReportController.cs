using Microsoft.AspNetCore.Mvc;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Services;

namespace PersonDirectoryApi.Controllers;

[ApiController]
[Route("api/report")]
public class ReportController : ControllerBase
{
    private readonly IPersonService _personService;
    public ReportController(IPersonService personService)
    {
        _personService = personService;
    }
    
    [HttpGet("relationships")]
    [ValidationActionFilter<GetRelationshipReportDto>]
    public async Task<IActionResult> GetRelationshipsReportAsync([FromQuery] GetRelationshipReportDto getRelationshipReportDto, CancellationToken cancellationToken)
    {
        var report = await _personService.GetRelationshipReportAsync(getRelationshipReportDto, cancellationToken);
        
        if (report.Count == 0)
            return NoContent();
        
        return Ok(report);
    }
}