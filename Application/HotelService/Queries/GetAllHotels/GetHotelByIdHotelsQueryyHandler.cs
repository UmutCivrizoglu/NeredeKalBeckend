using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Queries.GetAllHotels;

public class GetHotelDetailsQueryHandler : IRequestHandler<GetHotelDetailsQuery, HotelDetailsDto>
{
    private readonly IHotelRepository _hotelRepository;

    public GetHotelDetailsQueryHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<HotelDetailsDto> Handle(GetHotelDetailsQuery request, CancellationToken cancellationToken)
    {
      
        var hotel = await _hotelRepository.GetHotelByIdWithContactInformationAsync(request.HotelId);

        if (hotel == null)
        {
            throw new Exception("Hotel not found.");
        }

      
        var hotelDetails = new HotelDetailsDto
        {
            HotelName = hotel.CompanyName,
            HotelAddress = hotel.Address,
            HotelCity = hotel.City,
            HotelCountry = hotel.Country,
            ContactInformations = hotel.ContactInformations.Select(contact => new ContactInformationDto
            {
                InfoType = contact.InfoType,
                InfoDetail = contact.InfoDetail
            }).ToList()
        };

        return hotelDetails;
    }
}