﻿/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Entities.Maps
{
	internal class AuthorMap : EntityTypeConfiguration<Author>
	{
		public AuthorMap() { 
			Property(a => a.Name).HasMaxLength(128);
			Property(a => a.Email).HasMaxLength(128);
		}
	}
}
