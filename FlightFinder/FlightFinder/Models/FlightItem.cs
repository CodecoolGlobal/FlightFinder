using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class FlightItem
    {
        public double Id { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Price { get; set; }
        public string Link { get; set; }
    }
}
