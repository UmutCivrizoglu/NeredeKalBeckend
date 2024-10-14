using Application.DTOs;
using Core.Entity;
using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Commands.CreateContactInformation;


    public class CreateContactInformationCommandHandler : IRequestHandler<CreateContactInformationCommand, Unit>
    {
        private readonly IHotelRepository _hotelRepository;

        public CreateContactInformationCommandHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Unit> Handle(CreateContactInformationCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.HotelId);

            if (hotel == null)
            {
                throw new Exception("Hotel not found.");
            }
            
            var contactInformationDto = new ContactInformation
            {
                InfoType = request.InfoType,
                InfoDetail = request.InfoDetail,
                HotelId = request.HotelId
            };

            await _hotelRepository.AddContactInformationAsync(contactInformationDto);

            return Unit.Value;
        }
    }
