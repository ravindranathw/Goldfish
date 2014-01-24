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
		void OnLoad(Db db);

		/// <summary>
		/// Called when the entity is about to get saved.
		/// </summary>
		/// <param name="db">The db context</param>
		/// <param name="state">The current entity state</param>
		void OnSave(Db db, EntityState state);

		/// <summary>
		/// Called when the entity is about to get deleted.
		/// </summary>
		/// <param name="db">The db context</param>
		void OnDelete(Db db);
	}
}
