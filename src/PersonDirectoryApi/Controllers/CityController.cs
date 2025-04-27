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
    [ValidationActionFilter<CitySearchDto>]
    [ProducesResponseType<List<CityDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<List<ValidationError>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CityDto>>> GetCities([FromQuery] CitySearchDto citySearchDto, CancellationToken cancellationToken)
    {
        var cities = await _cityService.GetAsync(citySearchDto, cancellationToken);
        
        if (cities.Count == 0) 
            return NotFound();
        
        return Ok(cities);
    }
}