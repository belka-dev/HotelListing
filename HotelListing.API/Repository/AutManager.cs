using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
        {
            _mapper=mapper;
            _userManager=userManager;
        }

        public async Task<bool> Login(LogintDto logintDto)
        {
            bool isValidUser = false;
            try
            {
                var user = await _userManager.FindByEmailAsync(logintDto.Email);
                isValidUser = await _userManager.CheckPasswordAsync(user, logintDto.Password);

               
            }
            catch(Exception){
                return isValidUser;
            }
           return isValidUser;
        }

        public async Task<IEnumerable<IdentityError>> Regiter(ApiUserDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result.Errors;

        }

    }
}
