using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Data.Configurations;
using HotelListing.API.Models.Country;
using HotelListing.API.Models.Hotel;
using HotelListing.API.Models.User;

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
            CreateMap<ApiUserDto, ApiUser>().ReverseMap();

        }
    
    }
}
