/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Data.Entity;

namespace Goldfish.Entities
{
	/// <summary>
	/// The abstract base class for all entities.
	/// </summary>
	/// <typeparam name="T">The entity type</typeparam>
	public abstract class BaseEntity<T> : IBaseEntity where T : BaseEntity<T>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique id.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets/sets when the entity was intitially created.
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets/sets when the entity was last updated.
		/// </summary>
		public DateTime Updated { get; set; }
		#endregion

		#region Events
		/// <summary>
		/// Called when the entity has been loaded.
		/// </summary>
		/// <param name="db">The db context</param>
		public virtual void OnLoad(Db db) {
			Created = DateTime.SpecifyKind(Created, DateTimeKind.Utc).ToLocalTime();
			Updated = DateTime.SpecifyKind(Updated, DateTimeKind.Utc).ToLocalTime();
		}

		/// <summary>
		/// Called when the entity is about to get saved.
		/// </summary>
		/// <param name="db">The db context</param>
		/// <param name="state">The current entity state</param>
		public virtual void OnSave(Db db, EntityState state) {
			// Auto generate id.
			if (Id == Guid.Empty)
				Id = Guid.NewGuid();

			// Set dates
			if (state == EntityState.Modified || state == EntityState.Added)
				Updated = DateTime.Now.ToUniversalTime();
			if (state == EntityState.Added)
				Created = Updated;
		}

		/// <summary>
		/// Called when the entity is about to get deleted.
		/// </summary>
		/// <param name="db">The db context</param>
		public virtual void OnDelete(Db db) { }
		#endregion
	}
}
