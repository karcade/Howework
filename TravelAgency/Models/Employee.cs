using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    public class Employee:User
    {
        public int? IdPosition { get; set; }
        [ForeignKey("PositionInfoKey")]
        public Position? Position { get; set; }
        public double Salary { get; set; }
        public List<Contract> Contract { get; set; } = new();
    }
}
