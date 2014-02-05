/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Blocks.Maps
{
	/// <summary>
	/// Entity type configuration for blocks.
	/// </summary>
	internal class BlockMap : EntityTypeConfiguration<Block>
	{
		public BlockMap() {
			Property(b => b.InternalId).IsRequired().HasMaxLength(32);
			Property(b => b.Name).IsRequired().HasMaxLength(128);
		}
	}
}