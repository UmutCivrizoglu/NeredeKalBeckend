using Core.Entity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class HotelRepository : IHotelRepository
{
    
    private readonly HotelDbContext _context;
    
    public HotelRepository(HotelDbContext context)
    {
        _context = context;
     
    }
    public async Task<List<Hotel?>> GetAllHotelsAsync()
    {
        return await _context.Hotels.Include(h => h.ContactInformations).ToListAsync();
    }
    public async Task<Hotel> GetHotelByIdAsync(Guid id)
    {
        return await _context.Hotels.FindAsync(id);
    }

    public async Task AddHotelAsync(Hotel hotel)
    {
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateHotelAsync(Hotel hotel)
    {
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteHotelAsync(Guid hotelId)
    {
      var hotel = await _context.Hotels.FindAsync(hotelId);
      hotel.isDeleted = true;
      await _context.SaveChangesAsync();
    }
 


    public async Task AddContactInformationAsync(ContactInformation contactInformation)
    {
        await _context.ContactInformations.AddAsync(contactInformation);
        await _context.SaveChangesAsync();
    }

    public async Task<ContactInformation?> GetContactInformationByIdAsync(Guid contactInformationId)
    {
        return await _context.ContactInformations.FindAsync(contactInformationId);
    }

    public async Task DeleteContactInformationAsync(Guid id)
    {
        var hotel = await _context.Hotels.Include(h=>h.ContactInformations).
            FirstOrDefaultAsync(h=>h.Id==id);
        foreach (var contact in hotel.ContactInformations)
        {
            contact.IsDeleted = true;
        }
        await _context.SaveChangesAsync(); 
    }

    public async Task<Hotel> GetHotelByIdWithContactInformationAsync(Guid hotelId)
    {
        return await _context.Hotels
            .Include(h => h.ContactInformations) 
            .FirstOrDefaultAsync(h => h.Id == hotelId);
    }
    public async Task<List<Hotel>> GetAllHotelsWithContactInfo()
    {
        return await _context.Hotels
            .Include(h => h.ContactInformations).ToListAsync();

    }

    public Task PrepareReportAsync(Guid reportId, string cityName)
    {
        throw new NotImplementedException();
    }


  
}