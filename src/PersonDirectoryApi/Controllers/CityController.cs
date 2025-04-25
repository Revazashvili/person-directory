using Microsoft.AspNetCore.Mvc;
using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Services;

namespace PersonDirectoryApi.Controllers;

[ApiController]
[Route("api/city")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CityDto>>> GetCities([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var cities = await _cityService.GetAsync(pageNumber, pageSize, cancellationToken);
        
        if (cities.Count == 0) 
            return NoContent();
        
        return Ok(cities);
    }
}