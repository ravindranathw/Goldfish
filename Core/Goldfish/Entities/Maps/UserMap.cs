/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Entities.Maps
{
	internal class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap() { }
	}
}