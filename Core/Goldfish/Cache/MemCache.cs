/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;

namespace Goldfish.Cache
{
	/// <summary>
	/// The mem cache stores values in process memory.
	/// </summary>
	public sealed class MemCache : ICacheProvider
	{
		#region Members
		/// <summary>
		/// The private memory cache.
		/// </summary>
		private readonly Dictionary<string, object> Cache = new Dictionary<string, object>();
		#endregion

		/// <summary>
		/// Gets or creates the keymap for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The keymap</returns>
		public Dictionary<string, Guid> GetKeyMap(string id) {
			object map = null;

			// Create a new map if it doesn't exist
			if (!Cache.TryGetValue(id, out map)) {
				map = new Dictionary<string, Guid>();
				Cache[id] = map;
			}
			return (Dictionary<string, Guid>)map;
		}

		/// <summary>
		/// Sets the keymap for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="keyMap">The key map</param>
		public void SetKeyMap(string id, Dictionary<string, Guid> keyMap) {
			Cache[id] = keyMap;
		}

		/// <summary>
		/// Gets the cached model for the given id.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public T Get<T>(string id) {
			object model = null;

			if (Cache.TryGetValue(id, out model))
				return (T)model;
			return default(T);
		}

		/// <summary>
		/// Sets the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="obj">The model</param>
		public void Set(string id, object obj) {
			Cache[id] = obj;
		}

		/// <summary>
		/// Removes the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		public void Remove(string id) {
			Cache.Remove(id);
		}
	}
}