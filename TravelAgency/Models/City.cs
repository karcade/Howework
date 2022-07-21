namespace TravelAgency.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tour? Tour { get; set; }
        public List<Airport> Airports { get; set; } = new();
        public List<Hotel> Hotels { get; set; } = new();
    }
}
