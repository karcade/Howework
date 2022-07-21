using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    public class Client:User
    {
        public bool Insurance { get; set; }

        public int PassportId { get; set; }
        public Passport? Passport { get; set; }
        public List<Contract>? Contracts { get; set; } = new();
    }
}
