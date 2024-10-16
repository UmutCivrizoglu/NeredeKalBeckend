using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Commands.DeleteContactInformation;

public class DeleteContactInformationCommandHandler : IRequestHandler<DeleteContactInformatıonCommand, Unit>
{
    private readonly IHotelRepository _hotelRepository;

    public DeleteContactInformationCommandHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<Unit> Handle(DeleteContactInformatıonCommand request, CancellationToken cancellationToken)
    {

        await _hotelRepository.DeleteContactInformationAsync(request.HotelId);
        return Unit.Value;
    }
}