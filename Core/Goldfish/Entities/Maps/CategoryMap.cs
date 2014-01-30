/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Entities.Maps
{
	internal class CategoryMap : EntityTypeConfiguration<Category>
	{
		public CategoryMap() { 
			Property(c => c.Name).IsRequired().HasMaxLength(128);
			Property(c => c.Slug).IsRequired().HasMaxLength(128);
		}
	}
}
