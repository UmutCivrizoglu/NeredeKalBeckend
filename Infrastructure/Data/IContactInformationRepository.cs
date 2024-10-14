using Core.Entity;

namespace Core.Interfaces;

public interface IContactInformationRepository
{
    Task<List<ContactInformation>> GetAllContactInformationForHotelAsync(Guid hotelId);
    Task AddContactInformationAsync(ContactInformation contactInformation);
    Task DeleteContactInformationAsync(Guid id);
}