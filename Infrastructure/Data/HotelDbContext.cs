
using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class HotelDbContext : DbContext
    {
      

        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
          
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

        }
    }
}