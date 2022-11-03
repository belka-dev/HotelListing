using HotelListing.API.Data.Configurations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Data
{
    public class HotelDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public double Rating { get; set; }
        [ForeignKey (nameof(CountryId))]
        public int CountryId { get; set; }   
        public CountryDao Country { get; set; }
    }
}
