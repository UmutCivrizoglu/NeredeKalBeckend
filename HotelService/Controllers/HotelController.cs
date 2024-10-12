using Application.HotelService.Commands.CreateContactInformation;
using Application.HotelService.Commands.CreateHotel;
using Application.HotelService.Commands.DeleteContactInformation;
using Application.HotelService.Commands.DeleteHotel;
using Application.HotelService.Queries.GetAllHotelManagers;
using Application.HotelService.Queries.GetAllHotels;
using Application.HotelService.Queries.GetHotelManagerById;
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
    [HttpDelete("contact/{contactId}")]
    public async Task<IActionResult> DeleteContactInformation(Guid contactId)
    {
        var command = new DeleteContactInformatıonCommand.DeleteContactInformationCommand(contactId);
        await _mediator.Send(command);
        return NoContent(); 
    }
    [HttpGet("{id}/managers")]
    public async Task<IActionResult> GetHotelManagers(Guid id)
    {
        var query = new GetHotelManagersQuery(id);
        var managers = await _mediator.Send(query);
        return Ok(managers);
    }
    [HttpGet("managers")]
    public async Task<IActionResult> GetAllHotelManagers()
    {
        var query = new GetAllHotelManagersQuery();
        var hotelManagers = await _mediator.Send<List<HotelWithManagersDto>>(query);
        return Ok(hotelManagers);
    }
    [HttpGet("{id}/details")]
    public async Task<IActionResult> GetHotelDetails(Guid id)
    {
        var query = new GetHotelDetailsQuery(id);
        var hotelDetails = await _mediator.Send(query);
        return Ok(hotelDetails);
    }
}
