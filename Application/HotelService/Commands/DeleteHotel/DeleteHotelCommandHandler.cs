using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Commands.DeleteHotel;


    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Unit>
    {
        private readonly IHotelRepository _hotelRepository;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Unit> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.HotelId);

            if (hotel == null)
            {
                throw new Exception("Hotel not found.");
            }

            await _hotelRepository.DeleteHotelAsync(hotel);

            return Unit.Value;
        }
    }
