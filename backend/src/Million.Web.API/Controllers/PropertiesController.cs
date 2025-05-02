using MediatR;
using Microsoft.AspNetCore.Mvc;
using Million.Application.Features.Properties.Queries.GetAllProperties;
using Million.Application.Features.Properties.Queries.GetFilteredProperties;
using Million.Application.Features.Properties.Queries.GetPropertyById;

namespace Million.Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PropertiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllPropertiesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetPropertyByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFiltered([FromQuery] GetFilteredPropertiesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
