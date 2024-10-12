using MediatR;

namespace Application.HotelService.Commands.CreateHotel;

public class CreateHotelCommand : IRequest<Unit>
{
    public string ManagerFirstName { get; set; }  
    public string ManagerLastName { get; set; }  
    public string CompanyName { get; set; }  
    
    public string Address { get; set; }         
    public string City { get; set; }            
    public string Country { get; set; }           
}