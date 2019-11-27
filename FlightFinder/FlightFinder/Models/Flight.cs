using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Flight
    {
        public string Id { get; set; }
        public string imgUrl { get; set; }
        public string originStation { get; set; }
        public string departure { get; set; }
        public int duration { get; set; }
        public string destinationStation { get; set; }
        public string arrival { get; set; }

    }
}
