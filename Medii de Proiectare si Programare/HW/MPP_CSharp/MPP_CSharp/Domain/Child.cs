using System.Collections.Generic;
using System;

namespace MPP_CSharp.Domain
{
    public class Child : Entity<long>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        private List<Track> _tracks;

        public List<Track> Tracks { get; set; }
        public Child(long id, string name, int age) : base(id)
        {
            Name = name;
            Age = age;
        }
        
        public Child(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public Child(string name, int age, List<Track> tracks)
        {
            Name = name;
            Age = age;
            Tracks = tracks;
        }

        public override bool Equals(object? obj)
        {
            if ( obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var child = (Child) obj;
            return Id == child.Id && Name == child.Name && Age == child.Age;
        }

        public override int GetHashCode()
        {
           return HashCode.Combine(Id, Name, Age);
        }
    }
}