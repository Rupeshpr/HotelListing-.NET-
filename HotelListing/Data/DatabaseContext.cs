using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {            
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding data for Countries
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "USA", ShortName = "US" },
                new Country { Id = 2, Name = "Canada", ShortName = "CA" },
                new Country { Id = 3, Name = "Mexico", ShortName = "MX" }
            );

            // Seeding data for Hotels
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Hotel California", Address = "123 Sunset Blvd, Los Angeles", Rating = 4.5, CountryId = 1 },
                new Hotel { Id = 2, Name = "The Ritz", Address = "456 Park Ave, New York", Rating = 5.0, CountryId = 1 },
                new Hotel { Id = 3, Name = "Fairmont Royal York", Address = "100 Front St W, Toronto", Rating = 4.7, CountryId = 2 },
                new Hotel { Id = 4, Name = "Hotel Majestic", Address = "123 Avenida Reforma, Mexico City", Rating = 4.3, CountryId = 3 }
            );
        }
    }
}
