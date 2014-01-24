using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Entities.Maps
{
	internal class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap() {
			Property(u => u.Email).HasMaxLength(128);
		}
	}
}