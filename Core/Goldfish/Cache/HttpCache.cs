/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;
using System.Web;

namespace Goldfish.Cache
{
	/// <summary>
	/// The default cache uses the HttpContext Cache object to
	/// store its values in. 
	/// </summary>
	public sealed class HttpCache : ICacheProvider
	{
		/// <summary>
		/// Gets the cached model for the given id.
		/// </summary>
		/// <typeparam name="T">The model type</typeparam>
		/// <param name="id">The unique id</param>
		/// <returns>The model</returns>
		public T Get<T>(string id) {
			// Make sure we have a http context
			if (HttpRuntime.Cache != null) {
				return (T)HttpRuntime.Cache[id];
			}
			return default(T);
		}

		/// <summary>
		/// Sets the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <param name="obj">The model</param>
		public void Set(string id, object obj) {
			// Make sure we have a http context
			if (HttpRuntime.Cache != null)
				HttpRuntime.Cache[id] = obj;
		}

		/// <summary>
		/// Removes the cached model for the given id.
		/// </summary>
		/// <param name="id">The unique id</param>
		public void Remove(string id) {
			// Make sure we have a http context
			if (HttpRuntime.Cache != null) {
				HttpRuntime.Cache.Remove(id);
			}
		}
	}
}