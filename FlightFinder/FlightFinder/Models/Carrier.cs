using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Carrier
    {
        public string Code { get; set; }
        public string DisplayCode { get; set; }
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }

        public Carrier(string code, string displayCode, long id, string imageUrl, string name)
        {
            Code = code;
            DisplayCode = displayCode;
            Id = id;
            ImageUrl = imageUrl;
            Name = name;
        }
    }
}
