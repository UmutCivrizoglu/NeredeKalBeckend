using Application.HotelService.Queries.GetHotelManagerById;
using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Queries.GetAllHotelManagers;

public class GetAllHotelManagersQueryHandler : IRequestHandler<GetAllHotelManagersQuery, List<HotelWithManagersDto>>
{
    private readonly IHotelRepository _hotelRepository;

    public GetAllHotelManagersQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<List<HotelWithManagersDto>> Handle(GetAllHotelManagersQuery request, CancellationToken cancellationToken)
    {
        var hotels = await _hotelRepository.GetAllHotelsAsync();

        var hotelWithManagersList = hotels.Select(hotel => new HotelWithManagersDto
        {
            HotelName = hotel.CompanyName,
            HotelAddress = hotel.Address,
            Managers = new List<HotelManagerDto>
            {
                new HotelManagerDto
                {
                    ManagerFirstName = hotel.ManagerFirstName,
                    ManagerLastName = hotel.ManagerLastName
                }
            }
        }).ToList();

        return hotelWithManagersList;
    }
}