using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyApp.Application.DTO.Products.Queries;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Client,Manager")]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetProductsQuery());
        return Ok(result);
    }
}
