using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Leg
    {
        public string Arrival { get; set; }
        public string Carriers { get; set; }
        public string Departure { get; set; }
        public int DestinationStation { get; set; }
        public string Directionality { get; set; }
        public List<Flightnumber> FlighNumbers { get; set; }
        public string Id { get; set; }
        public string JourneyMode { get; set; }
        public List<int> OperatingCarriers { get; set; }
        public int OriginStation { get; set; }
        public List<int> SegmentIds { get; set; }
        public List<int> Stops { get; set; }

        public Leg(string arrival, string carriers, string departure, int destinationStation, string id, int originStation)
        {
            Arrival = arrival;
            Carriers = carriers;
            Departure = departure;
            DestinationStation = destinationStation;
            Id = id;
            OriginStation = originStation;
        }
    }

}
