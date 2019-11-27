using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Flight
    {
        public List<Agent> Agents { get; set; }
        public List<Carrier> Carriers { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public List<Leg> Legs { get; set; }
        public List<Place> Places { get; set; }
        public List<Query> Query { get; set; }
        public List<Segment> Segments { get; set; }
        public string SessionKey { get; set; }
        public string Status { get; set; }

        public Flight(object json)
        {
            JObject jObject = (JObject)json;
            SessionKey = (string)jObject["SessionKey"];
            Status = (string)jObject["Status"];
            Agents = new List<Agent>();
            Carriers = new List<Carrier>();
            Itineraries = new List<Itinerary>();
            Legs = new List<Leg>();
            Places = new List<Place>();
            Segments = new List<Segment>();
            FillAgents(jObject);
            FillCarriers(jObject);
            FillItineraries(jObject);
            FillLegs(jObject);
            FillPlaces(jObject);
            FillSegments(jObject);

        }

        private void FillAgents(JObject jObject)
        {
            foreach (var agent in jObject["Agents"])
            {
                Agents.Add(new Agent((long)agent["Id"],
                                     (string)agent["Name"],
                                     (string)agent["ImageUrl"],
                                     (string)agent["Status"],
                                     (bool)agent["OptimisedForMobile"],
                                     (string)agent["Type"]));
            }
        }

        private void FillCarriers(JObject jObject)
        {
            foreach (var carrier in jObject["Carriers"])
            {
                Carriers.Add(new Carrier(
                    (string)carrier["Code"],
                    (string)carrier["DisplayCode"],
                    (long)carrier["Id"],
                    (string)carrier["ImageUrl"],
                    (string)carrier["Name"]));
            }
        }

        private void FillItineraries(JObject jObject)
        {
            foreach (var itinerary in jObject["Itineraries"])
            {
                Itineraries.Add(new Itinerary(
                    (string)itinerary["InboundLegId"],
                    (string)itinerary["OutboundLegId"],
                    (string)itinerary["BookingDetailsLink"]["Uri"],
                    (string)itinerary["PricingOptions"][0]["Price"],
                    (string)itinerary["PricingOptions"][0]["DeeplinkUrl"]));
            }
        }

        private void FillLegs(JObject jObject)
        {
            foreach (var leg in jObject["Legs"])
            {
                Legs.Add(new Leg((string)leg["Arrival"],
                                 (string)leg["Carriers"][0],
                                 (string)leg["Departure"],
                                 (int)leg["DestinationStation"],
                                 (string)leg["Id"],
                                 (int)leg["OriginStation"]));
            }
        }

        private void FillPlaces(JObject jObject)
        {
            foreach (var place in jObject["Places"])
            {
                Places.Add(new Place((long)place["Id"], (string)place["Name"]));
            }
        }

        private void FillSegments(JObject jObject)
        {
            foreach (var segment in jObject["Segments"])
            {
                Segments.Add(new Segment(
                    (string)segment["ArrivalDateTime"],
                    (int)segment["Carrier"],
                    (string)segment["DepartureDateTime"],
                    (int)segment["DestinationStation"],
                    (int)segment["Duration"],
                    (int)segment["Id"],
                    (int)segment["OriginStation"]));
            }
        }
    }
}
