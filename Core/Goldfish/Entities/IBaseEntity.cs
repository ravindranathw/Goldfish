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
	/// Interface defining all base actions on entities.
	/// </summary>
	public interface IBaseEntity
	{
		/// <summary>
		/// Called when the entity has been loaded.
		/// </summary>
		/// <param name="db">The db context</param>
		void OnLoad(DbContext db);

		/// <summary>
		/// Called when the entity is about to get saved.
		/// </summary>
		/// <param name="db">The db context</param>
		/// <param name="state">The current entity state</param>
		void OnSave(DbContext db, EntityState state);

		/// <summary>
		/// Called when the entity is about to get deleted.
		/// </summary>
		/// <param name="db">The db context</param>
		void OnDelete(DbContext db);
	}
}
