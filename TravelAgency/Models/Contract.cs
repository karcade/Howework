using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public List<Client> Clients { get; set; } = new();
        public int IdEmployee { get; set; }
        [ForeignKey("EmployeeInfoKey")]
        public Employee? Employee { get; set; }
        public List<Tour> Tours { get; set; } = new();
        public List<Ticket> Ticket { get; set; } = new();
        public double Price { get; set; }
    }
}
