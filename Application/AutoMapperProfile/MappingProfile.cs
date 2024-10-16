using Application.HotelService.Commands.CreateContactInformation;
using Application.HotelService.Commands.CreateHotel;
using Application.HotelService.Queries.GetAllHotelManagers;
using Application.HotelService.Queries.GetAllHotels;
using AutoMapper;
using Core.Entity;

namespace Application.AutoMapperProfile;

public sealed class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<CreateHotelCommand, Hotel>().ReverseMap();
        CreateMap<CreateContactInformationCommand, ContactInformation>().ReverseMap();
        CreateMap<Hotel, HotelWithManagersDto>();
        CreateMap<Hotel, HotelsAllDetailDto>().ReverseMap();
        

    }
        
}