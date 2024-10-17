using Core.Entity;

namespace Core.Interfaces;

public interface IHotelRepository
{
    Task<List<Hotel?>> GetAllHotelsAsync();
    Task<Hotel?> GetHotelByIdAsync(Guid id);
    Task AddHotelAsync(Hotel hotel);
    Task UpdateHotelAsync(Hotel hotel);
    Task DeleteHotelAsync(Guid hotelId);
    Task AddContactInformationAsync(ContactInformation contactInformation);
    Task<ContactInformation?> GetContactInformationByIdAsync(Guid requestContactInformationId);
    Task DeleteContactInformationAsync(Guid hotelId);
    Task<Hotel?> GetHotelByIdWithContactInformationAsync(Guid requestHotelId);
    Task PrepareReportAsync(Guid reportId, string cityName);
    Task<List<Hotel>> GetAllHotelsWithContactInfo();

}