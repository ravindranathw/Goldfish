/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Entities.Maps
{
	internal class PostMap : EntityTypeConfiguration<Post>
	{
		public PostMap() { 
			Property(p => p.Title).IsRequired().HasMaxLength(128);
			Property(p => p.Slug).IsRequired().HasMaxLength(128);
			Property(p => p.Keywords).HasMaxLength(128);
			Property(p => p.Description).HasMaxLength(255);
			
			HasMany(p => p.Categories).WithMany();
			HasMany(p => p.Tags).WithMany();
		}
	}
}
