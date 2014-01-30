/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;

namespace Goldfish.Entities
{
	/// <summary>
	/// Params are used to store application settings that should be
	/// manageble by the content administrators.
	/// </summary>
	public sealed class Param : BaseEntity<Param>, Cache.ICacheEntity
	{
		#region Properties
		/// <summary>
		/// Gets/sets the unique internal id.
		/// </summary>
		public string InternalId { get; set; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets/sets the value.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Gets/sets if it is a system parameter.
		/// </summary>
		public bool IsSystemParam { get; internal set; }
		#endregion

		/// <summary>
		/// Removes the current entity from the application cache.
		/// </summary>
		public void RemoveFromCache() {
			App.Instance.EntityCache.Params.Remove(Id);

			if (InternalId == "THEME")
				Web.Mvc.ViewEngine.Register();
		}
	}
}
