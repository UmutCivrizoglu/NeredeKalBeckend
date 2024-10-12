using MediatR;

namespace Application.HotelService.Commands.DeleteContactInformation;

public class DeleteContactInformatıonCommand
{
    public class DeleteContactInformationCommand : IRequest<Unit>
    {
        public Guid ContactInformationId { get; set; }

        public DeleteContactInformationCommand(Guid contactInformationId)
        {
            ContactInformationId = contactInformationId;
        }
    }
}