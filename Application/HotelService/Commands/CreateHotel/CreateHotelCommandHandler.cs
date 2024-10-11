using Core.Entity;
using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Commands.CreateHotel;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand>
{
    private readonly IHotelRepository _hotelRepository;

    public CreateHotelCommandHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<Unit> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
       
        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            throw new Exception("Company name is required.");
        }

     
        var hotel = new Hotel
        {
            Id = Guid.NewGuid(),
            ManagerFirstName = request.ManagerFirstName,
            ManagerLastName = request.ManagerLastName,
            CompanyName = request.CompanyName
        };
        await _hotelRepository.AddHotelAsync(hotel);
        return Unit.Value;
    }
}
