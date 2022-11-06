using HotelListing.API.Contracts;
using HotelListing.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAuthManager _authManager;
        ILogger<AccountController> _logger;
        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }
        //api/account/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody] ApiUserDto apiUserDto)
        {
          
         
            _logger.LogInformation($"REGISTRZATION ATTEMPT FOR {apiUserDto.Email}");
            try
            {
                var errors = await _authManager.Regiter(apiUserDto);
                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                return Ok(apiUserDto);
            } catch(Exception ex)
            {
                _logger.LogError(ex, $"error happened in {nameof(Register)} - user Registration");
                return Problem($"error happened in {nameof(Register)} - user Registration", statusCode: 500);
            }   
               
        }
        //api/account/register
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(LogintDto logintDto)
        {
            var authreponse = await _authManager.Login(logintDto);
            if (authreponse == null)
            {
                return Unauthorized(); 
            }
            return Ok(authreponse);
        }
    }
}
