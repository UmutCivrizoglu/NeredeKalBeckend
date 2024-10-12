using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Commands.DeleteContactInformation;

public class DeleteContactInformationCommandHandler : IRequestHandler<DeleteContactInformatıonCommand.DeleteContactInformationCommand, Unit>
{
    private readonly IHotelRepository _hotelRepository;

    public DeleteContactInformationCommandHandler(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    public async Task<Unit> Handle(DeleteContactInformatıonCommand.DeleteContactInformationCommand request, CancellationToken cancellationToken)
    {
        var contactInformation = await _hotelRepository.GetContactInformationByIdAsync(request.ContactInformationId);

        if (contactInformation == null)
        {
            throw new Exception("Contact information not found.");
        }

        await _hotelRepository.DeleteContactInformationAsync(contactInformation);

        return Unit.Value;
    }
}