using System;
using System.Collections.Generic;

namespace MPP_CSharp.Domain
{
    public class Entity<TId>
    {
        public TId Id { get; set; }
       
        protected Entity(TId id)
        {
            Id = id;
        }

        public Entity()
        {
        }

        
    }
}