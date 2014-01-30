/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Entities.Maps
{
	internal class TagMap : EntityTypeConfiguration<Tag>
	{
		public TagMap() { 
			Property(t => t.Name).IsRequired().HasMaxLength(128);
			Property(t => t.Slug).IsRequired().HasMaxLength(128);
		}
	}
}
