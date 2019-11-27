using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Agent 
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public bool OptimisedForMobile { get; set; }
        public string Type { get; set; }

        public Agent(long id, string name, string imageUrl, string status, bool optimisedForMobile, string type)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Status = status;
            OptimisedForMobile = optimisedForMobile;
            Type = type;
        }
    }


}
