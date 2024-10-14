using Core.Entity;

namespace Application.DTOs;

public class ContactInformationDto
{
    public Guid Id { get; set; }
    public string InfoType { get; set; }  
    public string InfoDetail { get; set; }
    public Guid HotelId { get; set; }  
    public string HotelName { get; set; }  
}
