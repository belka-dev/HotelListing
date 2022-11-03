using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.User
{
    public class LogintDto
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Your Password is Limitted to {2} to {1} Carractres", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
