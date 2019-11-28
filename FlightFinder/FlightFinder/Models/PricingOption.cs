using System.Collections.Generic;

namespace FlightFinder.Models
{
    public class PricingOption
    {
        public List<int> Agents { get; set; }
        public string DeepLinkUrl { get; set; }
        public double Price { get; set; }
        public int QuoteAgeInMinutes { get; set; }
    }
}