using System;
using System.Collections.Generic;

namespace MPP_CSharp.Domain
{
    public class Track : Entity<long>
    {
        public string Name { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public int Distance { get; set; }
        public List<Child> Children { get; set; }
        
        public Track(long id, string name, int minimumAge, int maximumAge, int distance) : base(id)
        {
            Name = name;
            MinimumAge = minimumAge;
            MaximumAge = maximumAge;
            Distance = distance;
            Children = new List<Child>();
        }

        public Track(string name, int minimumAge, int maximumAge, int distance)
        {
            Name = name;
            MinimumAge = minimumAge;
            MaximumAge = maximumAge;
            Distance = distance;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var track = (Track) obj;
            return Id == track.Id && Name == track.Name && MinimumAge == track.MinimumAge && MaximumAge == track.MaximumAge && Distance == track.Distance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, MinimumAge, MaximumAge, Distance);
        }
    }
}