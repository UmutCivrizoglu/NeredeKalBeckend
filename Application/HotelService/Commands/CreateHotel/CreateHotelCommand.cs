using MediatR;

namespace Application.HotelService.Commands.CreateHotel;

public class CreateHotelCommand : IRequest
{
    public string ManagerFirstName { get; set; }  
    public string ManagerLastName { get; set; } 
    public string CompanyName { get; set; }     
}