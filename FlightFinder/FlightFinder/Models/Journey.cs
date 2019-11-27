using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Journey
    {
        public Flight inboundFlight { get; set; }
        public Flight outboundFlight { get; set; }
        public int price { get; set; }
        public string link { get; set; }
    }
}
