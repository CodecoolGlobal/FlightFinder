using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightFinder.Models
{
    public class Place
    {
        public string Code { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public string Type { get; set; }

        public Place(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
