using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _mapper=mapper;
            _userManager=userManager;
            this._configuration = configuration;
        }

        public Task<string> CreateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponseDto> Login(LogintDto logintDto)
        {
            
                var user = await _userManager.FindByEmailAsync(logintDto.Email);
                bool isValidUser = await _userManager.CheckPasswordAsync(user, logintDto.Password);
            if(user == null || isValidUser == false)
            {
                return null;
            }
               
    
           var token =GenerteToken(user);

            return new AuthResponseDto()
            {
              
                Token = token.Result,
                UserId = user.Id
            };
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

        public Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto authResponseDto)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerteToken(ApiUser apiUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSetting:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(apiUser);
            var rolesClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(apiUser);
            var claims = new List<Claim>
            {

                new Claim(JwtRegisteredClaimNames.Sub, apiUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, apiUser.Email),
                new Claim("uid", apiUser.Id),


            }.Union(userClaims).Union(rolesClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSetting:Issuer"],
                audience: _configuration["JwtSetting:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSetting:DurationInMinutes"])),
                signingCredentials : credentials
                ) ;
            return new JwtSecurityTokenHandler().WriteToken(token);

          }

    }
}
