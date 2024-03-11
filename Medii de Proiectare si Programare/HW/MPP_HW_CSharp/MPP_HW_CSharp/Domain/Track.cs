using System;
using System.Collections.Generic;

namespace MPP_HW_CSharp.Domain
{
    public class Track : Entity<String>
    {
        private string Name { get; set; }
        private int MinimumAge { get; set; }
        private int MaximumAge { get; set; }
        private int Distance { get; set; }
        private List<Child> Children { get; set; }
        
        public Track(string id, string name, int minimumAge, int maximumAge, int distance) : base(id)
        {
            Name = name;
            this.MinimumAge = minimumAge;
            this.MaximumAge = maximumAge;
            this.Distance = distance;
            Children = new List<Child>();
        }
        
        
    }
}