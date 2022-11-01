using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<HotelDao> Hotels { get; set; }   
        public DbSet<CountryDao> Counties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CountryDao>().HasData(
                new CountryDao()
                {
                    Id = 1,
                    Name   =  "Jamaica",
                    ShortName="JM"
                },
                new CountryDao()
                {
                    Id = 2,
                    Name   =  "France",
                    ShortName="FR"
                },
                new CountryDao()
                {
                    Id = 3,
                    Name   =  "United Kindom",
                    ShortName="UK"
                },
                new CountryDao()
                {
                    Id = 4,
                    Name   =  "Itally",
                    ShortName="IT"
                },
                new CountryDao()
                {
                    Id = 5,
                    Name   =  "Kabylie",
                    ShortName="KB"
                }
                );
            modelBuilder.Entity<HotelDao>().HasData(
                new HotelDao()
                {
                    Id = 1,
                    Name="SAndals Hotel",
                    CountryId=1,
                    Rating=4.5
                }, new HotelDao()
                {
                    Id = 2,
                    Name="Paris Hotel",
                    CountryId=1,
                    Rating=4.5
                }, new HotelDao()
                {
                    Id = 3,
                    Name="UK Hotel",
                    CountryId=3,
                    Rating=4.5
                }, new HotelDao()
                {
                    Id = 4,
                    Name="Itally Hotel",
                    CountryId=4,
                    Rating=4.5
                }, 
                new HotelDao(){
                    Id = 5,
                    Name="Djurdjura Hotel",
                    CountryId=5,
                    Rating=4.5
                }
                ); ;
        
        }

    }
}
