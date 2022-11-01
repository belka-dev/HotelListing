namespace HotelListing.API.Data
{
    public class CountryDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }   
        public virtual IList<HotelDao> Hotels { get; set; }
    }
}