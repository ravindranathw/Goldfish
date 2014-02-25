/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity;

namespace Goldfish.Pages
{
	/// <summary>
	/// The pages module db context.
	/// </summary>
	public class Db : Extend.ModuleContext
	{
		#region Db sets
		/// <summary>
		/// Gets/sets the available pages.
		/// </summary>
		public DbSet<Page> Pages { get; set; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Db() : base() { }

		/// <summary>
		/// Configures the context.
		/// </summary>
		/// <param name="modelBuilder">The model builder</param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Configurations.Add(new Maps.PageMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}