using MediatR;

namespace Application.HotelService.Queries.GetAllHotels;

public class GetHotelDetailsQuery : IRequest<HotelDetailsDto>
{
    public Guid HotelId { get; set; }

    public GetHotelDetailsQuery(Guid hotelId)
    {
        HotelId = hotelId;
    }
}

public class HotelDetailsDto
{
    public string HotelName { get; set; }
    public string HotelAddress { get; set; }
    public string HotelCity { get; set; }
    public string HotelCountry { get; set; }
    public List<ContactInformationDto> ContactInformations { get; set; }  
}

public partial class ContactInformationDto
{
    public string InfoType { get; set; }  
    public string InfoDetail { get; set; }  
}