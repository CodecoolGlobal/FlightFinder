using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Query
    {
        public int Adults { get; set; }
        public string CabinClass { get; set; }
        public int Children { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string DestinationPlace { get; set; }
        public bool GroupPricing { get; set; }
        public string InboundDate { get; set; }
        public int Infants { get; set; }
        public string Locale { get; set; }
        public string LocationSchema { get; set; }
        public string OriginPlace { get; set; }
        public string OutboundDate { get; set; }
    }
}
