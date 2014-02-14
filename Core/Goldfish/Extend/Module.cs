/*
 * Copyright (c) 2014 Håkan Edling
 *
 * See the file LICENSE for copying permission.
 */

using System;
using System.Collections.Generic;

namespace Goldfish.Extend
{
	/// <summary>
	/// Default implementation of IModule.
	/// </summary>
	public abstract class Module : IModule
	{
		#region Members
		/// <summary>
		/// The private module cache.
		/// </summary>
		private static Dictionary<Type, object> cache = new Dictionary<Type, object>();
		#endregion

		#region Properties
		/// <summary>
		/// Gets the current cache provider.
		/// </summary>
		public static Cache.ICacheProvider Cache {
			get {
				return App.Instance.IoCContainer.Resolve<Goldfish.Cache.ICacheProvider>();
			}
		}
		#endregion

		/// <summary>
		/// Initializes the module. This method should be used for
		/// ensuring runtime resources and registering hooks.
		/// </summary>
		public abstract void Init();

		/// Adds a cache for the registered type for the current module.
		/// If a cache is already registered for the current module and
		/// type, nothings is performed.
		/// </summary>
		/// <typeparam name="T">The cache model type</typeparam>
		/// <param name="getId">Function for get the model id</param>
		/// <param name="getKey">Function for getting the model key</param>
		public static void AddCache<T>(Func<T, Guid> getId, Func<T, string> getKey) {
			cache[typeof(T)] = new Cache.ModelCache<T>(App.Instance.IoCContainer.Resolve<Cache.ICacheProvider>(), getId, getKey);
		}

		/// <summary>
		/// Gets the module cache for the specified type.
		/// </summary>
		/// <typeparam name="T">The cache model type</typeparam>
		/// <returns>The cache</returns>
		public static Cache.ModelCache<T> GetCache<T>() {
			return (Cache.ModelCache<T>)cache[typeof(T)];
		}
	}
}