using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class Passport
    {
        public string PassportNumber { get; set; }
        public string PassportSeria { get; set; }
        public bool Liquid { get; set; }
        public Client? Client { get; set; }
    }
}
