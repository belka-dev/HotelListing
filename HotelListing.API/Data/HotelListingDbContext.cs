using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }   
        public DbSet<Country> Counties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country()
                {
                    Id = 1,
                    Name   =  "Jamaica",
                    ShortName="JM"
                },
                new Country()
                {
                    Id = 2,
                    Name   =  "France",
                    ShortName="FR"
                },
                new Country()
                {
                    Id = 3,
                    Name   =  "United Kindom",
                    ShortName="UK"
                },
                new Country()
                {
                    Id = 4,
                    Name   =  "Itally",
                    ShortName="IT"
                },
                new Country()
                {
                    Id = 5,
                    Name   =  "Kabylie",
                    ShortName="KB"
                }
                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel()
                {
                    Id = 1,
                    Name="SAndals Hotel",
                    CountryId=1,
                    Rating=4.5
                }, new Hotel()
                {
                    Id = 2,
                    Name="Paris Hotel",
                    CountryId=1,
                    Rating=4.5
                }, new Hotel()
                {
                    Id = 3,
                    Name="UK Hotel",
                    CountryId=3,
                    Rating=4.5
                }, new Hotel()
                {
                    Id = 4,
                    Name="Itally Hotel",
                    CountryId=4,
                    Rating=4.5
                }, 
                new Hotel(){
                    Id = 5,
                    Name="Djurdjura Hotel",
                    CountryId=5,
                    Rating=4.5
                }
                ); ;
        
        }

    }
}
