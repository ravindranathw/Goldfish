/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity;

namespace Goldfish.Blocks
{
	/// <summary>
	/// The block module db context.
	/// </summary>
	public class Db : DbContext
	{
		#region Db sets
		/// <summary>
		/// Gets/sets the available blocks.
		/// </summary>
		public DbSet<Entities.Block> Blocks { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Db() : base("goldfish") { }

		/// <summary>
		/// Configures the context.
		/// </summary>
		/// <param name="modelBuilder">The model builder</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new Entities.Maps.BlockMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}