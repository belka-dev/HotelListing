using HotelListing.API.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
    public interface IAuthManager
    {
         Task<IEnumerable<IdentityError>> Regiter(ApiUserDto userdto);
        Task<AuthResponseDto> Login(LogintDto logintDto);

        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto authResponseDto);
        Task<string> CreateRefreshToken();
        
    }
}
