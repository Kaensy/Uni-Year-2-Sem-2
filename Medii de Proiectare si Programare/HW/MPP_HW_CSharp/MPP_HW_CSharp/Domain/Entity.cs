using System;
using System.Collections.Generic;

namespace MPP_HW_CSharp.Domain
{
    public class Entity<TId>
    {
        public Entity(TId id)
        {
            Id = id;
        }
        
        public TId Id { get; set; }
    }
}