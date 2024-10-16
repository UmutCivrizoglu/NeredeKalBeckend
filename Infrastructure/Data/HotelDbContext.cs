using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class HotelDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

     
        public HotelDbContext(DbContextOptions<HotelDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

      
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=mydb;Username=umut.civrizoglu;Password=istanbul123;SSL Mode=Disable");
            }
        }

    }
}