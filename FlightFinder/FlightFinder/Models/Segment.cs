using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Segment
    {
        public string ArrivalDateTime { get; set; }
        public int Carrier { get; set; }
        public string DepartureDateTime { get; set; }
        public int DestinationStation { get; set; }
        public string Directionality { get; set; }
        public int Duration { get; set; }
        public string FlightNumber { get; set; }
        public int Id { get; set; }
        public string JourneyMode { get; set; }
        public int OperatingCarrier { get; set; }
        public int OriginStation { get; set; }

        public Segment(string arrivalDateTime, int carrier, string departureDateTime, int destinationStation, int duration, int id, int originStation)
        {
            ArrivalDateTime = arrivalDateTime;
            Carrier = carrier;
            DepartureDateTime = departureDateTime;
            DestinationStation = destinationStation;
            Duration = duration;
            Id = id;
            OriginStation = originStation;
        }
    }
}
