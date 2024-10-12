using Application.HotelService.Queries.GetHotelManagerById;
using MediatR;

namespace Application.HotelService.Queries.GetAllHotelManagers;

public class GetAllHotelManagersQuery : IRequest<List<HotelWithManagerDto>>, IRequest<List<HotelWithManagersDto>>
{
    
}
public class HotelWithManagersDto
{
    public string HotelName { get; set; }  
    public string HotelAddress { get; set; } 
    public List<HotelManagerDto> Managers { get; set; }
}

public class HotelWithManagerDto
{
    public string ManagagerFirstName { get; set; }
    public string ManagagerLastName { get; set; }
}