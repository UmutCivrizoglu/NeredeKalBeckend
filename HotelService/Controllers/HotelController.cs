using Application.HotelService.Commands.CreateHotel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelController(IMediator mediator)
    {
        _mediator = mediator;
    }

 
    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
    {
        await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateHotel), new { }, command);
    }
}
