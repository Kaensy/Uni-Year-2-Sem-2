using System;
using System.Collections.Generic;

namespace MPP_HW_CSharp.Domain
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
    }
}