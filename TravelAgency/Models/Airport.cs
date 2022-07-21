using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int IdCity { get; set; }
        [ForeignKey("CityInfoKey")]
        public City? City{ get; set; }
    }
}
