using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<CountryDao>, ICountriesRepository
    {

        private readonly HotelListingDbContext _context;
        public CountriesRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CountryDao> GetDetails(int id)
        {
            
            return await _context.Counties
                .Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);

        }
    }
}
