using Application.DTOs;
using AutoMapper;
using Core.Entity;
using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Commands.CreateHotel;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand,Unit>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;
    public CreateHotelCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
       
        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            throw new Exception("Company name is required.");
        }
       
      var hotel = _mapper.Map<Hotel>(request);
      hotel.Id = Guid.NewGuid();
        await _hotelRepository.AddHotelAsync(hotel);
        return Unit.Value;
    }
}
