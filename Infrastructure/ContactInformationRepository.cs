using Core.Entity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ContactInformationRepository : IContactInformationRepository
{

    private readonly HotelDbContext _context;

    public ContactInformationRepository(HotelDbContext context)
    {
            _context = context;
    }
    
    public async Task<List<ContactInformation>> GetAllContactInformationForHotelAsync(Guid hotelId)
    {
        return await _context.ContactInformations.Where(c => c.HotelId == hotelId).ToListAsync();
    }

    public async Task AddContactInformationAsync(ContactInformation contactInformation)
    { 
        await _context.ContactInformations.AddAsync(contactInformation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteContactInformationAsync(Guid id)
    {
        var ContactInformation = await _context.ContactInformations.FindAsync(id);
        if (ContactInformation != null)
        {
             _context.ContactInformations.Remove(ContactInformation);
             _context.SaveChanges();
        }
      
    }
}