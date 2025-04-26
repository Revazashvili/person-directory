using FluentValidation;
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

    [HttpGet("{personalNumber}")]
    [ProducesResponseType<PersonDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] string personalNumber, CancellationToken cancellationToken)
    {
        var dto = await _personService.GetAsync(personalNumber, cancellationToken);
        
        if (dto is null)
            return NoContent();

        return Ok(dto);
    }
    
    [HttpGet]
    [ProducesResponseType<List<PersonDto>>(StatusCodes.Status200OK)]
    [ValidationActionFilter<PersonSearchDto>]
    public async Task<IActionResult> Get([FromQuery] PersonSearchDto personSearchDto, CancellationToken cancellationToken)
    {
        var dto = await _personService.GetAllAsync(personSearchDto, cancellationToken);
        
        if (dto.Count == 0)
            return NoContent();

        return Ok(dto);
    }
    
    [HttpPost]
    [ValidationActionFilter<PersonCreateDto>]
    public async Task<IActionResult> Create([FromBody] PersonCreateDto personCreateDto, CancellationToken cancellationToken)
    {
        await _personService.CreateAsync(personCreateDto, cancellationToken);

        return Created();
    }
    
    [HttpPatch]
    [ValidationActionFilter<PersonUpdateDto>]
    public async Task<IActionResult> Update([FromBody] PersonUpdateDto personUpdateDto, CancellationToken cancellationToken)
    {
        await _personService.UpdateAsync(personUpdateDto, cancellationToken);

        return Ok();
    }
    
    [HttpDelete]
    [ValidationActionFilter<PersonDeleteDto>]
    public async Task<IActionResult> Delete([FromQuery] PersonDeleteDto personDeleteDto, CancellationToken cancellationToken)
    {
        await _personService.DeleteAsync(personDeleteDto, cancellationToken);

        return Ok();
    }
    
    [HttpPatch("{PersonalNumber}/image/{ImageUrl}")]
    [ValidationActionFilter<PersonImageChangeDto>]
    public async Task<IActionResult> ChangeImage([FromRoute] PersonImageChangeDto personImageChangeDto, CancellationToken cancellationToken)
    {
        await _personService.ChangeImageAsync(personImageChangeDto, cancellationToken);

        return Ok();
    }
    
    [HttpPost("{personalNumber}/relationship")]
    public async Task<IActionResult> CreateRelationship([FromRoute] string personalNumber, [FromBody] RelatedPersonDto relatedPerson, 
        CancellationToken cancellationToken, IValidator<RelationshipCreateDto> validator)
    {
        var relationshipCreateDto = new RelationshipCreateDto(personalNumber, relatedPerson);

        var validationResult = await validator.ValidateAsync(relationshipCreateDto, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ValidationResultObject(validationResult);
        
        await _personService.CreateRelationship(relationshipCreateDto, cancellationToken);

        return Created();
    }
    
    [HttpDelete("{PersonalNumber}/relationship/{RelatedPersonPersonalNumber}")]
    [ValidationActionFilter<RelationshipRemoveDto>]
    public async Task<IActionResult> RemoveRelationship([FromRoute] RelationshipRemoveDto relationshipRemoveDto, CancellationToken cancellationToken)
    {
        await _personService.RemoveRelationship(relationshipRemoveDto, cancellationToken);

        return Created();
    }
}