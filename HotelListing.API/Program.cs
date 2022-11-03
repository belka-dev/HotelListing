using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Mapper;
using HotelListing.API.Repository;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Startup;
using Serilog;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
//configuring services 
builder.Services.RegisterServices();
//adding connection string 


var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");

builder.Services.AddDbContext<HotelListingDbContext>(options => {
    
    options.UseSqlServer(connectionString); 
});


builder.Host.UseSerilog((context, loggerConfig) =>

loggerConfig.WriteTo.Console().ReadFrom.Configuration(context.Configuration));
//--- updating Identity

builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HotelListingDbContext>();
  

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
