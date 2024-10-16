using MediatR;

namespace Application.HotelService.Commands.DeleteContactInformation;

public class DeleteContactInformatıonCommand : IRequest<Unit>
{
        public Guid HotelId { get; set; }

        public DeleteContactInformatıonCommand(Guid hotelId)
        {
            HotelId = hotelId;
        }
    
}