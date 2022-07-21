using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string? RationName { get; set; }
        public int? TimesPerDay { get; set; }
    }
}
