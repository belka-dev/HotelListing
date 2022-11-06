using HotelListing.API.Contracts;
using HotelListing.API.Mapper;
using HotelListing.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HotelListing.API.Startup
{
    public static class DepencyInjectionSetup
    {
        public static IServiceCollection RegisterServices( this IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           services.AddEndpointsApiExplorer();
           services.AddSwaggerGen();

           services.AddCors(options =>
            {

                options.AddPolicy("AllowAll", b =>
                {
                    b.AllowAnyHeader();
                    b.AllowAnyOrigin();
                    b.AllowAnyMethod();
                });

                //updated

            });

           services.AddAutoMapper(typeof(MapperConfig));
           services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           services.AddScoped<ICountriesRepository, CountriesRepository>();

             services.AddScoped<IHotelsRepository, HotelsRepository>();
    
            services.AddScoped<IAuthManager, AuthManager>();
          
                
                return services;
        }
    }
}
