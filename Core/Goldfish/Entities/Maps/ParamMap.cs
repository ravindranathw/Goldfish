/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

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
