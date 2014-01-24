using System;
using System.Data.Entity.ModelConfiguration;

namespace Goldfish.Entities.Maps
{
	internal class CommentMap : EntityTypeConfiguration<Comment>
	{
		public CommentMap() {
			Property(c => c.UserId).HasMaxLength(128);
			Property(c => c.Author).IsRequired().HasMaxLength(128);
			Property(c => c.Email).HasMaxLength(128);
			Property(c => c.IP).HasMaxLength(16);
			Property(c => c.SessionID).HasMaxLength(64);
		}
	}
}