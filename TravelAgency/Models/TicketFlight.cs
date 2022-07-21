using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Models
{
    public class TicketFlight
    {
        public int TickerNumer { get; set; }
        public int FlightId { get; set; }
        public int? SeatAmount { get; set; }
        public int? AircraftCode { get; set; }
    }
}
