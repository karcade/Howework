using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCity { get; set; }
        public City? City { get; set; }
        public List<Entertainment> Entertainment { get; set; } = new();
    }
}
