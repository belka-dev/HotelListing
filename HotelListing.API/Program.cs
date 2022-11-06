using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Mapper;
using HotelListing.API.Repository;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Startup;
using Serilog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotelListing.API.Midelware;

var builder = WebApplication.CreateBuilder(args);
//configuring services 
builder.Services.RegisterServices();
//adding connection string 


var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");

builder.Services.AddDbContext<HotelListingDbContext>(options => {
    
    options.UseSqlServer(connectionString); 
});
Console.WriteLine(builder.Configuration["JwtSetting.Key"]);
builder.Host.UseSerilog((context, loggerConfig) =>

loggerConfig.WriteTo.Console().ReadFrom.Configuration(context.Configuration));
//--- updating Identity

builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HotelListingDbContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Bearer
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSetting.Issuer"],
        ValidAudience = builder.Configuration["JwtSetting.Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSetting.Key"]))

    };
}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureSwagger();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.UseMiddleware<ExceptionMilddleware>();

app.MapControllers();

app.Run();
