using System;
using System.Collections.Generic;

namespace MPP_HW_CSharp.Domain
{
    public class User : Entity<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public User(string id, string username, string password) : base(id)
        {
            Username = username;
            Password = password;
        }

        public User(string id) : base(id)
        {
            Username = "";
            Password = "";
        }
        
        
    }
}