using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<HotelDao>
    {
        public void Configure(EntityTypeBuilder<HotelDao> builder)
        {
            builder.HasData(
                new HotelDao()
                {
                    Id = 1,
                    Name = "SAndals Hotel",
                    CountryId = 1,
                    Rating = 4.5
                }, new HotelDao()
                {
                    Id = 2,
                    Name = "Paris Hotel",
                    CountryId = 1,
                    Rating = 4.5
                }, new HotelDao()
                {
                    Id = 3,
                    Name = "UK Hotel",
                    CountryId = 3,
                    Rating = 4.5
                }, new HotelDao()
                {
                    Id = 4,
                    Name = "Itally Hotel",
                    CountryId = 4,
                    Rating = 4.5
                },
                new HotelDao()
                {
                    Id = 5,
                    Name = "Djurdjura Hotel",
                    CountryId = 5,
                    Rating = 4.5
                }
                );
        }
    }
   
}
