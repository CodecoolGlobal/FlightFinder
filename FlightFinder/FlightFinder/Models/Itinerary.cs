using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Itinerary
    {
        public string InboundLegId { get; set; }
        public string OutboundLegId { get; set; }
        public string BookingDetailsLink { get; set; }
        public int Price { get; set; }
        public string Link { get; set; }

        public Itinerary(string inboundLegId, string outboundLegId, string bookingDetailsLink, int price, string link)
        {
            InboundLegId = inboundLegId;
            OutboundLegId = outboundLegId;
            BookingDetailsLink = bookingDetailsLink;
            Price = price;
            Link = link;
        }
    }
}
