using System;
using System.Collections.Generic;

namespace MPP_HW_CSharp.Domain
{
    public class Track : Entity<long>
    {
        private string Name { get; set; }
        private int MinimumAge { get; set; }
        private int MaximumAge { get; set; }
        private int Distance { get; set; }
        private List<Child> Children { get; set; }
        
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

        protected bool Equals(Track other)
        {
            return Name == other.Name && MinimumAge == other.MinimumAge && MaximumAge == other.MaximumAge && Distance == other.Distance && Children.Equals(other.Children);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Track)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ MinimumAge;
                hashCode = (hashCode * 397) ^ MaximumAge;
                hashCode = (hashCode * 397) ^ Distance;
                hashCode = (hashCode * 397) ^ Children.GetHashCode();
                return hashCode;
            }
        }
    }
}