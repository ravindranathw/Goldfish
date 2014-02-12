/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace Goldfish.Extend
{
	/// <summary>
	/// Base class for quickly creating DbContext's to modules.
	/// </summary>
	public abstract class ModuleContext : DbContext
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="nameOrConnectionString">The name of or the connection string</param>
		public ModuleContext(string nameOrConnectionString = "goldfish")
			: base(nameOrConnectionString) {

			// Attach OnModelCreating event
			((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized +=
				new ObjectMaterializedEventHandler(OnEntityLoad);
		}

		/// <summary>
		/// Called when an entity has been loaded.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="e">Event arguments</param>
		protected virtual void OnEntityLoad(object sender, ObjectMaterializedEventArgs e) {
			if (e.Entity is Entities.IBaseEntity)
				((Entities.IBaseEntity)e.Entity).OnLoad(this);
		}

		/// <summary>
		/// Saves the changes made to the context.
		/// </summary>
		/// <returns>The number of changes</returns>
		public override int SaveChanges() {
			var cached = new List<Cache.ICacheEntity>();

			foreach (var entry in ChangeTracker.Entries()) {
				// Call the correct entity event.
				if (entry.Entity is Entities.IBaseEntity) {
					if (entry.State == EntityState.Added || entry.State == EntityState.Modified) {
						((Entities.IBaseEntity)entry.Entity).OnSave(this, entry.State);
					} else if (entry.State == EntityState.Deleted) {
						((Entities.IBaseEntity)entry.Entity).OnDelete(this);
					}
				}
				// Check if entity is cached
				if (entry.Entity is Cache.ICacheEntity)
					cached.Add((Cache.ICacheEntity)entry.Entity);
			}
			// Save the changes
			var ret = base.SaveChanges();

			// Remove all cached entities from cache
			foreach (var entity in cached)
				entity.RemoveFromCache();

			// Return the number of saved changes
			return ret;
		}
	}
}