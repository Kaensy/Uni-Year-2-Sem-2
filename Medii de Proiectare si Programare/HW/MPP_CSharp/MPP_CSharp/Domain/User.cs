using System;
using System.Collections.Generic;

namespace MPP_CSharp.Domain
{
    public class User : Entity<long>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public User(long id, string username, string password) : base(id)
        {
            Username = username;
            Password = password;
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var user = (User) obj;
            return Id == user.Id && Username == user.Username && Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username, Password);
        }
    }
}