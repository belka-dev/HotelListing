using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.User
{
    public class ApiUserDto : LogintDto
    {
      
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lasttname { get; set; }
      
            
    }
}
