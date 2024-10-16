using Application.DTOs;
using AutoMapper;
using Core.Entity;
using Core.Interfaces;
using MediatR;

namespace Application.HotelService.Commands.CreateContactInformation;


    public class CreateContactInformationCommandHandler : IRequestHandler<CreateContactInformationCommand, Unit>
    {
        private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;
        public CreateContactInformationCommandHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateContactInformationCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.HotelId);

            if (hotel == null)
            {
                throw new Exception("Hotel not found.");
            }
            var contactInformation = _mapper.Map<ContactInformation>(request);
            await _hotelRepository.AddContactInformationAsync(contactInformation);

            return Unit.Value;
        }
    }
