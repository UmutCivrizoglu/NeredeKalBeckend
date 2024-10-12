using MediatR;

namespace Application.HotelService.Queries.GetHotelManagerById;

public class GetHotelManagersQuery : IRequest<List<HotelManagerDto>>
{
    public Guid HotelId { get; set; }

    public GetHotelManagersQuery(Guid hotelId)
    {
        HotelId = hotelId;
    }
}
public class HotelManagerDto
{
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
}