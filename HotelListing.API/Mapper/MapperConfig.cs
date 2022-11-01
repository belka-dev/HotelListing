using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
        
            CreateMap<CountryDao, CountryVo>().ReverseMap();
            CreateMap<CountryDao, CountryDetailsVo>().ReverseMap();
            CreateMap<CountryDao, UpdateCounrty>().ReverseMap();
            CreateMap<HotelDao, HotelDto>().ReverseMap();
        }
    
    }
}
