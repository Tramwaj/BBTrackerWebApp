using System;
using System.Collections.Generic;
using System.Text;

namespace BBTracker.Model.Models
{
    public class Location
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string OfficialName { get; set; }
        public ICollection<string> OtherNames { get; set; }
        public bool IsOutside { get; set; }

    }
}
