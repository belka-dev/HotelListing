using HotelListing.API.Data;

namespace HotelListing.API.Contracts
{

    public interface ICountriesRepository: IGenericRepository<CountryDao>
    {
        Task<CountryDao> GetDetails(int id);
    }
}
