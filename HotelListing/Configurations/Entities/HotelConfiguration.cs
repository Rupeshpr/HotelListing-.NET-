using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel { Id = 1, Name = "Hotel California", Address = "123 Sunset Blvd, Los Angeles", Rating = 4.5, CountryId = 1 },
                new Hotel { Id = 2, Name = "The Ritz", Address = "456 Park Ave, New York", Rating = 5.0, CountryId = 1 },
                new Hotel { Id = 3, Name = "Fairmont Royal York", Address = "100 Front St W, Toronto", Rating = 4.7, CountryId = 2 },
                new Hotel { Id = 4, Name = "Hotel Majestic", Address = "123 Avenida Reforma, Mexico City", Rating = 4.3, CountryId = 3 }
            );
        }
    }
}
