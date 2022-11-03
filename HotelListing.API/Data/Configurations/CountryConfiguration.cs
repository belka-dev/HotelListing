using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<CountryDao>
    {
        public void Configure(EntityTypeBuilder<CountryDao> builder)
        {
            builder.HasData(
                new CountryDao()
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new CountryDao()
                {
                    Id = 2,
                    Name = "France",
                    ShortName = "FR"
                },
                new CountryDao()
                {
                    Id = 3,
                    Name = "United Kindom",
                    ShortName = "UK"
                },
                new CountryDao()
                {
                    Id = 4,
                    Name = "Itally",
                    ShortName = "IT"
                },
                new CountryDao()
                {
                    Id = 5,
                    Name = "Kabylie",
                    ShortName = "KB"
                }
                );
        }
    }
    
}
