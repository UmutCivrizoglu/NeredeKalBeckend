using Application.HotelService.Queries.GetAllHotelManagers;
using AutoMapper;
using Core.Entity;
using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Queries.GetAllHotels;

public class GetAllHotelsHandler : IRequestHandler<GetAllHotelsQuery,List<HotelsAllDetailDto>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public GetAllHotelsHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<List<HotelsAllDetailDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
    {
        // Otel verilerini al
        var hotels = await _hotelRepository.GetAllHotelsWithContactInfo();

        // Otelleri DTO'lara dönüştür
        var hotelDtos = hotels.Select(h => new HotelsAllDetailDto
        {
            Id = h.Id,
            CompanyName = h.CompanyName,
            Address = h.Address, 
            City = h.City, 
            Country = h.Country, 
            ManagerFirstName = h.ManagerFirstName, 
            ManagerLastName = h.ManagerLastName, 
            ContactInformations = h.ContactInformations.Select(c => new GetContactInformationDto 
            {
                InfoType = c.InfoType, 
                InfoDetail = c.InfoDetail 
            }).ToList() 
        }).ToList(); 

        return hotelDtos; 
    }
}