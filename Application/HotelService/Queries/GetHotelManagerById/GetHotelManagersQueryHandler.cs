using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Queries.GetHotelManagerById;

public class GetHotelManagersQueryHandler : IRequestHandler<GetHotelManagersQuery, List<HotelManagerDto>>
{
    private readonly IHotelRepository _hotelRepository;

    public GetHotelManagersQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<List<HotelManagerDto>> Handle(GetHotelManagersQuery request, CancellationToken cancellationToken)
    {
        
        var hotel = await _hotelRepository.GetHotelByIdAsync(request.HotelId);

        if (hotel == null)
        {
            throw new Exception("Hotel not found.");
        }

      
        var managers = new List<HotelManagerDto>
        {
            new HotelManagerDto
            {
                ManagerFirstName = hotel.ManagerFirstName,
                ManagerLastName = hotel.ManagerLastName,
                
            }
        };

        return managers;
    }
}