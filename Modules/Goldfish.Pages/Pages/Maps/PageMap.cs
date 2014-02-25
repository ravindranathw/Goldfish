/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Pages.Maps
{
	internal class PageMap : EntityTypeConfiguration<Page>
	{
		public PageMap() { 
			Property(p => p.Title).IsRequired().HasMaxLength(128);
			Property(p => p.Slug).IsRequired().HasMaxLength(128);
			Property(p => p.Keywords).HasMaxLength(128);
			Property(p => p.Description).HasMaxLength(255);
			Property(p => p.View).HasMaxLength(128);

			Ignore(p => p.Html);
		}
	}
}