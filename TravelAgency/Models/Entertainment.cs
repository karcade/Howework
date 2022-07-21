using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Entertainment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
    }
}
