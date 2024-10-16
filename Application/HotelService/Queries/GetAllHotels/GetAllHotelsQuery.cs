using Application.HotelService.Queries.GetAllHotelManagers;
using Core.Entity;
using MediatR;

namespace Application.HotelService.Queries.GetAllHotels;

using System.Collections.Generic;

using MediatR;
using System.Collections.Generic;

public class GetAllHotelsQuery : IRequest<List<HotelsAllDetailDto>> 
{
  
   
}

public class HotelsAllDetailDto
{
    public Guid Id { get; set; }
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
    public string CompanyName { get; set; }

    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public List<GetContactInformationDto> ContactInformations { get; set; }
    
}
public class GetContactInformationDto
{
    public string InfoType { get; set; }
    public string InfoDetail { get; set; }
}