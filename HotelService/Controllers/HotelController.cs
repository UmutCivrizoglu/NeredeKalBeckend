using Application.HotelService.Commands.CreateContactInformation;
using Application.HotelService.Commands.CreateHotel;
using Application.HotelService.Commands.DeleteHotel;
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

 
    [HttpPost("CreateHotel")]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
    {
        await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateHotel), new { }, command);
    }
    
    [HttpDelete("DeleteHotel/{id}")]
    public async Task<IActionResult> DeleteHotel(Guid id)
    {
        var command = new DeleteHotelCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPost("{id}/Addcontact")]
    public async Task<IActionResult> AddContactInformation(Guid id, [FromBody] CreateContactInformationCommand command)
    {
        if (id != command.HotelId)
        {
            return BadRequest("Hotel ID mismatch.");
        }

        await _mediator.Send(command);
        return Ok("Contact information added successfully.");
    }
}
