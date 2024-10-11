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
        return await _context.Hotels.Include(h => h.ContactInformations)
            .FirstOrDefaultAsync(h => h.Id == id);
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

    public async Task DeleteHotelAsync(Guid id)
    {
        var hotel = await _context.Hotels.FindAsync(id);
        if (hotel != null)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
    }
}