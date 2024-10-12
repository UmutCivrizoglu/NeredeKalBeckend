using MediatR;

namespace Application.HotelService.Commands.CreateContactInformation;

public class CreateContactInformationCommand :IRequest<Unit>
{
    public Guid HotelId { get; set; }  
    public string InfoType { get; set; }  
    public string InfoDetail { get; set; }  

    public CreateContactInformationCommand(Guid hotelId, string infoType, string infoDetail)
    {
        HotelId = hotelId;
        InfoType = infoType;
        InfoDetail = infoDetail;
    }
}