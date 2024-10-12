using MediatR;

namespace Application.HotelService.Commands.DeleteHotel;

public class DeleteHotelCommand : IRequest<Unit>
{
    public Guid HotelId { get; set; }

    public DeleteHotelCommand(Guid hotelId)
    {
        HotelId = hotelId;
    }
}