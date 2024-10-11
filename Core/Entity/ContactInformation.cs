namespace Core.Entity;

public class ContactInformation
{
    public Guid Id { get; set; }  
    public string InfoType { get; set; }  
    public string InfoDetail { get; set; }  
    public Guid HotelId { get; set; } 
    public Hotel Hotel { get; set; }  
}
