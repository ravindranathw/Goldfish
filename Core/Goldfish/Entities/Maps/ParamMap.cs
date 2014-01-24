using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Entities.Maps
{
    internal class ParamMap : EntityTypeConfiguration<Param>
    {
        public ParamMap() { 
            Property(p => p.InternalId).IsRequired().HasMaxLength(32) ;
            Property(p => p.Name).IsRequired().HasMaxLength(128) ;
        }
    }
}
